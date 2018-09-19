# Import a whole load of stuff
from System.IO import *
from System.Drawing import *
from System.Runtime.Remoting import *
from System.Threading import *
from System.Windows.Forms import *
from System.Xml.Serialization import *
from System import *
from System.Collections.Generic import Dictionary

from DAQ.Environment import *
from DAQ import *
from MOTMaster import*
from time import sleep
import math
import time

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\TestingMWtrapAM.cs" 

def run_script():
	return 0

def RunGenTest(PeakMWPower,repeats):
	dic = Dictionary[String, Object]()
	dic["MWOnPower"] = PeakMWPower
	mm.SetScriptPath(path)
	for n in range(repeats):
		mm.Run(dic)
		print('repeat number: ', n+1)
	return 0