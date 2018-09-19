using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to look at how the MOT compresses via absorption imaging as the magnetic field is turned up. It then takes an 
// image of the atoms in the mag trap to see how the CMOT phase effects loading of the mag trap. First the atoms in the MOT are 
//imaged, then the magnetic field is increased, an image of the CMOT is taken. Finally the MOT light is switched off so the atoms are
// loaded into the magnetic trap and after some time (to allow all the hot atoms to escape) another image is taken. The last two
// images are the standard "probe beam, no atoms" picture and a background image (with the probe beam switched off). Currently the 
//field is simply switched to a higher current, it may be worth looking at what happens when the field is ramped slowly. 

public class Patterns : MOTMasterScript
{
    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 190000;
        Parameters["MOTStartTime"] = 1000;
        Parameters["CMOTStartTime"] = 101000;  //atoms are loaded into the CMOT
        Parameters["CoolingPulseStartTime"] = 101000; //start of cooling pulse
        Parameters["CMOTEndTime"] = 101650;  //atoms are loaded into the mag trap after 65ms in the CMOT
        //Parameters["ReformMOTTime"] = 130650; //atoms are recaptured in the MOT after spending 3s in the mag trap
        //Parameters["MOTEndTime"] = 135000; //time at which atoms are released ready for the background images
        Parameters["TopMOTCoilCurrent"] = 2.316;
        Parameters["BottomMOTCoilCurrent"] = 2.41;
        Parameters["TopMagCoilCurrent"] = 3.325;
        Parameters["BottomMagCoilCurrent"] = 3.431;
        //Parameters["TopMagCoilCurrentFinal"] = 5.04;
        //Parameters["BottomMagCoilCurrentFinal"] = 5.17;
        Parameters["NumberOfFrames"] = 4;
        Parameters["Frame0TriggerDuration"] = 10;
        Parameters["Frame0Trigger"] = 99000;
        //Parameters["Frame1TriggerDuration"] = 10;
        //Parameters["Frame1Trigger"] = 101640;  //image the atoms 1ms before releasing the cloud
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 101652;   //image atoms 3s after loading the mag trap (allowing time for parameters to ramp back down)
        Parameters["Frame3TriggerDuration"] = 10;
        Parameters["Frame3Trigger"] = 120000;
        Parameters["Frame4TriggerDuration"] = 10;
        Parameters["Frame4Trigger"] = 125000;
        //Parameters["Frame5TriggerDuration"] = 10;
        //Parameters["Frame5Trigger"] = 179000;
        Parameters["ExposureTime"] = 1;
        Parameters["aom0Detuning"] = 213.0;
        Parameters["aom1Detuning"] = 197.0;
        Parameters["aom2Detuning"] = 204.0;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["AbsorptionDetuning"] = 202.0;
        Parameters["AbsorptionPower"] = 1.66;
        Parameters["ProbeRepumpDetuning"] = 203.0;
        //Parameters["CoolingPulseDetuning"] = 210.0;
        //Parameters["CoolingPulseRepumpDetuning"] = 202.0; 
        Parameters["CoolingPulseRepumpIntensity"] = 1.48;
        Parameters["CoolingPulseIntensity"] = 1.2;
        Parameters["CoolingPulseDuration"] = 650; //should be chosen to match cooling pulse start time
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

        p.Pulse((int)Parameters["CMOTEndTime"] + 1100, 0, 15000, "TranslationStageTrigger");

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman and Zeeman repump beams during imaging, so that MOT is not reloaded, then takes image of MOT,
        // probe beam is pulsed on for image
        p.AddEdge("shutterenable", (int)Parameters["Frame0Trigger"] - 5000, false);
        p.AddEdge("aom2enable", (int)Parameters["Frame0Trigger"] - 500, false);
        p.AddEdge("aom3enable", (int)Parameters["Frame0Trigger"] - 500, false);
        p.AddEdge("probeshutterenable", (int)Parameters["Frame0Trigger"] - 500, true);

        //takes an image of the atoms in the MOT
        p.Pulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame0Trigger"], 0, (int)Parameters["Frame0TriggerDuration"], "CameraTrigger");       

        //pulses on abs beam in order to take an image of the compressed MOT
        //p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        //p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        //switches off MOT beams to load the magnetic trap
        p.AddEdge("aom0enable", (int)Parameters["CMOTEndTime"], false);
        p.AddEdge("aom1enable", (int)Parameters["CMOTEndTime"], false);

        //pulses on the absorption beam to optically pump the atoms
        //p.Pulse((int)Parameters["CMOTEndTime"], 0, 1, "aom3enable");
        //p.AddEdge("aom1enable", (int)Parameters["CMOTEndTime"] + 5, false);

        //use the following block of code to reform the MOT before imaging the magnetically trapped atoms
        //NOTE - either use this block or the next block but not both together
        
        //reforms the MOT by switching on the MOT beams, then takes an image of the atoms by pulsing on the abs beam
        //p.AddEdge("aom0enable", (int)Parameters["ReformMOTTime"], true);
        //p.AddEdge("aom1enable", (int)Parameters["ReformMOTTime"], true);
        //p.Pulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        //p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");


        //use the following block of code to image the atoms directly in the magnetic trap (or immediately after release from
        //mag trap

