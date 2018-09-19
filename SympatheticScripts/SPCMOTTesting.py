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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\CMOTcoilRampTesting.cs" 


print("hi")

def run_script():
	return 0

def str2bool(v):
	return v.lower() in ("yes", "true")

def MakeList(startvalue, endvalue, increment):
	N = int((endvalue-startvalue)/increment)
	return [startvalue + i*increment for i in range(N)]

def AddReferenceValueToList(list,referencevalue,period):
	
	for i in range(0,int(math.floor(len(list)/period))):
		list.insert((period+1)*i, referencevalue)
	return list

def CMOTScanCurrents(FinalTopCurrents,FinalBottomCurrents,FinalPower,RampTime,HoldTime,ImageDelays,nrepeats):
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	dic["FinalCMOTpower"] = FinalPower
	dic["CMOTholdTime"] = HoldTime
	dic["CMOTrampTime"] = RampTime
	for m in range(nrepeats):
		print("repeat number: "+ str(m))
		for n in range(len(FinalTopCurrents)):
			dic["FinalTopCMOTCurrent"] = FinalTopCurrents[n]
			dic["FinalBottomCMOTCurrent"] = FinalBottomCurrents[n]
			print(FinalTopCurrents[n])
			for l in range(len(ImageDelays)):
				dic["ImageDelay"] = ImageDelays[l]
				print(ImageDelays[l])
				mm.Run(dic)
				
	return 0

def CMOTScanPower(FinalTopCurrent,FinalBottomCurrent,FinalPowers,RampTime,HoldTime,ImageDelays,nrepeats):
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	dic["CMOTholdTime"] = HoldTime
	dic["CMOTrampTime"] = RampTime
	dic["FinalTopCMOTCurrent"] = FinalTopCurrent
	dic["FinalBottomCMOTCurrent"] = FinalBottomCurrent
	for m in range(nrepeats):
		print("repeat number: "+ str(m))
		for n in range(len(FinalPowers)):
			print(FinalPowers[n])
			dic["FinalCMOTpower"] = FinalPowers[n]
			for l in range(len(ImageDelays)):
				dic["ImageDelay"] = ImageDelays[l]
				print(ImageDelays[l])
				mm.Run(dic)
				
	return 0

def CMOTScanDetuning(FinalTopCurrent,FinalBottomCurrent,FinalPower,FinalDetunings,RampTime,HoldTime,ImageDelays,nrepeats):
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	dic["CMOTholdTime"] = HoldTime
	dic["CMOTrampTime"] = RampTime
	dic["FinalTopCMOTCurrent"] = FinalTopCurrent
	dic["FinalBottomCMOTCurrent"] = FinalBottomCurrent
	dic["FinalCMOTpower"] = FinalPower
	for m in range(nrepeats):
		print("repeat number: "+ str(m))
		for n in range(len(FinalDetunings)):
			print(FinalDetunings[n])
			dic["FinalCMOTdetuning"] = FinalDetunings[n]
			for l in range(len(ImageDelays)):
				dic["ImageDelay"] = ImageDelays[l]
				print(ImageDelays[l])
				mm.Run(dic)
				
	return 0

def CMOTScanRampTime(FinalTopCurrent,FinalBottomCurrent,FinalPower,FinalDetuning,RampTimes,HoldTime,ImageDelay,nrepeats):
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	dic["FinalTopCMOTCurrent"] = FinalTopCurrent
	dic["FinalBottomCMOTCurrent"] = FinalBottomCurrent
	dic["CMOTholdTime"] = HoldTime
	
	dic["ImageDelay"] = ImageDelay 
	dic["FinalCMOTpower"] = FinalPower
	dic["FinalCMOTdetuning"] = FinalDetuning
	for m in range(nrepeats):
		print("repeat number: "+ str(nrepeats))
		for n in range(len(RampTimes)):
			dic["CMOTrampTime"] = RampTimes[n]
			mm.Run(dic)
			print(RampTimes[n])
	return 0

def CMOTScanHoldTime(FinalTopCurrent,FinalBottomCurrent,FinalPower,FinalDetuning,RampTime,HoldTimes,ImageDelay,nrepeats):
	dic = Dictionary[String, Object]()
	mm.SetScriptPath(path)
	dic["FinalTopCMOTCurrent"] = FinalTopCurrent
	dic["FinalBottomCMOTCurrent"] = FinalBottomCurrent
	dic["CMOTrampTime"] = RampTime
	
	dic["ImageDelay"] = ImageDelay 
	dic["FinalCMOTpower"] = FinalPower
	dic["FinalCMOTdetuning"] = FinalDetuning
	for m in range(nrepeats):
		print("repeat number: "+ str(nrepeats))
		for n in range(len(HoldTimes)):
			dic["CMOTholdTime"] = HoldTimes[n]
			mm.Run(dic)
			print(HoldTimes[n])
	return 0

def LeaveScript():
	quit()
