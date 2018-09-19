namespace SympatheticHardwareControl
{
    partial class HardwareMonitorWindow
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chamber1PressureCheckBox = new System.Windows.Forms.CheckBox();
            this.chamber1PressureTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.OvenChamberLogPressureGraph = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot4 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis4 = new NationalInstruments.UI.XAxis();
            this.yAxis4 = new NationalInstruments.UI.YAxis();
            this.MOTpressureLogPlot = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot3 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis3 = new NationalInstruments.UI.XAxis();
            this.yAxis3 = new NationalInstruments.UI.YAxis();
            this.OvenChamberPressurePlot = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot2 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis2 = new NationalInstruments.UI.XAxis();
            this.yAxis2 = new NationalInstruments.UI.YAxis();
            this.MOTChamberPressurePlot = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.waveformPlot1 = new NationalInstruments.UI.WaveformPlot();
            this.xAxis1 = new NationalInstruments.UI.XAxis();
            this.yAxis1 = new NationalInstruments.UI.YAxis();
            this.chamber2PressureTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.chamber2PressureCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.laserLockErrorThresholdTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.laserErrorLED = new NationalInstruments.UI.WindowsForms.Led();
            this.laserErrorMonitorTextbox = new System.Windows.Forms.TextBox();
            this.laserErrorMonitorCheckBox = new System.Windows.Forms.CheckBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.monitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chamber3PressureCheckBox = new System.Windows.Forms.CheckBox();
            this.chamber3PressureTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.MWChamberPressurePlot = new NationalInstruments.UI.WindowsForms.WaveformGraph();
            this.xAxis5 = new NationalInstruments.UI.XAxis();
            this.yAxis5 = new NationalInstruments.UI.YAxis();
            this.waveformPlot5 = new NationalInstruments.UI.WaveformPlot();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OvenChamberLogPressureGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MOTpressureLogPlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OvenChamberPressurePlot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MOTChamberPressurePlot)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.laserErrorLED)).BeginInit();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MWChamberPressurePlot)).BeginInit();
            this.SuspendLayout();
            // 
            // chamber1PressureCheckBox
            // 
            this.chamber1PressureCheckBox.AutoSize = true;
            this.chamber1PressureCheckBox.Location = new System.Drawing.Point(8, 242);
            this.chamber1PressureCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.chamber1PressureCheckBox.Name = "chamber1PressureCheckBox";
            this.chamber1PressureCheckBox.Size = new System.Drawing.Size(185, 21);
            this.chamber1PressureCheckBox.TabIndex = 0;
            this.chamber1PressureCheckBox.Text = "Oven Chamber pressure";
            this.chamber1PressureCheckBox.UseVisualStyleBackColor = true;
            this.chamber1PressureCheckBox.CheckedChanged += new System.EventHandler(this.chamber1PressureCheckBox_CheckedChanged);
            // 
            // chamber1PressureTextBox
            // 
            this.chamber1PressureTextBox.Location = new System.Drawing.Point(8, 271);
            this.chamber1PressureTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.chamber1PressureTextBox.Name = "chamber1PressureTextBox";
            this.chamber1PressureTextBox.ReadOnly = true;
            this.chamber1PressureTextBox.Size = new System.Drawing.Size(90, 22);
            this.chamber1PressureTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(439, 268);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "mbar";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MWChamberPressurePlot);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.chamber3PressureTextBox);
            this.groupBox1.Controls.Add(this.chamber3PressureCheckBox);
            this.groupBox1.Controls.Add(this.OvenChamberLogPressureGraph);
            this.groupBox1.Controls.Add(this.MOTpressureLogPlot);
            this.groupBox1.Controls.Add(this.OvenChamberPressurePlot);
            this.groupBox1.Controls.Add(this.MOTChamberPressurePlot);
            this.groupBox1.Controls.Add(this.chamber2PressureTextBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.chamber2PressureCheckBox);
            this.groupBox1.Controls.Add(this.chamber1PressureCheckBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.chamber1PressureTextBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 98);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1123, 388);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pressure Gauges";
            // 
            // OvenChamberLogPressureGraph
            // 
            this.OvenChamberLogPressureGraph.Location = new System.Drawing.Point(153, 262);
            this.OvenChamberLogPressureGraph.Name = "OvenChamberLogPressureGraph";
            this.OvenChamberLogPressureGraph.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot4});
            this.OvenChamberLogPressureGraph.Size = new System.Drawing.Size(163, 119);
            this.OvenChamberLogPressureGraph.TabIndex = 8;
            this.OvenChamberLogPressureGraph.UseColorGenerator = true;
            this.OvenChamberLogPressureGraph.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis4});
            this.OvenChamberLogPressureGraph.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis4});
            // 
            // waveformPlot4
            // 
            this.waveformPlot4.XAxis = this.xAxis4;
            this.waveformPlot4.YAxis = this.yAxis4;
            // 
            // yAxis4
            // 
            this.yAxis4.EditRangeNumericFormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.yAxis4.ScaleType = NationalInstruments.UI.ScaleType.Logarithmic;
            // 
            // MOTpressureLogPlot
            // 
            this.MOTpressureLogPlot.Location = new System.Drawing.Point(486, 262);
            this.MOTpressureLogPlot.Name = "MOTpressureLogPlot";
            this.MOTpressureLogPlot.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot3});
            this.MOTpressureLogPlot.Size = new System.Drawing.Size(161, 119);
            this.MOTpressureLogPlot.TabIndex = 6;
            this.MOTpressureLogPlot.UseColorGenerator = true;
            this.MOTpressureLogPlot.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis3});
            this.MOTpressureLogPlot.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis3});
            // 
            // waveformPlot3
            // 
            this.waveformPlot3.XAxis = this.xAxis3;
            this.waveformPlot3.YAxis = this.yAxis3;
            // 
            // yAxis3
            // 
            this.yAxis3.EditRangeNumericFormatMode = NationalInstruments.UI.NumericFormatMode.CreateScientificMode(2);
            this.yAxis3.ScaleType = NationalInstruments.UI.ScaleType.Logarithmic;
            // 
            // OvenChamberPressurePlot
            // 
            this.OvenChamberPressurePlot.Location = new System.Drawing.Point(7, 25);
            this.OvenChamberPressurePlot.Name = "OvenChamberPressurePlot";
            this.OvenChamberPressurePlot.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot2});
            this.OvenChamberPressurePlot.Size = new System.Drawing.Size(310, 206);
            this.OvenChamberPressurePlot.TabIndex = 7;
            this.OvenChamberPressurePlot.UseColorGenerator = true;
            this.OvenChamberPressurePlot.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis2});
            this.OvenChamberPressurePlot.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis2});
            // 
            // waveformPlot2
            // 
            this.waveformPlot2.XAxis = this.xAxis2;
            this.waveformPlot2.YAxis = this.yAxis2;
            // 
            // MOTChamberPressurePlot
            // 
            this.MOTChamberPressurePlot.Location = new System.Drawing.Point(336, 25);
            this.MOTChamberPressurePlot.Name = "MOTChamberPressurePlot";
            this.MOTChamberPressurePlot.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot1});
            this.MOTChamberPressurePlot.Size = new System.Drawing.Size(311, 207);
            this.MOTChamberPressurePlot.TabIndex = 6;
            this.MOTChamberPressurePlot.UseColorGenerator = true;
            this.MOTChamberPressurePlot.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis1});
            this.MOTChamberPressurePlot.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis1});
            // 
            // waveformPlot1
            // 
            this.waveformPlot1.XAxis = this.xAxis1;
            this.waveformPlot1.YAxis = this.yAxis1;
            // 
            // chamber2PressureTextBox
            // 
            this.chamber2PressureTextBox.Location = new System.Drawing.Point(336, 267);
            this.chamber2PressureTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.chamber2PressureTextBox.Name = "chamber2PressureTextBox";
            this.chamber2PressureTextBox.ReadOnly = true;
            this.chamber2PressureTextBox.Size = new System.Drawing.Size(85, 22);
            this.chamber2PressureTextBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(106, 274);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "mbar";
            // 
            // chamber2PressureCheckBox
            // 
            this.chamber2PressureCheckBox.AutoSize = true;
            this.chamber2PressureCheckBox.Location = new System.Drawing.Point(336, 242);
            this.chamber2PressureCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.chamber2PressureCheckBox.Name = "chamber2PressureCheckBox";
            this.chamber2PressureCheckBox.Size = new System.Drawing.Size(182, 21);
            this.chamber2PressureCheckBox.TabIndex = 3;
            this.chamber2PressureCheckBox.Text = "MOT Chamber pressure";
            this.chamber2PressureCheckBox.UseVisualStyleBackColor = true;
            this.chamber2PressureCheckBox.CheckedChanged += new System.EventHandler(this.chamber2PressureCheckBox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.laserLockErrorThresholdTextBox);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.laserErrorLED);
            this.groupBox2.Controls.Add(this.laserErrorMonitorTextbox);
            this.groupBox2.Controls.Add(this.laserErrorMonitorCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(16, 34);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(676, 57);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Laser error signal";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(391, 25);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Threshold";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(612, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "V";
            // 
            // laserLockErrorThresholdTextBox
            // 
            this.laserLockErrorThresholdTextBox.Location = new System.Drawing.Point(471, 21);
            this.laserLockErrorThresholdTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.laserLockErrorThresholdTextBox.Name = "laserLockErrorThresholdTextBox";
            this.laserLockErrorThresholdTextBox.Size = new System.Drawing.Size(132, 22);
            this.laserLockErrorThresholdTextBox.TabIndex = 5;
            this.laserLockErrorThresholdTextBox.Text = "0.1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(348, 25);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "V";
            // 
            // laserErrorLED
            // 
            this.laserErrorLED.LedStyle = NationalInstruments.UI.LedStyle.Round3D;
            this.laserErrorLED.Location = new System.Drawing.Point(628, 12);
            this.laserErrorLED.Margin = new System.Windows.Forms.Padding(4);
            this.laserErrorLED.Name = "laserErrorLED";
            this.laserErrorLED.Size = new System.Drawing.Size(41, 37);
            this.laserErrorLED.TabIndex = 4;
            // 
            // laserErrorMonitorTextbox
            // 
            this.laserErrorMonitorTextbox.Location = new System.Drawing.Point(207, 21);
            this.laserErrorMonitorTextbox.Margin = new System.Windows.Forms.Padding(4);
            this.laserErrorMonitorTextbox.Name = "laserErrorMonitorTextbox";
            this.laserErrorMonitorTextbox.ReadOnly = true;
            this.laserErrorMonitorTextbox.Size = new System.Drawing.Size(132, 22);
            this.laserErrorMonitorTextbox.TabIndex = 3;
            // 
            // laserErrorMonitorCheckBox
            // 
            this.laserErrorMonitorCheckBox.AutoSize = true;
            this.laserErrorMonitorCheckBox.Location = new System.Drawing.Point(8, 23);
            this.laserErrorMonitorCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.laserErrorMonitorCheckBox.Name = "laserErrorMonitorCheckBox";
            this.laserErrorMonitorCheckBox.Size = new System.Drawing.Size(191, 21);
            this.laserErrorMonitorCheckBox.TabIndex = 0;
            this.laserErrorMonitorCheckBox.Text = "Monitor laser Error Signal";
            this.laserErrorMonitorCheckBox.UseVisualStyleBackColor = true;
            this.laserErrorMonitorCheckBox.CheckedChanged += new System.EventHandler(this.laserErrorMonitorCheckBox_CheckedChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.monitorToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(1191, 28);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // monitorToolStripMenuItem
            // 
            this.monitorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startAllToolStripMenuItem,
            this.stopAllToolStripMenuItem});
            this.monitorToolStripMenuItem.Name = "monitorToolStripMenuItem";
            this.monitorToolStripMenuItem.Size = new System.Drawing.Size(74, 24);
            this.monitorToolStripMenuItem.Text = "Monitor";
            // 
            // startAllToolStripMenuItem
            // 
            this.startAllToolStripMenuItem.Name = "startAllToolStripMenuItem";
            this.startAllToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.startAllToolStripMenuItem.Text = "Start All";
            this.startAllToolStripMenuItem.Click += new System.EventHandler(this.startAllToolStripMenuItem_Click);
            // 
            // stopAllToolStripMenuItem
            // 
            this.stopAllToolStripMenuItem.Name = "stopAllToolStripMenuItem";
            this.stopAllToolStripMenuItem.Size = new System.Drawing.Size(137, 26);
            this.stopAllToolStripMenuItem.Text = "Stop All";
            this.stopAllToolStripMenuItem.Click += new System.EventHandler(this.stopAllToolStripMenuItem_Click);
            // 
            // chamber3PressureCheckBox
            // 
            this.chamber3PressureCheckBox.AutoSize = true;
            this.chamber3PressureCheckBox.Location = new System.Drawing.Point(673, 244);
            this.chamber3PressureCheckBox.Name = "chamber3PressureCheckBox";
            this.chamber3PressureCheckBox.Size = new System.Drawing.Size(142, 21);
            this.chamber3PressureCheckBox.TabIndex = 9;
            this.chamber3PressureCheckBox.Text = "MW trap chamber";
            this.chamber3PressureCheckBox.UseVisualStyleBackColor = true;
            this.chamber3PressureCheckBox.CheckedChanged += new System.EventHandler(this.chamber3PressureCheckBox_CheckedChanged);
            // 
            // chamber3PressureTextBox
            // 
            this.chamber3PressureTextBox.BackColor = System.Drawing.SystemColors.Control;
            this.chamber3PressureTextBox.Location = new System.Drawing.Point(673, 271);
            this.chamber3PressureTextBox.Name = "chamber3PressureTextBox";
            this.chamber3PressureTextBox.ReadOnly = true;
            this.chamber3PressureTextBox.Size = new System.Drawing.Size(107, 22);
            this.chamber3PressureTextBox.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(802, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 17);
            this.label6.TabIndex = 11;
            this.label6.Text = "mbar";
            // 
            // MWChamberPressurePlot
            // 
            this.MWChamberPressurePlot.Location = new System.Drawing.Point(673, 22);
            this.MWChamberPressurePlot.Name = "MWChamberPressurePlot";
            this.MWChamberPressurePlot.Plots.AddRange(new NationalInstruments.UI.WaveformPlot[] {
            this.waveformPlot5});
            this.MWChamberPressurePlot.Size = new System.Drawing.Size(353, 216);
            this.MWChamberPressurePlot.TabIndex = 12;
            this.MWChamberPressurePlot.UseColorGenerator = true;
            this.MWChamberPressurePlot.XAxes.AddRange(new NationalInstruments.UI.XAxis[] {
            this.xAxis5});
            this.MWChamberPressurePlot.YAxes.AddRange(new NationalInstruments.UI.YAxis[] {
            this.yAxis5});
            // 
            // waveformPlot5
            // 
            this.waveformPlot5.XAxis = this.xAxis5;
            this.waveformPlot5.YAxis = this.yAxis5;
            // 
            // HardwareMonitorWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1191, 499);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "HardwareMonitorWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Hardware Monitor Window";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.HardwareMonitorWindow_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.OvenChamberLogPressureGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MOTpressureLogPlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OvenChamberPressurePlot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MOTChamberPressurePlot)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.laserErrorLED)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MWChamberPressurePlot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chamber1PressureCheckBox;
        private System.Windows.Forms.TextBox chamber1PressureTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox laserErrorMonitorTextbox;
        private NationalInstruments.UI.WindowsForms.Led laserErrorLED;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox laserErrorMonitorCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox laserLockErrorThresholdTextBox;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem monitorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem startAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopAllToolStripMenuItem;
        private System.Windows.Forms.CheckBox chamber2PressureCheckBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox chamber2PressureTextBox;
        private NationalInstruments.UI.WindowsForms.WaveformGraph MOTChamberPressurePlot;
        private NationalInstruments.UI.WaveformPlot waveformPlot1;
        private NationalInstruments.UI.XAxis xAxis1;
        private NationalInstruments.UI.YAxis yAxis1;
        private NationalInstruments.UI.WindowsForms.WaveformGraph OvenChamberPressurePlot;
        private NationalInstruments.UI.WaveformPlot waveformPlot2;
        private NationalInstruments.UI.XAxis xAxis2;
        private NationalInstruments.UI.YAxis yAxis2;
        private NationalInstruments.UI.WindowsForms.WaveformGraph MOTpressureLogPlot;
        private NationalInstruments.UI.WaveformPlot waveformPlot3;
        private NationalInstruments.UI.XAxis xAxis3;
        private NationalInstruments.UI.YAxis yAxis3;
        private NationalInstruments.UI.WindowsForms.WaveformGraph OvenChamberLogPressureGraph;
        private NationalInstruments.UI.WaveformPlot waveformPlot4;
        private NationalInstruments.UI.XAxis xAxis4;
        private NationalInstruments.UI.YAxis yAxis4;
        private System.Windows.Forms.CheckBox chamber3PressureCheckBox;
        private System.Windows.Forms.TextBox chamber3PressureTextBox;
        private System.Windows.Forms.Label label6;
        private NationalInstruments.UI.WindowsForms.WaveformGraph MWChamberPressurePlot;
        private NationalInstruments.UI.WaveformPlot waveformPlot5;
        private NationalInstruments.UI.XAxis xAxis5;
        private NationalInstruments.UI.YAxis yAxis5;
    }
}