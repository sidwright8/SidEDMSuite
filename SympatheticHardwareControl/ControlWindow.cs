using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;


using NationalInstruments.UI.WindowsForms;
using NationalInstruments.UI;


namespace SympatheticHardwareControl
{
    /// <summary>
    /// Front panel for the sympathetic hardware controller. Everything is just stuffed in there. No particularly
    /// clever structure. This class just hands everything straight off to the controller. It has a few
    /// thread safe wrappers so that remote calls can safely manipulate the front panel.
    /// </summary>
    public class ControlWindow : System.Windows.Forms.Form
    {
        #region Setup

        public Controller controller;
        private Dictionary<string, TextBox> AOTextBoxes = new Dictionary<string, TextBox>();
        private Dictionary<string, CheckBox> DOCheckBoxes = new Dictionary<string, CheckBox>();
        private Dictionary<string, NumericUpDown> AONumericUpDown = new Dictionary<string, NumericUpDown>();    

        public ControlWindow()
        {
            InitializeComponent();
            //AOTextBoxes["aom0amplitude"] = aom0rfAmplitudeTextBox;
            //AOTextBoxes["aom1amplitude"] = aom1rfAmplitudeTextBox;
            //AOTextBoxes["aom2amplitude"] = aom2rfAmplitudeTextBox;
            //AOTextBoxes["aom3amplitude"] = aom3rfAmplitudeTextBox;
            //AOTextBoxes["aom0frequency"] = aom0rfFrequencyTextBox;
            //AOTextBoxes["aom1frequency"] = aom1rfFrequencyTextBox;
            //AOTextBoxes["aom2frequency"] = aom2rfFrequencyTextBox;
            //AOTextBoxes["aom3frequency"] = aom3rfFrequencyTextBox;
            //AOTextBoxes["coil0current"] = coil0CurrentTextBox;
            //AOTextBoxes["coil1current"] = coil1CurrentTextBox;
            //AOTextBoxes["offsetlockfrequency"] = offsetLockFrequencyTextBox;
            //AOTextBoxes["D1EOMfrequency"] = D1EOMfrequencyTextBox;
            //AOTextBoxes["D2EOMfrequency"] = D2EOMfrequencyTextBox;
            //AOTextBoxes["D1EOMamplitude"] = D1EOMamplitudeTextBox;
            //AOTextBoxes["D2EOMamplitude"] = D2EOMamplitudeTextBox;

            AONumericUpDown["aom0amplitude"] = aom0amplitudeUpDown;
            AONumericUpDown["aom1amplitude"] = aom1amplitudeUpDown;
            AONumericUpDown["aom2amplitude"] = aom2amplitudeUpDown;
            AONumericUpDown["aom3amplitude"] = aom3amplitudeUpDown;
            AONumericUpDown["aom0frequency"] = aom0frequencyUpDown;
            AONumericUpDown["aom1frequency"] = aom1frequencyUpDown;
            AONumericUpDown["aom2frequency"] = aom2frequencyUpDown;
            AONumericUpDown["aom3frequency"] = aom3frequencyUpDown;
            AONumericUpDown["BottomTransportCurrent"] = BottomTransportCurrentUpDown;
            AONumericUpDown["TopTransportCurrent"] = TopTransportCurrentUpDown;
            AONumericUpDown["offsetlockfrequency"] = offsetlockvcoUpDown;
            AONumericUpDown["D1EOMfrequency"] = d1eomfrequencyUpDown;
            AONumericUpDown["D2EOMfrequency"] = d2eomfrequencyUpDown;
            AONumericUpDown["D1EOMamplitude"] = d1eomamplitudeUpDown;
            AONumericUpDown["D2EOMamplitude"] = d2eomamplitudeUpDown;
            AONumericUpDown["xcoilcurrent"] = xcoilcurrentUpDown;
            AONumericUpDown["ycoilcurrent"] = ycoilcurrentUpDown;
            AONumericUpDown["zcoilcurrent"] = zcoilcurrentUpDown;
            AONumericUpDown["TopTrappingCoilcurrent"] = TopTrappingCoilnumericUpDown;
            AONumericUpDown["BottomTrappingCoilcurrent"] = BottomTrappingCoilnumericUpDown;
            AONumericUpDown["MWGeneratorAM"] = MWGeneratorAMnumericUpDown;
            AONumericUpDown["aom4frequency"] = aom4frequencynumericUpDown;
            AONumericUpDown["aom4amplitude"] = aom4amplitudenumericUpDown;

            DOCheckBoxes["aom0enable"] = aom0CheckBox;
            DOCheckBoxes["aom1enable"] = aom1CheckBox;
            DOCheckBoxes["aom2enable"] = aom2CheckBox;
            DOCheckBoxes["aom3enable"] = aom3CheckBox;
            DOCheckBoxes["shutterenable"] = shutterCheckBox;
            DOCheckBoxes["ovenShutterOpen"] = ovenShutterCheckBox;
            DOCheckBoxes["probeshutterenable"] = probeShutterCheckBox;
            DOCheckBoxes["D2EOMenable"] = D2EOMCheckBox;
            DOCheckBoxes["aom4enable"] = aom4checkbox;
                        
        }

        private void WindowClosing(object sender, FormClosingEventArgs e)
        {
            controller.ControllerStopping();
        }

        private void WindowLoaded(object sender, EventArgs e)
        {
            controller.ControllerLoaded();
            
        }

