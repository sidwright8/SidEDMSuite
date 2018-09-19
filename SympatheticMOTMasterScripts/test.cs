using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for running grey molasses after a MOT. 
//Useful timescales to remember:
//  The coils take about 300us to switch off
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
        Parameters["PatternLength"] = 40000;//in units of 100microseconds.
        Parameters["NumberOfFrames"] = 3;
        Parameters["ROIforImageProcessingy1"] = 100;
        Parameters["ROIforImageProcessingy2"] = 950;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 250;
        Parameters["ROIforImageProcessingx2"] = 950;//must be between 0 1387

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
        
        //The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        
        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        
        p.AddEdge("CameraTrigger", 0, true);
        p.AddEdge("shutterenable", 0, true);
        p.AddEdge("shutterenable", 500, false);
        p.AddEdge("shutterenable", 1500, true);
      
        
        //p.DownPulse(10000 , -5, 2, "CameraTrigger"); //take an image without the cloud. 

        
        p.DownPulse(1000, 0, 2, "CameraTrigger"); //background image - no light.
        //p.DownPulse(10005, 0, 2, "CameraTrigger"); //take an image without the cloud. 
        //p.DownPulse(10010, 0, 2, "CameraTrigger"); //take an image without the cloud. 

        p.DownPulse(20005, 0, 50, "CameraTrigger"); //take an image without the cloud. 
        p.DownPulse(30010, 0, 50, "CameraTrigger"); //take an image without the cloud.
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);

        p.AddChannel("offsetlockfrequency");
        p.AddAnalogValue("offsetlockfrequency", 0, 9.0);

        p.SwitchAllOffAtEndOfPatternExcept(new string[] { "offsetlockfrequency"});
        return p;
    }

}
