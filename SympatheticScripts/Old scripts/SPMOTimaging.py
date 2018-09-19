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

def ScanOffsetFrequency(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["offsetlockfrequency"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def ScanAOMFreq(initial, final, interval):
	count = 0
	endcount = (final-initial)/interval
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount+1):
		dic["aom2Detuning"] = initial - (count*interval)
		dic["aom3Detuning"] = initial + (count*interval)
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansAOMF(initial, final, interval, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAOMFreq(initial,final,interval)
		j = j+1
	return 0

def ScanMOTFreq(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["aom0Detuning"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansMOTF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTFreq(values)
		j = j+1
	return 0

def ScanMOTRepumpFreq(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["aom1Detuning"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansMOTRF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTRepumpFreq(values)
		j = j+1
	return 0

def ScanZeemanFreq(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["aom2Detuning"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansZF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanZeemanFreq(values)
		j = j+1
	return 0

def ScanAbsFreq(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["absImageDetuning"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansAF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsFreq(values)
		j = j+1
	return 0

def ScanCoilCurrent(topValues,bottomValues):
	count = 0
	listTop = topValues
	listBottom = bottomValues
	endcount = len(listTop)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTimaging.cs")
	while(count < endcount):
		dic["TopMOTCoilCurrent"] = listTop[count]
		dic["BottomMOTCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanCoilCurrent(topValues, bottomValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoilCurrent(topValues, bottomValues)
		j = j+1
	return 0