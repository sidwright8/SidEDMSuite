using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script loads the magnetic trap. The MOT is loaded, then the AOMs are switched off to load the mag trap,
// three images are taken, one of the MOT, one of mag trap (AOMs are flashed on to obtain a fluorescence image 
// of the atoms loaded into the mag trap) and finally a background image with no atoms. Tranport of the atoms is
// also possible using this script. 

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 210000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 4.07;
        Parameters["BottomMOTCoilCurrent"] = 4.05;
        Parameters["MOTLoadDuration"] = 100000;
        Parameters["MagTrapDuration"] = 40000;
        Parameters["NumberOfFrames"] = 3; //4;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 90000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 141000;
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 190000;
        //Parameters["Frame3TriggerDuration"] = 100;
        //Parameters["Frame3Trigger"] = 99400;
        Parameters["CameraExposure"] = 500; // NOTE this does not change the camera exposure time, you have to change the 
                                           // value in the camera attributes file, this is used to switch the Zeeman light
                                           // when an image is taken. 
        Parameters["aom2Detuning"] = 200.875;
        Parameters["aom3Detuning"] = 200.875;
        Parameters["MagRampTime"] = 99900; // Only used if the current is increased before loading the magnetic trap
        Parameters["TopMagTrapCurrent"] = 10.0; // Only used if the current is increased before loading the magnetic trap
        Parameters["BottomMagTrapCurrent"] = 10.0; // Only used if the current is increased before loading the magnetic trap
        Parameters["TSAcceleration"] = 1200.0;
        Parameters["TSDeceleration"] = 1200.0;
        Parameters["TSDistance"] = 30.0;
        Parameters["TSVelocity"] = 260.0;
        Parameters["TSDistanceF"] = 446.0;
        Parameters["TSDistanceB"] = 446.0;

    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.Pulse((int)Parameters["MOTLoadDuration"] + 1000, 0, 20000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");
        //p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        p.DownPulse(200000, 0, 50, "CameraTrigger");
        p.DownPulse(205000, 0, 50, "CameraTrigger");

        // switches off the Zeeman and absorption beams to avoid reloading the MOT whilst imaging, and to allow fluorescence images to be obtained, Zeeman repump
        // is also switched off for imaging and is left off
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 5000, false);
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["CameraExposure"], "aom2enable");
        //p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["CameraExposure"], "aom3enable"); // shouldn't switch off abs. AOM as this will effect MOT beam intensity

        // loads the mag trap, Zeeman light is not switched back on again to avoid MOT reloading
        p.DownPulse((int)Parameters["MOTLoadDuration"], 0, (int)Parameters["MagTrapDuration"], "aom0enable");
        p.DownPulse((int)Parameters["MOTLoadDuration"], 0, (int)Parameters["MagTrapDuration"], "aom1enable");
        p.AddEdge("aom2enable", (int)Parameters["MOTLoadDuration"], false);
        p.AddEdge("aom3enable", (int)Parameters["MOTLoadDuration"], false);

        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);

        p.AddChannel("aom2frequency");
        p.AddChannel("aom3frequency");

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        //p.AddAnalogValue("coil0current", (int)Parameters["MOTLoadDuration"] - 800, (double)Parameters["BottomMagTrapCurrent"]);
        //p.AddAnalogValue("coil1current", (int)Parameters["MOTLoadDuration"] - 800, (double)Parameters["TopMagTrapCurrent"]);
        p.AddAnalogValue("coil0current", 180000, 0);
        p.AddAnalogValue("coil1current", 180000, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}

