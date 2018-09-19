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

testpath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\test.cs" 

path = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOT.cs" 

pathMOTcompare = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromMOT.cs" 

pulseswitchpath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOTPulseRamp.cs"

pathMOTcompareforpulseramp = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromMOTForPulseRampComparison.cs"

pulserampdownpath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOTPulseRampDown.cs"

pathMOTcompareforpulserampdown = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromMOTForPulseRampDownComparison.cs"

OLFramppath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOTPulseRampOLF.cs"

pathMOTcompareforOLFramp = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromMOTForOLFRampComparison.cs"

TwoStagePath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesPulseRamp2Stage.cs"

pathMOTcomparefor2Stageramp = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\ReleaseFromMOTfor2StageComparison.cs"

LifetimeRampPath = "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MolassesFromMOTLifetime.cs" 

 

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
	mm.SetScriptPath(pathMOTcompare)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
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
		
def test(repeats):
	dic = Dictionary[String, Object]()
	
	mm.SetScriptPath(testpath)
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		mm.Run(dic)	
	return 0


def MolassesPulsePowerSwitchTemp(IntensityVals, SwitchTime, ExpansionTimes, repeats):
	mm.SetScriptPath(pulseswitchpath)
	dic = Dictionary[String, Object]()
	dic["MolassesPowerSwitchTime"]= SwitchTime
	
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for m in range(len(IntensityVals)):
			dic["MolassesPrincipalFinalAmplitude"] = IntensityVals[m]
			print("MolassesPower"+ ': ' +str(IntensityVals[m]))
			for n in range(len(ExpansionTimes)):
				dic["ImageDelay"]=ExpansionTimes[n]
				print("Expansion Time"+ ': ' +str(ExpansionTimes[n]))
				mm.Run(dic)
		
	return 0

def MolassesPulseOLFSwitchTemp(OLFVals, SwitchTime, ExpansionTimes, repeats):
	mm.SetScriptPath(pulseswitchpath)
	dic = Dictionary[String, Object]()
	dic["OLFSwitchTime"]= SwitchTime
	
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for m in range(len(OLFVals)):
			dic["OLFfinal"] = OLFVals[m]
			print("Final OLF"+ ': ' +str(OLFVals[m]))
			for n in range(len(ExpansionTimes)):
				dic["ImageDelay"]=ExpansionTimes[n]
				print("Expansion Time"+ ': ' +str(ExpansionTimes[n]))
				mm.Run(dic)
		
	return 0

def CaptureEfficiencyOLFSwitch(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathMOTcompareforpulseramp)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
	mm.Run(dic)	

	mm.SetScriptPath(pulseswitchpath)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0

