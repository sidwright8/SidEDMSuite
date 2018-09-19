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

def run_script():
	return 0

def ScanExpansionTime(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	while(count < endcount):
		dic["ExpansionTime"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
	return 0

def RepeatScansET(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanExpansionTime(values)
		j = j+1
	return 0

def ScanAbsDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	while(count < endcount):
		dic["absImageDetuning"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
		print(list[count])
	return 0

def RepeatScanAD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuning(values)
		j = j+1
	return 0

def ScanAbsDetuningPower(freqValues,powerValues):
	count = 0
	listF = freqValues
	listP = powerValues
	endcount = len(listF)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	while(count < endcount):
		dic["absImageDetuning"] = listF[count]
		dic["absImagePower"] = listP[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
	return 0

def RepeatScanADP(freqValues, powerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuningPower(freqValues,powerValues)
		j = j+1
	return 0

def ScanAbsRepumpDetuning(freqValues, powerValues):
	count = 0
	listF = freqValues
	listP = powerValues
	endcount = len(listF)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	while(count < endcount):
		dic["absImageRepumpDetuning"] = listF[count]
		dic["absImageRepumpPower"] = listP[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
	return 0

def RepeatScanARD(freqValues, powerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsRepumpDetuning(freqValues, powerValues)
		j = j+1
	return 0

def ScanAbsRepumpPower(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	while(count < endcount):
		dic["absImageRepumpPower"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
	return 0

def RepeatScanARP(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsRepumpPower(values)
		j = j+1
	return 0

def ScanRepumpFreq(repumpFValues,repumpPValues, triggerValues):
	repumpListF = repumpFValues
	repumpListP = repumpPValues
	triggerList = triggerValues
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\Ballistic Expansion From MOT.cs")
	for n in range(len(repumpListF)):
		for m in range(len(triggerList)):
			dic["aom1Detuning"] = repumpListF[n]
			dic["aom1Power"] = repumpListP[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
			sleep(1.0)
	return 0

