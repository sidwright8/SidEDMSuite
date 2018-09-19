using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;


//This script is designed for measuring the temperature of the MOT using the ballistic expansion method. Due to problems with the background subtraction
// this script no longer takes an image of the MOT, have to do this separately using MOTImaging script. First the MOT is loaded, then the atoms are left 
// to expand by switching off both the light and the magnetic field. As they expand an image is taken. The time at which this second image is taken is 
// scanned in order to see how fast the atom cloud is expanding and hence find the its temperature.  

public class Patterns : MOTMasterScript
{


    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 200000;        
        Parameters["MOTStartTime"] = 1000;
        Parameters["CMOTStartTime"] = 101000;
        Parameters["CMOTEndTime"] = 101650;        
        Parameters["NumberOfFrames"] = 3;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 101658;          
        Parameters["Frame2TriggerDuration"] = 100;
        Parameters["Frame3TriggerDuration"] = 100;
        Parameters["Frame3Trigger"] = 140000;
        Parameters["TopMOTCoilCurrent"] = 2.273;
        Parameters["BottomMOTCoilCurrent"] = 2.355;
        Parameters["TopMagCoilCurrent"] = 3.325;
        Parameters["BottomMagCoilCurrent"] = 3.431;
        Parameters["aom0Detuning"] = 211.0;
        Parameters["aom1Detuning"] = 200.0;
        Parameters["aom2Detuning"] = 202.5;
        Parameters["aom3Detuning"] = 201.0;
        Parameters["absImageDetuning"] = 201.0;
        Parameters["absImageRepumpDetuning"] = 201.0;
        Parameters["absImagePower"] = 6.0;
        Parameters["absImageRepumpPower"] = 6.0;
        Parameters["aom1Power"] = 6.0;
        //Parameters["CoolingPulseDetuning"] = 210.0;
        //Parameters["CoolingPulseRepumpDetuning"] = 192.0;
        Parameters["CoolingPulseIntensity"] = 1.2;
        Parameters["CoolingPulseRepumpIntensity"] = 1.48;
        Parameters["CoolingPulseDuration"] = 650; //should be chosen to correspond with CMOT start time
        Parameters["ExposureTime"] = 100;
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
        p.AddEdge("D2aomshutter1", 0, true);//The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        p.AddEdge("D2aomshutter2", 0, true); //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);  // This is how you load "preset" patterns.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);

        //switches off Zeeman and Zeeman repump beams before the CMOT phase
        p.AddEdge("shutterenable", (int)Parameters["CMOTStartTime"] - 5000, false);
        p.AddEdge("aom2enable", (int)Parameters["CMOTStartTime"] - 20, false);
        p.AddEdge("aom3enable", (int)Parameters["CMOTStartTime"] - 20, false);
        p.AddEdge("probeshutterenable", (int)Parameters["CMOTEndTime"] - 500, true);

        //switches off the MOT and repump beams and takes an image of the expanding cloud 
        //(for the image, probe and repump beams are switched on)
        p.AddEdge("aom0enable", (int)Parameters["CMOTEndTime"], false);
        p.AddEdge("aom1enable", (int)Parameters["CMOTEndTime"], false);
        p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.Pulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["Frame1Trigger"], 0, (int)Parameters["Frame1TriggerDuration"], "CameraTrigger");

        //takes image of the probe beam with no atoms, this always happens 100ms after the atoms have been released from the CMOT
        p.Pulse((int)Parameters["CMOTEndTime"] + 1000, 0, (int)Parameters["ExposureTime"], "aom1enable");
        p.Pulse((int)Parameters["CMOTEndTime"] + 1000, 0, (int)Parameters["ExposureTime"], "aom3enable");
        p.DownPulse((int)Parameters["CMOTEndTime"] + 1000, 0, (int)Parameters["Frame2TriggerDuration"], "CameraTrigger");

        //takes a background image
        p.DownPulse((int)Parameters["Frame3Trigger"], 0, (int)Parameters["Frame3TriggerDuration"], "CameraTrigger");

        p.DownPulse(160000, 0, 50, "CameraTrigger");
        p.DownPulse(170000, 0, 50, "CameraTrigger");

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
        p.AddAnalogValue("aom3frequency", (int)Parameters["Frame1Trigger"] - 10, (double)Parameters["absImageDetuning"]);
        p.AddAnalogValue("aom3amplitude", (int)Parameters["Frame1Trigger"] - 10, (double)Parameters["absImagePower"]);
        
        //cooling pulse - linearly ramp MOT amplitude and repump amplitude
        //p.AddLinearRamp("aom0frequency", (int)Parameters["CMOTStartTime"], 500, (double)Parameters["CoolingPulseDetuning"]);
        p.AddLinearRamp("aom0amplitude", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["CoolingPulseIntensity"]);
        p.AddLinearRamp("aom1amplitude", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["CoolingPulseRepumpIntensity"]);
        
        //use to linearly ramp the magnetic field up during the CMOT phase
        p.AddLinearRamp("coil0current", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["BottomMagCoilCurrent"]);
        p.AddLinearRamp("coil1current", (int)Parameters["CMOTStartTime"], 650, (double)Parameters["TopMagCoilCurrent"]);

        p.AddAnalogValue("coil0current", (int)Parameters["CMOTEndTime"], 0);
        p.AddAnalogValue("coil1current", (int)Parameters["CMOTEndTime"], 0);
        p.AddAnalogValue("aom0amplitude", (int)Parameters["CMOTEndTime"] + 1, 6.0);
        p.AddAnalogValue("aom1amplitude", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["absImageRepumpPower"]);
        p.AddAnalogValue("aom1frequency", (int)Parameters["CMOTEndTime"] + 1, (double)Parameters["absImageRepumpDetuning"]);        

        //use these for looking at expansion from the MOT with the magnetic field still on or measuring the temperature of the 
        //atoms in the magnetic trap
        //p.AddAnalogValue("coil0current", 101150, 0);
        //p.AddAnalogValue("coil1current", 101150, 0);
        
        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
