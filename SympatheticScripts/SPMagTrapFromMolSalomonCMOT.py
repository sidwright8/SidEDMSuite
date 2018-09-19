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

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\SalomonCMOTMolMagTrapRamp.cs" 


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



def ScanAbsImageDetuning(vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic["absImageDetuning"] = j
			print('Image Detuning: ' +str(j))
			mm.Run(dic)	
	
	return 0

def ScanQWPangle(vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic["probeQWPangle"] = j
			print('probe QWP angle : ' +str(j))
			mm.Run(dic)	
	
	return 0

def ScanZShimCurrent(vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic["ZCoilCurrent"] = j
			print('Z Shim control voltage : ' +str(j))
			mm.Run(dic)	
	
	return 0

def ScanXShimCurrent(vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic["XCoilCurrent"] = j
			print('X Shim control voltage : ' +str(j))
			mm.Run(dic)	
	
	return 0

def ScanMOTEndTimes(vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic["MOTEndTime"] = j
			print('MOTEndTime: ' +str(j))
			mm.Run(dic)	
	
	return 0

def ScanFieldGradient(TopVals,BottomVals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	length = len(TopVals)
	if length != len(BottomVals):
		print('Lists must be equal length.')
		return 0 
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in range(length):
			dic["TopVacCoilCurrent"] = TopVals[j]
			dic["BottomVacCoilCurrent"] = BottomVals[j]
			print('Coil Currents: ' +str(TopVals[j]) + ', '+str(BottomVals[j]))
			mm.Run(dic)	
	
	return 0


def ScanParameter(param,vals, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	mm.SetScriptPath(path)
	for i in range(repeats):
		print('repeat number: '+str(i) )
		for j in vals:
			dic[param] = j
			print(param +': ' +str(j))
			mm.Run(dic)	
	
	return 0