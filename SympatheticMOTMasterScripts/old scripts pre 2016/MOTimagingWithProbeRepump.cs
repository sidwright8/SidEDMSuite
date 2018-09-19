using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to take absorption images of the MOT when you have F=1 repump light in the probe beam. Five images are taken in total. The first is of the atoms in the MOT
//(both F=1 and F=2 probe beams are on), the second is of the combined F=1 and F=2 probe light with no atoms present. Three is a background image where both the F=1 and F=2 beams are off.
//The fourth image is of the F=1 light on its own with the atoms present and finally a picture of only the F=2 light with no atoms present. 

public class Patterns : MOTMasterScript
{
    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 170000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.33;
        Parameters["BottomMOTCoilCurrent"] = 2.41;
        Parameters["NumberOfFrames"] = 5;
        Parameters["Frame0TriggerDuration"] = 10;
        Parameters["Frame0Trigger"] = 130000;
        Parameters["Frame1TriggerDuration"] = 10;
        Parameters["Frame1Trigger"] = 131000;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 145000;
        Parameters["Frame3TriggerDuration"] = 10;
        Parameters["Frame3Trigger"] = 147000;
        Parameters["Frame4TriggerDuration"] = 10;
        Parameters["Frame4Trigger"] = 149000;
        Parameters["aom0Detuning"] = 213.0;
        Parameters["aom1Detuning"] = 197.0;
        Parameters["aom2Detuning"] = 204.0;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["absImageDetuning"] = 203.3;
        Parameters["absImageRepumpDetuning"] = 203.0;
        Parameters["ExposureTime"] = 1;
        Parameters["TSDistance"] = 0.0;
        Parameters["TSVelocity"] = 10.0;
        Parameters["TSAcceleration"] = 10.0;
        Parameters["TSDeceleration"] = 10.0;
        Parameters["TSDistanceF"] = 0.0;
        Parameters["TSDistanceB"] = 0.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);

        //switches off Zeeman and Zeeman repump beams during imaging, so that MOT is not reloaded, also switches off probe beam 
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 1000, false);
        p.AddEdge("aom2enable", (int)Parameters["Frame0Trigger"] - 20, false);
        p.AddEdge("aom3enable", (int)Parameters["Frame0Trigger"] - 20, false);

        //switches on the probe beam to take an image of the MOT (the F=1 probe repump is also on during this image)
        p.Pulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //takes an image of the atoms with only the F=1 probe repump on
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        //switches on the probe beam to take the no atoms image, then switches off the F=1 light to take a background image
        p.Pulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");
        p.AddEdge("aom1enable", (int)Parameters["Frame3Trigger"] - 100, false);
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");

        //finally takes an image of the F=2 probe beam with no atoms present
        //p.Pulse((int)Parameters["Frame4Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame4Trigger"], 0, (int)Parameters["Frame4TriggerDuration"], "CameraTrigger");

        p.DownPulse(160000, 0, 50, "CameraTrigger");
        p.DownPulse(165000, 0, 50, "CameraTrigger");

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
        p.AddChannel("proberepumpshutter");

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["aom0Detuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom0amplitude", 0, 6.0);
        p.AddAnalogValue("aom1amplitude", 0, 6.0);
        p.AddAnalogValue("proberepumpshutter", 0, 5.0);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImageDetuning"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame0Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame1Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame2Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("coil0current", 135000, 0);
        p.AddAnalogValue("coil1current", 135000, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