        #endregion

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlWindow));
            this.shcTabs = new System.Windows.Forms.TabControl();
            this.tabCamera = new System.Windows.Forms.TabPage();
            this.stopStreamButton = new System.Windows.Forms.Button();
            this.streamButton = new System.Windows.Forms.Button();
            this.snapshotButton = new System.Windows.Forms.Button();
            this.tabLasers = new System.Windows.Forms.TabPage();
            this.aom4groupbox = new System.Windows.Forms.GroupBox();
            this.aom4amplitudeunitlabel = new System.Windows.Forms.Label();
            this.aom4amplitudenumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom4amplitudelabel = new System.Windows.Forms.Label();
            this.aom4frequencyunitlabel = new System.Windows.Forms.Label();
            this.aom4frequencynumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom4frequencylabel = new System.Windows.Forms.Label();
            this.aom4checkbox = new System.Windows.Forms.CheckBox();
            this.D2EOMControlBox = new System.Windows.Forms.GroupBox();
            this.D2EOMCheckBox = new System.Windows.Forms.CheckBox();
            this.d2eomamplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.d2eomfrequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.D2EOMfrequencylabel = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.D1EOMControlBox = new System.Windows.Forms.GroupBox();
            this.d1eomamplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.d1eomfrequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.D1EOMfrequencylabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.offsetLockControlBox = new System.Windows.Forms.GroupBox();
            this.offsetlockvcoUpDown = new System.Windows.Forms.NumericUpDown();
            this.offsetLockLabel1 = new System.Windows.Forms.Label();
            this.offsetLockLabel0 = new System.Windows.Forms.Label();
            this.ovenShutterControlBox = new System.Windows.Forms.GroupBox();
            this.ovenShutterCheckBox = new System.Windows.Forms.CheckBox();
            this.ProbeControlBox = new System.Windows.Forms.GroupBox();
            this.probeShutterCheckBox = new System.Windows.Forms.CheckBox();
            this.ShutterControlBox = new System.Windows.Forms.GroupBox();
            this.shutterCheckBox = new System.Windows.Forms.CheckBox();
            this.aom3ControlBox = new System.Windows.Forms.GroupBox();
            this.aom3amplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom3frequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom3Label3 = new System.Windows.Forms.Label();
            this.aom3Label1 = new System.Windows.Forms.Label();
            this.aom3CheckBox = new System.Windows.Forms.CheckBox();
            this.aom3Label2 = new System.Windows.Forms.Label();
            this.aom3Label0 = new System.Windows.Forms.Label();
            this.aom2ControlBox = new System.Windows.Forms.GroupBox();
            this.aom2amplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom2frequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom2Label3 = new System.Windows.Forms.Label();
            this.aom2Label1 = new System.Windows.Forms.Label();
            this.aom2CheckBox = new System.Windows.Forms.CheckBox();
            this.aom2Label2 = new System.Windows.Forms.Label();
            this.aom2Label0 = new System.Windows.Forms.Label();
            this.aom1ControlBox = new System.Windows.Forms.GroupBox();
            this.aom1amplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom1frequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom1Label3 = new System.Windows.Forms.Label();
            this.aom1Label1 = new System.Windows.Forms.Label();
            this.aom1CheckBox = new System.Windows.Forms.CheckBox();
            this.aom1Label2 = new System.Windows.Forms.Label();
            this.aom1Label0 = new System.Windows.Forms.Label();
            this.aom0ControlBox = new System.Windows.Forms.GroupBox();
            this.aom0amplitudeUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom0frequencyUpDown = new System.Windows.Forms.NumericUpDown();
            this.aom0Label3 = new System.Windows.Forms.Label();
            this.aom0Label1 = new System.Windows.Forms.Label();
            this.aom0CheckBox = new System.Windows.Forms.CheckBox();
            this.aom0Label2 = new System.Windows.Forms.Label();
            this.aom0Label0 = new System.Windows.Forms.Label();
            this.tabCoils = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.BottomTrappingCoilnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.TopTrappingCoilnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.ShimCoilGroupBox = new System.Windows.Forms.GroupBox();
            this.ycoilcurrentlabel = new System.Windows.Forms.Label();
            this.ycoilcurrentunitlabel = new System.Windows.Forms.Label();
            this.ycoilcurrentUpDown = new System.Windows.Forms.NumericUpDown();
            this.Zshimlabel = new System.Windows.Forms.Label();
            this.zshimunitlabel = new System.Windows.Forms.Label();
            this.zcoilcurrentUpDown = new System.Windows.Forms.NumericUpDown();
            this.xcoilNumericUpDownLabel = new System.Windows.Forms.Label();
            this.xcoilcurrentUpDown = new System.Windows.Forms.NumericUpDown();
            this.XCoilLabel1 = new System.Windows.Forms.Label();
            this.coil0GroupBox = new System.Windows.Forms.GroupBox();
            this.CoilDescriptionLabel = new System.Windows.Forms.Label();
            this.BottomTransportCurrentUpDown = new System.Windows.Forms.NumericUpDown();
            this.TopTransportCurrentUpDown = new System.Windows.Forms.NumericUpDown();
            this.coil1Label1 = new System.Windows.Forms.Label();
            this.coil0Label1 = new System.Windows.Forms.Label();
            this.coil1Label0 = new System.Windows.Forms.Label();
            this.coil0Label0 = new System.Windows.Forms.Label();
            this.tabTranslationStage = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.AutoTriggerCheckBox = new System.Windows.Forms.CheckBox();
            this.RS232GroupBox = new System.Windows.Forms.GroupBox();
            this.TSConnectButton = new System.Windows.Forms.Button();
            this.disposeButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.TSListAllButton = new System.Windows.Forms.Button();
            this.checkStatusButton = new System.Windows.Forms.Button();
            this.TSClearButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.TSHomeButton = new System.Windows.Forms.Button();
            this.TSGoButton = new System.Windows.Forms.Button();
            this.TSReturnButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TSRestartButton = new System.Windows.Forms.Button();
            this.TSOnButton = new System.Windows.Forms.Button();
            this.TSOffButton = new System.Windows.Forms.Button();
            this.initParamsBox = new System.Windows.Forms.GroupBox();
            this.TSInitButton = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TSVelTextBox = new System.Windows.Forms.TextBox();
            this.TSStepsTextBox = new System.Windows.Forms.TextBox();
            this.TSDecTextBox = new System.Windows.Forms.TextBox();
            this.TSAccTextBox = new System.Windows.Forms.TextBox();
            this.tabMicrowaveControl = new System.Windows.Forms.TabPage();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.MWgenAMunits = new System.Windows.Forms.Label();
            this.MWgenAMlabel = new System.Windows.Forms.Label();
            this.MWGeneratorAMnumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.consoleRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveParametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hardwareMonitorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openImageViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openLiveCameraAnalysisWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteControlLED = new NationalInstruments.UI.WindowsForms.Led();
            this.label1 = new System.Windows.Forms.Label();
            this.updateHardwareButton = new System.Windows.Forms.Button();
            this.AutoUpdateCheckBox = new System.Windows.Forms.CheckBox();
            this.DriveFaultButton = new System.Windows.Forms.Button();
            this.UserFaultButton = new System.Windows.Forms.Button();
            this.shcTabs.SuspendLayout();
            this.tabCamera.SuspendLayout();
            this.tabLasers.SuspendLayout();
            this.aom4groupbox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom4amplitudenumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom4frequencynumericUpDown)).BeginInit();
            this.D2EOMControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.d2eomamplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2eomfrequencyUpDown)).BeginInit();
            this.D1EOMControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.d1eomamplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1eomfrequencyUpDown)).BeginInit();
            this.offsetLockControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetlockvcoUpDown)).BeginInit();
            this.ovenShutterControlBox.SuspendLayout();
            this.ProbeControlBox.SuspendLayout();
            this.ShutterControlBox.SuspendLayout();
            this.aom3ControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom3amplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom3frequencyUpDown)).BeginInit();
            this.aom2ControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom2amplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom2frequencyUpDown)).BeginInit();
            this.aom1ControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom1amplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom1frequencyUpDown)).BeginInit();
            this.aom0ControlBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom0amplitudeUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom0frequencyUpDown)).BeginInit();
            this.tabCoils.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomTrappingCoilnumericUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopTrappingCoilnumericUpDown)).BeginInit();
            this.ShimCoilGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ycoilcurrentUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.zcoilcurrentUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xcoilcurrentUpDown)).BeginInit();
            this.coil0GroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomTransportCurrentUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopTransportCurrentUpDown)).BeginInit();
            this.tabTranslationStage.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.RS232GroupBox.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.initParamsBox.SuspendLayout();
            this.tabMicrowaveControl.SuspendLayout();
            this.groupBox6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MWGeneratorAMnumericUpDown)).BeginInit();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remoteControlLED)).BeginInit();
            this.SuspendLayout();
            // 
            // shcTabs
            // 
            this.shcTabs.AllowDrop = true;
            this.shcTabs.Controls.Add(this.tabCamera);
            this.shcTabs.Controls.Add(this.tabLasers);
            this.shcTabs.Controls.Add(this.tabCoils);
            this.shcTabs.Controls.Add(this.tabTranslationStage);
            this.shcTabs.Controls.Add(this.tabMicrowaveControl);
            this.shcTabs.Location = new System.Drawing.Point(3, 67);
            this.shcTabs.Name = "shcTabs";
            this.shcTabs.SelectedIndex = 0;
            this.shcTabs.Size = new System.Drawing.Size(832, 560);
            this.shcTabs.TabIndex = 0;
            // 
            // tabCamera
            // 
            this.tabCamera.Controls.Add(this.stopStreamButton);
            this.tabCamera.Controls.Add(this.streamButton);
            this.tabCamera.Controls.Add(this.snapshotButton);
            this.tabCamera.Location = new System.Drawing.Point(4, 25);
            this.tabCamera.Name = "tabCamera";
            this.tabCamera.Padding = new System.Windows.Forms.Padding(3);
            this.tabCamera.Size = new System.Drawing.Size(824, 531);
            this.tabCamera.TabIndex = 0;
            this.tabCamera.Text = "Camera Control";
            this.tabCamera.UseVisualStyleBackColor = true;
            // 
            // stopStreamButton
            // 
            this.stopStreamButton.Location = new System.Drawing.Point(170, 6);
            this.stopStreamButton.Name = "stopStreamButton";
            this.stopStreamButton.Size = new System.Drawing.Size(78, 29);
            this.stopStreamButton.TabIndex = 18;
            this.stopStreamButton.Text = "Stop";
            this.stopStreamButton.UseVisualStyleBackColor = true;
            this.stopStreamButton.Click += new System.EventHandler(this.stopStreamButton_Click);
            // 
            // streamButton
            // 
            this.streamButton.Location = new System.Drawing.Point(88, 6);
            this.streamButton.Name = "streamButton";
            this.streamButton.Size = new System.Drawing.Size(78, 29);
            this.streamButton.TabIndex = 17;
            this.streamButton.Text = "Stream";
            this.streamButton.UseVisualStyleBackColor = true;
            this.streamButton.Click += new System.EventHandler(this.streamButton_Click);
            // 
            // snapshotButton
            // 
            this.snapshotButton.Location = new System.Drawing.Point(5, 6);
            this.snapshotButton.Name = "snapshotButton";
            this.snapshotButton.Size = new System.Drawing.Size(78, 29);
            this.snapshotButton.TabIndex = 15;
            this.snapshotButton.Text = "Snapshot";
            this.snapshotButton.UseVisualStyleBackColor = true;
            this.snapshotButton.Click += new System.EventHandler(this.snapshotButton_Click);
            // 
            // tabLasers
            // 
            this.tabLasers.AutoScroll = true;
            this.tabLasers.Controls.Add(this.aom4groupbox);
            this.tabLasers.Controls.Add(this.D2EOMControlBox);
            this.tabLasers.Controls.Add(this.D1EOMControlBox);
            this.tabLasers.Controls.Add(this.offsetLockControlBox);
            this.tabLasers.Controls.Add(this.ovenShutterControlBox);
            this.tabLasers.Controls.Add(this.ProbeControlBox);
            this.tabLasers.Controls.Add(this.ShutterControlBox);
            this.tabLasers.Controls.Add(this.aom3ControlBox);
            this.tabLasers.Controls.Add(this.aom2ControlBox);
            this.tabLasers.Controls.Add(this.aom1ControlBox);
            this.tabLasers.Controls.Add(this.aom0ControlBox);
            this.tabLasers.Location = new System.Drawing.Point(4, 25);
            this.tabLasers.Name = "tabLasers";
            this.tabLasers.Padding = new System.Windows.Forms.Padding(3);
            this.tabLasers.Size = new System.Drawing.Size(824, 531);
            this.tabLasers.TabIndex = 1;
            this.tabLasers.Text = "Laser Control";
            this.tabLasers.UseVisualStyleBackColor = true;
            // 
            // aom4groupbox
            // 
            this.aom4groupbox.Controls.Add(this.aom4amplitudeunitlabel);
            this.aom4groupbox.Controls.Add(this.aom4amplitudenumericUpDown);
            this.aom4groupbox.Controls.Add(this.aom4amplitudelabel);
            this.aom4groupbox.Controls.Add(this.aom4frequencyunitlabel);
            this.aom4groupbox.Controls.Add(this.aom4frequencynumericUpDown);
            this.aom4groupbox.Controls.Add(this.aom4frequencylabel);
            this.aom4groupbox.Controls.Add(this.aom4checkbox);
            this.aom4groupbox.Location = new System.Drawing.Point(6, 263);
            this.aom4groupbox.Name = "aom4groupbox";
            this.aom4groupbox.Size = new System.Drawing.Size(540, 72);
            this.aom4groupbox.TabIndex = 31;
            this.aom4groupbox.TabStop = false;
            this.aom4groupbox.Text = "AOM 4 (Optical Pumping)";
            // 
            // aom4amplitudeunitlabel
            // 
            this.aom4amplitudeunitlabel.AutoSize = true;
            this.aom4amplitudeunitlabel.Location = new System.Drawing.Point(516, 33);
            this.aom4amplitudeunitlabel.Name = "aom4amplitudeunitlabel";
            this.aom4amplitudeunitlabel.Size = new System.Drawing.Size(17, 17);
            this.aom4amplitudeunitlabel.TabIndex = 21;
            this.aom4amplitudeunitlabel.Text = "V";
            // 
            // aom4amplitudenumericUpDown
            // 
            this.aom4amplitudenumericUpDown.DecimalPlaces = 2;
            this.aom4amplitudenumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom4amplitudenumericUpDown.Location = new System.Drawing.Point(418, 30);
            this.aom4amplitudenumericUpDown.Name = "aom4amplitudenumericUpDown";
            this.aom4amplitudenumericUpDown.Size = new System.Drawing.Size(92, 22);
            this.aom4amplitudenumericUpDown.TabIndex = 20;
            this.aom4amplitudenumericUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.aom4amplitudenumericUpDown.Click += new System.EventHandler(this.aom4amplitudenumericUpDown_Click);
            // 
            // aom4amplitudelabel
            // 
            this.aom4amplitudelabel.AutoSize = true;
            this.aom4amplitudelabel.Location = new System.Drawing.Point(319, 35);
            this.aom4amplitudelabel.Name = "aom4amplitudelabel";
            this.aom4amplitudelabel.Size = new System.Drawing.Size(87, 17);
            this.aom4amplitudelabel.TabIndex = 18;
            this.aom4amplitudelabel.Text = "VCA Voltage";
            // 
            // aom4frequencyunitlabel
            // 
            this.aom4frequencyunitlabel.AutoSize = true;
            this.aom4frequencyunitlabel.Location = new System.Drawing.Point(263, 33);
            this.aom4frequencyunitlabel.Name = "aom4frequencyunitlabel";
            this.aom4frequencyunitlabel.Size = new System.Drawing.Size(36, 17);
            this.aom4frequencyunitlabel.TabIndex = 17;
            this.aom4frequencyunitlabel.Text = "MHz";
            // 
            // aom4frequencynumericUpDown
            // 
            this.aom4frequencynumericUpDown.DecimalPlaces = 2;
            this.aom4frequencynumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom4frequencynumericUpDown.Location = new System.Drawing.Point(155, 33);
            this.aom4frequencynumericUpDown.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.aom4frequencynumericUpDown.Name = "aom4frequencynumericUpDown";
            this.aom4frequencynumericUpDown.Size = new System.Drawing.Size(89, 22);
            this.aom4frequencynumericUpDown.TabIndex = 2;
            this.aom4frequencynumericUpDown.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.aom4frequencynumericUpDown.Click += new System.EventHandler(this.aom4frequencynumericUpDown_Click);
            // 
            // aom4frequencylabel
            // 
            this.aom4frequencylabel.AutoSize = true;
            this.aom4frequencylabel.Location = new System.Drawing.Point(55, 33);
            this.aom4frequencylabel.Name = "aom4frequencylabel";
            this.aom4frequencylabel.Size = new System.Drawing.Size(97, 17);
            this.aom4frequencylabel.TabIndex = 1;
            this.aom4frequencylabel.Text = "RF Frequency";
            // 
            // aom4checkbox
            // 
            this.aom4checkbox.AutoSize = true;
            this.aom4checkbox.Location = new System.Drawing.Point(27, 33);
            this.aom4checkbox.Name = "aom4checkbox";
            this.aom4checkbox.Size = new System.Drawing.Size(18, 17);
            this.aom4checkbox.TabIndex = 0;
            this.aom4checkbox.UseVisualStyleBackColor = true;
            this.aom4checkbox.Click += new System.EventHandler(this.aom4checkbox_Click);
            // 
            // D2EOMControlBox
            // 
            this.D2EOMControlBox.Controls.Add(this.D2EOMCheckBox);
            this.D2EOMControlBox.Controls.Add(this.d2eomamplitudeUpDown);
            this.D2EOMControlBox.Controls.Add(this.d2eomfrequencyUpDown);
            this.D2EOMControlBox.Controls.Add(this.label15);
            this.D2EOMControlBox.Controls.Add(this.D2EOMfrequencylabel);
            this.D2EOMControlBox.Controls.Add(this.label11);
            this.D2EOMControlBox.Controls.Add(this.label10);
            this.D2EOMControlBox.Location = new System.Drawing.Point(5, 412);
            this.D2EOMControlBox.Name = "D2EOMControlBox";
            this.D2EOMControlBox.Size = new System.Drawing.Size(539, 64);
            this.D2EOMControlBox.TabIndex = 30;
            this.D2EOMControlBox.TabStop = false;
            this.D2EOMControlBox.Text = "D2 EOM";
            // 
            // D2EOMCheckBox
            // 
            this.D2EOMCheckBox.AutoSize = true;
            this.D2EOMCheckBox.Location = new System.Drawing.Point(28, 29);
            this.D2EOMCheckBox.Name = "D2EOMCheckBox";
            this.D2EOMCheckBox.Size = new System.Drawing.Size(18, 17);
            this.D2EOMCheckBox.TabIndex = 8;
            this.D2EOMCheckBox.UseVisualStyleBackColor = true;
            this.D2EOMCheckBox.Click += new System.EventHandler(this.D2EOMcheckbox_Click);
            // 
            // d2eomamplitudeUpDown
            // 
            this.d2eomamplitudeUpDown.DecimalPlaces = 2;
            this.d2eomamplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.d2eomamplitudeUpDown.Location = new System.Drawing.Point(418, 23);
            this.d2eomamplitudeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.d2eomamplitudeUpDown.Name = "d2eomamplitudeUpDown";
            this.d2eomamplitudeUpDown.Size = new System.Drawing.Size(92, 22);
            this.d2eomamplitudeUpDown.TabIndex = 7;
            this.d2eomamplitudeUpDown.Click += new System.EventHandler(this.d2eomamplitudeUpDown_Click);
            // 
            // d2eomfrequencyUpDown
            // 
            this.d2eomfrequencyUpDown.DecimalPlaces = 2;
            this.d2eomfrequencyUpDown.Location = new System.Drawing.Point(158, 23);
            this.d2eomfrequencyUpDown.Maximum = new decimal(new int[] {
            900,
            0,
            0,
            0});
            this.d2eomfrequencyUpDown.Minimum = new decimal(new int[] {
            700,
            0,
            0,
            0});
            this.d2eomfrequencyUpDown.Name = "d2eomfrequencyUpDown";
            this.d2eomfrequencyUpDown.Size = new System.Drawing.Size(87, 22);
            this.d2eomfrequencyUpDown.TabIndex = 6;
            this.d2eomfrequencyUpDown.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.d2eomfrequencyUpDown.Click += new System.EventHandler(this.d2eomfrequencyUpDown_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(324, 28);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(85, 17);
            this.label15.TabIndex = 5;
            this.label15.Text = "VCA voltage";
            // 
            // D2EOMfrequencylabel
            // 
            this.D2EOMfrequencylabel.AutoSize = true;
            this.D2EOMfrequencylabel.Location = new System.Drawing.Point(52, 28);
            this.D2EOMfrequencylabel.Name = "D2EOMfrequencylabel";
            this.D2EOMfrequencylabel.Size = new System.Drawing.Size(93, 17);
            this.D2EOMfrequencylabel.TabIndex = 4;
            this.D2EOMfrequencylabel.Text = "RF frequency";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(518, 28);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(17, 17);
            this.label11.TabIndex = 3;
            this.label11.Text = "V";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(264, 28);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(36, 17);
            this.label10.TabIndex = 2;
            this.label10.Text = "MHz";
            // 
            // D1EOMControlBox
            // 
            this.D1EOMControlBox.Controls.Add(this.d1eomamplitudeUpDown);
            this.D1EOMControlBox.Controls.Add(this.d1eomfrequencyUpDown);
            this.D1EOMControlBox.Controls.Add(this.label14);
            this.D1EOMControlBox.Controls.Add(this.D1EOMfrequencylabel);
            this.D1EOMControlBox.Controls.Add(this.label13);
            this.D1EOMControlBox.Controls.Add(this.label12);
            this.D1EOMControlBox.Location = new System.Drawing.Point(5, 341);
            this.D1EOMControlBox.Name = "D1EOMControlBox";
            this.D1EOMControlBox.Size = new System.Drawing.Size(540, 65);
            this.D1EOMControlBox.TabIndex = 29;
            this.D1EOMControlBox.TabStop = false;
            this.D1EOMControlBox.Text = "D1 EOM";
            // 
            // d1eomamplitudeUpDown
            // 
            this.d1eomamplitudeUpDown.DecimalPlaces = 2;
            this.d1eomamplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.d1eomamplitudeUpDown.Location = new System.Drawing.Point(419, 22);
            this.d1eomamplitudeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.d1eomamplitudeUpDown.Name = "d1eomamplitudeUpDown";
            this.d1eomamplitudeUpDown.Size = new System.Drawing.Size(92, 22);
            this.d1eomamplitudeUpDown.TabIndex = 7;
            this.d1eomamplitudeUpDown.Click += new System.EventHandler(this.d1eomamplitudeUpDown_Click);
            // 
            // d1eomfrequencyUpDown
            // 
            this.d1eomfrequencyUpDown.DecimalPlaces = 2;
            this.d1eomfrequencyUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.d1eomfrequencyUpDown.Location = new System.Drawing.Point(158, 19);
            this.d1eomfrequencyUpDown.Maximum = new decimal(new int[] {
            850,
            0,
            0,
            0});
            this.d1eomfrequencyUpDown.Minimum = new decimal(new int[] {
            788,
            0,
            0,
            0});
            this.d1eomfrequencyUpDown.Name = "d1eomfrequencyUpDown";
            this.d1eomfrequencyUpDown.Size = new System.Drawing.Size(87, 22);
            this.d1eomfrequencyUpDown.TabIndex = 6;
            this.d1eomfrequencyUpDown.Value = new decimal(new int[] {
            800,
            0,
            0,
            0});
            this.d1eomfrequencyUpDown.Click += new System.EventHandler(this.d1eomfrequencyUpDown_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(320, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(85, 17);
            this.label14.TabIndex = 5;
            this.label14.Text = "VCA voltage";
            // 
            // D1EOMfrequencylabel
            // 
            this.D1EOMfrequencylabel.AutoSize = true;
            this.D1EOMfrequencylabel.Location = new System.Drawing.Point(53, 25);
            this.D1EOMfrequencylabel.Name = "D1EOMfrequencylabel";
            this.D1EOMfrequencylabel.Size = new System.Drawing.Size(97, 17);
            this.D1EOMfrequencylabel.TabIndex = 4;
            this.D1EOMfrequencylabel.Text = "RF Frequency";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(265, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(36, 17);
            this.label13.TabIndex = 3;
            this.label13.Text = "MHz";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(521, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(17, 17);
            this.label12.TabIndex = 2;
            this.label12.Text = "V";
            // 
            // offsetLockControlBox
            // 
            this.offsetLockControlBox.Controls.Add(this.offsetlockvcoUpDown);
            this.offsetLockControlBox.Controls.Add(this.offsetLockLabel1);
            this.offsetLockControlBox.Controls.Add(this.offsetLockLabel0);
            this.offsetLockControlBox.Location = new System.Drawing.Point(5, 482);
            this.offsetLockControlBox.Name = "offsetLockControlBox";
            this.offsetLockControlBox.Size = new System.Drawing.Size(539, 43);
            this.offsetLockControlBox.TabIndex = 26;
            this.offsetLockControlBox.TabStop = false;
            this.offsetLockControlBox.Text = "Offset lock";
            // 
            // offsetlockvcoUpDown
            // 
            this.offsetlockvcoUpDown.DecimalPlaces = 3;
            this.offsetlockvcoUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.offsetlockvcoUpDown.Location = new System.Drawing.Point(158, 18);
            this.offsetlockvcoUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.offsetlockvcoUpDown.Name = "offsetlockvcoUpDown";
            this.offsetlockvcoUpDown.Size = new System.Drawing.Size(87, 22);
            this.offsetlockvcoUpDown.TabIndex = 3;
            this.offsetlockvcoUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.offsetlockvcoUpDown.Click += new System.EventHandler(this.offsetlockvcoUpDown_Click);
            // 
            // offsetLockLabel1
            // 
            this.offsetLockLabel1.AutoSize = true;
            this.offsetLockLabel1.Location = new System.Drawing.Point(268, 20);
            this.offsetLockLabel1.Name = "offsetLockLabel1";
            this.offsetLockLabel1.Size = new System.Drawing.Size(17, 17);
            this.offsetLockLabel1.TabIndex = 2;
            this.offsetLockLabel1.Text = "V";
            // 
            // offsetLockLabel0
            // 
            this.offsetLockLabel0.AutoSize = true;
            this.offsetLockLabel0.Location = new System.Drawing.Point(56, 18);
            this.offsetLockLabel0.Name = "offsetLockLabel0";
            this.offsetLockLabel0.Size = new System.Drawing.Size(104, 17);
            this.offsetLockLabel0.TabIndex = 0;
            this.offsetLockLabel0.Text = "VCO frequency";
            // 
            // ovenShutterControlBox
            // 
            this.ovenShutterControlBox.Controls.Add(this.ovenShutterCheckBox);
            this.ovenShutterControlBox.Location = new System.Drawing.Point(557, 128);
            this.ovenShutterControlBox.Name = "ovenShutterControlBox";
            this.ovenShutterControlBox.Size = new System.Drawing.Size(126, 54);
            this.ovenShutterControlBox.TabIndex = 25;
            this.ovenShutterControlBox.TabStop = false;
            this.ovenShutterControlBox.Text = "Oven shutter";
            // 
            // ovenShutterCheckBox
            // 
            this.ovenShutterCheckBox.AutoSize = true;
            this.ovenShutterCheckBox.Location = new System.Drawing.Point(28, 27);
            this.ovenShutterCheckBox.Name = "ovenShutterCheckBox";
            this.ovenShutterCheckBox.Size = new System.Drawing.Size(65, 21);
            this.ovenShutterCheckBox.TabIndex = 0;
            this.ovenShutterCheckBox.Text = "Open";
            this.ovenShutterCheckBox.UseVisualStyleBackColor = true;
            this.ovenShutterCheckBox.Click += new System.EventHandler(this.ovenShutterCheckBox_Click);
            // 
            // ProbeControlBox
            // 
            this.ProbeControlBox.Controls.Add(this.probeShutterCheckBox);
            this.ProbeControlBox.Location = new System.Drawing.Point(557, 72);
            this.ProbeControlBox.Name = "ProbeControlBox";
            this.ProbeControlBox.Size = new System.Drawing.Size(126, 50);
            this.ProbeControlBox.TabIndex = 24;
            this.ProbeControlBox.TabStop = false;
            this.ProbeControlBox.Text = "Probe shutter";
            // 
            // probeShutterCheckBox
            // 
            this.probeShutterCheckBox.AutoSize = true;
            this.probeShutterCheckBox.Location = new System.Drawing.Point(28, 20);
            this.probeShutterCheckBox.Name = "probeShutterCheckBox";
            this.probeShutterCheckBox.Size = new System.Drawing.Size(18, 17);
            this.probeShutterCheckBox.TabIndex = 0;
            this.probeShutterCheckBox.UseVisualStyleBackColor = true;
            this.probeShutterCheckBox.Click += new System.EventHandler(this.probeShutterCheckBox_Click);
            // 
            // ShutterControlBox
            // 
            this.ShutterControlBox.Controls.Add(this.shutterCheckBox);
            this.ShutterControlBox.Location = new System.Drawing.Point(557, 10);
            this.ShutterControlBox.Name = "ShutterControlBox";
            this.ShutterControlBox.Size = new System.Drawing.Size(126, 56);
            this.ShutterControlBox.TabIndex = 22;
            this.ShutterControlBox.TabStop = false;
            this.ShutterControlBox.Text = "Zeeman shutter";
            // 
            // shutterCheckBox
            // 
            this.shutterCheckBox.AutoSize = true;
            this.shutterCheckBox.Location = new System.Drawing.Point(28, 22);
            this.shutterCheckBox.Name = "shutterCheckBox";
            this.shutterCheckBox.Size = new System.Drawing.Size(65, 21);
            this.shutterCheckBox.TabIndex = 21;
            this.shutterCheckBox.Text = "Open";
            this.shutterCheckBox.UseVisualStyleBackColor = true;
            this.shutterCheckBox.Click += new System.EventHandler(this.shutterCheckBox_Click);
            // 
            // aom3ControlBox
            // 
            this.aom3ControlBox.Controls.Add(this.aom3amplitudeUpDown);
            this.aom3ControlBox.Controls.Add(this.aom3frequencyUpDown);
            this.aom3ControlBox.Controls.Add(this.aom3Label3);
            this.aom3ControlBox.Controls.Add(this.aom3Label1);
            this.aom3ControlBox.Controls.Add(this.aom3CheckBox);
            this.aom3ControlBox.Controls.Add(this.aom3Label2);
            this.aom3ControlBox.Controls.Add(this.aom3Label0);
            this.aom3ControlBox.Location = new System.Drawing.Point(1, 199);
            this.aom3ControlBox.Name = "aom3ControlBox";
            this.aom3ControlBox.Size = new System.Drawing.Size(543, 58);
            this.aom3ControlBox.TabIndex = 19;
            this.aom3ControlBox.TabStop = false;
            this.aom3ControlBox.Text = "AOM 3 (D2 MOT)";
            // 
            // aom3amplitudeUpDown
            // 
            this.aom3amplitudeUpDown.DecimalPlaces = 2;
            this.aom3amplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom3amplitudeUpDown.Location = new System.Drawing.Point(422, 21);
            this.aom3amplitudeUpDown.Name = "aom3amplitudeUpDown";
            this.aom3amplitudeUpDown.Size = new System.Drawing.Size(92, 22);
            this.aom3amplitudeUpDown.TabIndex = 19;
            this.aom3amplitudeUpDown.Click += new System.EventHandler(this.aom3amplitudeUpDown_Click);
            // 
            // aom3frequencyUpDown
            // 
            this.aom3frequencyUpDown.DecimalPlaces = 2;
            this.aom3frequencyUpDown.Location = new System.Drawing.Point(163, 21);
            this.aom3frequencyUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.aom3frequencyUpDown.Name = "aom3frequencyUpDown";
            this.aom3frequencyUpDown.Size = new System.Drawing.Size(83, 22);
            this.aom3frequencyUpDown.TabIndex = 18;
            this.aom3frequencyUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.aom3frequencyUpDown.Click += new System.EventHandler(this.aom3frequencyUpDown_Click);
            // 
            // aom3Label3
            // 
            this.aom3Label3.AutoSize = true;
            this.aom3Label3.Location = new System.Drawing.Point(522, 21);
            this.aom3Label3.Name = "aom3Label3";
            this.aom3Label3.Size = new System.Drawing.Size(17, 17);
            this.aom3Label3.TabIndex = 17;
            this.aom3Label3.Text = "V";
            // 
            // aom3Label1
            // 
            this.aom3Label1.AutoSize = true;
            this.aom3Label1.Location = new System.Drawing.Point(268, 20);
            this.aom3Label1.Name = "aom3Label1";
            this.aom3Label1.Size = new System.Drawing.Size(36, 17);
            this.aom3Label1.TabIndex = 16;
            this.aom3Label1.Text = "MHz";
            // 
            // aom3CheckBox
            // 
            this.aom3CheckBox.AutoSize = true;
            this.aom3CheckBox.Location = new System.Drawing.Point(33, 21);
            this.aom3CheckBox.Name = "aom3CheckBox";
            this.aom3CheckBox.Size = new System.Drawing.Size(18, 17);
            this.aom3CheckBox.TabIndex = 10;
            this.aom3CheckBox.UseVisualStyleBackColor = true;
            this.aom3CheckBox.Click += new System.EventHandler(this.aom3CheckBox_Click);
            // 
            // aom3Label2
            // 
            this.aom3Label2.AutoSize = true;
            this.aom3Label2.Location = new System.Drawing.Point(323, 20);
            this.aom3Label2.Name = "aom3Label2";
            this.aom3Label2.Size = new System.Drawing.Size(87, 17);
            this.aom3Label2.TabIndex = 7;
            this.aom3Label2.Text = "VCA Voltage";
            // 
            // aom3Label0
            // 
            this.aom3Label0.AutoSize = true;
            this.aom3Label0.Location = new System.Drawing.Point(56, 20);
            this.aom3Label0.Name = "aom3Label0";
            this.aom3Label0.Size = new System.Drawing.Size(97, 17);
            this.aom3Label0.TabIndex = 6;
            this.aom3Label0.Text = "RF Frequency";
            // 
            // aom2ControlBox
            // 
            this.aom2ControlBox.Controls.Add(this.aom2amplitudeUpDown);
            this.aom2ControlBox.Controls.Add(this.aom2frequencyUpDown);
            this.aom2ControlBox.Controls.Add(this.aom2Label3);
            this.aom2ControlBox.Controls.Add(this.aom2Label1);
            this.aom2ControlBox.Controls.Add(this.aom2CheckBox);
            this.aom2ControlBox.Controls.Add(this.aom2Label2);
            this.aom2ControlBox.Controls.Add(this.aom2Label0);
            this.aom2ControlBox.Location = new System.Drawing.Point(0, 136);
            this.aom2ControlBox.Name = "aom2ControlBox";
            this.aom2ControlBox.Size = new System.Drawing.Size(546, 57);
            this.aom2ControlBox.TabIndex = 20;
            this.aom2ControlBox.TabStop = false;
            this.aom2ControlBox.Text = "AOM 2 (D1 Mollasses)";
            // 
            // aom2amplitudeUpDown
            // 
            this.aom2amplitudeUpDown.DecimalPlaces = 2;
            this.aom2amplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom2amplitudeUpDown.Location = new System.Drawing.Point(423, 19);
            this.aom2amplitudeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.aom2amplitudeUpDown.Name = "aom2amplitudeUpDown";
            this.aom2amplitudeUpDown.Size = new System.Drawing.Size(91, 22);
            this.aom2amplitudeUpDown.TabIndex = 19;
            this.aom2amplitudeUpDown.Click += new System.EventHandler(this.aom2amplitudeUpDown_Click);
            // 
            // aom2frequencyUpDown
            // 
            this.aom2frequencyUpDown.DecimalPlaces = 2;
            this.aom2frequencyUpDown.Location = new System.Drawing.Point(163, 21);
            this.aom2frequencyUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.aom2frequencyUpDown.Name = "aom2frequencyUpDown";
            this.aom2frequencyUpDown.Size = new System.Drawing.Size(87, 22);
            this.aom2frequencyUpDown.TabIndex = 18;
            this.aom2frequencyUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.aom2frequencyUpDown.Click += new System.EventHandler(this.aom2frequencyUpDown_Click);
            // 
            // aom2Label3
            // 
            this.aom2Label3.AutoSize = true;
            this.aom2Label3.Location = new System.Drawing.Point(522, 21);
            this.aom2Label3.Name = "aom2Label3";
            this.aom2Label3.Size = new System.Drawing.Size(17, 17);
            this.aom2Label3.TabIndex = 17;
            this.aom2Label3.Text = "V";
            // 
            // aom2Label1
            // 
            this.aom2Label1.AutoSize = true;
            this.aom2Label1.Location = new System.Drawing.Point(268, 21);
            this.aom2Label1.Name = "aom2Label1";
            this.aom2Label1.Size = new System.Drawing.Size(36, 17);
            this.aom2Label1.TabIndex = 16;
            this.aom2Label1.Text = "MHz";
            // 
            // aom2CheckBox
            // 
            this.aom2CheckBox.AutoSize = true;
            this.aom2CheckBox.Location = new System.Drawing.Point(33, 21);
            this.aom2CheckBox.Name = "aom2CheckBox";
            this.aom2CheckBox.Size = new System.Drawing.Size(18, 17);
            this.aom2CheckBox.TabIndex = 10;
            this.aom2CheckBox.UseVisualStyleBackColor = true;
            this.aom2CheckBox.Click += new System.EventHandler(this.aom2CheckBox_Click);
            // 
            // aom2Label2
            // 
            this.aom2Label2.AutoSize = true;
            this.aom2Label2.Location = new System.Drawing.Point(323, 21);
            this.aom2Label2.Name = "aom2Label2";
            this.aom2Label2.Size = new System.Drawing.Size(87, 17);
            this.aom2Label2.TabIndex = 7;
            this.aom2Label2.Text = "VCA Voltage";
            // 
            // aom2Label0
            // 
            this.aom2Label0.AutoSize = true;
            this.aom2Label0.Location = new System.Drawing.Point(56, 20);
            this.aom2Label0.Name = "aom2Label0";
            this.aom2Label0.Size = new System.Drawing.Size(97, 17);
            this.aom2Label0.TabIndex = 6;
            this.aom2Label0.Text = "RF Frequency";
            // 
            // aom1ControlBox
            // 
            this.aom1ControlBox.Controls.Add(this.aom1amplitudeUpDown);
            this.aom1ControlBox.Controls.Add(this.aom1frequencyUpDown);
            this.aom1ControlBox.Controls.Add(this.aom1Label3);
            this.aom1ControlBox.Controls.Add(this.aom1Label1);
            this.aom1ControlBox.Controls.Add(this.aom1CheckBox);
            this.aom1ControlBox.Controls.Add(this.aom1Label2);
            this.aom1ControlBox.Controls.Add(this.aom1Label0);
            this.aom1ControlBox.Location = new System.Drawing.Point(3, 72);
            this.aom1ControlBox.Name = "aom1ControlBox";
            this.aom1ControlBox.Size = new System.Drawing.Size(541, 58);
            this.aom1ControlBox.TabIndex = 19;
            this.aom1ControlBox.TabStop = false;
            this.aom1ControlBox.Text = "AOM 1 (D2 Imaging)";
            // 
            // aom1amplitudeUpDown
            // 
            this.aom1amplitudeUpDown.DecimalPlaces = 2;
            this.aom1amplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom1amplitudeUpDown.Location = new System.Drawing.Point(421, 18);
            this.aom1amplitudeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.aom1amplitudeUpDown.Name = "aom1amplitudeUpDown";
            this.aom1amplitudeUpDown.Size = new System.Drawing.Size(88, 22);
            this.aom1amplitudeUpDown.TabIndex = 19;
            this.aom1amplitudeUpDown.Click += new System.EventHandler(this.aom1amplitudeUpDown_Click);
            // 
            // aom1frequencyUpDown
            // 
            this.aom1frequencyUpDown.DecimalPlaces = 2;
            this.aom1frequencyUpDown.Location = new System.Drawing.Point(159, 16);
            this.aom1frequencyUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.aom1frequencyUpDown.Name = "aom1frequencyUpDown";
            this.aom1frequencyUpDown.Size = new System.Drawing.Size(88, 22);
            this.aom1frequencyUpDown.TabIndex = 18;
            this.aom1frequencyUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.aom1frequencyUpDown.Click += new System.EventHandler(this.aom1frequencyUpDown_Click);
            // 
            // aom1Label3
            // 
            this.aom1Label3.AutoSize = true;
            this.aom1Label3.Location = new System.Drawing.Point(522, 21);
            this.aom1Label3.Name = "aom1Label3";
            this.aom1Label3.Size = new System.Drawing.Size(17, 17);
            this.aom1Label3.TabIndex = 17;
            this.aom1Label3.Text = "V";
            // 
            // aom1Label1
            // 
            this.aom1Label1.AutoSize = true;
            this.aom1Label1.Location = new System.Drawing.Point(268, 21);
            this.aom1Label1.Name = "aom1Label1";
            this.aom1Label1.Size = new System.Drawing.Size(36, 17);
            this.aom1Label1.TabIndex = 16;
            this.aom1Label1.Text = "MHz";
            // 
            // aom1CheckBox
            // 
            this.aom1CheckBox.AutoSize = true;
            this.aom1CheckBox.Location = new System.Drawing.Point(33, 21);
            this.aom1CheckBox.Name = "aom1CheckBox";
            this.aom1CheckBox.Size = new System.Drawing.Size(18, 17);
            this.aom1CheckBox.TabIndex = 10;
            this.aom1CheckBox.UseVisualStyleBackColor = true;
            this.aom1CheckBox.Click += new System.EventHandler(this.aom1CheckBox_Click);
            // 
            // aom1Label2
            // 
            this.aom1Label2.AutoSize = true;
            this.aom1Label2.Location = new System.Drawing.Point(323, 20);
            this.aom1Label2.Name = "aom1Label2";
            this.aom1Label2.Size = new System.Drawing.Size(87, 17);
            this.aom1Label2.TabIndex = 7;
            this.aom1Label2.Text = "VCA Voltage";
            // 
            // aom1Label0
            // 
            this.aom1Label0.AutoSize = true;
            this.aom1Label0.Location = new System.Drawing.Point(56, 21);
            this.aom1Label0.Name = "aom1Label0";
            this.aom1Label0.Size = new System.Drawing.Size(97, 17);
            this.aom1Label0.TabIndex = 6;
            this.aom1Label0.Text = "RF Frequency";
            // 
            // aom0ControlBox
            // 
            this.aom0ControlBox.Controls.Add(this.aom0amplitudeUpDown);
            this.aom0ControlBox.Controls.Add(this.aom0frequencyUpDown);
            this.aom0ControlBox.Controls.Add(this.aom0Label3);
            this.aom0ControlBox.Controls.Add(this.aom0Label1);
            this.aom0ControlBox.Controls.Add(this.aom0CheckBox);
            this.aom0ControlBox.Controls.Add(this.aom0Label2);
            this.aom0ControlBox.Controls.Add(this.aom0Label0);
            this.aom0ControlBox.Location = new System.Drawing.Point(5, 10);
            this.aom0ControlBox.Name = "aom0ControlBox";
            this.aom0ControlBox.Size = new System.Drawing.Size(541, 56);
            this.aom0ControlBox.TabIndex = 12;
            this.aom0ControlBox.TabStop = false;
            this.aom0ControlBox.Text = "AOM 0 (lock)";
            // 
            // aom0amplitudeUpDown
            // 
            this.aom0amplitudeUpDown.DecimalPlaces = 2;
            this.aom0amplitudeUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.aom0amplitudeUpDown.Location = new System.Drawing.Point(421, 21);
            this.aom0amplitudeUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.aom0amplitudeUpDown.Name = "aom0amplitudeUpDown";
            this.aom0amplitudeUpDown.Size = new System.Drawing.Size(88, 22);
            this.aom0amplitudeUpDown.TabIndex = 32;
            this.aom0amplitudeUpDown.Click += new System.EventHandler(this.aom0amplitudeUpDown_Click);
            // 
            // aom0frequencyUpDown
            // 
            this.aom0frequencyUpDown.DecimalPlaces = 2;
            this.aom0frequencyUpDown.Location = new System.Drawing.Point(159, 18);
            this.aom0frequencyUpDown.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.aom0frequencyUpDown.Name = "aom0frequencyUpDown";
            this.aom0frequencyUpDown.Size = new System.Drawing.Size(90, 22);
            this.aom0frequencyUpDown.TabIndex = 31;
            this.aom0frequencyUpDown.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.aom0frequencyUpDown.Click += new System.EventHandler(this.aom0frequencyUpDown_Click);
            // 
            // aom0Label3
            // 
            this.aom0Label3.AutoSize = true;
            this.aom0Label3.Location = new System.Drawing.Point(522, 21);
            this.aom0Label3.Name = "aom0Label3";
            this.aom0Label3.Size = new System.Drawing.Size(17, 17);
            this.aom0Label3.TabIndex = 17;
            this.aom0Label3.Text = "V";
            // 
            // aom0Label1
            // 
            this.aom0Label1.AutoSize = true;
            this.aom0Label1.Location = new System.Drawing.Point(268, 21);
            this.aom0Label1.Name = "aom0Label1";
            this.aom0Label1.Size = new System.Drawing.Size(36, 17);
            this.aom0Label1.TabIndex = 16;
            this.aom0Label1.Text = "MHz";
            // 
            // aom0CheckBox
            // 
            this.aom0CheckBox.AutoSize = true;
            this.aom0CheckBox.Location = new System.Drawing.Point(33, 21);
            this.aom0CheckBox.Name = "aom0CheckBox";
            this.aom0CheckBox.Size = new System.Drawing.Size(18, 17);
            this.aom0CheckBox.TabIndex = 10;
            this.aom0CheckBox.UseVisualStyleBackColor = true;
            this.aom0CheckBox.Click += new System.EventHandler(this.aom0CheckBox_Click);
            // 
            // aom0Label2
            // 
            this.aom0Label2.AutoSize = true;
            this.aom0Label2.Location = new System.Drawing.Point(323, 20);
            this.aom0Label2.Name = "aom0Label2";
            this.aom0Label2.Size = new System.Drawing.Size(87, 17);
            this.aom0Label2.TabIndex = 7;
            this.aom0Label2.Text = "VCA Voltage";
            // 
            // aom0Label0
            // 
            this.aom0Label0.AutoSize = true;
            this.aom0Label0.Location = new System.Drawing.Point(56, 20);
            this.aom0Label0.Name = "aom0Label0";
            this.aom0Label0.Size = new System.Drawing.Size(97, 17);
            this.aom0Label0.TabIndex = 6;
            this.aom0Label0.Text = "RF Frequency";
            // 
            // tabCoils
            // 
            this.tabCoils.Controls.Add(this.groupBox5);
            this.tabCoils.Controls.Add(this.ShimCoilGroupBox);
            this.tabCoils.Controls.Add(this.coil0GroupBox);
            this.tabCoils.Location = new System.Drawing.Point(4, 25);
            this.tabCoils.Name = "tabCoils";
            this.tabCoils.Size = new System.Drawing.Size(824, 531);
            this.tabCoils.TabIndex = 2;
            this.tabCoils.Text = "Magnetic Field Control";
            this.tabCoils.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label19);
            this.groupBox5.Controls.Add(this.label18);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.BottomTrappingCoilnumericUpDown);
            this.groupBox5.Controls.Add(this.TopTrappingCoilnumericUpDown);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Location = new System.Drawing.Point(5, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(577, 134);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Trapping Coils (in chamber)";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(241, 69);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(17, 17);
            this.label19.TabIndex = 5;
            this.label19.Text = "V";
            this.label19.Click += new System.EventHandler(this.label19_Click);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(241, 29);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(17, 17);
            this.label18.TabIndex = 4;
            this.label18.Text = "V";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 69);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(128, 17);
            this.label17.TabIndex = 3;
            this.label17.Text = "Bottom Coil current";
            // 
            // BottomTrappingCoilnumericUpDown
            // 
            this.BottomTrappingCoilnumericUpDown.DecimalPlaces = 3;
            this.BottomTrappingCoilnumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.BottomTrappingCoilnumericUpDown.Location = new System.Drawing.Point(149, 67);
            this.BottomTrappingCoilnumericUpDown.Name = "BottomTrappingCoilnumericUpDown";
            this.BottomTrappingCoilnumericUpDown.Size = new System.Drawing.Size(80, 22);
            this.BottomTrappingCoilnumericUpDown.TabIndex = 2;
            this.BottomTrappingCoilnumericUpDown.Click += new System.EventHandler(this.updateHardwareButton_Click);
            // 
            // TopTrappingCoilnumericUpDown
            // 
            this.TopTrappingCoilnumericUpDown.DecimalPlaces = 3;
            this.TopTrappingCoilnumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.TopTrappingCoilnumericUpDown.Location = new System.Drawing.Point(149, 27);
            this.TopTrappingCoilnumericUpDown.Name = "TopTrappingCoilnumericUpDown";
            this.TopTrappingCoilnumericUpDown.Size = new System.Drawing.Size(81, 22);
            this.TopTrappingCoilnumericUpDown.TabIndex = 1;
            this.TopTrappingCoilnumericUpDown.Click += new System.EventHandler(this.updateHardwareButton_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 29);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(109, 17);
            this.label16.TabIndex = 0;
            this.label16.Text = "Top Coil current";
            // 
            // ShimCoilGroupBox
            // 
            this.ShimCoilGroupBox.Controls.Add(this.ycoilcurrentlabel);
            this.ShimCoilGroupBox.Controls.Add(this.ycoilcurrentunitlabel);
            this.ShimCoilGroupBox.Controls.Add(this.ycoilcurrentUpDown);
            this.ShimCoilGroupBox.Controls.Add(this.Zshimlabel);
            this.ShimCoilGroupBox.Controls.Add(this.zshimunitlabel);
            this.ShimCoilGroupBox.Controls.Add(this.zcoilcurrentUpDown);
            this.ShimCoilGroupBox.Controls.Add(this.xcoilNumericUpDownLabel);
            this.ShimCoilGroupBox.Controls.Add(this.xcoilcurrentUpDown);
            this.ShimCoilGroupBox.Controls.Add(this.XCoilLabel1);
            this.ShimCoilGroupBox.Location = new System.Drawing.Point(3, 241);
            this.ShimCoilGroupBox.Name = "ShimCoilGroupBox";
            this.ShimCoilGroupBox.Size = new System.Drawing.Size(579, 213);
            this.ShimCoilGroupBox.TabIndex = 15;
            this.ShimCoilGroupBox.TabStop = false;
            this.ShimCoilGroupBox.Text = "Shim Coils";
            // 
            // ycoilcurrentlabel
            // 
            this.ycoilcurrentlabel.AutoSize = true;
            this.ycoilcurrentlabel.Location = new System.Drawing.Point(12, 72);
            this.ycoilcurrentlabel.Name = "ycoilcurrentlabel";
            this.ycoilcurrentlabel.Size = new System.Drawing.Size(91, 17);
            this.ycoilcurrentlabel.TabIndex = 22;
            this.ycoilcurrentlabel.Text = "Y coil current";
            // 
            // ycoilcurrentunitlabel
            // 
            this.ycoilcurrentunitlabel.AutoSize = true;
            this.ycoilcurrentunitlabel.Location = new System.Drawing.Point(243, 72);
            this.ycoilcurrentunitlabel.Name = "ycoilcurrentunitlabel";
            this.ycoilcurrentunitlabel.Size = new System.Drawing.Size(17, 17);
            this.ycoilcurrentunitlabel.TabIndex = 21;
            this.ycoilcurrentunitlabel.Text = "V";
            // 
            // ycoilcurrentUpDown
            // 
            this.ycoilcurrentUpDown.DecimalPlaces = 3;
            this.ycoilcurrentUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ycoilcurrentUpDown.Location = new System.Drawing.Point(151, 70);
            this.ycoilcurrentUpDown.Name = "ycoilcurrentUpDown";
            this.ycoilcurrentUpDown.Size = new System.Drawing.Size(86, 22);
            this.ycoilcurrentUpDown.TabIndex = 20;
            this.ycoilcurrentUpDown.Click += new System.EventHandler(this.updateHardwareButton_Click);
            // 
            // Zshimlabel
            // 
            this.Zshimlabel.AutoSize = true;
            this.Zshimlabel.Location = new System.Drawing.Point(10, 105);
            this.Zshimlabel.Name = "Zshimlabel";
            this.Zshimlabel.Size = new System.Drawing.Size(93, 17);
            this.Zshimlabel.TabIndex = 19;
            this.Zshimlabel.Text = "Z coil Current";
            this.Zshimlabel.Click += new System.EventHandler(this.label17_Click);
            // 
            // zshimunitlabel
            // 
            this.zshimunitlabel.AutoSize = true;
            this.zshimunitlabel.Location = new System.Drawing.Point(243, 105);
            this.zshimunitlabel.Name = "zshimunitlabel";
            this.zshimunitlabel.Size = new System.Drawing.Size(17, 17);
            this.zshimunitlabel.TabIndex = 18;
            this.zshimunitlabel.Text = "V";
            // 
            // zcoilcurrentUpDown
            // 
            this.zcoilcurrentUpDown.DecimalPlaces = 3;
            this.zcoilcurrentUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.zcoilcurrentUpDown.Location = new System.Drawing.Point(151, 103);
            this.zcoilcurrentUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.zcoilcurrentUpDown.Name = "zcoilcurrentUpDown";
            this.zcoilcurrentUpDown.Size = new System.Drawing.Size(86, 22);
            this.zcoilcurrentUpDown.TabIndex = 17;
            this.zcoilcurrentUpDown.Click += new System.EventHandler(this.updateHardwareButton_Click);
            // 
            // xcoilNumericUpDownLabel
            // 
            this.xcoilNumericUpDownLabel.AutoSize = true;
            this.xcoilNumericUpDownLabel.Location = new System.Drawing.Point(243, 33);
            this.xcoilNumericUpDownLabel.Name = "xcoilNumericUpDownLabel";
            this.xcoilNumericUpDownLabel.Size = new System.Drawing.Size(17, 17);
            this.xcoilNumericUpDownLabel.TabIndex = 16;
            this.xcoilNumericUpDownLabel.Text = "V";
            // 
            // xcoilcurrentUpDown
            // 
            this.xcoilcurrentUpDown.DecimalPlaces = 3;
            this.xcoilcurrentUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.xcoilcurrentUpDown.Location = new System.Drawing.Point(151, 33);
            this.xcoilcurrentUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.xcoilcurrentUpDown.Name = "xcoilcurrentUpDown";
            this.xcoilcurrentUpDown.Size = new System.Drawing.Size(84, 22);
            this.xcoilcurrentUpDown.TabIndex = 15;
            this.xcoilcurrentUpDown.Click += new System.EventHandler(this.xcoilcurrentUpDown_Click);
            // 
            // XCoilLabel1
            // 
            this.XCoilLabel1.AutoSize = true;
            this.XCoilLabel1.Location = new System.Drawing.Point(10, 33);
            this.XCoilLabel1.Name = "XCoilLabel1";
            this.XCoilLabel1.Size = new System.Drawing.Size(93, 17);
            this.XCoilLabel1.TabIndex = 14;
            this.XCoilLabel1.Text = "X Coil current";
            // 
            // coil0GroupBox
            // 
            this.coil0GroupBox.Controls.Add(this.CoilDescriptionLabel);
            this.coil0GroupBox.Controls.Add(this.BottomTransportCurrentUpDown);
            this.coil0GroupBox.Controls.Add(this.TopTransportCurrentUpDown);
            this.coil0GroupBox.Controls.Add(this.coil1Label1);
            this.coil0GroupBox.Controls.Add(this.coil0Label1);
            this.coil0GroupBox.Controls.Add(this.coil1Label0);
            this.coil0GroupBox.Controls.Add(this.coil0Label0);
            this.coil0GroupBox.Location = new System.Drawing.Point(3, 153);
            this.coil0GroupBox.Name = "coil0GroupBox";
            this.coil0GroupBox.Size = new System.Drawing.Size(579, 82);
            this.coil0GroupBox.TabIndex = 13;
            this.coil0GroupBox.TabStop = false;
            this.coil0GroupBox.Text = "Transport Coils";
            // 
            // CoilDescriptionLabel
            // 
            this.CoilDescriptionLabel.AutoSize = true;
            this.CoilDescriptionLabel.Location = new System.Drawing.Point(266, 27);
            this.CoilDescriptionLabel.Name = "CoilDescriptionLabel";
            this.CoilDescriptionLabel.Size = new System.Drawing.Size(0, 17);
            this.CoilDescriptionLabel.TabIndex = 20;
            this.CoilDescriptionLabel.Click += new System.EventHandler(this.CoilDescriptionLabel_Click);
            // 
            // BottomTransportCurrentUpDown
            // 
            this.BottomTransportCurrentUpDown.DecimalPlaces = 4;
            this.BottomTransportCurrentUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.BottomTransportCurrentUpDown.Location = new System.Drawing.Point(151, 45);
            this.BottomTransportCurrentUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.BottomTransportCurrentUpDown.Name = "BottomTransportCurrentUpDown";
            this.BottomTransportCurrentUpDown.Size = new System.Drawing.Size(86, 22);
            this.BottomTransportCurrentUpDown.TabIndex = 19;
            this.BottomTransportCurrentUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.BottomTransportCurrentUpDown.Click += new System.EventHandler(this.coil0currentUpDown_Click);
            // 
            // TopTransportCurrentUpDown
            // 
            this.TopTransportCurrentUpDown.DecimalPlaces = 4;
            this.TopTransportCurrentUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.TopTransportCurrentUpDown.Location = new System.Drawing.Point(151, 15);
            this.TopTransportCurrentUpDown.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.TopTransportCurrentUpDown.Name = "TopTransportCurrentUpDown";
            this.TopTransportCurrentUpDown.Size = new System.Drawing.Size(85, 22);
            this.TopTransportCurrentUpDown.TabIndex = 18;
            this.TopTransportCurrentUpDown.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.TopTransportCurrentUpDown.Click += new System.EventHandler(this.coil1currentUpDown_Click);
            // 
            // coil1Label1
            // 
            this.coil1Label1.AutoSize = true;
            this.coil1Label1.Location = new System.Drawing.Point(243, 17);
            this.coil1Label1.Name = "coil1Label1";
            this.coil1Label1.Size = new System.Drawing.Size(17, 17);
            this.coil1Label1.TabIndex = 17;
            this.coil1Label1.Text = "V";
            // 
            // coil0Label1
            // 
            this.coil0Label1.AutoSize = true;
            this.coil0Label1.Location = new System.Drawing.Point(243, 48);
            this.coil0Label1.Name = "coil0Label1";
            this.coil0Label1.Size = new System.Drawing.Size(17, 17);
            this.coil0Label1.TabIndex = 17;
            this.coil0Label1.Text = "V";
            // 
            // coil1Label0
            // 
            this.coil1Label0.AutoSize = true;
            this.coil1Label0.Location = new System.Drawing.Point(10, 21);
            this.coil1Label0.Name = "coil1Label0";
            this.coil1Label0.Size = new System.Drawing.Size(107, 17);
            this.coil1Label0.TabIndex = 7;
            this.coil1Label0.Text = "Top coil current";
            // 
            // coil0Label0
            // 
            this.coil0Label0.AutoSize = true;
            this.coil0Label0.Location = new System.Drawing.Point(10, 47);
            this.coil0Label0.Name = "coil0Label0";
            this.coil0Label0.Size = new System.Drawing.Size(126, 17);
            this.coil0Label0.TabIndex = 7;
            this.coil0Label0.Text = "Bottom coil current";
            // 
            // tabTranslationStage
            // 
            this.tabTranslationStage.Controls.Add(this.groupBox4);
            this.tabTranslationStage.Controls.Add(this.RS232GroupBox);
            this.tabTranslationStage.Controls.Add(this.groupBox3);
            this.tabTranslationStage.Controls.Add(this.groupBox2);
            this.tabTranslationStage.Controls.Add(this.groupBox1);
            this.tabTranslationStage.Controls.Add(this.initParamsBox);
            this.tabTranslationStage.Location = new System.Drawing.Point(4, 25);
            this.tabTranslationStage.Name = "tabTranslationStage";
            this.tabTranslationStage.Size = new System.Drawing.Size(824, 531);
            this.tabTranslationStage.TabIndex = 3;
            this.tabTranslationStage.Text = "Translation Stage Control";
            this.tabTranslationStage.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.AutoTriggerCheckBox);
            this.groupBox4.Location = new System.Drawing.Point(570, 7);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(118, 46);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Triggering";
            // 
            // AutoTriggerCheckBox
            // 
            this.AutoTriggerCheckBox.AutoSize = true;
            this.AutoTriggerCheckBox.Checked = true;
            this.AutoTriggerCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoTriggerCheckBox.Location = new System.Drawing.Point(12, 22);
            this.AutoTriggerCheckBox.Name = "AutoTriggerCheckBox";
            this.AutoTriggerCheckBox.Size = new System.Drawing.Size(105, 21);
            this.AutoTriggerCheckBox.TabIndex = 12;
            this.AutoTriggerCheckBox.Text = "AutoTrigger";
            this.AutoTriggerCheckBox.UseVisualStyleBackColor = true;
            this.AutoTriggerCheckBox.CheckedChanged += new System.EventHandler(this.AutoTriggerCheckBox_CheckedChanged);
            // 
            // RS232GroupBox
            // 
            this.RS232GroupBox.Controls.Add(this.TSConnectButton);
            this.RS232GroupBox.Controls.Add(this.disposeButton);
            this.RS232GroupBox.Location = new System.Drawing.Point(5, 3);
            this.RS232GroupBox.Name = "RS232GroupBox";
            this.RS232GroupBox.Size = new System.Drawing.Size(273, 55);
            this.RS232GroupBox.TabIndex = 16;
            this.RS232GroupBox.TabStop = false;
            this.RS232GroupBox.Text = "RS232";
            // 
            // TSConnectButton
            // 
            this.TSConnectButton.Location = new System.Drawing.Point(17, 20);
            this.TSConnectButton.Name = "TSConnectButton";
            this.TSConnectButton.Size = new System.Drawing.Size(100, 27);
            this.TSConnectButton.TabIndex = 16;
            this.TSConnectButton.Text = "Connect";
            this.TSConnectButton.UseVisualStyleBackColor = true;
            this.TSConnectButton.Click += new System.EventHandler(this.TSConnectButton_Click);
            // 
            // disposeButton
            // 
            this.disposeButton.Location = new System.Drawing.Point(123, 20);
            this.disposeButton.Name = "disposeButton";
            this.disposeButton.Size = new System.Drawing.Size(100, 27);
            this.disposeButton.TabIndex = 4;
            this.disposeButton.Text = "Disconnect";
            this.disposeButton.UseVisualStyleBackColor = true;
            this.disposeButton.Click += new System.EventHandler(this.disposeButton_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.UserFaultButton);
            this.groupBox3.Controls.Add(this.DriveFaultButton);
            this.groupBox3.Controls.Add(this.TSListAllButton);
            this.groupBox3.Controls.Add(this.checkStatusButton);
            this.groupBox3.Controls.Add(this.TSClearButton);
            this.groupBox3.Location = new System.Drawing.Point(284, 183);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(312, 119);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Console";
            // 
            // TSListAllButton
            // 
            this.TSListAllButton.Location = new System.Drawing.Point(118, 21);
            this.TSListAllButton.Name = "TSListAllButton";
            this.TSListAllButton.Size = new System.Drawing.Size(83, 27);
            this.TSListAllButton.TabIndex = 17;
            this.TSListAllButton.Text = "List all";
            this.TSListAllButton.UseVisualStyleBackColor = true;
            this.TSListAllButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkStatusButton
            // 
            this.checkStatusButton.Location = new System.Drawing.Point(9, 21);
            this.checkStatusButton.Name = "checkStatusButton";
            this.checkStatusButton.Size = new System.Drawing.Size(103, 27);
            this.checkStatusButton.TabIndex = 18;
            this.checkStatusButton.Text = "Check status";
            this.checkStatusButton.UseVisualStyleBackColor = true;
            this.checkStatusButton.Click += new System.EventHandler(this.checkStatusButton_Click);
            // 
            // TSClearButton
            // 
            this.TSClearButton.Location = new System.Drawing.Point(207, 21);
            this.TSClearButton.Name = "TSClearButton";
            this.TSClearButton.Size = new System.Drawing.Size(83, 27);
            this.TSClearButton.TabIndex = 8;
            this.TSClearButton.Text = "Clear";
            this.TSClearButton.UseVisualStyleBackColor = true;
            this.TSClearButton.Click += new System.EventHandler(this.TSClearButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.TSHomeButton);
            this.groupBox2.Controls.Add(this.TSGoButton);
            this.groupBox2.Controls.Add(this.TSReturnButton);
            this.groupBox2.Location = new System.Drawing.Point(283, 126);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 54);
            this.groupBox2.TabIndex = 11;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Motion";
            // 
            // TSHomeButton
            // 
            this.TSHomeButton.Location = new System.Drawing.Point(186, 19);
            this.TSHomeButton.Name = "TSHomeButton";
            this.TSHomeButton.Size = new System.Drawing.Size(83, 27);
            this.TSHomeButton.TabIndex = 17;
            this.TSHomeButton.Text = "Home";
            this.TSHomeButton.UseVisualStyleBackColor = true;
            this.TSHomeButton.Click += new System.EventHandler(this.TSHomeButton_Click);
            // 
            // TSGoButton
            // 
            this.TSGoButton.Location = new System.Drawing.Point(9, 19);
            this.TSGoButton.Name = "TSGoButton";
            this.TSGoButton.Size = new System.Drawing.Size(83, 27);
            this.TSGoButton.TabIndex = 2;
            this.TSGoButton.Text = "Go";
            this.TSGoButton.UseVisualStyleBackColor = true;
            this.TSGoButton.Click += new System.EventHandler(this.TSGoButton_Click);
            // 
            // TSReturnButton
            // 
            this.TSReturnButton.Location = new System.Drawing.Point(98, 19);
            this.TSReturnButton.Name = "TSReturnButton";
            this.TSReturnButton.Size = new System.Drawing.Size(83, 27);
            this.TSReturnButton.TabIndex = 6;
            this.TSReturnButton.Text = "Return";
            this.TSReturnButton.UseVisualStyleBackColor = true;
            this.TSReturnButton.Click += new System.EventHandler(this.TSReturnButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.TSRestartButton);
            this.groupBox1.Controls.Add(this.TSOnButton);
            this.groupBox1.Controls.Add(this.TSOffButton);
            this.groupBox1.Location = new System.Drawing.Point(284, 7);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(277, 56);
            this.groupBox1.TabIndex = 10;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Motor Power";
            // 
            // TSRestartButton
            // 
            this.TSRestartButton.Location = new System.Drawing.Point(186, 19);
            this.TSRestartButton.Name = "TSRestartButton";
            this.TSRestartButton.Size = new System.Drawing.Size(83, 27);
            this.TSRestartButton.TabIndex = 7;
            this.TSRestartButton.Text = "Restart";
            this.TSRestartButton.UseVisualStyleBackColor = true;
            this.TSRestartButton.Click += new System.EventHandler(this.TSRestartButton_Click);
            // 
            // TSOnButton
            // 
            this.TSOnButton.Location = new System.Drawing.Point(9, 20);
            this.TSOnButton.Name = "TSOnButton";
            this.TSOnButton.Size = new System.Drawing.Size(83, 26);
            this.TSOnButton.TabIndex = 1;
            this.TSOnButton.Text = "On";
            this.TSOnButton.UseVisualStyleBackColor = true;
            this.TSOnButton.Click += new System.EventHandler(this.TSOnButton_Click);
            // 
            // TSOffButton
            // 
            this.TSOffButton.Location = new System.Drawing.Point(98, 19);
            this.TSOffButton.Name = "TSOffButton";
            this.TSOffButton.Size = new System.Drawing.Size(83, 27);
            this.TSOffButton.TabIndex = 3;
            this.TSOffButton.Text = "Off";
            this.TSOffButton.UseVisualStyleBackColor = true;
            this.TSOffButton.Click += new System.EventHandler(this.TSOffButton_Click);
            // 
            // initParamsBox
            // 
            this.initParamsBox.Controls.Add(this.TSInitButton);
            this.initParamsBox.Controls.Add(this.label9);
            this.initParamsBox.Controls.Add(this.label8);
            this.initParamsBox.Controls.Add(this.label7);
            this.initParamsBox.Controls.Add(this.label6);
            this.initParamsBox.Controls.Add(this.label5);
            this.initParamsBox.Controls.Add(this.label4);
            this.initParamsBox.Controls.Add(this.label3);
            this.initParamsBox.Controls.Add(this.label2);
            this.initParamsBox.Controls.Add(this.TSVelTextBox);
            this.initParamsBox.Controls.Add(this.TSStepsTextBox);
            this.initParamsBox.Controls.Add(this.TSDecTextBox);
            this.initParamsBox.Controls.Add(this.TSAccTextBox);
            this.initParamsBox.Location = new System.Drawing.Point(5, 63);
            this.initParamsBox.Name = "initParamsBox";
            this.initParamsBox.Size = new System.Drawing.Size(273, 153);
            this.initParamsBox.TabIndex = 9;
            this.initParamsBox.TabStop = false;
            this.initParamsBox.Text = "Initialize parameters";
            // 
            // TSInitButton
            // 
            this.TSInitButton.Location = new System.Drawing.Point(123, 120);
            this.TSInitButton.Name = "TSInitButton";
            this.TSInitButton.Size = new System.Drawing.Size(100, 27);
            this.TSInitButton.TabIndex = 0;
            this.TSInitButton.Text = "Initialize";
            this.TSInitButton.UseVisualStyleBackColor = true;
            this.TSInitButton.Click += new System.EventHandler(this.TSInitButton_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(223, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "mm/s";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(224, 72);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 17);
            this.label8.TabIndex = 14;
            this.label8.Text = "mm";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(224, 46);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "mm/s2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(224, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 17);
            this.label6.TabIndex = 12;
            this.label6.Text = "mm/s2";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Velocity";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(118, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Distance to travel";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Deceleration";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Acceleration";
            // 
            // TSVelTextBox
            // 
            this.TSVelTextBox.Location = new System.Drawing.Point(123, 95);
            this.TSVelTextBox.Name = "TSVelTextBox";
            this.TSVelTextBox.Size = new System.Drawing.Size(100, 22);
            this.TSVelTextBox.TabIndex = 4;
            this.TSVelTextBox.Text = "50";
            // 
            // TSStepsTextBox
            // 
            this.TSStepsTextBox.Location = new System.Drawing.Point(123, 69);
            this.TSStepsTextBox.Name = "TSStepsTextBox";
            this.TSStepsTextBox.Size = new System.Drawing.Size(100, 22);
            this.TSStepsTextBox.TabIndex = 3;
            this.TSStepsTextBox.Text = "5";
            // 
            // TSDecTextBox
            // 
            this.TSDecTextBox.Location = new System.Drawing.Point(123, 43);
            this.TSDecTextBox.Name = "TSDecTextBox";
            this.TSDecTextBox.Size = new System.Drawing.Size(100, 22);
            this.TSDecTextBox.TabIndex = 2;
            this.TSDecTextBox.Text = "50";
            // 
            // TSAccTextBox
            // 
            this.TSAccTextBox.Location = new System.Drawing.Point(123, 17);
            this.TSAccTextBox.Name = "TSAccTextBox";
            this.TSAccTextBox.Size = new System.Drawing.Size(100, 22);
            this.TSAccTextBox.TabIndex = 1;
            this.TSAccTextBox.Text = "50";
            // 
            // tabMicrowaveControl
            // 
            this.tabMicrowaveControl.Controls.Add(this.groupBox6);
            this.tabMicrowaveControl.Location = new System.Drawing.Point(4, 25);
            this.tabMicrowaveControl.Name = "tabMicrowaveControl";
            this.tabMicrowaveControl.Padding = new System.Windows.Forms.Padding(3);
            this.tabMicrowaveControl.Size = new System.Drawing.Size(824, 531);
            this.tabMicrowaveControl.TabIndex = 4;
            this.tabMicrowaveControl.Text = "Microwave control";
            this.tabMicrowaveControl.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.richTextBox1);
            this.groupBox6.Controls.Add(this.MWgenAMunits);
            this.groupBox6.Controls.Add(this.MWgenAMlabel);
            this.groupBox6.Controls.Add(this.MWGeneratorAMnumericUpDown);
            this.groupBox6.Location = new System.Drawing.Point(18, 24);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(796, 157);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Microwave Control";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(293, 32);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(476, 79);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // MWgenAMunits
            // 
            this.MWgenAMunits.AutoSize = true;
            this.MWgenAMunits.Location = new System.Drawing.Point(205, 49);
            this.MWgenAMunits.Name = "MWgenAMunits";
            this.MWgenAMunits.Size = new System.Drawing.Size(36, 17);
            this.MWgenAMunits.TabIndex = 2;
            this.MWgenAMunits.Text = "dBm";
            // 
            // MWgenAMlabel
            // 
            this.MWgenAMlabel.AutoSize = true;
            this.MWgenAMlabel.Location = new System.Drawing.Point(6, 49);
            this.MWgenAMlabel.Name = "MWgenAMlabel";
            this.MWgenAMlabel.Size = new System.Drawing.Size(97, 17);
            this.MWgenAMlabel.TabIndex = 1;
            this.MWgenAMlabel.Text = "Generator AM";
            // 
            // MWGeneratorAMnumericUpDown
            // 
            this.MWGeneratorAMnumericUpDown.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.MWGeneratorAMnumericUpDown.Location = new System.Drawing.Point(109, 47);
            this.MWGeneratorAMnumericUpDown.Minimum = new decimal(new int[] {
            37,
            0,
            0,
            -2147483648});
            this.MWGeneratorAMnumericUpDown.Name = "MWGeneratorAMnumericUpDown";
            this.MWGeneratorAMnumericUpDown.Size = new System.Drawing.Size(90, 22);
            this.MWGeneratorAMnumericUpDown.TabIndex = 0;
            this.MWGeneratorAMnumericUpDown.Value = new decimal(new int[] {
            37,
            0,
            0,
            -2147483648});
            this.MWGeneratorAMnumericUpDown.Click += new System.EventHandler(this.MWGeneratorAMnumericUpDown_ValueChanged);
            this.MWGeneratorAMnumericUpDown.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MWGeneratorAMnumericUpDown_MouseClick);
            // 
            // consoleRichTextBox
            // 
            this.consoleRichTextBox.BackColor = System.Drawing.Color.Black;
            this.consoleRichTextBox.ForeColor = System.Drawing.Color.Lime;
            this.consoleRichTextBox.Location = new System.Drawing.Point(3, 633);
            this.consoleRichTextBox.Name = "consoleRichTextBox";
            this.consoleRichTextBox.ReadOnly = true;
            this.consoleRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.consoleRichTextBox.Size = new System.Drawing.Size(850, 173);
            this.consoleRichTextBox.TabIndex = 23;
            this.consoleRichTextBox.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.Location = new System.Drawing.Point(0, 0);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(104, 24);
            this.checkBox1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(0, 0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 22);
            this.textBox1.TabIndex = 0;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(0, 0);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 22);
            this.textBox2.TabIndex = 0;
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.windowsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(874, 28);
            this.menuStrip.TabIndex = 15;
            this.menuStrip.Text = "menuStrip";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadParametersToolStripMenuItem,
            this.saveParametersToolStripMenuItem,
            this.toolStripSeparator1,
            this.saveImageToolStripMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadParametersToolStripMenuItem
            // 
            this.loadParametersToolStripMenuItem.Name = "loadParametersToolStripMenuItem";
            this.loadParametersToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.loadParametersToolStripMenuItem.Text = "Load parameters";
            this.loadParametersToolStripMenuItem.Click += new System.EventHandler(this.loadParametersToolStripMenuItem_Click);
            // 
            // saveParametersToolStripMenuItem
            // 
            this.saveParametersToolStripMenuItem.Name = "saveParametersToolStripMenuItem";
            this.saveParametersToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.saveParametersToolStripMenuItem.Text = "Save parameters on UI";
            this.saveParametersToolStripMenuItem.Click += new System.EventHandler(this.saveParametersToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(230, 6);
            // 
            // saveImageToolStripMenuItem
            // 
            this.saveImageToolStripMenuItem.Name = "saveImageToolStripMenuItem";
            this.saveImageToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.saveImageToolStripMenuItem.Text = "Save image";
            this.saveImageToolStripMenuItem.Click += new System.EventHandler(this.saveImageToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(230, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(233, 26);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hardwareMonitorToolStripMenuItem,
            this.openImageViewerToolStripMenuItem,
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem,
            this.openLiveCameraAnalysisWindowToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(82, 24);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // hardwareMonitorToolStripMenuItem
            // 
            this.hardwareMonitorToolStripMenuItem.Name = "hardwareMonitorToolStripMenuItem";
            this.hardwareMonitorToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.hardwareMonitorToolStripMenuItem.Text = "Open new hardware monitor";
            this.hardwareMonitorToolStripMenuItem.Click += new System.EventHandler(this.hardwareMonitorToolStripMenuItem_Click);
            // 
            // openImageViewerToolStripMenuItem
            // 
            this.openImageViewerToolStripMenuItem.Name = "openImageViewerToolStripMenuItem";
            this.openImageViewerToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.openImageViewerToolStripMenuItem.Text = "Start Pike and open image viewer";
            this.openImageViewerToolStripMenuItem.Click += new System.EventHandler(this.openImageViewerToolStripMenuItem_Click);
            // 
            // startMarlinCameraAndOpenImageViewerToolStripMenuItem
            // 
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem.Name = "startMarlinCameraAndOpenImageViewerToolStripMenuItem";
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem.Text = "Start Marlin Camera and open image viewer";
            this.startMarlinCameraAndOpenImageViewerToolStripMenuItem.Click += new System.EventHandler(this.startMarlinCameraAndOpenImageViewerToolStripMenuItem_Click);
            // 
            // openLiveCameraAnalysisWindowToolStripMenuItem
            // 
            this.openLiveCameraAnalysisWindowToolStripMenuItem.Name = "openLiveCameraAnalysisWindowToolStripMenuItem";
            this.openLiveCameraAnalysisWindowToolStripMenuItem.Size = new System.Drawing.Size(376, 26);
            this.openLiveCameraAnalysisWindowToolStripMenuItem.Text = "Open Live Camera Analysis Window";
            this.openLiveCameraAnalysisWindowToolStripMenuItem.Click += new System.EventHandler(this.openLiveCameraAnalysisWindowToolStripMenuItem_Click);
            // 
            // remoteControlLED
            // 
            this.remoteControlLED.Location = new System.Drawing.Point(822, 44);
            this.remoteControlLED.Name = "remoteControlLED";
            this.remoteControlLED.Size = new System.Drawing.Size(25, 24);
            this.remoteControlLED.TabIndex = 16;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(715, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 18;
            this.label1.Text = "Remote Control";
            // 
            // updateHardwareButton
            // 
            this.updateHardwareButton.Location = new System.Drawing.Point(563, 41);
            this.updateHardwareButton.Name = "updateHardwareButton";
            this.updateHardwareButton.Size = new System.Drawing.Size(127, 29);
            this.updateHardwareButton.TabIndex = 3;
            this.updateHardwareButton.Text = "Update hardware";
            this.updateHardwareButton.UseVisualStyleBackColor = true;
            this.updateHardwareButton.Click += new System.EventHandler(this.updateHardwareButton_Click);
            // 
            // AutoUpdateCheckBox
            // 
            this.AutoUpdateCheckBox.AutoSize = true;
            this.AutoUpdateCheckBox.Checked = true;
            this.AutoUpdateCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AutoUpdateCheckBox.Location = new System.Drawing.Point(448, 43);
            this.AutoUpdateCheckBox.Name = "AutoUpdateCheckBox";
            this.AutoUpdateCheckBox.Size = new System.Drawing.Size(109, 21);
            this.AutoUpdateCheckBox.TabIndex = 31;
            this.AutoUpdateCheckBox.Text = "Auto Update";
            this.AutoUpdateCheckBox.UseVisualStyleBackColor = true;
            // 
            // DriveFaultButton
            // 
            this.DriveFaultButton.Location = new System.Drawing.Point(9, 52);
            this.DriveFaultButton.Name = "DriveFaultButton";
            this.DriveFaultButton.Size = new System.Drawing.Size(102, 52);
            this.DriveFaultButton.TabIndex = 19;
            this.DriveFaultButton.Text = "Check Drive Faults";
            this.DriveFaultButton.UseVisualStyleBackColor = true;
            this.DriveFaultButton.Click += new System.EventHandler(this.DriveFaultButton_Click);
            // 
            // UserFaultButton
            // 
            this.UserFaultButton.Location = new System.Drawing.Point(117, 52);
            this.UserFaultButton.Name = "UserFaultButton";
            this.UserFaultButton.Size = new System.Drawing.Size(102, 52);
            this.UserFaultButton.TabIndex = 20;
            this.UserFaultButton.Text = "Check User Faults";
            this.UserFaultButton.UseVisualStyleBackColor = true;
            this.UserFaultButton.Click += new System.EventHandler(this.UserFaultButton_Click);
            // 
            // ControlWindow
            // 
            this.ClientSize = new System.Drawing.Size(874, 807);
            this.Controls.Add(this.consoleRichTextBox);
            this.Controls.Add(this.AutoUpdateCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.updateHardwareButton);
            this.Controls.Add(this.remoteControlLED);
            this.Controls.Add(this.shcTabs);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "ControlWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sympathetic Hardware Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WindowClosing);
            this.Load += new System.EventHandler(this.WindowLoaded);
            this.shcTabs.ResumeLayout(false);
            this.tabCamera.ResumeLayout(false);
            this.tabLasers.ResumeLayout(false);
            this.aom4groupbox.ResumeLayout(false);
            this.aom4groupbox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom4amplitudenumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom4frequencynumericUpDown)).EndInit();
            this.D2EOMControlBox.ResumeLayout(false);
            this.D2EOMControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.d2eomamplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d2eomfrequencyUpDown)).EndInit();
            this.D1EOMControlBox.ResumeLayout(false);
            this.D1EOMControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.d1eomamplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.d1eomfrequencyUpDown)).EndInit();
            this.offsetLockControlBox.ResumeLayout(false);
            this.offsetLockControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.offsetlockvcoUpDown)).EndInit();
            this.ovenShutterControlBox.ResumeLayout(false);
            this.ovenShutterControlBox.PerformLayout();
            this.ProbeControlBox.ResumeLayout(false);
            this.ProbeControlBox.PerformLayout();
            this.ShutterControlBox.ResumeLayout(false);
            this.ShutterControlBox.PerformLayout();
            this.aom3ControlBox.ResumeLayout(false);
            this.aom3ControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom3amplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom3frequencyUpDown)).EndInit();
            this.aom2ControlBox.ResumeLayout(false);
            this.aom2ControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom2amplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom2frequencyUpDown)).EndInit();
            this.aom1ControlBox.ResumeLayout(false);
            this.aom1ControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom1amplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom1frequencyUpDown)).EndInit();
            this.aom0ControlBox.ResumeLayout(false);
            this.aom0ControlBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.aom0amplitudeUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.aom0frequencyUpDown)).EndInit();
            this.tabCoils.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomTrappingCoilnumericUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopTrappingCoilnumericUpDown)).EndInit();
            this.ShimCoilGroupBox.ResumeLayout(false);
            this.ShimCoilGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ycoilcurrentUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.zcoilcurrentUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xcoilcurrentUpDown)).EndInit();
            this.coil0GroupBox.ResumeLayout(false);
            this.coil0GroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BottomTransportCurrentUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TopTransportCurrentUpDown)).EndInit();
            this.tabTranslationStage.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.RS232GroupBox.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.initParamsBox.ResumeLayout(false);
            this.initParamsBox.PerformLayout();
            this.tabMicrowaveControl.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MWGeneratorAMnumericUpDown)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.remoteControlLED)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        #region ThreadSafe wrappers

        private void setCheckBox(CheckBox box, bool state)
        {
            box.Invoke(new setCheckDelegate(setCheckHelper), new object[] { box, state });
        }
        private delegate void setCheckDelegate(CheckBox box, bool state);
        private void setCheckHelper(CheckBox box, bool state)
        {
            box.Checked = state;
        }

        private void setTabEnable(TabControl box, bool state)
        {
            box.Invoke(new setTabEnableDelegate(setTabEnableHelper), new object[] { box, state });
        }
        private delegate void setTabEnableDelegate(TabControl box, bool state);
        private void setTabEnableHelper(TabControl box, bool state)
        {
            box.Enabled = state;
        }

        private void setTextBox(TextBox box, string text)
        {
            box.Invoke(new setTextDelegate(setTextHelper), new object[] { box, text });
        }
        private delegate void setTextDelegate(TextBox box, string text);
        private void setTextHelper(TextBox box, string text)
        {
            box.Text = text;
        }
        
        private void setRichTextBox(RichTextBox box, string text)
        {
            box.Invoke(new setRichTextDelegate(setRichTextHelper), new object[] { box, text });
        }
        private delegate void setRichTextDelegate(RichTextBox box, string text);
        private void setRichTextHelper(RichTextBox box, string text)
        {
            box.AppendText(text);
            consoleRichTextBox.ScrollToCaret();
        }

        private void setLED(NationalInstruments.UI.WindowsForms.Led led, bool val)
        {
            led.Invoke(new SetLedDelegate(SetLedHelper), new object[] { led, val });
        }
        private delegate void SetLedDelegate(NationalInstruments.UI.WindowsForms.Led led, bool val);
        private void SetLedHelper(NationalInstruments.UI.WindowsForms.Led led, bool val)
        {
            led.Value = val;
        }
        private void setNumericUpDown(NumericUpDown UpDown, string text)
        {
            UpDown.Invoke(new SetUpDownDelegate(SetUpDownHelper), new object[] { UpDown, text });
        }
        private delegate void SetUpDownDelegate(NumericUpDown UpDown, string text);
        private void SetUpDownHelper(NumericUpDown UpDown, string text)
        {
            UpDown.Text = text;
        }

        #endregion

        #region Declarations
        //Declare stuff here
        public TabControl shcTabs;
        public TabPage tabCamera;
        public TabPage tabLasers;
        public TabPage tabCoils;
        private GroupBox aom0ControlBox;
        public CheckBox aom0CheckBox;
        private Label aom0Label2;
        private Label aom0Label0;
        private Label aom0Label1;
        private Label aom0Label3;
        private GroupBox aom3ControlBox;
        private Label aom3Label1;
        public CheckBox aom3CheckBox;
        private Label aom3Label2;
        private Label aom3Label0;
        private GroupBox aom2ControlBox;
        private Label aom2Label3;
        private Label aom2Label1;
        public CheckBox aom2CheckBox;
        private Label aom2Label2;
        private Label aom2Label0;
        private GroupBox aom1ControlBox;
        private Label aom1Label3;
        private Label aom1Label1;
        public CheckBox aom1CheckBox;
        private Label aom1Label2;
        private Label aom1Label0;
        private Label coil1Label1;
        private Label coil1Label0;
        private GroupBox coil0GroupBox;
        private Label coil0Label1;
        private Label coil0Label0;
        private Button button1;
        public CheckBox checkBox1;
        public TextBox textBox1;
        public TextBox textBox2;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadParametersToolStripMenuItem;
        private ToolStripMenuItem saveParametersToolStripMenuItem;
        private ToolStripMenuItem saveImageToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private Button snapshotButton;
        private Led remoteControlLED;
        private Label label1;
        private Button updateHardwareButton;
        private ToolStripMenuItem windowsToolStripMenuItem;
        private ToolStripMenuItem hardwareMonitorToolStripMenuItem;
        private RichTextBox consoleRichTextBox;
        private ToolStripMenuItem openImageViewerToolStripMenuItem;
        private Button streamButton;
        private Button stopStreamButton;
        private TabPage tabTranslationStage;
        private Button TSOnButton;
        private Button TSInitButton;
        private Button TSOffButton;
        private Button TSGoButton;
        private Button disposeButton;
        private Button TSReturnButton;
        private Button TSClearButton;
        private Button TSRestartButton;
        private Button TSHomeButton;
        private Button checkStatusButton;
        private Button TSListAllButton;
        private GroupBox initParamsBox;
        private TextBox TSVelTextBox;
        private TextBox TSStepsTextBox;
        private TextBox TSDecTextBox;
        private TextBox TSAccTextBox;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private GroupBox groupBox1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label9;
        private Label label8;
        private Label label7;
        private Label label6;
        private GroupBox groupBox4;
        private CheckBox AutoTriggerCheckBox;
        private Button TSConnectButton;
        private GroupBox RS232GroupBox;
        public CheckBox shutterCheckBox;
        private GroupBox ShutterControlBox;
        private GroupBox ProbeControlBox;
        private GroupBox ovenShutterControlBox;
        public CheckBox ovenShutterCheckBox;
        public CheckBox probeShutterCheckBox;
        private Label offsetLockLabel0;
        private Label offsetLockLabel1;
        private GroupBox offsetLockControlBox;
       
        private GroupBox D1EOMControlBox;
        private GroupBox D2EOMControlBox;
        private Label label10;
        private Label label11;
        private Label label13;
        private Label label12;
        private Label D2EOMfrequencylabel;
        private Label D1EOMfrequencylabel;
        #endregion

        #region Click Handlers

        private void saveParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.SaveParametersWithDialog();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.SaveImageWithDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void updateHardwareButton_Click(object sender, EventArgs e)
        {
            controller.UpdateHardware();
        }
        private void loadParametersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.LoadParametersWithDialog();
        }
        private void TSInitButton_Click(object sender, EventArgs e)
        {
            controller.TSInitialize(double.Parse(TSAccTextBox.Text), double.Parse(TSDecTextBox.Text),
                double.Parse(TSStepsTextBox.Text), double.Parse(TSVelTextBox.Text));
        }

        private void TSOnButton_Click(object sender, EventArgs e)
        {

            controller.TSOn();
        }

        private void TSGoButton_Click(object sender, EventArgs e)
        {
            controller.TSGo();
        }

        private void TSOffButton_Click(object sender, EventArgs e)
        {
            controller.TSOff();
        }

       
        private void TSReturnButton_Click(object sender, EventArgs e)
        {
            controller.TSReturn();
        }

        private void TSRestartButton_Click(object sender, EventArgs e)
        {
            controller.TSRestart();
        }

        private void TSClearButton_Click(object sender, EventArgs e)
        {
            controller.TSClear();
        }

        private void disposeButton_Click(object sender, EventArgs e)
        {
            controller.TSDisconnect();
        }

        private void TSConnectButton_Click(object sender, EventArgs e)
        {
            controller.TSConnect();
        }

        private void AutoTriggerCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoTriggerCheckBox.Checked)
            {
                controller.TSAutoTriggerEnable();
            }
            if (!AutoTriggerCheckBox.Checked)
            {
                controller.TSAutoTriggerDisable();
            }
        }

        private void TSHomeButton_Click(object sender, EventArgs e)
        {
            controller.TSHome();
        }

        private void checkStatusButton_Click(object sender, EventArgs e)
        {
            controller.TSCheckStatus();
        }

        private void DriveFaultButton_Click(object sender, EventArgs e)
        {
            controller.TSReadDriveFaults();
        }

        private void UserFaultButton_Click(object sender, EventArgs e)
        {
            controller.TSReadUserFaults();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            controller.TSListAll();
        }

        private void aom0frequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom0amplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom1frequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom1amplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom2frequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom2amplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom3frequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom3amplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void d1eomfrequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void d1eomamplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void d2eomfrequencyUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void d2eomamplitudeUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void xcoilcurrentUpDown_Click(object sender, EventArgs e)
        {

            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void D2EOMcheckbox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void offsetlockvcoUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }
        

        private CheckBox AutoUpdateCheckBox;

        private void coil1currentUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void coil0currentUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom0CheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom1CheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom2CheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom3CheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom4checkbox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }


        private void startMarlinCameraAndOpenImageViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.StartMarlinCameraControl("cam0");
        }

        private void MWGeneratorAMnumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void MWGeneratorAMnumericUpDown_MouseClick(object sender, MouseEventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void aom4frequencynumericUpDown_ValueChanged(object sender, EventArgs e)
        {

        }

        private void aom4amplitudenumericUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        

        private void ovenShutterCheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }
        

        private void aom4frequencynumericUpDown_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }


        private void shutterCheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }

        private void probeShutterCheckBox_Click(object sender, EventArgs e)
        {
            if (AutoUpdateCheckBox.Checked)
            {
                controller.UpdateHardware();
            }
        }
        #endregion

        #region Public properties for controlling UI.
        //This gets/sets the values on the GUI panel
        public void WriteToConsole(string text)
        {
            setRichTextBox(consoleRichTextBox, ">> " + text + "\n");
            
           
        }
        public double ReadAnalog(string channelName)
        {
            //return double.Parse(AOTextBoxes[channelName].Text);
            return double.Parse(AONumericUpDown[channelName].Text);
        }
        public void SetAnalog(string channelName, double value)
        {
            setNumericUpDown(AONumericUpDown[channelName], Convert.ToString(value));
            //setTextBox(AOTextBoxes[channelName], Convert.ToString(value));
        }
        public bool ReadDigital(string channelName)
        {
            return DOCheckBoxes[channelName].Checked;
        }
        public void SetDigital(string channelName, bool value)
        {
            setCheckBox(DOCheckBoxes[channelName], value);
        }
        #endregion

        #region Camera Control

        private void snapshotButton_Click(object sender, EventArgs e)
        {
            controller.CameraSnapshot();         
        }
        private void streamButton_Click(object sender, EventArgs e)
        {
            controller.CameraStream();
        }
        private void stopStreamButton_Click(object sender, EventArgs e)
        {
            controller.StopCameraStream();
        }

        #endregion

        #region UI state
        
        public void UpdateUIState(Controller.SHCUIControlState state)
        {
            switch (state)
            {
                case Controller.SHCUIControlState.OFF:

                    setLED(remoteControlLED, false);
                    setTabEnable(shcTabs, true);

                    break;

                case Controller.SHCUIControlState.LOCAL:

                    setLED(remoteControlLED, false);
                    setTabEnable(shcTabs, true);
                    break;

                case Controller.SHCUIControlState.REMOTE:

                    setLED(remoteControlLED, true);
                    setTabEnable(shcTabs, false) ;

                    break;
            }
        }

       
        #endregion

        #region Other Windows


        private void hardwareMonitorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.OpenNewHardwareMonitorWindow();
        }
        
        private void openImageViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string camera = "cam3";
            controller.StartCameraControl(camera);
        }

        private ToolStripMenuItem openLiveCameraAnalysisWindowToolStripMenuItem;

        private void openLiveCameraAnalysisWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.OpenLiveCameraAnalysisWindow();
        }

        #endregion

        private Label label15;
        private Label label14;
        private NumericUpDown aom0frequencyUpDown;
        private NumericUpDown d2eomamplitudeUpDown;
        private NumericUpDown d2eomfrequencyUpDown;
        private NumericUpDown d1eomamplitudeUpDown;
        private NumericUpDown d1eomfrequencyUpDown;
        private NumericUpDown offsetlockvcoUpDown;
        private NumericUpDown aom3amplitudeUpDown;
        private NumericUpDown aom3frequencyUpDown;
        private NumericUpDown aom2amplitudeUpDown;
        private NumericUpDown aom2frequencyUpDown;
        private NumericUpDown aom1amplitudeUpDown;
        private NumericUpDown aom1frequencyUpDown;
        private NumericUpDown aom0amplitudeUpDown;
        private NumericUpDown BottomTransportCurrentUpDown;
        private NumericUpDown TopTransportCurrentUpDown;
        private Label CoilDescriptionLabel;
        private ToolStripMenuItem startMarlinCameraAndOpenImageViewerToolStripMenuItem;
        private CheckBox D2EOMCheckBox;
        private GroupBox ShimCoilGroupBox;
        private NumericUpDown xcoilcurrentUpDown;
        private Label XCoilLabel1;
        private Label xcoilNumericUpDownLabel;
        private Label Zshimlabel;
        private Label zshimunitlabel;
        private NumericUpDown zcoilcurrentUpDown;
        private NumericUpDown aom4frequencynumericUpDown;
        private GroupBox aom4groupbox;
        private Label aom4amplitudeunitlabel;
        private NumericUpDown aom4amplitudenumericUpDown;
        private Label aom4amplitudelabel;
        private Label aom4frequencyunitlabel;
        private Label aom4frequencylabel;
        private CheckBox aom4checkbox;

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private Label ycoilcurrentlabel;
        private Label ycoilcurrentunitlabel;
        private NumericUpDown ycoilcurrentUpDown;
        private GroupBox groupBox5;
        private Label label19;
        private Label label18;
        private Label label17;
        private NumericUpDown BottomTrappingCoilnumericUpDown;
        private NumericUpDown TopTrappingCoilnumericUpDown;
        private Label label16;

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void CoilDescriptionLabel_Click(object sender, EventArgs e)
        {

        }

        private TabPage tabMicrowaveControl;
        private GroupBox groupBox6;
        private Label MWgenAMunits;
        private Label MWgenAMlabel;
        private NumericUpDown MWGeneratorAMnumericUpDown;

        private RichTextBox richTextBox1;

        private Label aom3Label3;
        private Button DriveFaultButton;
        private Button UserFaultButton;

        

        

        
       

       

        

        

        

       

        

        

        

        
       
        
       

        

       

  
        

      

        

        
        
       

       

  
       

       

        
                  

       
        

              

          

        

 










    }
}
