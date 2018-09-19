using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAQ.Pattern;
using DAQ.Analog;
using DAQ;

namespace MOTMaster.SnippetLibrary
{
    public class SHLoadMOT : MOTMasterScriptSnippet
    {
        public SHLoadMOT(PatternBuilder32 p, Dictionary<String, Object> parameters)
        {
            AddDigitalSnippet(p, parameters);
        }
        public SHLoadMOT(AnalogPatternBuilder p, Dictionary<String, Object> parameters)
        {
            AddAnalogSnippet(p, parameters);
        }


        public void AddDigitalSnippet(PatternBuilder32 p, Dictionary<String, Object> parameters)
        {
            //p.AddEdge("aom0enable", (int)parameters["MOTStartTime"], true);
            //p.AddEdge("aom1enable", (int)parameters["MOTStartTime"], true);
            //p.AddEdge("aom2enable", (int)parameters["MOTStartTime"], true); //Not with new optics setup
            p.AddEdge("aom3enable", (int)parameters["MOTStartTime"], true);
            p.AddEdge("D2EOMenable", (int)parameters["MOTStartTime"], true);
            p.AddEdge("ovenShutterOpen",(int)parameters["MOTStartTime"] -7000,true);//open the oven shutter
        }

        public void AddAnalogSnippet(AnalogPatternBuilder p, Dictionary<String, Object> parameters)
        {
            
            p.AddChannel("BottomTrappingCoilcurrent");
            p.AddChannel("TopTrappingCoilcurrent");
            p.AddChannel("xcoilcurrent");
            p.AddChannel("ycoilcurrent");
            p.AddChannel("zcoilcurrent");

            
            p.AddAnalogValue("xcoilcurrent", (int)parameters["MOTStartTime"], (double)parameters["XCoilCurrent"]);
            p.AddAnalogValue("ycoilcurrent", (int)parameters["MOTStartTime"], (double)parameters["YCoilCurrent"]);
            p.AddAnalogValue("zcoilcurrent", (int)parameters["MOTStartTime"], (double)parameters["ZCoilCurrent"]);

            p.AddAnalogValue("BottomTrappingCoilcurrent", (int)parameters["MOTStartTime"], (double)parameters["BottomVacCoilCurrent"]);
            p.AddAnalogValue("TopTrappingCoilcurrent", (int)parameters["MOTStartTime"], (double)parameters["TopVacCoilCurrent"]);
           

        }


    }
}
