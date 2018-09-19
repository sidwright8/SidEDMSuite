using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for running calirbrating the frequency offset lock using the atoms. By flashing on the molasses beams after the magentic field has 
//decayed at low power, we should be able to see a loss of atoms to the F =1 ground state when resonant.
//By imaging on the F=2 -> F=3 transition we only see the atoms left in the f=2 ground state
//Useful timescales to remember:
//  The coils take about 300us to switch off
//  The AOMs take a few tens of us to switch on/off
//  The units are 100us. So I have made a function to convert from seconds to the 100us units. 
public class Patterns : MOTMasterScript
{
    public int ConvertFromSeconds(double inputValue) //this converts to the horrible units from seconds
    {
        return Convert.ToInt32(10000 * inputValue);
    }

    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 100000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 10000;
        Parameters["MOTLoadEndTime"] = 68000;
        Parameters["MOTEndTime"] = 70000;
        Parameters["TopMOTCoilCurrent"] = 0.0;
        Parameters["BottomMOTCoilCurrent"] = 0.0;
        Parameters["TopVacCoilCurrent"] = 2.76;
        Parameters["BottomVacCoilCurrent"] = 2.70;


        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 70000;
        Parameters["ImageDelay"] = 2;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 81100;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 81500;
        Parameters["ExposureTime"] = 10;
        
                                
        //MOT settings
        Parameters["aom0amplitude"] = 6.0;
        Parameters["aom0Detuning"] = 202.5; //lock 
        Parameters["aom3amplitude"] = 6.0;
        Parameters["aom3Detuning"] = 177.8; // MOT cooling
        Parameters["MotRepumpFrequency"] = 814.5; //repump detuning
        Parameters["MotRepumpAmplitude"] = 3.8;// repump amplitude, as voltage applied to VCA
        Parameters["XCoilCurrent"] = 0.9;
        Parameters["YCoilCurrent"] = 6.55;
        Parameters["ZCoilCurrent"] = 1.37;
       

        //Molasses probe Settings
        Parameters["MolassesStartTime"] = 70004;
        Parameters["MolassesPrincipalFrequency"]=200.0;
        Parameters["MolassesPrincipalAmplitude"] = 0.0;
        Parameters["MolassesRepumpDetuning"] = 802.0;
        Parameters["MolassesPulseLength"] =2;
        Parameters["offsetlockvcofrequency"] = 9.51;//-300MHz/V
        Parameters["MolassesRepumpAmplitude"] = 0.0;
        Parameters["DoMolassesPulse"] = true;
        Parameters["FieldSwitchOffTime"] = 4;
        
        //Imaging settings
        Parameters["absImageDetuning"] = 188.0;
        Parameters["absImagePower"] = 1.3;
        Parameters["backgroundImagePower"] = 1.25;
        Parameters["absImageRepumpDetuning"] = 814.2;
        Parameters["absImageRepumpAmplitude"] = 4.2;


        Parameters["TSDistance"] = 10.0;
        Parameters["TSVelocity"] = 10.0;
        Parameters["TSAcceleration"] = 10.0;
        Parameters["TSDeceleration"] = 10.0;
        Parameters["TSDistanceF"] = 0.0;
        Parameters["TSDistanceB"] = 0.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();
        
        //The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This just loads the MOT, and leaves it "on". You need 
           //turn off the MOT and Zeeman light yourself

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
               
        //switches off Zeeman beams after loading
        p.AddEdge("shutterenable", (int)Parameters["MOTLoadEndTime"], false);

                                    
        //turn OFF the MOT AOMs, cutting off all light to the chamber
        p.AddEdge("aom3enable", (int)Parameters["MOTEndTime"], false);
        p.AddEdge("D2EOMenable", (int)Parameters["MOTEndTime"]+1, false);
      
        //pulse on molasses light
        p.PulseSwitchable((int)Parameters["MolassesStartTime"], 0, (int)Parameters["MolassesPulseLength"], "aom2enable", (bool)Parameters["DoMolassesPulse"]);

        //Imaging - no repump since we want to see how much has gone into the f=1 ground state
        p.Pulse((int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"]+(int)Parameters["ImageDelay"], 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"]+(int)Parameters["ImageDelay"], 0, 100, "CameraTrigger"); //take an image of the cloud after D1 stage

        p.Pulse((int)Parameters["Frame1Trigger"] , 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["Frame1Trigger"] , 0, 100, "CameraTrigger"); //take an image without the cloud. 

        //p.AddEdge("aom1enable", 150000, false);
        p.DownPulse(95000, 0, 50, "CameraTrigger"); //background image - no light.
        
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters); //loading the MOT

        p.AddChannel("aom1frequency");
        p.AddChannel("aom2frequency");
        p.AddChannel("aom3frequency");
        
        p.AddChannel("aom1amplitude");
        p.AddChannel("aom2amplitude");
        p.AddChannel("aom3amplitude");
        
        p.AddChannel("D1EOMfrequency");
        p.AddChannel("D1EOMamplitude");
        p.AddChannel("D2EOMfrequency"); 
        p.AddChannel("D2EOMamplitude");
        p.AddChannel("offsetlockfrequency");
               
            
        p.AddAnalogValue("D2EOMfrequency", 0, (double)Parameters["MotRepumpFrequency"]);
        p.AddAnalogValue("D2EOMamplitude", 0, (double)Parameters["MotRepumpAmplitude"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom3amplitude", 0, 6.0); //setting up the MOT parameters

        p.AddAnalogValue("D1EOMfrequency", 0, (double)Parameters["MolassesRepumpDetuning"]);
        p.AddAnalogValue("D1EOMamplitude", 0, (double)Parameters["MolassesRepumpAmplitude"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["MolassesPrincipalFrequency"]);
        p.AddAnalogValue("aom2amplitude", 0, (double)Parameters["MolassesPrincipalAmplitude"]);
        p.AddAnalogValue("offsetlockfrequency", 0, (double)Parameters["offsetlockvcofrequency"]);//setting up molasses parameters
        
        p.AddLinearRamp("aom3amplitude", (int)Parameters["MOTEndTime"] - 10, 10, 3);
        p.AddLinearRamp("aom3frequency", (int)Parameters["MOTEndTime"] - 10, 10, 180.0);
        p.AddLinearRamp("TopTrappingCoilcurrent", (int)Parameters["MOTEndTime"] - 100, 100, 3.3);
        p.AddLinearRamp("BottomTrappingCoilcurrent", (int)Parameters["MOTEndTime"] - 100, 100, 3.3);


        p.AddAnalogValue("aom3amplitude", (int)Parameters["MOTEndTime"] + 1, 6.0);
        p.AddAnalogValue("aom3frequency", (int)Parameters["MOTEndTime"] + 1, (double)Parameters["aom3Detuning"]);

        p.AddAnalogValue("TopTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);
        p.AddAnalogValue("BottomTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);

                             
        //Taking the pictures
        p.AddAnalogValue("D2EOMfrequency", (int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"] +(int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpDetuning"]);
        p.AddAnalogValue("D2EOMamplitude", (int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"]+(int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpAmplitude"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["MolassesStartTime"] + (int)Parameters["MolassesPulseLength"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImagePower"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["Frame1Trigger"] - 1, (double)Parameters["backgroundImagePower"]);

        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "offsetlockfrequency", "xcoilCurrent", "ycoilcurrent", "zcoilcurrent" });
        return p;
    }

}
