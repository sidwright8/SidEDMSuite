using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;


//This script is designed for testing to see if there is a delay between the start of the camera trigger and the point at which it starts actually taking an image.
//To do this the clock frequency must be increased (it's usually set to 10,000, which gives a time resolution of 100 microseconds, for this we want ideally want a resolution of 
//1 microsecond which requires a frequency of 1,000,000, for some reason this produces a DAQ exception so we can only use a frequency of 100,000 which gives a time resolution of 
//10 microseconds). We keep the camera trigger at some fixed point in time and scan the start time of a short pulse of light from the probe AOM. By scanning this over the camera 
//trigger pulse we can determine the delay between the camera trigger and the time at which the camera starts taking an image, and we can also determine how long the exposure is.   

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 10000;
        Parameters["NumberOfFrames"] = 1;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 1000;
        Parameters["aom0Detuning"] = 213.0;
        Parameters["aom1Detuning"] = 197.0;
        Parameters["aom2Detuning"] = 204.0;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["probePulseStartTime"] = 1009;
        Parameters["ExposureTime"] = 1;
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

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        //initialise all channels, all AOMs are switched off at the start of the script
        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("aom0enable", 0, false);
        p.AddEdge("aom1enable", 0, false);
        p.AddEdge("aom2enable", 0, false);
        p.AddEdge("aom3enable", 0, false);

        //pulse the probe beam on and take a photo
        p.Pulse((int)Parameters["probePulseStartTime"], 0, 1, "aom3enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        p.AddChannel("aom0frequency");
        p.AddChannel("aom1frequency");
        p.AddChannel("aom2frequency");
        p.AddChannel("aom3frequency");
        p.AddChannel("aom0amplitude");
        p.AddChannel("aom1amplitude");
        p.AddChannel("coil0current");
        p.AddChannel("coil1current");

        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom0frequency", 0, (double)Parameters["aom0Detuning"]);
        p.AddAnalogValue("aom1frequency", 0, (double)Parameters["aom1Detuning"]);
        p.AddAnalogValue("aom2frequency", 0, (double)Parameters["aom2Detuning"]);
        p.AddAnalogValue("aom3frequency", 0, (double)Parameters["aom3Detuning"]);
        p.AddAnalogValue("aom0amplitude", 0, 6.0);
        p.AddAnalogValue("aom1amplitude", 0, 6.0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}

