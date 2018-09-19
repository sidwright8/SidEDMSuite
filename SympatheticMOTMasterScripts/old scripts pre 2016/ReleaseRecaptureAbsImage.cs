using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;


//This is a release and recapture script that can be used to measure the temperature of the MOT. It takes absorption images
// of the MOT. First it switches on the MOT, takes an image, switches off the coils to release the MOT, 
//switches them back on to recapture the MOT and then takes another image of the cloud. Then there are a few background images
// as needed for absorption imaging.   
public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["MOTLoadDuration"] = 100000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["PatternLength"] = 160000;
        Parameters["NumberOfFrames"] = 4;
        Parameters["ReleaseTime"] = 10;
        Parameters["Frame0TriggerDuration"] = 100;
        Parameters["Frame0Trigger"] = 98000;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 100210;
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame2Trigger"] = 115000;
        Parameters["Frame3TriggerDuration"] = 100;
        Parameters["Frame3Trigger"] = 120000;
        Parameters["TopMOTCoilCurrent"] = 2.9;
        Parameters["BottomMOTCoilCurrent"] = 2.61;
        Parameters["aom0Detuning"] = 213.0;
        Parameters["aom1Detuning"] = 202.0;
        Parameters["aom2Detuning"] = 204.0;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["absImageDetuning"] = 205.0;
        Parameters["TSDistance"] = 0.0;
        Parameters["TSVelocity"] = 10.0;
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

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);

        //switches off Zeeman and Zeeman repump beams during imaging, so that MOT is not reloaded, then images the MOT
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 5000, false);
        p.AddEdge("aom2enable", (int)Parameters["Frame0Trigger"] - 10, false);
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");

        //switches off all light at the same time as the magnetic field is switched off to allow the cloud to expand freely
        p.DownPulse((int)Parameters["MOTLoadDuration"], 0, (int)Parameters["ReleaseTime"], "aom0enable");
        p.DownPulse((int)Parameters["MOTLoadDuration"], 0, (int)Parameters["ReleaseTime"], "aom1enable");
        p.DownPulse((int)Parameters["MOTLoadDuration"], 0, (int)Parameters["ReleaseTime"], "aom3enable");

        //take an image of the recaptured atoms, note that there is a slight delay between the MOT switching back on and the image
        // being taken, this should allow the atoms to cool slightly so the cloud width remains the same. 
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        //MOT is dumped by switching off the magnetic field in order to take a picture of the probe beam
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        //probe beam is switched off ready for the background image
        p.AddEdge("aom3enable", (int)Parameters["Frame3Trigger"] - 10, false);
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");

        p.DownPulse(140000, 0, 50, "CameraTrigger");
        p.DownPulse(150000, 0, 50, "CameraTrigger");

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
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["absImageDetuning"]);
        p.AddAnalogPulse("coil0current", (int)Parameters["MOTLoadDuration"], (int)Parameters["ReleaseTime"], 0, (double)Parameters["BottomMOTCoilCurrent"]);
        p.AddAnalogPulse("coil1current", (int)Parameters["MOTLoadDuration"], (int)Parameters["ReleaseTime"], 0, (double)Parameters["TopMOTCoilCurrent"]);
        p.AddAnalogValue("coil0current", 110000, 0);
        p.AddAnalogValue("coil1current", 110000, 0);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
