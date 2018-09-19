using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for testing switching the generator output power. 
// The units are 100us. So I have made a function to convert from seconds to the 100us units. 
public class Patterns : MOTMasterScript
{
    public int ConvertFromSeconds(double inputValue) //this converts to the horrible units from seconds
    {
        return Convert.ToInt32(10000 * inputValue);
    }

    public Patterns()
    {
        Parameters = new Dictionary<string, object>();
        Parameters["PatternLength"] = 150000;//in units of 100microseconds.

        //MW generator Settings
        Parameters["MWStartPower"]=-17.0; //dBm
        Parameters["MWOnPower"]=-12.0;
        Parameters["MWFinalPower"]=-17.0;
        Parameters["MWStartTime"] = 20000;
        Parameters["MWRampDownTime"] = 130000;

                       
        //Imaging settings
        Parameters["NumberOfFrames"] = 1;
        Parameters["ROIforImageProcessingy1"] = 100;
        Parameters["ROIforImageProcessingy2"] = 950;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 250;
        Parameters["ROIforImageProcessingx2"] = 1150;//must be between 0 1387


        Parameters["TSDistance"] = 10.0;
        Parameters["TSVelocity"] = 750.0;
        Parameters["TSAcceleration"] = 2000.0;
        Parameters["TSDeceleration"] = 2000.0;
        Parameters["TSDistanceF"] = 200.0;
        Parameters["TSDistanceB"] = 200.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();
        
        //The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        
        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        
        //Imaging
               
        p.DownPulse(20000, 0, 50, "CameraTrigger"); //background image - no light.
        
        
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);



        p.AddChannel("MWGeneratorAM");
        
               
       

        p.AddAnalogValue("MWGeneratorAM", 0, (double)Parameters["MWStartPower"]);

        //Ramp the power linearly by ramping the dBm logarhythmically{
        p.AddLinFromDbRamp("MWGeneratorAM", (int)Parameters["MWStartTime"], 100000, (double)Parameters["MWOnPower"]);
        p.AddLinFromDbRamp("MWGeneratorAM", (int)Parameters["MWRampDownTime"], 5000, (double)Parameters["MWFinalPower"]);
        //}

        //Ramp the dBm linearly{
        //p.AddLinearRamp("MWGeneratorAM", 90000,100,(double)Parameters["MWOnPower"]-6.0);//switch to quarter of the input power over 10ms 
        //p.AddLinearRamp("MWGeneratorAM", 90100, 400, (double)Parameters["MWOnPower"]);
        //p.AddLinearRamp("MWGeneratorAM",135000,500,(double)Parameters["MWFinalPower"]);
        //} 
       
        //p.AddAnalogValue("TopTrappingCoilcurrent", 90000, 0.0);
        //p.AddAnalogValue("BottomTrappingCoilcurrent", 90000,0.0);


        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "MWGeneratorAM" });
        return p;
    }

}
