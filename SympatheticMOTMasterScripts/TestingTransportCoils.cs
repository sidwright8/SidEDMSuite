using MOTMaster;
using MOTMaster.SnippetLibrary;

using System;
using System.Collections.Generic;

using DAQ.Pattern;
using DAQ.Analog;

// This script is for running testing moving the trnasport coils over the microwave cavity. 
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
        Parameters["PatternLength"] = 110000;//in units of 100microseconds.
        Parameters["MOTStartTime"] = 10000;
        Parameters["MOTLoadEndTime"] = 68000;
        Parameters["MOTEndTime"] = 70000;
        Parameters["TopMOTCoilCurrent"] = 0.0;
        Parameters["BottomMOTCoilCurrent"] = 0.0;
        Parameters["TopCoilCurrent"] = 0.7;
        Parameters["BottomCoilCurrent"] = 0.7;

               
        //Imaging settings
        Parameters["NumberOfFrames"] = 1;
        Parameters["ROIforImageProcessingy1"] = 100;
        Parameters["ROIforImageProcessingy2"] = 950;//must be between 0 and 1037
        Parameters["ROIforImageProcessingx1"] = 250;
        Parameters["ROIforImageProcessingx2"] = 1150;//must be between 0 1387


        Parameters["TSDistance"] = 1.0;
        Parameters["TSVelocity"] = 750.0;
        Parameters["TSAcceleration"] = 20.0;
        Parameters["TSDeceleration"] = 20.0;
        Parameters["TSDistanceF"] = 5.0;
        Parameters["TSDistanceB"] = 5.0;
    }

    public override PatternBuilder32 GetDigitalPattern()
    {
        PatternBuilder32 p = new PatternBuilder32();
        
        //The pattern builder assumes that digital channels are off at time zero, unless you tell them so.  
        //Turning anything Off as a first command will cause "edge conflict error", unless it was turned On at time zero.

        
        p.Pulse(0, 0, 1, "AnalogPatternTrigger");  //NEVER CHANGE THIS!!!! IT TRIGGERS THE ANALOG PATTERN!

        p.AddEdge("CameraTrigger", 0, true);
        

        p.AddEdge("TranslationStageTrigger", 30000, true);
        p.AddEdge("shutterenable", 30000, true);
        p.AddEdge("shutterenable", 35000, false);

        p.AddEdge("shutterenable", 90000, true);
        p.AddEdge("shutterenable", 95000, false);
        p.AddEdge("TranslationStageTrigger", 90000, false);
        

        //Imaging
        

        
        p.DownPulse(90000, 0, 50, "CameraTrigger"); //background image - no light.
        
        
        return p;
    }

    public override AnalogPatternBuilder GetAnalogPattern()
    {
        AnalogPatternBuilder p = new AnalogPatternBuilder((int)Parameters["PatternLength"]);



        p.AddChannel("TopTrappingCoilcurrent");
        p.AddChannel("BottomTrappingCoilcurrent");
               
       

        p.AddAnalogValue("TopTrappingCoilcurrent", 0, (double)Parameters["TopCoilCurrent"]);
        p.AddAnalogValue("BottomTrappingCoilcurrent", 0, (double)Parameters["BottomCoilCurrent"]);
        //p.AddAnalogValue("TopTrappingCoilcurrent", 90000, 0.0);
        //p.AddAnalogValue("BottomTrappingCoilcurrent", 90000,0.0);


        p.SwitchAllOffAtEndOfPatternExcept(new string[] { });
        return p;
    }

}
