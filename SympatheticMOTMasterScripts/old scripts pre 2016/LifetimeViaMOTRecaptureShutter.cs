using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used for measuring the lifetime of the magnetic trap by taking the MOT atoms, loading them into the mag trap for some 
// period of time before reforming the MOT and imaging the cloud. Note that this script only takes an image of the atoms recaptured in the MOT
// and does not take an image of the initial MOT due to the background subtraction problem. So to find the fraction of atoms that remain vs. time
// also have to use MOTimaging script to get initial MOT atom number.

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 210000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 2.273;
        Parameters["BottomMOTCoilCurrent"] = 2.355;
        Parameters["MOTDetuning"] = 212.0;
        Parameters["MOTRepumpDetuning"] = 201.0;
        Parameters["ZeemanDetuning"] = 203.0;
        Parameters["AbsDetuning"] = 199.0;
        Parameters["ProbeDetuning"] = 203.3;
        Parameters["ProbeRepumpDetuning"] = 203.0;
        Parameters["ProbeRepumpIntensity"] = 6.0;
        Parameters["MOTIntensity"] = 6.0;
        Parameters["MOTRepumpIntensity"] = 6.0;
        Parameters["MOTEndTime"] = 102000;
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame0TriggerDuration"] = 10;
        Parameters["MagTrapEndTime"] = 107000;   //image atoms after 0.5s in mag trap
        Parameters["Frame1TriggerDuration"] = 10;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 185000;
        Parameters["ExposureTime"] = 100;    //Remember to change this when the MM camera shutter value is changed in text file!
        Parameters["TSAcceleration"] = 2000.0;
        Parameters["TSDeceleration"] = 2000.0;
        Parameters["TSDistance"] = 0.0;
        Parameters["TSVelocity"] = 500.0;
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

        p.Pulse((int)Parameters["MOTEndTime"] + 1100, 0, 20000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);

        //switches off Zeeman and Zeeman repump beams before releasing atoms into the magnetic trap
        p.AddEdge("shutterenable", (int)Parameters["MOTEndTime"] - 5000, false);
        p.AddEdge("ovenShutterClose", (int)Parameters["MOTEndTime"] - 1600, true);
        p.AddEdge("aom2enable", (int)Parameters["MOTEndTime"] - 20, false);
        p.AddEdge("aom3enable", (int)Parameters["MOTEndTime"] - 20, false);

        // loads the mag trap by switching off the MOT and repump beams
        p.AddEdge("aom0enable", (int)Parameters["MOTEndTime"], false);
        p.AddEdge("aom1enable", (int)Parameters["MOTEndTime"], false);

        //reforms the MOT
        p.AddEdge("aom0enable", (int)Parameters["MagTrapEndTime"], true);
        p.AddEdge("aom1enable", (int)Parameters["MagTrapEndTime"], true);

        //flashes on probe beam in order to image the atoms in the reformed MOT
        p.Pulse((int)Parameters["MagTrapEndTime"] + 10, 0, (int)Parameters["ExposureTime"] + 2, "aom3enable");
        p.DownPulse((int)Parameters["MagTrapEndTime"] + 10, 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //flashes on probe beam for the "no atoms" picture, where the no atoms image is always taken 100ms after the atoms are released
        //from the trap, then a background image is taken 
        p.Pulse((int)Parameters["MagTrapEndTime"] + 1205, 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["MagTrapEndTime"] + 1205, 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
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
        //p.AddChannel("proberepumpshutter");

        //initialises all channels before the MOT starts loading
        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["MOTDetuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["MOTRepumpDetuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["ZeemanDetuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["AbsDetuning"]);
        p.AddAnalogValue("aom0amplitude", 0, (double)Parameters["MOTIntensity"]);
        p.AddAnalogValue("aom1amplitude", 0, (double)Parameters["MOTRepumpIntensity"]);
        //p.AddAnalogValue("proberepumpshutter", 0, 0.0);

        //switches the probe repump light to the correct frequency for all subsequent images
        p.AddAnalogValue("aom3frequency", (int)Parameters["MOTEndTime"] + 1, (double)Parameters["ProbeDetuning"]);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["MOTEndTime"] + 1, (double)Parameters["ProbeRepumpIntensity"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["MOTEndTime"] + 1, (double)Parameters["ProbeRepumpDetuning"]);

        //switches off the magnetic field ready for the no atoms image
        p.AddAnalogValue("coil0current", (int)Parameters["MagTrapEndTime"] + 205, 0);
        p.AddAnalogValue("coil1current", (int)Parameters["MagTrapEndTime"] + 205, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}