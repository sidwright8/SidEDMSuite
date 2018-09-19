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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOTPulseRamp.cs" 

def run_script():
	return 0

def str2bool(v):
	return v.lower() in ("yes", "true")

def MakeList(startvalue, endvalue, increment):
	N = int((endvalue-startvalue)/increment)
	return [startvalue + i*increment for i in range(N)]

def PseudoShuffle(list):
	x = floor(len(list))
	evenelements = [list[2*i] for i in range(0,len(list)-1)] 
	return 0


def AddReferenceValueToList(list,referencevalue,period):
	
	for i in range(0,int(math.floor(len(list)/period))):
		list.insert((period+1)*i, referencevalue)
	return list

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

def ScanExpansionTimeWithReferenceShot(exptimeValues):
	exptimeList = exptimeValues
	MPList = ["true","false"]
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(exptimeValues)):
		for m in range(len(MPList)):
			dic["ImageDelay"] = exptimeList[n]
			dic["DoMolassesPulse"] = str2bool(MPList[m])
			mm.Run(dic)
			print(exptimeList[n],'MolassesPulse: ' +  MPList[m])
	return 0

def ScanExpansionTime(exptimeValues):
	exptimeList = exptimeValues
	
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(exptimeValues)):
		dic["ImageDelay"] = exptimeList[n]
		mm.Run(dic)
		print(exptimeList[n])
	return 0

def ScanD2RepumpSwitchOffTime(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["D2RepumpSwitchOffTime"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
		print(list[count])	
	return 0

def RepeatScansD2RSOT(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanD2RepumpSwitchOffTime(values)
		j = j+1
	return 0

def ScanMolassesPulseLength(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["MolassesPulseLength"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanMolassesFinalAmplitude(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["MolassesFinalAmplitude"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanMolassesStartAmplitude(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["MolassesStartAmplitude"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanXCoilCurrent(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["XCoilCurrent"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanYCoilCurrent(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["YCoilCurrent"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanZCoilCurrent(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["ZCoilCurrent"] = list[count]
		mm.Run(dic)
		count=count + 1
		
		sleep(1.0)
	return 0

def ScanXCoilCurrentAndMeasureExpansion(xcoilvalues,ExpansionTimes):
	count = 0
	list = xcoilvalues
	endcount = len(xcoilvalues)
	mm.SetScriptPath(path)
	
	dic = Dictionary[String, Object]()
	while(count < endcount):
		dic["XCoilCurrent"] = xcoilvalues[count]
		i=0
		while(i<len(ExpansionTimes)):
			dic["ImageDelay"]=ExpansionTimes[i]
			mm.Run(dic)
			i = i+1
		count = count+1
	return 0
		
		


def RepeatScansMPL(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMolassesPulseLength(values)
		j = j+1
	return 0

def ScanMPLWithReferenceShot(Values):
	MPLList = Values
	MPList = ["true","false"]
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(Values)):
		for m in range(len(MPList)):
			dic["MolassesPulseLength"] = MPLList[n]
			dic["DoMolassesPulse"] = str2bool(MPList[m])
			mm.Run(dic)
			print(MPLList[n],'MolassesPulse: ' +  MPList[m])
	return 0


def ScanOffsetLockFrequency(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["offsetlockvcofrequency"] = list[count]
		mm.Run(dic)
		
		sleep(1.0)
		print(list[count])
		count=count + 1	
	return 0

def ScanMolassesRepumpFrequency(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		dic["MolassesRepumpDetuning"] = list[count]
		print(list[count])
		mm.Run(dic)
		count=count + 1
		sleep(1.0)	
	return 0

def ScanMRDWithReferenceShot(Values):
	MRDList = Values
	MPList = ["true","false"]
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(Values)):
		for m in range(len(MPList)):
			dic["MolassesRepumpDetuning"] = MRDList[n]
			dic["DoMolassesPulse"] = str2bool(MPList[m])
			mm.Run(dic)
			print(MRDList[n],'MolassesPulse: ' +  MPList[m])
	return 0

def ScanMolassesRepumpAmplitude(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	while(count < endcount):
		print(list[count])
		dic["MolassesRepumpAmplitude"] = list[count]
		mm.Run(dic)
		count=count + 1
		sleep(1.0)
			
	return 0

def ScanMRAWithReferenceShot(Values):
	MRAList = Values
	MPList = ["true","false"]
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	for n in range(len(Values)):
		for m in range(len(MPList)):
			dic["MolassesRepumpAmplitude"] = MRAList[n]
			dic["DoMolassesPulse"] = str2bool(MPList[m])
			mm.Run(dic)
			print(MRAList[n],'MolassesPulse: ' +  MPList[m])
	return 0

def RepeatScansOLF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanOffsetLockFrequency(values)
		j = j+1
	return 0


def ScanAbsDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
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
	mm.SetScriptPath(path)
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
	mm.SetScriptPath(path)
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
	mm.SetScriptPath(path)
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
	mm.SetScriptPath(path)
	for n in range(len(repumpListF)):
		for m in range(len(triggerList)):
			dic["aom1Detuning"] = repumpListF[n]
			dic["aom1Power"] = repumpListP[n]
			dic["Frame1Trigger"] = triggerList[m]
			mm.Run(dic)
			sleep(1.0)
	return 0

