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

def run_script():
	return 0

def ScanLoadTime(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTLoadingTimeAbsImage.cs")
	while(count < endcount):
		dic["Frame0Trigger"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanLoadTime(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanLoadTime(values)
		j = j+1
	return 0