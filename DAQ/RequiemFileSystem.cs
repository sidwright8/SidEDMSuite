﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DAQ
{
    class RequiemFileSystem : DAQ.Environment.FileSystem
    {
        public RequiemFileSystem()
        {
            Paths.Add("mathPath", "c:\\Program Files\\Wolfram Research\\Mathematica\\7.0\\mathkernel.exe");
            Paths.Add("settingsPath", "c:\\Data\\Settings\\");
            Paths.Add("scanMasterDataPath", "c:\\Data\\ScanMasterData\\");
            Paths.Add("dataPath", "c:\\Data\\");
            Paths.Add("fakeData", "c:\\Data\\Examples\\");

            Paths.Add("MOTMasterDataPath", "c:\\Data\\MOTMasterData\\");
            Paths.Add("scriptListPath", "C:\\ExperimentControl\\EDMSuite\\SympatheticMOTMasterScripts\\");
            Paths.Add("daqDLLPath", "C:\\ExperimentControl\\EDMSuite\\DAQ\\bin\\Sympathetic\\daq.dll");
            Paths.Add("MOTMasterExePath",
                "C:\\ExperimentControl\\EDMSuite\\MOTMaster\\bin\\Sympathetic\\");
            Paths.Add("UntriggeredCameraAttributesPath", "c:\\Data\\Settings\\CameraAttributes\\SHCCameraAttributes.txt");
            Paths.Add("CameraAttributesPath", "c:\\Data\\Settings\\CameraAttributes\\MOTMasterCameraAttributes.txt");
            Paths.Add("HardwareClassPath", "C:\\ExperimentControl\\EDMSuite\\DAQ\\PXISympatheticHardware.cs");

            DataSearchPaths.Add(Paths["scanMasterDataPath"]);

            SortDataByDate = false;
        }
    }
}
