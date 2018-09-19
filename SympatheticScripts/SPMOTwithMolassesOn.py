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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MOTwithMolassesOn.cs" 

def run_script():
	return 0

def str2bool(v):
	return v.lower() in ("yes", "true")

def MakeList(startvalue, endvalue, increment):
	N = int((endvalue-startvalue)/increment)
	return [startvalue + i*increment for i in range(N)]

def ScanOffsetLockFrequency(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["offsetlockvcofrequency"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)	
	return 0

def RepeatScansOFL(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanOffsetLockFrequency(values)
		j = j+1
	return 0

def ScanOLFWithReferenceShot(olfValues):
	olfList = olfValues
	MPList = ["true","false"]
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(olfValues)):
		for m in range(len(MPList)):
			dic["offsetlockvcofrequency"] = olfList[n]
			dic["DoMolassesPulse"] = str2bool(MPList[m])
			mm.Run(dic)
			print(olfList[n],'MolassesPulse: ' +  MPList[m])
	return 0

def RepeatScansCMI(initial, final, interval, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCmotImage(initial, final, interval)
		j = j+1
	return 0

def RandScanCmotImage(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["Frame1Trigger"] = 100000 + list[count]
		print(list[count])
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansRCMI(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		RandScanCmotImage(values)
		j = j+1
	return 0

def ScanAbsDetuning(absValues):
	count = 0
	list = absValues
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["AbsorptionDetuning"] = list[count]
		print(list[count])
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanAD(absValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuning(absValues)
		j = j+1
	return 0

def ScanCmotStageLength(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["CMOTStartTime"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanCmotStageLength(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCmotStageLength(values)
		j = j+1
	return 0

def ScanProbeRepumpDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["ProbeRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanProbeRepumpDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanProbeRepumpDetuning(values)
		j = j+1
	return 0

def ScanCoolingPulseDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["CoolingPulseDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanCoolingPulseDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingPulseDetuning(values)
		j = j+1
	return 0

def ScanCoolingPulseRepumpDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["CoolingPulseRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanCoolingPulseRepumpDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingPulseRepumpDetuning(values)
		j = j+1
	return 0

def ScanCoolingPulseIntensity(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["CoolingPulseIntensity"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanCoolingPulseIntensity(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingPulseIntensity(values)
		j = j+1
	return 0

def ScanCoolingPulseStartTime(startTimeValues, durationValues):
	count = 0
	listST = startTimeValues
	listD = durationValues
	endcount = len(listST)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["CoolingPulseStartTime"] = listST[count]
		dic["CoolingPulseStartTimeIntensity"] = listST[count]
		dic["CoolingPulseDuration"] = listD[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanCoolingPulseStartTime(startTimeValues, durationValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingPulseStartTime(startTimeValues, durationValues)
		j = j+1
	return 0

def ScanDistanceTravelled(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["TSDistance"] = list[count]
		dic["TSDistanceF"] = list[count]
		dic["TSDistanceB"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanDistanceTravelled(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanDistanceTravelled(values)
		j = j+1
	return 0

def ScanVerticalMagPos(topCurrent):
	count = 0
	list = topCurrent
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["TopMagCoilCurrentFinal"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanVerticalMagPos(topCurrent, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanVerticalMagPos(topCurrent)
		j = j+1
	return 0