using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to test switching between the D1 and D2 lasers. 
//Useful timescales to remember:
//  The coils take about 500us to switch off (though the magnetic field will persist longer than this 
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
        Parameters["PatternLength"] = 170000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.281;
        Parameters["BottomMOTCoilCurrent"] = 2.365;
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = ConvertFromSeconds(0.1);
        Parameters["Frame0Trigger"] = 130002;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 131100;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 135000;
        Parameters["ExposureTime"] = 10;
        Parameters["D1StartTime"] = 110000;
        Parameters["D1CoolingDuration"] = 5;
        
        //MOT aom Detunings
        Parameters["aom0Detuning"] = 210.0;
        Parameters["aom1Detuning"] = 199.0;
        Parameters["aom2Detuning"] = 202.0;
        Parameters["aom3Detuning"] = 201.0;

        //D1 stage Detunings and powers
        Parameters["D1PrincipalDetuning"] = 213.0;
        Parameters["D1RepumperDetuning"] = 200.0;
        Parameters["D1RepumperPower"] = 1.6;


        //Imaging detunings
        Parameters["absImageDetuning"] = 201.0;
        Parameters["absImagePower"] = 5.0;
        Parameters["absImageRepumpDetuning"] = 201.0;


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
        
        p.AddEdge("D2aomshutter1", 0 , true);//The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        p.AddEdge("D2aomshutter2", 0, true); //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.


        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This just loads the MOT, and leaves it "on". You need 
           //turn off the MOT and Zeeman light yourself

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman repump, and Zeeman beam before imaging, so that MOT is not reloaded
        p.AddEdge("shutterenable", (int)Parameters["D1StartTime"] - 5000, false);
        p.AddEdge("probeshutterenable", (int)Parameters["D1StartTime"] - 500, true);
        p.AddEdge("aom2enable", (int)Parameters["D1StartTime"] - 500, false);
        p.AddEdge("aom3enable", (int)Parameters["D1StartTime"] - 500, false);

        //turn OFF the MOT AOMs, cutting off all light to the chamber
        p.AddEdge("aom0enable", (int)Parameters["D1StartTime"] - 5, false);
        p.AddEdge("aom1enable", (int)Parameters["D1StartTime"] - 5, false);

        //turn ON the D1 light, then turn OFF the D2 light
        p.AddEdge("D1aomshutter1", (int)Parameters["D1StartTime"] - 4, true);
        p.AddEdge("D2aomshutter1", (int)Parameters["D1StartTime"] -3, false);
        p.AddEdge("D2aomshutter2", (int)Parameters["D1StartTime"] -3, false);

        //turn ON the MOT AOMs, allowing the D1 light through
        //p.AddEdge("aom0enable", (int)Parameters["D1StartTime"], true);
        //p.AddEdge("aom1enable", (int)Parameters["D1StartTime"], true);

        //p.AddEdge("D1aomshutter1", (int)Parameters["D1StartTime"] +10000, false);

        //turn off MOT cooling beam, directing the D1 light into the repumper beam
        //p.AddEdge("aom0enable", (int)Parameters["D1StartTime"]+0, false);

        //turn on the Zeeman AOM which acts as a shutter for the probe beam. Happens 1ms after the D1 cooling starts
        p.AddEdge("aom3enable", (int)Parameters["D1StartTime"] + 0, true);        

        //turn ON the D2 light. Now both lasers go through the TA but nothing goes to the chamber.
        p.AddEdge("D2aomshutter1", (int)Parameters["D1StartTime"] + (int)Parameters["D1CoolingDuration"], true);
        p.AddEdge("D2aomshutter2", (int)Parameters["D1StartTime"] + (int)Parameters["D1CoolingDuration"], true);
        
        
        p.DownPulse((int)Parameters["D1StartTime"]+0, 0, 50, "CameraTrigger"); //take an image of the cloud after D1 stage
                
        p.DownPulse(120000, 0, 50, "CameraTrigger"); //take an image without the cloud. 

        p.AddEdge("aom3enable", 155000, false); //turn off the probe beam.
        p.AddEdge("D1aomshutter1", 155000 , false); //turn off D1 light.
        p.DownPulse(160000, 0, 50, "CameraTrigger"); //background image - no light.
        
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);

        p.AddChannel("aom0frequency");
        p.AddChannel("aom1frequency");
        p.AddChannel("aom2frequency");
        p.AddChannel("aom3frequency");
        p.AddChannel("aom0amplitude");
        p.AddChannel("aom1amplitude");
        p.AddChannel("aom3amplitude");

        p.AddChannel("offsetlockfrequency");
        
        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["aom0Detuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom0amplitude", 0, 6.0);
        p.AddAnalogValue("aom1amplitude", 0, 6.0);
        p.AddAnalogValue("aom3amplitude", 0, 6.0); //setting up the MOT
        p.AddAnalogValue("offsetlockfrequency", 0, 10.0);//set the offset frequency for the D1 laser
               
        //Turn coils off to start D1 phase, adjust repumper beam power for D1 cooling
        p.AddAnalogValue("coil0current", (int)Parameters["D1StartTime"]-2, 0);
        p.AddAnalogValue("coil1current", (int)Parameters["D1StartTime"]-2, 0);
        p.AddAnalogValue("coil1current", (int)Parameters["D1StartTime"] , 0);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["D1StartTime"]-3, (double)Parameters["D1RepumperPower"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["D1StartTime"] - 3, (double)Parameters["D1RepumperDetuning"]);
        p.AddAnalogValue("aom0frequency", (int)Parameters["D1StartTime"] - 3, (double)Parameters["D1PrincipalDetuning"]);
        
        //Adjust the repumper beam power/detuning so we can optically pump to f=2 ground state
        p.AddAnalogValue("aom1amplitude", (int)Parameters["D1StartTime"] +0, 0.5);
        p.AddAnalogValue("aom1frequency", (int)Parameters["D1StartTime"] + 0, (double)Parameters["absImageRepumpDetuning"]);
        
        //Taking the pictures
        p.AddAnalogValue("aom3frequency", (int)Parameters["D1StartTime"] +0, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["D1StartTime"] + 0, (double)Parameters["absImagePower"]);
        

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
