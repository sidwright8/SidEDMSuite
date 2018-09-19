using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script takes the atoms from the MOT, loads them into the magnetic trap, holds them there for some period of time, releases them and then images them. 
// It does not image the MOT due to the background subtraction issue, so should be used with MOTimaging script if MOT images are required. 
// It takes three images in total, the first is of the atoms which have been released from the magnetic trap. The second is of the probe beam with no atoms present
// and finally there's a background image where the probe beam has been switched off.

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 210000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.316;
        Parameters["BottomMOTCoilCurrent"] = 2.41;
        Parameters["TopMagCoilCurrent"] = 3.325;
        Parameters["BottomMagCoilCurrent"] = 3.44;
        //Parameters["TopMagCoilCurrentFinal"] = 5.04;
        //Parameters["BottomMagCoilCurrentFinal"] = 5.17;
        Parameters["MOTDetuning"] = 213.0;
        Parameters["MOTRepumpDetuning"] = 197.0;
        Parameters["ZeemanDetuning"] = 204.0;
        Parameters["AbsorptionDetuning"] = 201.0;
        Parameters["MOTIntensity"] = 6.0;
        Parameters["MOTRepumpIntensity"] = 6.0;
        //Parameters["ZeemanIntensity"] = 5.0;
        Parameters["CMOTStartTime"] = 101000;
        Parameters["CMOTEndTime"] = 101650;
        //Parameters["SecondRampLength"] = 1000;
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 10;
        Parameters["MagTrapEndTime"] = 106950;   //image atoms after 0.53s in mag trap
        Parameters["Frame1TriggerDuration"] = 10;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 180000;
        Parameters["ExposureTime"] = 1; //Remember to change this when the MM camera shutter value is changed in text file!
        Parameters["AbsDetuning"] = 202.0;
        Parameters["AbsPower"] = 1.15;
        Parameters["AbsRepumpDetuning"] = 203.0;
        Parameters["AbsRepumpIntensity"] = 6.0;
        Parameters["CoolingPulseDuration"] = 650;
        //Parameters["CoolingPulseDetuning"] = 210.0;
        Parameters["CoolingPulseRepumpIntensity"] = 1.48;
        Parameters["CoolingPulseIntensity"] = 1.2;
        Parameters["TSAcceleration"] = 2000.0;
        Parameters["TSDeceleration"] = 2000.0;
        Parameters["TSDistance"] = 55.0;
        Parameters["TSVelocity"] = 500.0;
        Parameters["TSDistanceF"] = 55.0;
        Parameters["TSDistanceB"] = 55.0;

    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.Pulse((int)Parameters["CMOTEndTime"] + 100, 0, 20000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman, Zeeman repump and probe beams before CMOT stage to stop loading the MOT
        p.AddEdge("shutterenable", (int)Parameters["CMOTStartTime"] - 5000, false);
        p.AddEdge("aom2enable", (int)Parameters["CMOTStartTime"] - 20, false);
        p.AddEdge("aom3enable", (int)Parameters["CMOTStartTime"] - 20, false);

        // loads the mag trap by switching off the MOT and repump beams
        p.AddEdge("aom0enable", (int)Parameters["CMOTEndTime"], false);
        p.AddEdge("aom1enable", (int)Parameters["CMOTEndTime"], false);

        //opens the probe beam shutter 50ms before the magnetic trap is switched off
        p.AddEdge("probeshutterenable", (int)Parameters["MagTrapEndTime"] - 500, true);

        //flashes on probe and repump beam in order to image the atoms in the mag trap
        p.Pulse((int)Parameters["MagTrapEndTime"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        //p.Pulse((int)Parameters["MagTrapEndTime"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.DownPulse((int)Parameters["MagTrapEndTime"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //flashes on probe and repump beam for the "no atoms" picture, where the no atoms image is always taken 100ms after the atoms are released from the
        //magnetic trap, then takes background image 
        p.Pulse((int)Parameters["MagTrapEndTime"] + 1000, 0, (int)Parameters["ExposureTime"], "aom3enable");
        //p.Pulse((int)Parameters["MagTrapEndTime"] + 1000, 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.DownPulse((int)Parameters["MagTrapEndTime"] + 1000, 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        p.DownPulse(205000, 0, 50, "CameraTrigger");
        p.DownPulse(208000, 0, 50, "CameraTrigger");

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
        //p.AddChannel("aom2amplitude");

        //initialises all channels before the MOT starts loading
        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["MOTDetuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["MOTRepumpDetuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["ZeemanDetuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["AbsorptionDetuning"]);
        p.AddAnalogValue("aom0amplitude", 0, (double)Parameters["MOTIntensity"]);
        p.AddAnalogValue("aom1amplitude", 0, (double)Parameters["MOTRepumpIntensity"]);
        p.AddAnalogValue("aom3amplitude", 0, 6.0);
        //p.AddAnalogValue("aom2amplitude", 0, (double)Parameters["ZeemanIntensity"]);

        //cooling pulse - linearly ramp MOT amplitude and repump amplitude
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseDetuning"]);
        p.AddLinearRamp("aom0amplitude", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseIntensity"]);
        p.AddLinearRamp("aom1amplitude", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseRepumpIntensity"]);

        //use to linearly ramp the magnetic field up during the CMOT phase
        p.AddLinearRamp("coil0current", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["BottomMagCoilCurrent"]);
        p.AddLinearRamp("coil1current", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["TopMagCoilCurrent"]);
        p.AddAnalogValue("aom0amplitude", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["MOTIntensity"]);
        //p.AddAnalogValue("aom0frequency", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["MOTDetuning"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["AbsRepumpIntensity"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["AbsRepumpDetuning"]);
        p.AddAnalogValue("aom3frequency", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["AbsDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["AbsPower"]);

        //use to linearly ramp the magnetic field whilst the atoms are in the magnetic trap
        //p.AddLinearRamp("coil0current", (int)Parameters["CMOTEndTime"] + 1, (int)Parameters["SecondRampLength"], (double)Parameters["BottomMagCoilCurrentFinal"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["CMOTEndTime"] + 1, (int)Parameters["SecondRampLength"], (double)Parameters["TopMagCoilCurrentFinal"]);

        //switches off the magnetic field to image the mag trap atoms
        p.AddAnalogValue("coil0current", (int)Parameters["MagTrapEndTime"] + 4, 0);
        p.AddAnalogValue("coil1current", (int)Parameters["MagTrapEndTime"] + 4, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}