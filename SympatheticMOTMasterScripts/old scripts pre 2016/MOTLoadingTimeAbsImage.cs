using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to measure the loading time of the MOT by taking absorption images. First it dumps the MOT, then 
// loads it and takes three images, one of the MOT, one with no atoms and one background. The trigger for the MOT 
// image can then be scanned using the scripting console.
public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 220000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.273;
        Parameters["BottomMOTCoilCurrent"] = 2.355;
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 120000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 190000;
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 200000;
        Parameters["aom0Detuning"] = 211.0;
        Parameters["aom1Detuning"] = 200.0;
        Parameters["aom2Detuning"] = 202.5;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["AbsorptionDetuning"] = 201.0;
        Parameters["TSAcceleration"] = 10.0;
        Parameters["TSDeceleration"] = 10.0;
        Parameters["TSDistance"] = 0.0;
        Parameters["TSVelocity"] = 10.0;
        Parameters["TSDistanceF"] = 0.0;
        Parameters["TSDistanceB"] = 0.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!



        p.AddEdge("D2aomshutter1", 0, true);//The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        p.AddEdge("D2aomshutter2", 0, true); //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        p.AddEdge("aom2enable", 119000, false);//turn off Zeeman beam to measure lifetime.
        p.AddEdge("aom3enable", 199000, false);
        p.AddEdge("aom1enable", 199000, false);
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        p.DownPulse(210000, 0, 50, "CameraTrigger");
        p.DownPulse(215000, 0, 50, "CameraTrigger");

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

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["aom0Detuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 2, (double)Parameters["AbsorptionDetuning"]);
        p.AddAnalogValue("coil0current", 185000, 0);
        p.AddAnalogValue("coil1current", 185000, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
