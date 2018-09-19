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

def ScanRepumperDetuning(values):
	count = 0
	list = [float(i) for i in values]
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	while(count < endcount):
		dic["D1RepumperDetuning"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScansRD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanDetuning(values)
		j = j+1
	return 0

def ScanAbsDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	while(count < endcount):
		dic["absImageDetuning"] = list[count]
		mm.Run(dic)
		count=count + 1
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
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	while(count < endcount):
		dic["absImageDetuning"] = listF[count]
		dic["absImagePower"] = listP[count]
		mm.Run(dic)
		count=count + 1
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
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	while(count < endcount):
		dic["absImageRepumpDetuning"] = listF[count]
		dic["absImageRepumpPower"] = listP[count]
		mm.Run(dic)
		count=count + 1
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
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	while(count < endcount):
		dic["absImageRepumpPower"] = list[count]
		mm.Run(dic)
		count=count + 1
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
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	for n in range(len(repumpListF)):
		for m in range(len(triggerList)):
			dic["aom1Detuning"] = repumpListF[n]
			dic["aom1Power"] = repumpListP[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
	return 0

def RepeatScanRF(repumpFValues, repumpPValues, triggerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanRepumpFreq(repumpFValues, repumpPValues, triggerValues)
		j = j+1
	return 0

def ScanCPRepumpPower(repumpPValues,triggerValues):
	repumpList = repumpPValues
	triggerList = triggerValues
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	for n in range(len(repumpList)):
		for m in range(len(triggerList)):
			dic["CoolingPulseRepumpIntensity"] = repumpList[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
	return 0

def RepeatScanCPRP(repumpPValues, triggerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCPRepumpPower(repumpPValues, triggerValues)
		j = j+1
	return 0

def ScanCPMOTFreq(motFValues, motPValues, repumpPValues, triggerValues):
	motListF = motFValues
	motListP = motPValues	
	repumpList = repumpPValues
	triggerList = triggerValues
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	for n in range(len(motListF)):
		for m in range(len(triggerList)):
			dic["CoolingPulseDetuning"] = motListF[n]
			dic["CoolingPulseIntensity"] = motListP[n]
			dic["CoolingPulseRepumpIntensity"] = repumpList[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
	return 0

def RepeatScanCPMF(motFValues, motPValues, repumpPValues, triggerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCPMOTFreq(motFValues, motPValues, repumpPValues, triggerValues)
		j = j+1
	return 0

def ScanCPMOTPower(motPValues, repumpPValues, triggerValues):
	motListP = motPValues	
	repumpListP = repumpPValues
	triggerList = triggerValues
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	for n in range(len(motListP)):
		for m in range(len(triggerList)):
			dic["CoolingPulseIntensity"] = motListP[n]
			dic["CoolingPulseRepumpIntensity"] = repumpListP[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
	return 0

def RepeatScanCPMP(motPValues, repumpPValues, triggerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCPMOTPower(motPValues, repumpPValues, triggerValues)
		j = j+1
	return 0

def ScanMagCoilCurrent(topValues, bottomValues, triggerValues):
	listTop = topValues	
	listBottom = bottomValues
	triggerList = triggerValues
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\D1 Testing.cs")
	for n in range(len(listTop)):
		for m in range(len(triggerList)):
			dic["TopMagCoilCurrent"] = listTop[n]
			dic["BottomMagCoilCurrent"] = listBottom[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
	return 0

def RepeatScanMCC(topValues, bottomValues, triggerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagCoilCurrent(topValues, bottomValues, triggerValues)
		j = j+1
	return 0