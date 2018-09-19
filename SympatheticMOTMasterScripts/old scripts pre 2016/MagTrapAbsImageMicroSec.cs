using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script takes an absorption image of atoms in the MOT and magnetic trap, it takes four images, the first is of the MOT, the second is of the mag trap
// next there are two probe beam with no atom pictures (one where the MOT beams are left on and one where the MOT beams are off).Then there are a further 
// two images, where there are no atoms and the probe beam is switched off (again one with the MOT beams on and one with them off).

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 1000000;
        Parameters["MOTStartTime"] = 10000;
        Parameters["TopMOTCoilCurrent"] = 4.07;
        Parameters["BottomMOTCoilCurrent"] = 4.05;
        Parameters["TopMagCoilCurrent"] = 10.0;
        Parameters["BottomMagCoilCurrent"] = 10.0;
        //Parameters["MOTIntensity"] = 1.92;
        Parameters["MOTLoadTime"] = 1000000;
        Parameters["NumberOfFrames"] = 7;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 950000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 1000050;  //imaging after 0.2ms in mag trap
        //Parameters["Frame1Trigger"] = 1000060;    // imaging after 0.6ms (use this for looking at free expansion of cloud)
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 1100000;
        Parameters["Frame3TriggerDuration"] = 100;
        Parameters["Frame3Trigger"] = 1110000;
        Parameters["Frame4TriggerDuration"] = 100;
        Parameters["Frame4Trigger"] = 1120000;
        Parameters["Frame5TriggerDuration"] = 100;
        Parameters["Frame5Trigger"] = 1130000;
        Parameters["Frame6TriggerDuration"] = 100;
        Parameters["Frame6Trigger"] = 990000;
        Parameters["ExposureTime"] = 4; //Remember to change this when the MM camera shutter value is changed in text file!
        Parameters["AbsDetuning"] = 197.875;
        Parameters["CoolingPulseDetuning"] = 207.713;
        Parameters["CoolingPulseRepumpDetuning"] = 203.875;
        Parameters["CoolingPulseAmplitude"] = 1.36;
        Parameters["CoolingPulseDuration"] = 50;
        Parameters["MOTRepumpDetuning"] = 203.875;
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

        p.Pulse(100000, 0, 1000000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 50000, false);
        p.DownPulse((int)Parameters["Frame0Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom2enable"); //switches off Zeeman beam 
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame6Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom2enable");
        p.DownPulse((int)Parameters["Frame6Trigger"], 0, (int)Parameters["Frame6TriggerDuration"], "CameraTrigger");


        // loads the mag trap by switching off the all beams
        p.AddEdge("aom0enable", (int)Parameters["MOTLoadTime"], false);
        p.AddEdge("aom1enable", (int)Parameters["MOTLoadTime"], false);
        p.AddEdge("aom2enable", (int)Parameters["MOTLoadTime"], false);
        p.AddEdge("aom3enable", (int)Parameters["MOTLoadTime"], false);
        //p.Pulse((int)Parameters["Frame1Trigger"] - 100, 0, (int)Parameters["ExposureTime"] + 200, "aom3enable");
        p.Pulse((int)Parameters["Frame1Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom3enable");
        p.Pulse((int)Parameters["Frame1Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom1enable"); // switches on MOT repump during free expansion image
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
        p.Pulse((int)Parameters["Frame2Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom3enable");
        p.Pulse((int)Parameters["Frame2Trigger"] - 5, 0, (int)Parameters["ExposureTime"] + 10, "aom1enable"); // switches on MOT repump for probe beam with no atoms image
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");
        p.AddEdge("aom0enable", (int)Parameters["Frame3Trigger"] + 1000, true);
        p.AddEdge("aom1enable", (int)Parameters["Frame3Trigger"] + 1000, true);
        p.AddEdge("aom3enable", (int)Parameters["Frame3Trigger"] + 1000, true);
        p.DownPulse((int)Parameters["Frame4Trigger"], 0, (int)Parameters["Frame4TriggerDuration"], "CameraTrigger");


        p.DownPulse(1500000, 0, 500, "CameraTrigger");
        p.DownPulse(1550000, 0, 500, "CameraTrigger");

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
        //p.AddChannel("aom1amplitude");

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", (int)Parameters["MOTStartTime"], 212.713);
        p.AddAnalogValue("aom1frequency", (int)Parameters["MOTStartTime"], 203.875);
        p.AddAnalogValue("aom2frequency", (int)Parameters["MOTStartTime"], 200.875);
        p.AddAnalogValue("aom3frequency", (int)Parameters["MOTStartTime"], 200.875);
        p.AddAnalogValue("aom0amplitude", (int)Parameters["MOTStartTime"], 5.5);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["AbsDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] + (int)Parameters["ExposureTime"] + 10, 200.875);
        p.AddAnalogValue("coil0current", (int)Parameters["MOTLoadTime"] - 20000, (double)Parameters["BottomMagCoilCurrent"]);
        p.AddAnalogValue("coil1current", (int)Parameters["MOTLoadTime"] - 20000, (double)Parameters["TopMagCoilCurrent"]);
        //p.AddAnalogValue("coil0current", (int)Parameters["MOTLoadTime"], 0); // use this for looking at free expansion of the cloud
        //p.AddAnalogValue("coil1current", (int)Parameters["MOTLoadTime"], 0); // use this for looking at free expansion of the cloud
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame6Trigger"] - 10, (double)Parameters["AbsDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame6Trigger"] + (int)Parameters["ExposureTime"] + 10, 200.875);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame1Trigger"] - 10, (double)Parameters["AbsDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame1Trigger"] + (int)Parameters["ExposureTime"] + 10, 200.875);
        p.AddAnalogValue("aom0frequency", (int)Parameters["MOTLoadTime"] - (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseDetuning"]); //use for cooling pulse MOT detuning
        p.AddAnalogValue("aom1frequency", (int)Parameters["MOTLoadTime"] - (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseRepumpDetuning"]); //use for cooling pulse MOT repump detuning
        p.AddAnalogValue("aom0amplitude", (int)Parameters["MOTLoadTime"] - (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseAmplitude"]); //use for cooling pulse MOT amplitude
        p.AddAnalogValue("aom0frequency", (int)Parameters["Frame3Trigger"] + 1010, 212.713); //use for cooling pulse MOT detuning
        p.AddAnalogValue("aom1frequency", (int)Parameters["Frame3Trigger"] + 1010, 203.875); //use for cooling pulse MOT repump detuning
        p.AddAnalogValue("aom0amplitude", (int)Parameters["Frame3Trigger"] + 1010, 5.5); //use for cooling pulse MOT amplitude
        p.AddAnalogValue("coil0current", 1050000, 0);
        p.AddAnalogValue("coil1current", 1050000, 0);
        //p.AddAnalogValue("aom1frequency", (int)Parameters["Frame2Trigger"] - 10, (double)Parameters["MOTRepumpDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame2Trigger"] - 10, (double)Parameters["AbsDetuning"]);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}