using System;
using System.Collections;

using NationalInstruments.DAQmx;

using DAQ.Pattern;

namespace DAQ.HAL

{
    public class BufferGasHardware : DAQ.HAL.Hardware
    {
        public BufferGasHardware()
        {
          // add the boards
            Boards.Add("daq", "/dev1");
            Boards.Add("pg", "/dev2");

            // map the digital channels
            string pgBoard = (string)Boards["pg"];

            AddDigitalOutputChannel("q", pgBoard, 0, 0);
            AddDigitalOutputChannel("aom", pgBoard, 0, 1);//pin 44
            AddDigitalOutputChannel("flash", pgBoard, 0, 2);
            //(0,3) pin 12 is unconnected
            //AddDigitalOutputChannel("shutter", pgBoard, 0, 4);//pin 13
            //AddDigitalOutputChannel("probe", pgBoard, 0, 1);//pin 13
            //(0,5) is reserved as the switch line
            AddDigitalOutputChannel("valve", pgBoard, 0, 6);

            AddDigitalOutputChannel("detector", pgBoard, 2, 0); //Pin 16

            // add things to the info
            // the analog triggers
            Info.Add("analogTrigger0", (string)Boards["daq"] + "/PFI0");
            Info.Add("analogTrigger1", (string)Boards["daq"] + "/PFI1");
            Info.Add("phaseLockControlMethod", "analog");
            Info.Add("PGClockLine", Boards["pg"] + "/PFI2");
            Info.Add("PatternGeneratorBoard", pgBoard);

            // map the analog channels
            string daqBoard = (string)Boards["daq"];
            AddAnalogInputChannel("pmt", daqBoard + "/ai0", AITerminalConfiguration.Rse);//Pin 68
            AddAnalogInputChannel("photodiode", daqBoard + "/ai1", AITerminalConfiguration.Rse);//Pin 33
            AddAnalogInputChannel("bogus", daqBoard + "/ai2", AITerminalConfiguration.Rse);//Pin 65
            AddAnalogOutputChannel("laser", daqBoard + "/ao0");//Pin 22
            AddAnalogOutputChannel("phaseLockAnalogOutput", daqBoard + "/ao1");

            //map the counter channels
            AddCounterChannel("pmt", daqBoard + "/ctr0");
            AddCounterChannel("sample clock", daqBoard + "/ctr1");

           //These need to be activated for the phase lock
          //AddCounterChannel("phaseLockOscillator", daqBoard + "/ctr0"); //This should be the source pin of a counter
          //AddCounterChannel("phaseLockReference", daqBoard + "/PFI9"); //This should be the gate pin of the same counter - need to check it's name
        }
    }
}
