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

def SwitchCoils(maxCurrent,minCurrent):
	count = 0
	endcount = 2
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		if count == 0:
			dic["TopMOTCoilCurrent"] = maxCurrent
			dic["BottomMOTCoilCurrent"] = maxCurrent
			mm.Run(dic)
			count = count + 1
		elif count == 1:
			dic["TopMOTCoilCurrent"] = minCurrent
			dic["BottomMOTCoilCurrent"] = minCurrent
			mm.Run(dic)
			count = count + 1
	
	return 0

def RepeatScanSWC(maxCurrent,minCurrent,numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		SwitchCoils(maxCurrent,minCurrent)
		j = j+1
	return 0

def ScanMagTrapCurrent(topCoilValues, bottomCoilValues):
	count = 0
	listTop = topCoilValues
	listBottom = bottomCoilValues
	endcount = len(listTop)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["TopMagCoilCurrent"] = listTop[count]
		dic["BottomMagCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMTC(topCoilValues, bottomCoilValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagTrapCurrent(topCoilValues, bottomCoilValues)
		j = j+1
	return 0

def ScanBottomMagCoilCurrent(bottomCoilValues):
	count = 0
	listBottom = bottomCoilValues
	endcount = len(listBottom)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["BottomMagCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanBMCC(bottomCoilValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanBottomMagCoilCurrent(bottomCoilValues)
		j = j+1
	return 0

def ScanMOTDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["MOTDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMOTDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTDetuning(values)
		j = j+1
	return 0

def ScanZeemanDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["ZeemanDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanZeemanDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanZeemanDetuning(values)
		j = j+1
	return 0

def ScanMOTIntensity(MOTvalues,MOTRepumpvalues):
	count = 0
	listM = MOTvalues
	listMR = MOTRepumpvalues
	endcount = len(listM)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["MOTIntensity"] = listM[count]
		dic["MOTRepumpIntensity"] = listMR[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMOTIntensity(MOTvalues, MOTRepumpvalues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTIntensity(MOTvalues, MOTRepumpvalues)
		j = j+1
	return 0

def ScanZeemanIntensity(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["ZeemanIntensity"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanZeemanIntensity(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanZeemanIntensity(values)
		j = j+1
	return 0

def ScanMotRepumpIntensity(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["MOTRepumpIntensity"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMotRepumpIntensity(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMotRepumpIntensity(values)
		j = j+1
	return 0

def ScanAbsDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["AbsDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanAD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuning(values)
		j = j+1
	return 0

def ScanAbsDetuningPower(freqValues, powerValues):
	count = 0
	listF = freqValues
	listP = powerValues
	endcount = len(listF)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["AbsDetuning"] = listF[count]
		dic["AbsPower"] = listP[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanADP(freqValues, powerValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuningPower(freqValues, powerValues)
		j = j+1
	return 0

def ScanAbsRepumpDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["AbsRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanARD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsRepumpDetuning(values)
		j = j+1
	return 0

def ScanMOTRepumpDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["MOTRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMOTRepumpDetuning(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTRepumpDetuning(values)
		j = j+1
	return 0

def ScanMagImage(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["MagTrapEndTime"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMagIm(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagImage(values)
		j = j+1
	return 0

def ScanMOTCoilCurrent(topCurrent, bottomCurrent):
	count = 0
	listTop = topCurrent
	listBottom = bottomCurrent
	endcount = len(listTop)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["TopMOTCoilCurrent"] = listTop[count]
		dic["BottomMOTCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanMOTCoilCurrent(topCurrent, bottomCurrent, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTCoilCurrent(topCurrent, bottomCurrent)
		j = j+1
	return 0

def ScanSecondRampLength(rampLength, imageTime):
	count = 0
	listRamp = rampLength
	listImage = imageTime
	endcount = len(listRamp)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["SecondRampLength"] = listRamp[count]
		dic["MagTrapEndTime"] = listImage[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanSecondRampLength(rampLength, imageTime, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanSecondRampLength(rampLength, imageTime)
		j = j+1
	return 0

def ScanDistanceTravelled(distance):
	count = 0
	list = distance
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["TSDistance"] = list[count]
		dic["TSDistanceF"] = list[count]	
		dic["TSDistanceB"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanDistanceTravelled(distance, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanDistanceTravelled(distance)
		j = j+1
	return 0

def ScanImageTimeAndDistance(magTrapEndTime, distance):
	count = 0
	listD = distance
	listT = magTrapEndTime
	endcount = len(listD)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImage.cs")
	while(count < endcount):
		dic["TSDistance"] = listD[count]
		dic["TSDistanceF"] = listD[count]	
		dic["TSDistanceB"] = listD[count]
		dic["MagTrapEndTime"] = listT[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScanImageTimeAndDistance(magTrapEndTime, distance, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanImageTimeAndDistance(magTrapEndTime, distance)
		j = j+1
	return 0