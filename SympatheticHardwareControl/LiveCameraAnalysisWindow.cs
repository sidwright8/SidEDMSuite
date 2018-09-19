using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SympatheticHardwareControl
{
    public partial class LiveCameraAnalysisWindow : Form
    {
        
        public Controller controller;
        
        public LiveCameraAnalysisWindow()
        {
            InitializeComponent();
                        
        }

        public void startPlottingValues()
        {

            Thread PixelValsPlotThread = new Thread(new ThreadStart(plotSummedPixelVals));
            PixelValsPlotThread.Start();
        }
        
        public void plotSummedPixelVals()
        {

            for (; ; )
            {

                double newval = controller.ImageController.SummedPixels;
                summedPixelsWaveformGraph.PlotYAppend(1.1);
            }
        }
        
    }
}
