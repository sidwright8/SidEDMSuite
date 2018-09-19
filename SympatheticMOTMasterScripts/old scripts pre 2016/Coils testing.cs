using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is used to test switching switching off the MOT coils. 
//Useful timescales to remember:
//  The coils take about 500us to switch off (though the magnetic field will persist longer than this 
//  The AOMs take a few tens of us to switch on/off
//  The units are 100us. So I have made a function to convert from seconds to the 100us units. 
public class Patterns : MOTMasterScript
{
    public int ConvertFromSeconds(double inputValue) //this converts to the horrible units from seconds
    {
        return Convert.ToInt32(10000 * inputValue);
    }

    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 50000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 1000;
        Parameters["TopMOTCoilCurrent"] = 1.85;
        Parameters["BottomMOTCoilCurrent"] = 0.0;
        Parameters["NumberOfFrames"] = 1;
        //Parameters["Frame0TriggerDuration"] = ConvertFromSeconds(0.1);
        Parameters["Frame0Trigger"] = 130002;
        Parameters["Frame1TriggerDuration"] = 100;
        Parameters["Frame1Trigger"] = 131100;
        Parameters["Frame2TriggerDuration"] = 10;
        Parameters["Frame2Trigger"] = 135000;
        Parameters["ExposureTime"] = 10;
        Parameters["D1StartTime"] = 110000;
        Parameters["D1CoolingDuration"] = 5;
        
        //MOT aom Detunings
        Parameters["aom0Detuning"] = 210.0;
        Parameters["aom1Detuning"] = 199.0;
        Parameters["aom2Detuning"] = 202.0;
        Parameters["aom3Detuning"] = 201.0;

        //D1 stage Detunings and powers
        Parameters["D1PrincipalDetuning"] = 213.0;
        Parameters["D1RepumperDetuning"] = 200.0;
        Parameters["D1RepumperPower"] = 1.6;


        //Imaging detunings
        Parameters["absImageDetuning"] = 201.0;
        Parameters["absImagePower"] = 5.0;
        Parameters["absImageRepumpDetuning"] = 201.0;


        Parameters["TSDistance"] = 10.0;
        Parameters["TSVelocity"] = 10.0;
        Parameters["TSAcceleration"] = 10.0;
        Parameters["TSDeceleration"] = 10.0;
        Parameters["TSDistanceF"] = 0.0;
        Parameters["TSDistanceB"] = 0.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();
        
        //p.AddEdge("D2aomshutter1", 0 , true);//The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //p.AddEdge("D2aomshutter2", 0, true); //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.
        
        MOTMasterScriptSnippet lm = new SHLoadMOT(p, Parameters);

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("ovenShutterClose", 0, false);
        p.AddEdge("probeshutterenable", 0, false);
        p.Pulse(10000, 0, 100, "AbsorptionScopeTrigger");
        p.Pulse(10000, 0, 500, "D1aomshutter1");
        

        p.DownPulse(15000 + 0, 0, 50, "CameraTrigger"); //take an image of the cloud after D1 stage
                     
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

        p.AddChannel("offsetlockfrequency");
        
        p.AddAnalogValue("coil0current", 0, 0);
        p.AddAnalogValue("coil1current", 0, 0);
        p.AddAnalogValue("aom3amplitude", 0, 5.3);
        p.AddAnalogValue("aom3frequency", 0, 198);
        p.AddLinearRamp("aom3frequency", 10000,10, 203);
        
        //p.AddAnalogValue("coil0current", 30000, 1.6);
        //p.AddAnalogValue("coil1current", 30000, 2.1);
        //p.AddLinearRamp("coil0current", 30000,4, 0);//bottom coil
        //p.AddLinearRamp("coil1current", 30001,3, 1.7);//top coil
        //p.AddLinearRamp("coil0current", 30007, 11, 0);//bottom coil
        //p.AddLinearRamp("coil1current", 30008, 10, 0);
        p.SwitchAllOffAtEndOfPattern();
        return p;
    }

}
