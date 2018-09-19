using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to look at how the MOT compresses via absorption imaging as the magnetic field is turned up. Currently the field is simply switched
// to a higher current, it may be worth looking at what happens when the field is ramped slowly. 

public class Patterns : MOTMasterScript
{
    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 1600000;
        Parameters["MOTStartTime"] = 10000;
        Parameters["TopMOTCoilCurrent"] = 4.07;
        Parameters["BottomMOTCoilCurrent"] = 4.05;
        //Parameters["TopMOTCoilCurrent"] = 10.0;
        //Parameters["BottomMOTCoilCurrent"] = 10.0;
        Parameters["TopFinalCoilCurrent"] = 10.0;
        Parameters["BottomFinalCoilCurrent"] = 10.0;
        Parameters["AbsorptionDetuning"] = 200.875;
        Parameters["ZeemanDetuning"] = 200.875;
        Parameters["NumberOfFrames"] = 4;
        //Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 950000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 1005000;
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 1100000;
        //Parameters["Frame4TriggerDuration"] = 100;
        //Parameters["Frame4Trigger"] = 1120000;
        Parameters["Frame3TriggerDuration"] = 100;
        Parameters["Frame3Trigger"] = 1200000;
        Parameters["ExposureTime"] = 4;
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

        p.Pulse(0, 0, 10, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.Pulse(100000, 0, 100000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.DownPulse((int)Parameters["Frame0Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom2enable");
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame1Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom2enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame2Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom2enable");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");
        //p.DownPulse((int)Parameters["Frame4Trigger"] - 100, 0, (int)Parameters["ExposureTime"] + 200, "aom2enable");
        //p.DownPulse((int)Parameters["Frame4Trigger"], 0, (int)Parameters["Frame4TriggerDuration"], "CameraTrigger");

        //switches off Zeeman and Absoroption beams to obtain an image of the background scattered light with no probe beam present
        p.AddEdge("aom2enable", 1150000, false);
        p.AddEdge("aom3enable", 1150000, false);
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");

        p.DownPulse(1500000, 0, 50, "CameraTrigger");
        p.DownPulse(1550000, 0, 50, "CameraTrigger");

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
        p.AddAnalogValue("aom0frequency", 0, 212.713);
        p.AddAnalogValue("aom1frequency", 0, 203.875);
        p.AddAnalogValue("aom2frequency", 0, 200.875);
        p.AddAnalogValue("aom3frequency", 0, 200.875);
        p.AddAnalogValue("aom2frequency", (int)Parameters["Frame0Trigger"] - 200, (double)Parameters["ZeemanDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 200, (double)Parameters["AbsorptionDetuning"]);
        p.AddAnalogValue("aom2frequency", (int)Parameters["Frame0Trigger"] + (int)Parameters["ExposureTime"] + 100, 200.875);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] + (int)Parameters["ExposureTime"] + 100, 200.875);
        //p.AddAnalogPulse("aom2frequency", (int)Parameters["Frame0Trigger"] - 200, (int)Parameters["ExposureTime"] + 300, 200.875, (double)Parameters["ZeemanDetuning"]);
        //p.AddAnalogPulse("aom3frequency", (int)Parameters["Frame0Trigger"] - 200, (int)Parameters["ExposureTime"] + 300, 200.875, (double)Parameters["AbsorptionDetuning"]);
        p.AddAnalogValue("aom2frequency", (int)Parameters["Frame1Trigger"] - 200, (double)Parameters["ZeemanDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame1Trigger"] - 200, (double)Parameters["AbsorptionDetuning"]);
        p.AddLinearRamp("coil0current", 1000000, 2500, (double)Parameters["BottomFinalCoilCurrent"]);
        p.AddLinearRamp("coil1current", 1000000, 2500, (double)Parameters["TopFinalCoilCurrent"]);
        //p.AddAnalogValue("coil0current", 1000000, (double)Parameters["BottomFinalCoilCurrent"]);
        //p.AddAnalogValue("coil1current", 1000000, (double)Parameters["TopFinalCoilCurrent"]);
        p.AddAnalogValue("coil0current", 1050000, 0);
        p.AddAnalogValue("coil1current", 1050000, 0);
        
        //p.AddAnalogValue("aom2frequency", (int)Parameters["Frame4Trigger"] - 200, 200.875);
        //p.AddAnalogValue("aom3frequency", (int)Parameters["Frame4Trigger"] - 200, 200.875);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
