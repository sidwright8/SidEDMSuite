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

def ScanCoolingDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["CoolingPulseDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansCD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingDetuning(values)
		j = j+1
	return 0

def ScanCoolingRepumpDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["CoolingPulseRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansCRD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingRepumpDetuning(values)
		j = j+1
	return 0

def ScanCoolingDuration(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["CoolingPulseDuration"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansCDur(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingDuration(values)
		j = j+1
	return 0

def ScanCoolingIntensity(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["CoolingPulseAmplitude"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansCI(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanCoolingIntensity(values)
		j = j+1
	return 0

def ScanAbsDetuning(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["AbsDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansAD(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanAbsDetuning(values)
		j = j+1
	return 0

def ScanMagImage(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["Frame1Trigger"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansMagI(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMagImage(values)
		j = j+1
	return 0

def ScanMOTRepumpFreq(values):
	count = 0
	list = values
	endcount = len(list)
	dic = Dictionary[String,Object]()
	mm.SetScriptPath("C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\MagTrapAbsImageMicroSec.cs")
	while(count < endcount):
		dic["MOTRepumpDetuning"] = list[count]
		mm.Run(dic)
		count = count + 1
	return 0

def RepeatScansMRF(values, numberofrepeats):
	j = 0
	while(j < numberofrepeats):
		ScanMOTRepumpFreq(values)
		j = j+1
	return 0