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

def ScanMagCoilCurrent(topValues, bottomValues):
	count = 0
	listTop = topValues
	listBottom = bottomValues
	endcount = len(listTop)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["TopMagCoilCurrent"] = listTop[count]
		dic["BottomMagCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMCC(topValues, bottomValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagCoilCurrent(topValues, bottomValues)
		j = j+1
	return 0

def ScanBottomMagCoilCurrent(bottomValues):
	count = 0
	listBottom = bottomValues
	endcount = len(listBottom)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["BottomMagCoilCurrent"] = listBottom[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanBMCC(bottomValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanBottomMagCoilCurrent(bottomValues)
		j = j+1
	return 0

def ScanMOTPower(motPValues, repumpPValues):
	count = 0
	listMot = motPValues
	listRepump = repumpPValues
	endcount = len(listMot)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["CoolingPulseIntensity"] = listMot[count]
		dic["CoolingPulseRepumpIntensity"] = listRepump[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMP(motPValues, repumpPValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTPower(motPValues, repumpPValues)
		j = j+1
	return 0

def ScanMOTFreq(motFValues, motPValues, repumpPValues):
	count = 0
	listMotF = motFValues
	listMotP = motPValues
	listRepump = repumpPValues
	endcount = len(listMotF)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["CoolingPulseIntensity"] = listMotP[count]
		dic["CoolingPulseDetuning"] = listMotF[count]
		dic["CoolingPulseRepumpIntensity"] = listRepump[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMF(motFValues, motPValues, repumpPValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTFreq(motFValues, motPValues, repumpPValues)
		j = j+1
	return 0

def ScanMOTRepumpPower(repumpPValues):
	count = 0
	listRepump = repumpPValues
	endcount = len(listRepump)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["CoolingPulseRepumpIntensity"] = listRepump[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMRP(repumpPValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTRepumpPower(repumpPValues)
		j = j+1
	return 0


def ScanMOTRepumpFreq(repumpFValues, repumpPValues):
	count = 0
	listRepumpF = repumpFValues
	listRepumpP = repumpPValues
	endcount = len(listRepumpF)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["aom1Detuning"] = listRepumpF[count]
		dic["aom1Power"] = listRepumpP[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMRF(repumpFValues, repumpPValues, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTRepumpFreq(repumpFValues, repumpPValues)
		j = j+1
	return 0

def ScanSecondaryRampLength(rampLength, cmotOffValues, imageTrigger, cmotRecapture, magRampDown):
	count = 0
	listRamp = rampLength
	listCmotO = cmotOffValues
	listImage = imageTrigger
	listCmotR = cmotRecapture
	listMagR = magRampDown
	endcount = len(listRamp)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["SecondaryRampLength"] = listRamp[count]
		dic["CMOTOffTime"] = listCmotO[count]
		dic["Frame1Trigger"] = listImage[count]
		dic["CMOTRecaptureTime"] = listCmotR[count]
		dic["MagRampDownTime"] = listMagR[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanSRL(rampLength, cmotOffValues, imageTrigger, cmotRecapture, magRampDown, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanSecondaryRampLength(rampLength, cmotOffValues, imageTrigger, cmotRecapture, magRampDown)
		j = j+1
	return 0

def ScanMagTrapLength(cmotOffValues, imageTrigger, cmotRecapture):
	count = 0
	listCmotO = cmotOffValues
	listImage = imageTrigger
	listCmotR = cmotRecapture
	endcount = len(listCmotO)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["CMOTOffTime"] = listCmotO[count]
		dic["Frame1Trigger"] = listImage[count]
		dic["CMOTRecaptureTime"] = listCmotR[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMTL(cmotOffValues, imageTrigger, cmotRecapture, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagTrapLength(cmotOffValues, imageTrigger, cmotRecapture)
		j = j+1
	return 0


def ScanTransportDistance(distance):
	count = 0
	list = distance
	endcount = len(list)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["TSDistance"] = list[count]
		dic["TSDistanceF"] = list[count]
		dic["TSDistanceB"] = list[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanTD(distance, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanTransportDistance(distance)
		j = j+1
	return 0

def ScanMagTrapLengthTransport(cmotOffValues, imageTrigger, cmotRecapture, transStageTrigger):
	count = 0
	listCmotO = cmotOffValues
	listImage = imageTrigger
	listCmotR = cmotRecapture
	listTrans = transStageTrigger
	endcount = len(listCmotO)
	dic = Dictionary[String, Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MotCmotMot.cs")
	while(count < endcount):
		dic["CMOTOffTime"] = listCmotO[count]
		dic["Frame1Trigger"] = listImage[count]
		dic["CMOTRecaptureTime"] = listCmotR[count]
		dic["TranslationStageTriggerDuration"] = listTrans[count]
		mm.Run(dic)
		count=count + 1
	return 0

def RepeatScanMTLT(cmotOffValues, imageTrigger, cmotRecapture, transStageTrigger, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagTrapLengthTransport(cmotOffValues, imageTrigger, cmotRecapture, transStageTrigger)
		j = j+1
	return 0