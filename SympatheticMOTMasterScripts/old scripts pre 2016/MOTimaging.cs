using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to take absorption images of the MOT. Three images are taken in total, one of the atoms in the MOT,
// the second is of the probe beam with no atoms present. The third is an image of the background light, when no probe beam is
// present. 

public class Patterns : MOTMasterScript
{
    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 170000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.306;
        Parameters["BottomMOTCoilCurrent"] = 2.396;
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 1000;
        Parameters["Frame0Trigger"] = 130002;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 131100;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 135000;
        Parameters["ExposureTime"] = 10;
        Parameters["offsetlockfrequency"] = 10.056;//VCO freq for offset lock

        Parameters["aom0Detuning"] = 210.0;
        Parameters["aom1Detuning"] = 200.0;
        Parameters["aom2Detuning"] = 202.5;
        Parameters["aom3Detuning"] = 200.0;

        Parameters["absImageDetuning"] = 199.0;
        Parameters["absImagePower"] = 5.0;
        Parameters["absImageRepumpDetuning"] = 199.0;


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
        p.AddEdge("D2aomshutter1", 0, true);//The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        p.AddEdge("D2aomshutter2", 0, true); //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.
        p.AddEdge("D1aomshutter1", 0, true);
        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman repump, and Zeeman beam before imaging, so that MOT is not reloaded
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 5000, false);
        p.AddEdge("probeshutterenable", (int)Parameters["Frame0Trigger"] - 500, true);
        p.AddEdge("aom2enable", (int)Parameters["Frame0Trigger"] - 500, false);
        p.AddEdge("aom3enable", (int)Parameters["Frame0Trigger"] - 500, false);

        p.AddEdge("aom0enable", (int)Parameters["Frame0Trigger"] - 2, false);
        p.AddEdge("aom1enable", (int)Parameters["Frame0Trigger"] - 2, false);

        //switches on the probe beam to take an image of the MOT
        p.Pulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.Pulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //switches on the probe beam to take the no atoms image, then takes a background image
        p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        p.DownPulse(150000, 0, 50, "CameraTrigger");
        p.DownPulse(160000, 0, 50, "CameraTrigger");

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
        p.AddAnalogValue("aom3amplitude", 0, 6.0);
        p.AddAnalogValue("offsetlockfrequency",0,(double)Parameters["offsetlockfrequency"]);

        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImagePower"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame0Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame1Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("coil0current", 130000, 0);
        p.AddAnalogValue("coil1current", 130000, 0);


        //p.SwitchAllOffAtEndOfPattern(); //I've commented this as it switches the VCO off, which unlocks the D1 laser.
        return p;
    }

}