        //pulses on the abs and MOT repump beams to image the atoms in the magnetic trap
        p.Pulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.Pulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame2Trigger"], 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");


        //abs and MOT repump beams are pulsed on for the "no atoms" image, and then a background image is taken
        //p.Pulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.Pulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");
        p.DownPulse((int)Parameters["Frame4Trigger"], 0, (int)Parameters["Frame4TriggerDuration"], "CameraTrigger");

        //switches on mot and mot repump beams to take background image for atoms in the MOT (note F=1 probe beam power increases
        // if the MOT AOM is switched off)
        //p.AddEdge("aom1enable", 159000, true);
        //p.AddEdge("aom0enable", 159000, true);
        //p.Pulse((int)Parameters["Frame5Trigger"] - 1, 0, (int)Parameters["ExposureTime"] + 2, "aom3enable");
        //p.DownPulse((int)Parameters["Frame5Trigger"], 0, (int)Parameters["Frame5TriggerDuration"], "CameraTrigger");

        p.DownPulse(183000, 0, 5, "CameraTrigger");
        p.DownPulse(185000, 0, 5, "CameraTrigger");

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
        p.AddAnalogValue("aom1amplitude", 0, 6.0);
        p.AddAnalogValue("aom3amplitude", 0, 6.0);
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["AbsorptionDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["Frame0Trigger"] - 10, (double)Parameters["AbsorptionPower"]);
        p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame0Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["ProbeRepumpDetuning"], (double)Parameters["aom1Detuning"]);

        //cooling pulse - switch MOT frequency, amplitude and repump amplitude
        //p.AddAnalogPulse("aom0frequency", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseDetuning"], (double)Parameters["aom0Detuning"]);
        //p.AddAnalogPulse("aom0amplitude", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseIntensity"], 6.0);
        //p.AddAnalogPulse("aom1amplitude", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseRepumpIntensity"], 6.0);

        //cooling pulse - linearly ramp MOT frequency, amplitude and repump amplitude
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseDetuning"]);
        p.AddLinearRamp("aom0amplitude", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseIntensity"]);
        p.AddLinearRamp("aom1amplitude", (int)Parameters["CoolingPulseStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["CoolingPulseRepumpIntensity"]);

        //use for switching the magnetic field to a higher value during the CMOT phase
        //p.AddAnalogValue("coil0current", (int)Parameters["CMOTStartTime"], (double)Parameters["BottomMagCoilCurrent"]);
        //p.AddAnalogValue("coil1current", (int)Parameters["CMOTStartTime"], (double)Parameters["TopMagCoilCurrent"]);

        //use to linearly ramp the magnetic field up during the CMOT phase
        p.AddLinearRamp("coil0current", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["BottomMagCoilCurrent"]);
        p.AddLinearRamp("coil1current", (int)Parameters["CMOTStartTime"], (int)Parameters["CoolingPulseDuration"], (double)Parameters["TopMagCoilCurrent"]);

        //secondary ramp - use to linearly ramp the magnetic field up whilst the atoms are in the magnetic trap
        //p.AddLinearRamp("coil0current", (int)Parameters["CMOTEndTime"] + 1, 1000, (double)Parameters["BottomMagCoilCurrentFinal"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["CMOTEndTime"] + 1, 1000, (double)Parameters["TopMagCoilCurrentFinal"]);

        // pulsing off magnetic field in order to optically pump the atoms
        //p.AddAnalogPulse("coil0current", (int)Parameters["CMOTEndTime"], 10000, 0.0, (double)Parameters["BottomMagCoilCurrent"]);
        //p.AddAnalogPulse("coil1current", (int)Parameters["CMOTEndTime"], 10000, 0.0, (double)Parameters["TopMagCoilCurrent"]);

        //use for imaging the cloud released from the mag trap
        //p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame1Trigger"] - 2, (int)Parameters["ExposureTime"] + 4, (double)Parameters["ProbeRepumpDetuning"], (double)Parameters["aom1Detuning"]);
        //p.AddAnalogValue("aom1amplitude", (int)Parameters["CMOTEndTime"] + 2, 6.0);
        //p.AddAnalogValue("aom1frequency", (int)Parameters["CMOTEndTime"] + 2, (double)Parameters["ProbeRepumpDetuning"]);
        //p.AddAnalogValue("coil0current", (int)Parameters["Frame2Trigger"] - 2, 0);
        //p.AddAnalogValue("coil1current", (int)Parameters["Frame2Trigger"] - 2, 0);

        //secondary ramp - use for ramping the current down whilst the atoms are in the magnetic trap
        //p.AddLinearRamp("coil0current", (int)Parameters["ReformMOTTime"] - 1000, 1000, (double)Parameters["BottomMagCoilCurrent"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["ReformMOTTime"] - 1000, 1000, (double)Parameters["TopMagCoilCurrent"]);

        //use for imaging the atoms in the reformed MOT (linear ramp of parameters back to usual MOT values)
        //p.AddLinearRamp("aom0amplitude", (int)Parameters["ReformMOTTime"] + 1, (int)Parameters["CoolingPulseDuration"], 6.0);
        //p.AddLinearRamp("aom1amplitude", (int)Parameters["ReformMOTTime"] + 1, (int)Parameters["CoolingPulseDuration"], 6.0);
        //p.AddLinearRamp("coil0current", (int)Parameters["ReformMOTTime"] + 1, (int)Parameters["CoolingPulseDuration"], (double)Parameters["BottomMOTCoilCurrent"]);
        //p.AddLinearRamp("coil1current", (int)Parameters["ReformMOTTime"] + 1, (int)Parameters["CoolingPulseDuration"], (double)Parameters["TopMOTCoilCurrent"]);
        //p.AddAnalogPulse("aom1frequency", (int)Parameters["Frame2Trigger"] - 1, (int)Parameters["ExposureTime"] + 2, (double)Parameters["ProbeRepumpDetuning"], (double)Parameters["aom1Detuning"]);

        p.AddAnalogValue("coil0current", (int)Parameters["CMOTEndTime"], 0);
        p.AddAnalogValue("coil1current", (int)Parameters["CMOTEndTime"], 0);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["CMOTEndTime"] + 1, 6.0);
        p.AddAnalogValue("aom1frequency", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["ProbeRepumpDetuning"]);

        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
