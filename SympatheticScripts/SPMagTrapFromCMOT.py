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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapFromMolCMOT.cs" 


print("hello")

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

def TemperatureMeasurement(dic,exptimeValues):
	mm.SetScriptPath(path)
	if not exptimeValues:
		exptimeValues = [5,25,35,40]
	for n in range(len(exptimeValues)):
		dic["ImageDelay"] = exptimeValues[n]
		print('ImageDelay: ' + str(exptimeValues[n]))
		mm.Run(dic)
		
	return 0

def TemperatureScan(ParameterName,ParameterValues,expTimeValues,repeats):
	dic = Dictionary[String, Object]()
	if not expTimeValues:
		expTimeValues = [5,25,35,40]
	if not repeats:
		repeats = 1
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(ParameterValues)):
			dic[ParameterName]=ParameterValues[n]
			print(ParameterName + ': ' +str(ParameterValues[n]))
			TemperatureMeasurement(dic,expTimeValues)
	return 0



def ScanMagTrapHoldTime(HoldTimes, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in HoldTimes:
			dic["MagTrapHoldTime"] = j
			print('Magnetic Trap Hold Time: ' +str(j))
			mm.Run(dic)	
	
	return 0