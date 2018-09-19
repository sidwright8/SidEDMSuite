using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;


//This script is designed for measuring if atoms are lost during the CMOT phase. This is done by first imaging the atoms in the MOT,
// next the usual CMOT parameters are applied, finally the atoms are retured to the MOT and imaged. By comparing these two images
// it is possible to determine if any atoms have been lost.

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["CMOTStartTime"] = 101000;
        Parameters["CMOTEndTime"] = 101650;
        Parameters["MOTStartTime"] = 1000;
        Parameters["MOTEndTime"] = 346000;
        //Parameters["SecondaryRampLength"] = 1000;
        Parameters["CMOTOffTime"] = 215200;
        Parameters["CMOTRecaptureTime"] = 316850;
        //Parameters["MagRampDownTime"] = 101651;
        Parameters["PatternLength"] = 400000;
        Parameters["NumberOfFrames"] = 4;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 99000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 317710;
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 350000;
        Parameters["Frame3TriggerDuration"] = 100;
        Parameters["Frame3Trigger"] = 351000;
        Parameters["TopMOTCoilCurrent"] = 2.316;
        Parameters["BottomMOTCoilCurrent"] = 2.41;
        Parameters["TopMagCoilCurrent"] = 3.325;
        Parameters["BottomMagCoilCurrent"] = 3.52;
        //Parameters["TopMagCoilCurrentFinal"] = 5.04;
        //Parameters["BottomMagCoilCurrentFinal"] = 5.17;
        Parameters["aom0Detuning"] = 213.0;
        Parameters["aom1Detuning"] = 197.0;
        Parameters["aom2Detuning"] = 204.0;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["absImageDetuning"] = 202.0;
        Parameters["absImagePower"] = 1.15;
        Parameters["absImageRepumpDetuning"] = 203.0;
        Parameters["aom1Power"] = 6.0;
        //Parameters["CoolingPulseDetuning"] = 218.0;
        Parameters["CoolingPulseIntensity"] = 1.2;
        //Parameters["CoolingPulseRepumpDetuning"] = 192.0;
        Parameters["CoolingPulseRepumpIntensity"] = 1.48;
        Parameters["ExposureTime"] = 1;
        Parameters["TSDistance"] = 440.0;
        Parameters["TSVelocity"] = 500.0;
        Parameters["TSAcceleration"] = 2000.0;
        Parameters["TSDeceleration"] = 2000.0;
        Parameters["TSDistanceF"] = 440.0;
        Parameters["TSDistanceB"] = 440.0;
        Parameters["TranslationStageTriggerDuration"] = 203800;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.Pulse((int)Parameters["CMOTEndTime"] + 100, 0, (int)Parameters["TranslationStageTriggerDuration"], "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman and Zeeman repump beams during imaging, so that MOT is not reloaded and opens the probe beam 
        //shutter 50ms before the MOT image is taken
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 5000, false);
        p.AddEdge("aom2enable", (int)Parameters["Frame0Trigger"] - 500, false);
        p.AddEdge("aom3enable", (int)Parameters["Frame0Trigger"] - 500, false);
        p.AddEdge("probeshutterenable", (int)Parameters["Frame0Trigger"] - 500, true);

        //takes an image of the MOT
        p.Pulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //switches off the MOT beams to load the mag trap - use if you want to go to the mag trap then back to the MOT
        p.DownPulse((int)Parameters["CMOTEndTime"], 0, (int)Parameters["CMOTOffTime"], "aom0enable");
        p.DownPulse((int)Parameters["CMOTEndTime"], 0, (int)Parameters["CMOTOffTime"], "aom1enable");

        //takes an image of the MOT after the atoms have gone through the CMOT phase
        p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        //takes image of the probe beam with no atoms
        p.Pulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        //takes a background image
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");


        p.DownPulse(354000, 0, 50, "CameraTrigger");
        p.DownPulse(354500, 0, 50, "CameraTrigger");

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

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["aom0Detuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom0amplitude", 0, 6.0);
        p.AddAnalogValue("aom1amplitude", 0, (double)Parameters["aom1Power"]);
        p.AddAnalogValue("aom3amplitude", 0, 6.0);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImagePower"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame0Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["absImageRepumpDetuning"], (double)Parameters["aom1Detuning"]);

        //cooling pulse - linearly ramp MOT frequency, amplitude and repump amplitude
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["CoolingPulseDetuning"]);
        p.AddLinearRamp("aom0amplitude", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["CoolingPulseIntensity"]);
        p.AddLinearRamp("aom1amplitude", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["CoolingPulseRepumpIntensity"]);

        //use to linearly ramp the magnetic field up during the CMOT phase
        p.AddLinearRamp("coil0current", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["BottomMagCoilCurrent"]);
        p.AddLinearRamp("coil1current", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["TopMagCoilCurrent"]);

        //use to linearly ramp the magnetic field up whilst the atoms are in the magnetic trap
        //p.AddLinearRamp("coil0current", (int)Parameters["CMOTEndTime"] + 1, (int)Parameters["SecondaryRampLength"], (double)Parameters["BottomMagCoilCurrentFinal"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["CMOTEndTime"] + 1, (int)Parameters["SecondaryRampLength"], (double)Parameters["TopMagCoilCurrentFinal"]);

        //use to switch the magnetic field up during the CMOT phase
        //p.AddAnalogValue("coil0current", (int)Parameters["CMOTStartTime"], (double)Parameters["BottomMagCoilCurrent"]);
        //p.AddAnalogValue("coil1current", (int)Parameters["CMOTStartTime"], (double)Parameters["TopMagCoilCurrent"]);

        //linearly ramp MOT frequency, amplitude and repump amplitude back to usual MOT values
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CMOTStartTime"] + 501, 500, (double)Parameters["aom0Detuning"]);
        //p.AddLinearRamp("aom0amplitude", (int)Parameters["CMOTStartTime"] + 501, 500, 6.0);
        //p.AddLinearRamp("aom1amplitude", (int)Parameters["CMOTStartTime"] + 501, 500, (double)Parameters["aom1Power"]);

        //linearly ramp the magnetic field back down to the usual MOT parameters
        //p.AddLinearRamp("coil0current", (int)Parameters["CMOTStartTime"] + 501, 500, (double)Parameters["BottomMOTCoilCurrent"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["CMOTStartTime"] + 501, 500, (double)Parameters["TopMOTCoilCurrent"]);

        //for loading into the mag trap - linearly ramp the magnetic field back down whilst the atoms are in the mag trap
        //p.AddLinearRamp("coil0current", (int)Parameters["MagRampDownTime"] + 1, (int)Parameters["SecondaryRampLength"], (double)Parameters["BottomMagCoilCurrent"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["MagRampDownTime"] + 1, (int)Parameters["SecondaryRampLength"], (double)Parameters["TopMagCoilCurrent"]);

        //for loading into mag trap - linearly ramp MOT frequency, amplitude and repump amplitude back to usual MOT values
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CMOTEndTime"] + 501, 650, (double)Parameters["aom0Detuning"]);
        p.AddLinearRamp("aom0amplitude", (int)Parameters["CMOTRecaptureTime"] + 10, 650, 6.0);
        p.AddLinearRamp("aom1amplitude", (int)Parameters["CMOTRecaptureTime"] + 10, 650, (double)Parameters["aom1Power"]);

        //for loading into the mag trap - linearly ramp the magnetic field back down to the usual MOT parameters
        p.AddLinearRamp("coil0current", (int)Parameters["CMOTRecaptureTime"] + 10, 650, (double)Parameters["BottomMOTCoilCurrent"]);
        p.AddLinearRamp("coil1current", (int)Parameters["CMOTRecaptureTime"] + 10, 650, (double)Parameters["TopMOTCoilCurrent"]);

        //for loading into the mag trap - switch the magnetic field back down to the usual MOT parameters
        //p.AddAnalogValue("coil0current", (int)Parameters["CMOTEndTime"] + 501, (double)Parameters["BottomMOTCoilCurrent"]);
        //p.AddAnalogValue("coil1current", (int)Parameters["CMOTEndTime"] + 501, (double)Parameters["TopMOTCoilCurrent"]);

        //switch MOT frequency, amplitude and repump amplitude back to usual MOT values
        //p.AddAnalogValue("aom0frequency", (int)Parameters["CMOTStartTime"] + 501, (double)Parameters["aom0Detuning"]);
        //p.AddAnalogValue("aom0amplitude", (int)Parameters["CMOTStartTime"] + 501, 6.0);
        //p.AddAnalogValue("aom1amplitude", (int)Parameters["CMOTStartTime"] + 501, (double)Parameters["aom1Power"]);

        //switch magnetic field back down to the usual MOT parameters
        //p.AddAnalogValue("coil0current", (int)Parameters["CMOTStartTime"] + 501, (double)Parameters["BottomMOTCoilCurrent"]);
        //p.AddAnalogValue("coil1current", (int)Parameters["CMOTStartTime"] + 501, (double)Parameters["TopMOTCoilCurrent"]);

        p.AddAnalogValue("aom1frequency", (int)Parameters["Frame1Trigger"] - 1, (double)Parameters["absImageRepumpDetuning"]);

        p.AddAnalogValue("coil0current", (int)Parameters["MOTEndTime"], 0);
        p.AddAnalogValue("coil1current", (int)Parameters["MOTEndTime"], 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
