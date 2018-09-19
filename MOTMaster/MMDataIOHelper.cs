using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace MOTMaster
{
    public class MMDataIOHelper
    {
        MMDataZipper zipper = new MMDataZipper();
        private string motMasterDataPath;
        private string element;


        public MMDataIOHelper(string motMasterDataPath, string element)
        {
            this.motMasterDataPath = motMasterDataPath;
            this.element = element;
        }



        public void StoreRun(string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[,] imageData)
        {
            string fileTag = getDataID(element, batchNumber);

            saveToFiles(fileTag, saveFolder, batchNumber, pathToPattern, pathToHardwareClass, dict, report, cameraAttributesPath, imageData);

            string[] files = putCopiesOfFilesToZip(saveFolder, fileTag);

            //deleteFiles(saveFolder, fileTag);
            deleteFiles(files);
        }
        public void StoreRun(string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[][,] imageData)
        {
            string fileTag = getDataID(element, batchNumber);

            saveToFiles(fileTag, saveFolder, batchNumber, pathToPattern, pathToHardwareClass, dict, report, cameraAttributesPath, imageData);

            string[] files = putCopiesOfFilesToZip(saveFolder, fileTag);

            //deleteFiles(saveFolder, fileTag);
            deleteFiles(files);
        }
        public void StoreRun(string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[][,] imageData, double[,] absImage, double[] columnSum, double[] rowSum)
        {
            string fileTag = getDataID(element, batchNumber);
            Console.WriteLine("got Data ID");

            saveToFiles(fileTag, saveFolder, batchNumber, pathToPattern, pathToHardwareClass, dict, report, cameraAttributesPath, imageData,absImage,columnSum,rowSum);

            string[] files = putCopiesOfFilesToZip(saveFolder, fileTag);
            Console.WriteLine("zipped files");

            //deleteFiles(saveFolder, fileTag);
            deleteFiles(files);
        }

        private void deleteFiles(string[] files)
        {
            foreach (string s in files)
            {
                File.Delete(s);
            }
        }
        private string[] putCopiesOfFilesToZip(string saveFolder, string fileTag)
        {

            string[] files = Directory.GetFiles(saveFolder, fileTag + "*");
            System.IO.FileStream fs = new FileStream(saveFolder + fileTag + ".zip", FileMode.Create);
            zipper.PrepareZip(fs);
            foreach (string s in files)
            {
                string[] bits = (s.Split('\\'));
                string name = bits[bits.Length - 1];
                zipper.AppendToZip(saveFolder, name);
            }
 
            zipper.CloseZip();
            fs.Close();
            return files;
        }
        private void saveToFiles(string fileTag, string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[,] imageData)
        {
            storeDictionary(saveFolder + fileTag + "_parameters.txt", dict);
            File.Copy(pathToPattern, saveFolder + fileTag + "_script.cs");
            File.Copy(pathToHardwareClass, saveFolder + fileTag + "_hardwareClass.cs");
            storeCameraAttributes(saveFolder + fileTag + "_cameraParameters.txt", cameraAttributesPath);
            storeImage(saveFolder + fileTag, imageData);
            storeDictionary(saveFolder + fileTag + "_hardwareReport.txt", report);
        }
        private void saveToFiles(string fileTag, string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[][,] imageData)
        {
            storeDictionary(saveFolder + fileTag + "_parameters.txt", dict);
            File.Copy(pathToPattern, saveFolder + fileTag + "_script.cs");
            File.Copy(pathToHardwareClass, saveFolder + fileTag + "_hardwareClass.cs");
            storeCameraAttributes(saveFolder + fileTag + "_cameraParameters.txt", cameraAttributesPath);
            storeImage(saveFolder + fileTag, imageData);
            storeDictionary(saveFolder + fileTag + "_hardwareReport.txt", report);
        }
        private void saveToFiles(string fileTag, string saveFolder, int batchNumber, string pathToPattern, string pathToHardwareClass,
            Dictionary<String, Object> dict, Dictionary<String, Object> report,
            string cameraAttributesPath, ushort[][,] imageData, double[,]absImage,double[] columnSum,double[] rowSum)
        {
            storeDictionary(saveFolder + fileTag + "_parameters.txt", dict);
            Console.WriteLine("stored experiment params");
            File.Copy(pathToPattern, saveFolder + fileTag + "_script.cs");
            Console.WriteLine("copied script");
            File.Copy(pathToHardwareClass, saveFolder + fileTag + "_hardwareClass.cs");
            Console.WriteLine("copied hardware class");
            storeCameraAttributes(saveFolder + fileTag + "_cameraParameters.txt", cameraAttributesPath);
            Console.WriteLine("stored camera attrributes");
            storeImage(saveFolder + fileTag, imageData);
            Console.WriteLine("stored image");
            //storeAbsImage(saveFolder + fileTag, absImage);
            //Console.WriteLine("stored abs image");
            storeArray(saveFolder + fileTag + "_row.csv", rowSum);
            storeArray(saveFolder + fileTag + "_col.csv", columnSum);
            Console.WriteLine("stored row/column data");

            storeDictionary(saveFolder + fileTag + "_hardwareReport.txt", report);
        }

        public string SelectSavedScriptPathDialog()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "DataSets|*.zip";
            dialog.Title = "Load previously saved pattern";
            dialog.Multiselect = false;
            dialog.InitialDirectory = motMasterDataPath;
            dialog.ShowDialog();
            return dialog.FileName;
        }

        public void UnzipFolder(string path)
        {
            zipper.Unzip(path);
        }

        public Dictionary<string, object> LoadDictionary(string dictionaryPath)
        {
            string[] parameterStrings = File.ReadAllLines(dictionaryPath);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            char separator = '\t';
            foreach (string str in parameterStrings)
            {
                string[] keyValuePairs = str.Split(separator);
                Type t = System.Type.GetType(keyValuePairs[2]);
                dict.Add(keyValuePairs[0], Convert.ChangeType(keyValuePairs[1], t));
            }
            return dict;
        }

        public void DisposeReplicaScript(string folderPath)
        {
            Directory.Delete(folderPath, true);
        }


        private void storeCameraAttributes(string savePath, string attributesPath)
        {
            File.Copy(attributesPath, savePath);
        }

        private void storeImage(string savePath, ushort[][,] imageData)
        {
            for (int i = 0; i < imageData.Length; i++)
            {
                storeImage(savePath + "_" + i.ToString(), imageData[i]);
            }
        }

        private void storeAbsImage(string savePath, double[,] absImage)
        {
                write2DArray(savePath + "_absImage.txt", absImage);            
        }

        private void write2DArray(string savePath, double[,] arrayData)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(savePath);
            string output = "";
            for (int i = 0; i < arrayData.GetUpperBound(0); i++)
            {
                for (int j = 0; j < arrayData.GetUpperBound(1); j++)
                {
                    output += arrayData[i, j].ToString();
                }
                streamWriter.WriteLine(output);
                output = "";
            }
            streamWriter.Close();  

        }

        private void storeArray(string savePath, double[] arrayData)
        {
            writeArray(savePath,arrayData);
        }
        private void writeArray(string savePath, double[] arrayData)
        {
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(savePath);
            string output = "";
            for (int i = 0; i < arrayData.GetUpperBound(0); i++)
            {
                output = arrayData[i].ToString();
                streamWriter.WriteLine(output);
            }
            output = "";
            streamWriter.Close();

        }

        private void storeImage(string savePath, ushort[,] imageData)
        {
            try
            {
                int width = imageData.GetLength(1);
                int height = imageData.GetLength(0);
                ushort[] pixels = new ushort[width * height];
                for (int j = 0; j < height; j++)
                {
                    for (int i = 0; i < width; i++)
                    {
                        pixels[(width * j) + i] = imageData[j, i];
                    }
                }

                // Creates a new empty image with no palette (16 bit grayscale image)

                BitmapSource image = BitmapSource.Create(
                    width,
                    height,
                    96,
                    96,
                    PixelFormats.Gray16,
                    null,
                    pixels,
                    2*width);

                FileStream stream = new FileStream(savePath + ".png", FileMode.Create, FileAccess.Write);
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Interlace = PngInterlaceOption.Off;
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                stream.Dispose();                
                
            }
            catch (ArgumentException e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void storeDictionary(String dataStoreFilePath, Dictionary<string, object> dict)
        {
            TextWriter output = File.CreateText(dataStoreFilePath);
            foreach (KeyValuePair<string, object> pair in dict)
            {
                output.Write(pair.Key);
                output.Write('\t');
                output.Write(pair.Value.ToString());
                output.Write('\t');
                output.WriteLine(pair.Value.GetType());
            }
            output.Close();


        }

        private string getDataID(string element, int batchNumber)
        {
            DateTime dt = DateTime.Now;
            string dateTag;
            string batchTag;
            int subTag = 0;

            dateTag = String.Format("{0:ddMMMyy}", dt);
            batchTag = batchNumber.ToString().PadLeft(2, '0');
            subTag = (Directory.GetFiles(motMasterDataPath, element +
                dateTag + batchTag + "*.zip")).Length;
            string id = element + dateTag + batchTag
                + "_" + subTag.ToString().PadLeft(3, '0');
            return id;
        }

        
    }
}
