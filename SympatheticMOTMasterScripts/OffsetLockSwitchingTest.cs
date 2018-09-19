using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for testing the speed at which the offset lock can switch
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
        Parameters["PatternLength"] = 30000;//in units of 100microseconds.
        Parameters["PatternLength"] = 30000;
        Parameters["NumberOfFrames"] = 3;
        Parameters["StartOLF"] = 9.15;
        Parameters["FinalOLF"] = 8.85;
        Parameters["SwitchTime"] = 10000;

        Parameters["ROIforImageProcessingy1"] = 100;
        Parameters["ROIforImageProcessingy2"] = 950;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 250;
        Parameters["ROIforImageProcessingx2"] = 1150;//must be between 0 1387

    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();
        
        //The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);

        p.AddEdge("D2EOMenable", 0, true);
        p.AddEdge("D2EOMenable", (int)Parameters["SwitchTime"],false);

        p.DownPulse(10, 0, 50, "CameraTrigger");
        p.DownPulse(10010, 0, 50, "CameraTrigger");
        p.DownPulse(20010, 0, 50, "CameraTrigger");
        
        
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        p.AddChannel("offsetlockfrequency");

        p.AddAnalogValue("offsetlockfrequency", 0, (double)Parameters["StartOLF"]);
        p.AddLinearRamp("offsetlockfrequency", (int)Parameters["SwitchTime"],100, (double)Parameters["FinalOLF"]);



        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "offsetlockfrequency"});
        return p;
    }

}
