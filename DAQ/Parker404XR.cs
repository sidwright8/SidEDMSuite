using System;
using NationalInstruments.VisaNS;
using DAQ.Environment;
using System.Collections.Generic;
using System.Threading;


namespace DAQ.HAL
{
    public class Parker404XR : RS232Instrument
    {
        string initFile;
        bool autoTrigger;
        //List of bit descriptions for Status read. Note there is no entry in the manaul for bit 15...
        List<string> StatusPropertyList = new List<string> { "Command Processing Paused: ", "Looping: ", "Wait for trigger: ", "Running Program: ", "Going Home: ", "Waiting for delay timeout", "Registration In Progress", "Last Trigger Command Timed out: ", "Motor Energised: ", "Motor Undefined: ", "Event Triggered: ", "Input in LSEL not matching label: ", "-ve limit seen during last move: ", "+ve limit seen during last move: ", "Duty cycle too high, excessive motor current: ", "[Reserved]: ", "[Reserved]: ", "Moving: ", "Stationary: ", "No registration signal seen in registration window: ", "Cannot stop within the defined registration distance: ", "Tracking limit is greater than max. allowed position error: ", "Last SETUPFB command failed: ", "In motion (0: +ve, 1: -ve): ", "Brake applied: " };
        //List of bit descriptions for Drive fault read. Annoyingly the first letter of each definition is missing in the manual. Hoepfully I inferred them all correctly. 
        List<string> DriveFaultList = new List<string> { "Composite Fault: ", "+/-15V supply rail: ", "HV under-voltage tripped : ", "HV over-voltage tripped: ", "[Reserved]: ", "I/O over voltage tripped: ", "Encoder/Aux 5V under voltage tripped: ", "Impending Power loss, V I/O under-voltage: ", "Commutation Fault: ", "Resolver Fault: ", "Motor Over Temperature: ", "Ambient over-temperature: ", "Drive over temperature: ", "Incompatible firmware version: ", "Unregonised Power Stage: ", "Controller Diagnostic Failure: ", "Output Stage Over-Current: ", "Output Driver Under-Current: ", "Trakcing Limit exceeded while in motion: ", "Velocity Exceeded: ", "Drive Disabled (check enable input and state of ES variable): ", "[Reserved]: ", "[Reserved]: ", "[Reserved]: ", "Watchdog 1: ", "[Reserved]: ", "[Reserved]: ", "[Reserved]: ", "[Reserved]: ", "[Reserved]: ", "[Reserved]: ", "CAN I/O errors: ", };
        //List of bit descriptions for User fault read. There is no entry in the manual for 20-21... 
        List<string> UserFaultList = new List<string> { "Value is out of range: ","Incorrect command syntax: ","Last label already in use: ","Label not defined: ","Missing Z pulse when homing: ","Homing failed (no signal detected): ","Home signal too narrow: ","Drive de-energised: ","Cannot relate END statement to a label: ","Program memory buffer full: ","No more motion profiles available: ","No more sequences labels available: ","End of travel limit hit: ","Still moving: ","[Reserved]: ","Transmit buffer overflow: ","User program nesting overflow: ","Cannot use an undefined profile: ","Drive not ready: ","Save/restore error: ","Cammond not supported by this product: ", "Fieldbus error: ", "Input buffer overflow: ", "Cannot execute motion, brake engaged: "}; 

