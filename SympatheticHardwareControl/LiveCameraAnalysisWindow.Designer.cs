namespace SympatheticHardwareControl
{
    partial class LiveCameraAnalysisWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void UpdateIntensityPlot(double value)
        {
            summedPixelsWaveformGraph.PlotYAppend(value); 
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.summedPixelsWaveformGraph = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            ((System.ComponentModel.ISupportInitialize)(this.summedPixelsWaveformGraph)).BeginInit();
            this.SuspendLayout();
            // 
            // summedPixelsWaveformGraph
            // 
            this.summedPixelsWaveformGraph.Location = new System.Drawing.Point(22, 107);
            this.summedPixelsWaveformGraph.Name = "summedPixelsWaveformGraph";
            this.summedPixelsWaveformGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.summedPixelsWaveformGraph.Size = new System.Drawing.Size(431, 276);
            this.summedPixelsWaveformGraph.TabIndex = 0;
            this.summedPixelsWaveformGraph.UseColorGenerator = true;
            this.summedPixelsWaveformGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.summedPixelsWaveformGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1});
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // LiveCameraAnalysisWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(730, 408);
            this.Controls.Add(this.summedPixelsWaveformGraph);
            this.Name = "LiveCameraAnalysisWindow";
            this.Text = "Live Camera Analysis Window";
            ((System.ComponentModel.ISupportInitialize)(this.summedPixelsWaveformGraph)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private NationalInstruments.UI.WindowsForms.WaveformGraph summedPixelsWaveformGraph;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;


    }
}