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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromCMOTD2.cs" 

pathCMOTcompare = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromCMOTforD2MolassesComparison.cs" 

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

def CaptureEfficiency(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathCMOTcompare)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from CMOT')
	mm.Run(dic)	

	mm.SetScriptPath(path)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0

def CaptureEfficiencyScan(ParameterName,ParameterValues, NoMolMeasureTime, MolMeasureTime, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(ParameterValues)):
			dic[ParameterName]=ParameterValues[n]
			print(ParameterName + ': ' +str(ParameterValues[n]))
			CaptureEfficiency(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0

def LifetimeInMolasses(MolTimes, MeasureTime, repeats):
	dic = Dictionary[String, Object]()
	dic["ImageDelay"] = MeasureTime
	mm.SetScriptPath(path)
	if not MeasureTime:
		MeasureTime = 5

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))

		for n in range(len(MolTimes)):
			dic["MolassesPulseLength"]=MolTimes[n]
			print("Molasses HoldTime"+ ': ' +str(MolTimes[n]))
			mm.Run(dic)
	
	
	return 0

def LifetimeInMolassesRamp(MolTimes,HoldPower, MeasureTime, repeats):
	dic = Dictionary[String, Object]()
	dic["ImageDelay"] = MeasureTime
	dic["MolassesPrincipalHoldPower"] = HoldPower
	mm.SetScriptPath(LifetimeRampPath)
	if not MeasureTime:
		MeasureTime = 5

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))

		for n in range(len(MolTimes)):
			dic["MolassesPulseLength"]=MolTimes[n]
			print("Molasses HoldTime"+ ': ' +str(MolTimes[n]))
			mm.Run(dic)
	
	
	return 0
		
