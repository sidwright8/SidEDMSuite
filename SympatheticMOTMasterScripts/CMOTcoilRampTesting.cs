using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for trying out a cmot directly from the mot.
// Image Delay here means the delay after MOT has been switched off. 
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
        Parameters["PatternLength"] = 110000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 10000;
        Parameters["MOTLoadEndTime"] = 68000;
        Parameters["MOTEndTime"] = 70000;
        Parameters["TopMOTCoilCurrent"] = 0.0;
        Parameters["BottomMOTCoilCurrent"] = 0.0;
        Parameters["TopVacCoilCurrent"] = 2.8;
        Parameters["BottomVacCoilCurrent"] = 2.72;


        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 70000;
        Parameters["ImageDelay"] = 5;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 81100;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 81500;
        Parameters["ExposureTime"] = 10;
        Parameters["D2RepumpSwitchOffTime"] = 1;

        //MOT settings
        Parameters["aom0amplitude"] = 6.0;
        Parameters["aom0Detuning"] = 200.0; //lock 
        Parameters["aom3amplitude"] = 6.0;
        Parameters["aom3Detuning"] = 178.8; // MOT cooling
        Parameters["MotRepumpFrequency"] = 814.3; //repump detuning
        Parameters["MotRepumpAmplitude"] = 3.8;// repump amplitude, as voltage applied to VCA
        Parameters["XCoilCurrent"] = 0.9;
        Parameters["YCoilCurrent"] = 6.55;
        Parameters["ZCoilCurrent"] = 1.37;

        //CMOT settings
        Parameters["FinalTopCMOTCurrent"] = 3.2;
        Parameters["FinalBottomCMOTCurrent"] = 3.07;
        Parameters["CMOTrampTime"] = 250;
        Parameters["CMOTholdTime"] = 10;
        Parameters["FinalCMOTpower"] = 6.0;
        Parameters["FinalCMOTdetuning"] = 178.8;
                    
        //Imaging settings
        Parameters["absImageDetuning"] = 194.0;
        Parameters["absImagePower"] = 0.9;
        Parameters["backgroundImagePower"] = 1.1;
        Parameters["absImageRepumpDetuning"] = 814.2;
        Parameters["absImageRepumpAmplitude"] = 4.2;
        Parameters["ROIforImageProcessingy1"] = 120;
        Parameters["ROIforImageProcessingy2"] = 970;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 300;
        Parameters["ROIforImageProcessingx2"] = 1200;//must be between 0 1387



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

        //Imaging - no need to redistribute atoms amongst the ground states since we didn't pump to the f =1 ground state
        p.Pulse((int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"], 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"], 0, 100, "CameraTrigger"); //take an image of the cloud after D1 stage

        
        p.Pulse((int)Parameters["Frame1Trigger"], 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, 100, "CameraTrigger"); //take an image without the cloud. 

        //p.AddEdge("aom1enable", 150000, false);
        p.DownPulse(90000, 0, 50, "CameraTrigger"); //background image - no light.
        p.DownPulse(100000, 0, 50, "CameraTrigger");
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
        
        p.AddAnalogValue("aom3amplitude", (int)Parameters["MOTEndTime"] + 1, 6.0);
        p.AddAnalogValue("aom3frequency", (int)Parameters["MOTEndTime"] + 1, (double)Parameters["aom3Detuning"]);

        //Implement the CMOT
        p.AddLinearRamp("aom3amplitude", (int)Parameters["MOTEndTime"] - (int)Parameters["CMOTrampTime"] - (int)Parameters["CMOTholdTime"], (int)Parameters["CMOTrampTime"], (double)Parameters["FinalCMOTpower"]);
        p.AddLinearRamp("aom3frequency", (int)Parameters["MOTEndTime"] - (int)Parameters["CMOTrampTime"] - (int)Parameters["CMOTholdTime"], (int)Parameters["CMOTrampTime"], (double)Parameters["FinalCMOTdetuning"]);
        p.AddLinearRamp("TopTrappingCoilcurrent", (int)Parameters["MOTEndTime"] - (int)Parameters["CMOTrampTime"] - (int)Parameters["CMOTholdTime"], (int)Parameters["CMOTrampTime"], (double)Parameters["FinalTopCMOTCurrent"]);
        p.AddLinearRamp("BottomTrappingCoilcurrent", (int)Parameters["MOTEndTime"] - (int)Parameters["CMOTrampTime"] - (int)Parameters["CMOTholdTime"], (int)Parameters["CMOTrampTime"], (double)Parameters["FinalBottomCMOTCurrent"]);

        p.AddAnalogValue("TopTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);
        p.AddAnalogValue("BottomTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);

        //Taking the pictures
        p.AddAnalogValue("D2EOMfrequency", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpDetuning"]);
        p.AddAnalogValue("D2EOMamplitude", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpAmplitude"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImagePower"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["Frame1Trigger"] - 1, (double)Parameters["backgroundImagePower"]);

        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "offsetlockfrequency", "xcoilCurrent", "ycoilcurrent", "zcoilcurrent" });
        return p;
    }

}