        public Parker404XR(String visaAddress, String initFile): base(visaAddress)
        {
            this.initFile = initFile; 
        }
        public new void Connect()
        {
            base.Connect(SerialTerminationMethod.TerminationCharacter);
            autoTrigger = true;
            //base.Write("1E0\r\n"); 
        }
        public void Initialize(double acceleration, double deceleration, double distance, double velocity)
        {
            
            //Sid's notes:
            //The number in front of each command specifies the address to send the command 
            //The START routine is always called first to initialise a run. Think of it a bit like a header which declares all variables and functions that are not inbuilt
            //The syntax NAME: means a code block will follow which will run when NAME is called (with GOTO(NAME)). Each code block has to be terminated with END.
            //Think of GOTO like a function call. 

            base.Write("1CLEAR(ALL)\r\n");//Clears all routines and variables etc.

            base.Write("1START:\r\n");
            base.Write("1DECLARE(MOVE)\r\n");
            base.Write("1DECLARE(MOVE2)\r\n");
            base.Write("1DECLARE(INIT)\r\n");
            base.Write("1DECLARE(ALPHA)\r\n");
            base.Write("1DECLARE(BETA)\r\n");
            base.Write("1DECLARE(GAMMA)\r\n");
            base.Write("1DECLARE(DOALL)\r\n");
            base.Write("1END\r\n");// This declares all routines to be used in the run, and has to be done within the "START" block if you are uploading to the servo controller memory. 

            //The first move
            //20,20,52580,40
            //Note conversion factors for accleration/ distance etc. using 1 step = 5*10^(-3)mm and 1 revolution = 4000 steps
            base.Write("1ALPHA:\r\n");//tell it you are definining alpha
            base.Write("1O(000)\r\n");//set all 3 outputs to off. Not sure why.
            base.Write("1USE(1)\r\n");//activates profile 1. If you now use the GO command it will exectute profile 1.
            base.Write("1GOTO(MOVE)\r\n");//This now calls to routine MOVE
            base.Write("1END\r\n");

            //The return
            base.Write("1BETA:\r\n");
            base.Write("1O(000)\r\n");
            base.Write("1USE(2)\r\n");//select profile 2
            base.Write("1GOTO(MOVE)\r\n");//now go to routine MOVE
            base.Write("1END\r\n");

            //Homing routine
            base.Write("1GAMMA:\r\n");
            base.Write("1O(000)\r\n");
            base.Write("1HOME1(-,0,1,10,1)\r\n");//tells it to configure homing, the last 1 in 1HOME1 tells it to arm homing; 
            //tells it whether to use to +/- edge of the homing switch as a signal that it is home. 
            //tells it the homing switch is normally open, 
            //velocity (and direction) to initially try searching for home, acceleration/deceleration used in homing routine, mode 1 which
            //tells it when the switch edge is encountered it means the stage is definitely home (seems pointless given we already told it this in the first variable). 
            base.Write("1GOTO(MOVE2)\r\n");//Go to routine MOVE2
            base.Write("1END\r\n");

            //What to do in case of an error
            base.Write("1FAULT:\r\n");
            base.Write("1\"FAULT\"\r\n");
            base.Write("1GH\r\n");//tells it to go home. This presumably follows the homing routine, although it seems to be defined within the GAMMA variable...
            base.Write("1END\r\n");

            //Other stuff I don't really understand
            base.Write("1INIT:\r\n");//This is the initialisation routine
            //base.Write("1OFF\r\n");
            base.Write("1W(AO,0)\r\n");//W means write system variable. AO is analog distance offset 
            base.Write("1W(AB,0)\r\n");// Analogue deadband
            base.Write("1W(AM,0)\r\n");//Analogue monitor mode set to 0, which means the torque is monitored rather than the velocity
            base.Write("1W(EX,1)\r\n");//Comms response style, set to 3, meaning "speak whenever, echo on", apparently default for RS232
            //1 means speak whenever, echo OFF
            base.Write("1W(EQ,0)\r\n");//Echo queueing, 0 sets to "normal"
            base.Write("1W(BR,9600)\r\n");//Baud rate, can be 19200 or 9600
            base.Write("1W(CL,100)\r\n");//Sets the current clamp to 100% of peak drive current.
            base.Write("1W(CQ,1)\r\n");//Command queueing 
            base.Write("1W(IC,7904)\r\n");// Sets user inputs (including limits and home). 7904 means triggers on high for all channels & triggers on 24V (except input 1 which triggers on 5V. Use this for sending trigger TTLs!)
            base.Write("1W(EI,2)\r\n");//Encoder input, 2 means "quad ABZ". Drive must be de-energised to change this
            base.Write("1W(EO,2)\r\n");//Encoder signal output
            base.Write("1LIMITS(0,1,0,200.0)\r\n");//Configure limits; 0 menas the limits are enabled, 1 means limits normally closed,
            //0 means stop motion when a limit is hit and also abort program (then raise FAULT or STOP), 200.0 is the deceleration used to stop. 
            base.Write("1W(EW,50)\r\n");//Error window, range 0 to 65535. Not sure what this means.
            base.Write("1W(IT,10)\r\n");//"In position time" - not clear what this is
            base.Write("1GAINS(5.00,0.00,10.00,5.00,0)\r\n"); //sets the gain configuration for the servo loop I think; gain feedforward, gain integral action, gain proportional, gain velocity feedback, filter time constant.
            base.Write("1W(IM,1)\r\n");//"Integral mode", 1 means within integral window
            base.Write("1W(IW,25)\r\n");//Integral window, range 0 to 65535
            base.Write("1W(ES,1)\r\n");//Enable sense, 1 means the enble signal high enables. 
            base.Write("1W(TT,0)\r\n");//Optional trigger timeout in seconds. I assume trigger timeout 0 means no timeout.
            base.Write("1MOTOR(39169,6.7,4096,4100,1680,1.30,3.60,0.191)\r\n");//This tells the rest of the drive what motor is being used. 
            base.Write("1W(PC,300)\r\n");//sets the peak current, 300 means 300% of MC
            base.Write("1W(TL,4096)\r\n");//Tracking limit, range 0 to 400,000. 
            base.Write("1PROFILE1(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + "," + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            base.Write("1PROFILE2(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + ",-" + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            //this defines two profiles which can be selected using ALPHA and BETA respectively. 
            base.Write("1END\r\n");
    
            base.Write("1DOALL:\r\n");//Defines routine DOALL
            //base.Write("1PROFILE1(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + "," + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            //base.Write("1PROFILE2(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + ",-" + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            base.Write("1TR(IN,=,1XXXX)\r\n");
            base.Write("1USE(1)\r\n");
            base.Write("1G\r\n");
            base.Write("1T0.2\r\n");
            base.Write("1TR(IN,=,0XXXX)\r\n");
            base.Write("1USE(2)\r\n");
            base.Write("1G\r\n");
            base.Write("1END\r\n");

            base.Write("1MOVE:\r\n");//Defines routine MOVE. I think the changing of output 1 is just for user tracking so you can test things more easily. 
            base.Write("1O(1)\r\n");//Sets output 1 to 1, leaves 2 and 3 unchanged (normally you need O(ABC), but trailing X's can be dropped)
            base.Write("1G\r\n");//Go
            base.Write("1O(0)\r\n");//Sets output 1 to 0
            base.Write("1T1\r\n");//Pause program execution in units of 50 ms. 
            base.Write("1END\r\n");

            base.Write("1MOVE2:\r\n");//Defines MOVE2
            base.Write("1O(0)\r\n");
            base.Write("1GH\r\n");
            base.Write("1END\r\n");

            base.Write("1ARM01\r\n");//This disables the START function, but enables the FAULT label (it means a fault will trigger commands and the program to stop)
            base.Write("1GOTO(INIT)\r\n");//Runs the initialisation code. 
            //base.Write("1SV\r\n"); This line isn't really needed for immediate control, it saves your TS script into memory.
        }

        public void Restart()
        {
            base.Write("1Z\r\n");//This resets the drive's controller. The START command will only run if you have set ARM1X.   
        }
        public void On()
        {
            base.Write("1ON\r\n");//This turns on the motor power, allowing moves to be executed when called. Will not work if there is a fault.

        }

        public void Move()
        {
            if (autoTrigger)// I had call this before each move operation, as TR (wait for trigger) config gets wiped after each move.
            {
                base.Write("1TR(IN,=,XXXXX)\r\n");
            }
            else
            {
                base.Write("1TR(IN,=,1XXXX)\r\n");
            }
            base.Write("1GOTO(ALPHA)\r\n");
            base.Write("1R(ST)\r\n");
        }

        public void RunDoAll(double acceleration, double deceleration, double distanceF, double distanceB, double velocity)//This function writes to the drive during the experiment run, which isn't sensible. 
        {
            //base.Write("1PROFILE1(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + "," + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            //base.Write("1PROFILE2(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + ",-" + (distance / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            //base.Write("1PROFILE1(0.5,0.5,500,0.5)\r\n");
            // base.Write("1PROFILE2(0.5,0.5,-500,0.5)\r\n");
            base.Write("1PROFILE3(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + "," + (distanceF / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            Thread.Sleep(50);
            base.Write("1PROFILE4(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + ",-" + (distanceB / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            Thread.Sleep(50); 
            base.Write("1TR(IN,=,1XXXX)\r\n");
            Thread.Sleep(50); 
            base.Write("1USE(3)\r\n");
            Thread.Sleep(50); 
            base.Write("1G\r\n");
            Thread.Sleep(50); 
            base.Write("1T0.2\r\n");
            Thread.Sleep(50); 
            base.Write("1TR(IN,=,0XXXX)\r\n");
            Thread.Sleep(50); 
            base.Write("1USE(4)\r\n");
            Thread.Sleep(50); 
            base.Write("1G\r\n");
        }

        public void LoadExperimentProfile(double acceleration, double deceleration, double distanceF, double distanceB, double velocity)
        {
            base.Write("1CLEAR(ALL)\r\n");//Clears all routines and variables etc.
            Thread.Sleep(50);
            base.Write("1START:\r\n");
            Thread.Sleep(50);
            base.Write("1DECLARE(FMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1DECLARE(BMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1PROFILE5(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + "," + (distanceF / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            Thread.Sleep(50);
            base.Write("1PROFILE6(" + (acceleration / 20).ToString() + "," + (deceleration / 20).ToString() + ",-" + (distanceB / (5 * Math.Pow(10, -3))).ToString() + "," + (velocity / 20).ToString() + ")\r\n");
            Thread.Sleep(50);
            base.Write("1END\r\n");
            Thread.Sleep(50);

            base.Write("1FMOVE:\r\n");//Defines routine MOVE. I think the changing of output 1 is just for user tracking so you can test things more easily. 
            Thread.Sleep(50);
            base.Write("1USE(5)\r\n");//select forward move profile
            Thread.Sleep(50);
            
            //base.Write("1TR(IN,=,1XXXX)\r\n");//wait for trigger
            base.Write("1G\r\n");//execute move
            Thread.Sleep(50);
            base.Write("1END\r\n");
            Thread.Sleep(50);

            base.Write("1BMOVE:\r\n");//Defines routine 
            Thread.Sleep(50);
            base.Write("1USE(6)\r\n");//select backward move profile
            Thread.Sleep(50);
            
            //base.Write("1TR(IN,=,0XXXX)\r\n");//wait for trigger
            base.Write("1G\r\n");//execute move
            Thread.Sleep(50);
            base.Write("1END\r\n");
            Thread.Sleep(50);

            base.Write("1GOSUB(START)\r\n");
            Thread.Sleep(50);

            base.Write("1GOSUB(FMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1T0.5\r\n");
            Thread.Sleep(50);
            base.Write("1GOSUB(FMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1T0.5\r\n");
            Thread.Sleep(50);
            base.Write("1GOSUB(FMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1T0.5\r\n");
            Thread.Sleep(50);
            base.Write("1GOSUB(FMOVE)\r\n");
            Thread.Sleep(50);
            base.Write("1T0.5\r\n");
            Thread.Sleep(50);
            


        }

        public void Return()
        {
            if (autoTrigger)
            {
                base.Write("1TR(IN,=,XXXXX)\r\n");
            }
            else
            {
                base.Write("1TR(IN,=,0XXXX)\r\n");
            }
            base.Write("1GOTO(BETA)\r\n");//says go to routine BETA
            base.Write("1R(ST)\r\n");//Read "Status of indexing"
        }
        public void DisarmMove()
        {
            base.Write("1OFF\r\n");//shuts down the motor power. 
        }
        public new string Read()
        {
                return base.Read();
        }
        public new void Clear()//clears all declared variables on the drive
        {
            base.serial.Clear();
            base.Write("1CLEAR(ALL)\r\n");
        }
        public void AutoTriggerEnable()
        {
            autoTrigger = true;     
        }
        public void AutoTriggerDisable()
        {
            autoTrigger = false;
        }
        public void Home()
        {
            if (autoTrigger)
            {
                base.Write("1TR(IN,=,XXXXX)\r\n");
            }
            else
            {
                base.Write("1TR(IN,=,1XXXX)\r\n");
            }
            base.Write("1GOTO(GAMMA)\r\n");
            
        }
        public Dictionary<string, string> CheckStatus()
        {
            string statusMessage;
            string statusChars;
            statusMessage = base.Query("1R(ST)\r\n");//get the 32 bit word, with spacer characters 
            statusChars = statusMessage.Replace("_", "").Replace("*", "").Replace("\n", "").Replace("\r", "");//remove the dummy characters
            return GenerateStatusTable(statusChars, StatusPropertyList);
        }

        public Dictionary<string, string> ReadDriveFaults()
        {
            string statusMessage;
            string statusChars;
            statusMessage = base.Query("1R(DF)\r\n");//get the 32 bit word, with spacer characters 
            statusChars = statusMessage.Replace("_", "").Replace("*", "").Replace("\n", "").Replace("\r", "");//remove the dummy characters
            return GenerateStatusTable(statusChars, DriveFaultList);

        }

        public Dictionary<string, string> ReadUserFaults()
        {
            string statusMessage;
            string statusChars;
            statusMessage = base.Query("1R(UF)\r\n");//get the 32 bit word, with spacer characters 
            statusChars = statusMessage.Replace("_", "").Replace("*", "").Replace("\n", "").Replace("\r", "");//remove the dummy characters
            
            return GenerateStatusTable(statusChars, UserFaultList);

        }

        public Dictionary<string,string> GenerateStatusTable(string Message, List<string> LookupBitNumber)
        {
            string ErrorFlag = "E";
            Dictionary<string, string> StatusDict = new Dictionary<string,string>();
            if (Message[0] == ErrorFlag[0])
            {
                StatusDict.Add("0", "Communication Error \n");
            }
            else
            {
                for (int i = 0; i < LookupBitNumber.Count; i++)
                {
                    StatusDict.Add((i + 1).ToString(), String.Concat(LookupBitNumber[i], Message[i].ToString()));

                }
            }
            
            return StatusDict;
        }

        


        //This is supposed to list all routines loaded onto the stage controller. I'm not using it, if you want to then use query and print the string to the console 
        public void ListAll()
        {
            base.Write("1LIST(ALL)\r\n");
        }

        public void CommsDisable() // This disables communications without disconnecting RS232. Nothing will write to the stage. To re-enable you need to use "1E1".
        {
            base.Write("1E0\r\n");
        }
    }
}
