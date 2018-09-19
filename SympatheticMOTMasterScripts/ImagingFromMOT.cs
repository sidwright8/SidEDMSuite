using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for measuring directly from the mot.
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
        Parameters["PatternLength"] = 100000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 5000;
        Parameters["MOTLoadEndTime"] = 2000;
        Parameters["MOTEndTime"] = 70000;
        Parameters["TopMOTCoilCurrent"] = 0.0;
        Parameters["BottomMOTCoilCurrent"] = 0.0;
        Parameters["TopVacCoilCurrent"] = 0.7;
        Parameters["BottomVacCoilCurrent"] = 0.70;


        Parameters["NumberOfFrames"] = 3;
        Parameters["ImageDelay"] = 5;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1TriggerDelay"] = 10000;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 81500;
        
        Parameters["D2RepumpSwitchOffTime"] = 1;

        //MOT settings
        Parameters["aom0amplitude"] = 6.0;
        Parameters["aom0Detuning"] = 188.0; //lock 
        Parameters["aom3amplitude"] = 6.0;
        Parameters["aom3Detuning"] = 179.8; // MOT cooling
        Parameters["MotRepumpFrequency"] = 814.3; //repump detuning
        Parameters["MotRepumpAmplitude"] = 3.8;// repump amplitude, as voltage applied to VCA
        Parameters["XCoilCurrent"] = 0.9;
        Parameters["YCoilCurrent"] = 3.0;
        Parameters["ZCoilCurrent"] = 1.5;

        //Imaging settings
        Parameters["absImageDetuning"] = 190.0;
        Parameters["absImagePower"] = 2.2;
        Parameters["backgroundImagePower"] = 2.2;
        Parameters["absImageRepumpDetuning"] = 814.2;
        Parameters["absImageRepumpAmplitude"] = 4.2;
        Parameters["ROIforImageProcessingy1"] = 100;
        Parameters["ROIforImageProcessingy2"] = 950;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 200;
        Parameters["ROIforImageProcessingx2"] = 1100;//must be between 0 1387
        Parameters["probeQWPangle"] = 50; //use to add the lambda/4 angle to the parameter list for analysis if needed later


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
        p.AddEdge("shutterenable", (int)Parameters["MOTEndTime"]-(int)Parameters["MOTLoadEndTime"], false);

        //turn OFF the MOT AOMs, cutting off all light to the chamber
        p.AddEdge("aom3enable", (int)Parameters["MOTEndTime"], false);

        //optional turn off of the Repump at this time
        p.AddEdge("D2EOMenable", (int)Parameters["MOTEndTime"], false);

        //Imaging
        p.Pulse((int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"], 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"], 0, 100, "CameraTrigger"); //take an image of the cloud after D1 stage

        p.Pulse((int)Parameters["Frame1TriggerDelay"] + (int)Parameters["MOTEndTime"], 0, 100, "aom1enable");
        p.DownPulse((int)Parameters["Frame1TriggerDelay"] + (int)Parameters["MOTEndTime"], 0, 100, "CameraTrigger"); //take an image without the cloud. 

        //p.AddEdge("aom1enable", 150000, false);
        p.DownPulse(90000, 0, 50, "CameraTrigger"); //background image - no light.
        p.DownPulse(95000, 0, 50, "CameraTrigger");
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

        p.AddAnalogValue("TopTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);
        p.AddAnalogValue("BottomTrappingCoilcurrent", (int)Parameters["MOTEndTime"], 0);

        //Taking the pictures
        p.AddAnalogValue("D2EOMfrequency", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpDetuning"]);
        p.AddAnalogValue("D2EOMamplitude", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 3, (double)Parameters["absImageRepumpAmplitude"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["MOTEndTime"] + (int)Parameters["ImageDelay"] - 1, (double)Parameters["absImagePower"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["Frame1TriggerDelay"] + (int)Parameters["MOTEndTime"] - 1, (double)Parameters["backgroundImagePower"]);

        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "offsetlockfrequency", "xcoilCurrent", "ycoilcurrent", "zcoilcurrent" });
        return p;
    }

}