def CaptureEfficiencyScanOLFSwitch(OLFValues, NoMolMeasureTime, MolMeasureTime, SwitchTime,repeats):
	dic = Dictionary[String, Object]()
	dic["OLFSwitchTime"]=SwitchTime
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(OLFValues)):
			dic["OLFfinal"]=OLFValues[n]
			dic["OLFSwitchTime"]=SwitchTime
			print("Final OLF" + ': ' +str(OLFValues[n]))
			CaptureEfficiencyOLFSwitch(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0

def CaptureEfficiencyOLFRamp(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathMOTcompareforOLFramp)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
	mm.Run(dic)	

	mm.SetScriptPath(OLFramppath)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0

def TemperatureScanOLFRamp(RampSwitchOffTimeVals, ExpansionTimes,repeats):
	mm.SetScriptPath(OLFramppath)
	dic = Dictionary[String, Object]()  
	
	for i in range(repeats):
		print("Repeat number: " + str(i+1))
		for n in range(len(RampSwitchOffTimeVals)):
			print("Time at fixed ramp rate: " +str(RampSwitchTimeVals[n]))
			dic["RampSwitchOffTime"]=RampSwitchOffTimeVals[n]
			for m in range(len(ExpansionTimes)):
				dic["ImageDelay"] = ExpansionTimes[m]
				
				mm.Run(dic)
				print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after release from Molasses')							
	return 0

def TemperatureScanTwoStageRamp(Param,ParamVals, ExpansionTimes,repeats):
	mm.SetScriptPath(TwoStagePath)
	dic = Dictionary[String, Object]()  
	
	for i in range(repeats):
		print("Repeat number: " + str(i+1))
		for n in range(len(ParamVals)):
			print(Param+ ": " +str(ParamVals[n]))
			dic[Param]=ParamVals[n]
			for m in range(len(ExpansionTimes)):
				dic["ImageDelay"] = ExpansionTimes[m]
				
				mm.Run(dic)
				print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after release from Molasses')							
	return 0

def CEScan2StageRamp(FinalPowerVals, MidPower, NoMolMeasureTime, MolMeasureTime,repeats):
	dic = Dictionary[String, Object]()
	dic["MolassesPrincipalIntermediateAmplitude"]=MidPower
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(FinalPowerVals)):
			dic["MolassesPrincipalFinalAmplitude"]=FinalPowerVals[n]
			print("Final Power" + ': ' +str(FinalPowerVals[n]))
			CaptureEfficiency2StageRamp(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0

def CaptureEfficiency2StageRamp(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathMOTcomparefor2Stageramp)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
	mm.Run(dic)	

	mm.SetScriptPath(TwoStagePath)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0

def CaptureEfficiencyScanOLFRamp(OLFstart, OLFfinal,RampTimes, NoMolMeasureTime, MolMeasureTime, SwitchTime,repeats):
	dic = Dictionary[String, Object]()
	dic["offsetlockvcofrequency"]=OLFstart
	dic["OLFfinal"]=OLFfinal
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(RampTimes)):
			dic["RampSwitchOffTime"]=RampTimes[n]
			print("Ramp Switch Off at " + ': ' +str(RampTimes[n]))
			CaptureEfficiencyOLFRamp(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0

def CaptureEfficiencyPulsePowerSwitch(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathMOTcompareforpulseramp)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
	mm.Run(dic)	

	mm.SetScriptPath(pulseswitchpath)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0


def CaptureEfficiencyScanPulsePowerSwitch(PowerValues, NoMolMeasureTime, MolMeasureTime, SwitchTime,repeats):
	dic = Dictionary[String, Object]()
	dic["MolassesPowerSwitchTime"]=SwitchTime
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(PowerValues)):
			dic["MolassesPrincipalFinalAmplitude"]=PowerValues[n]
			dic["MolassesPowerSwitchTime"]=SwitchTime
			print("Final Power" + ': ' +str(PowerValues[n]))
			CaptureEfficiencyPulsePowerSwitch(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0
		

def MolassesPulsePowerRampTemp(IntensityVals, RampTime, ExpansionTimes, repeats):
	mm.SetScriptPath(pulseswitchpath)
	dic = Dictionary[String, Object]()
	dic["MolassesFullRampTime"]= RampTime
	
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for m in range(len(IntensityVals)):
			dic["MolassesPrincipalFinalAmplitude"] = IntensityVals[m]
			print("MolassesPower"+ ': ' +str(IntensityVals[m]))
			for n in range(len(ExpansionTimes)):
				dic["ImageDelay"]=ExpansionTimes[n]
				print("Expansion Time"+ ': ' +str(ExpansionTimes[n]))
				mm.Run(dic)
		
	return 0

def PulsePowerRampRateTemp(RampTimeVals, StartPower,FinalPower,TimeAfterRampStart, ExpansionTimes, repeats):
	mm.SetScriptPath(pulserampdownpath)
	dic = Dictionary[String, Object]()
	dic["RampSwitchOffTime"]= TimeAfterRampStart
	dic["MolassesPrincipalStartAmplitude"]= StartPower
	dic["MolassesPrincipalFinalAmplitude"]= FinalPower
	
	
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for m in range(len(RampTimeVals)):
			dic["MolassesTimeForFullRamp"] = RampTimeVals[m]
			print("Ramp Time"+ ': ' +str(RampTimeVals[m]))
			for n in range(len(ExpansionTimes)):
				dic["ImageDelay"]=ExpansionTimes[n]
				print("Expansion Time"+ ': ' +str(ExpansionTimes[n]))
				mm.Run(dic)
		
	return 0

def PulsePowerRateCEScan(RampTimeVals, StartPower,FinalPower,TimeAfterRampStart,NoMolMeasureTime,MolMeasureTime,repeats):
	dic = Dictionary[String, Object]()
	dic["RampSwitchOffTime"]= TimeAfterRampStart
	dic["MolassesPrincipalStartAmplitude"]= StartPower
	dic["MolassesPrincipalFinalAmplitude"]= FinalPower
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(RampTimeVals)):
			dic["MolassesTimeForFullRamp"]=RampTimeVals[n]
			print("Time Over Which Ramp Rate applied" + ': ' +str(RampTimeVals[n]))
			CaptureEfficiencyPulsePowerRamp(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0

def CaptureEfficiencyScanPulsePowerDown(RampSwitchOffTimes,NoMolMeasureTime, MolMeasureTime, repeats):
	dic = Dictionary[String, Object]()
	if not repeats:
		repeats = 1
	if not NoMolMeasureTime:
		NoMolMeasureTime = 5
	
	if not MolMeasureTime:
		MolMeasureTime = 40

	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for n in range(len(RampSwitchOffTimes)):
			dic["RampSwitchOffTime"]=RampSwitchOffTimes[n]
			print("Time Over Which Constant Ramp Rate applied" + ': ' +str(RampSwitchOffTimes[n]))
			CaptureEfficiencyPulsePowerRamp(dic,NoMolMeasureTime,MolMeasureTime)
	
	return 0


def CaptureEfficiencyPulsePowerRamp(dic, NoMolMeasureTime, MolMeasureTime):
	mm.SetScriptPath(pathMOTcompareforpulserampdown)
	dic["ImageDelay"] = NoMolMeasureTime
	dic["DoMolassesPulse"] = False
	print('MOT Measurement. Imaging ' + str(dic["ImageDelay"]) + ' units after relase from MOT')
	mm.Run(dic)	

	mm.SetScriptPath(pulserampdownpath)
	dic["ImageDelay"] = MolMeasureTime	
	dic["DoMolassesPulse"] = True
	print('Molasses Measurement. Imaging ' + str(dic["ImageDelay"])+ ' units after relase from Molasses')
	mm.Run(dic)

	
	return 0


def MolassesPulsePowerRampDownTemp(RampSwitchOffTimes, ExpansionTimes, repeats):
	mm.SetScriptPath(pulserampdownpath)
	dic = Dictionary[String, Object]()
		
	for i in range(repeats):
		print('Repeat number: ' + str(i+1))
		for m in range(len(RampSwitchOffTimes)):
			dic["RampSwitchOffTime"] = RampSwitchOffTimes[m]
			print("Power Switched off at"+ ': ' +str(RampSwitchOffTimes[m]))
			for n in range(len(ExpansionTimes)):
				dic["ImageDelay"]=ExpansionTimes[n]
				print("Expansion Time"+ ': ' +str(ExpansionTimes[n]))
				mm.Run(dic)
		
	return 0