namespace IMAQ
{
    partial class ImageViewerWindow
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
            this.components = new System.ComponentModel.Container();
            this.imageViewer = new NationalInstruments.Vision.WindowsForms.ImageViewer();
            this.consoleRichTextBox = new System.Windows.Forms.RichTextBox();
            this.hScrollBar = new System.Windows.Forms.HScrollBar();
            this.thermometer1 = new NationalInstruments.UI.WindowsForms.Thermometer();
            this.StreamButton = new System.Windows.Forms.Button();
            this.SnapshotButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.thermometer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // imageViewer
            // 
            this.imageViewer.ActiveTool = NationalInstruments.Vision.WindowsForms.ViewerTools.ZoomIn;
            this.imageViewer.AutoSize = true;
            this.imageViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageViewer.ImeMode = System.Windows.Forms.ImeMode.On;
            this.imageViewer.Location = new System.Drawing.Point(102, 60);
            this.imageViewer.Margin = new System.Windows.Forms.Padding(5);
            this.imageViewer.Name = "imageViewer";
            this.imageViewer.ShowToolbar = true;
            this.imageViewer.Size = new System.Drawing.Size(725, 458);
            this.imageViewer.TabIndex = 0;
            this.imageViewer.ZoomToFit = true;
            // 
            // consoleRichTextBox
            // 
            this.consoleRichTextBox.BackColor = System.Drawing.Color.Black;
            this.consoleRichTextBox.ForeColor = System.Drawing.Color.Lime;
            this.consoleRichTextBox.Location = new System.Drawing.Point(0, 544);
            this.consoleRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.consoleRichTextBox.Name = "consoleRichTextBox";
            this.consoleRichTextBox.ReadOnly = true;
            this.consoleRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.consoleRichTextBox.Size = new System.Drawing.Size(827, 270);
            this.consoleRichTextBox.TabIndex = 24;
            this.consoleRichTextBox.Text = "";
            // 
            // hScrollBar
            // 
            this.hScrollBar.Location = new System.Drawing.Point(0, 523);
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size(827, 17);
            this.hScrollBar.TabIndex = 25;
            this.hScrollBar.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar_Scroll);
            // 
            // thermometer1
            // 
            this.thermometer1.InteractionMode = NationalInstruments.UI.LinearNumericPointerInteractionModes.EditRange;
            this.thermometer1.Location = new System.Drawing.Point(0, 336);
            this.thermometer1.Name = "thermometer1";
            this.thermometer1.OutOfRangeMode = NationalInstruments.UI.NumericOutOfRangeMode.CoerceToRange;
            this.thermometer1.Size = new System.Drawing.Size(85, 184);
            this.thermometer1.TabIndex = 26;
            // 
            // StreamButton
            // 
            this.StreamButton.Location = new System.Drawing.Point(102, 17);
            this.StreamButton.Name = "StreamButton";
            this.StreamButton.Size = new System.Drawing.Size(123, 29);
            this.StreamButton.TabIndex = 27;
            this.StreamButton.Text = "Stream";
            this.StreamButton.UseVisualStyleBackColor = true;
            this.StreamButton.Click += new System.EventHandler(this.StreamButton_Click);
            // 
            // SnapshotButton
            // 
            this.SnapshotButton.Location = new System.Drawing.Point(261, 17);
            this.SnapshotButton.Name = "SnapshotButton";
            this.SnapshotButton.Size = new System.Drawing.Size(121, 28);
            this.SnapshotButton.TabIndex = 28;
            this.SnapshotButton.Text = "Snapshot";
            this.SnapshotButton.UseVisualStyleBackColor = true;
            this.SnapshotButton.Click += new System.EventHandler(this.SnapshotButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Location = new System.Drawing.Point(419, 20);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(109, 24);
            this.StopButton.TabIndex = 29;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ImageViewerWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(856, 807);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.SnapshotButton);
            this.Controls.Add(this.StreamButton);
            this.Controls.Add(this.thermometer1);
            this.Controls.Add(this.hScrollBar);
            this.Controls.Add(this.consoleRichTextBox);
            this.Controls.Add(this.imageViewer);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ImageViewerWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Image";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewerWindow_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.thermometer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public NationalInstruments.Vision.WindowsForms.ImageViewer imageViewer;
        private System.Windows.Forms.RichTextBox consoleRichTextBox;
        private System.Windows.Forms.HScrollBar hScrollBar;
        public NationalInstruments.UI.WindowsForms.Thermometer thermometer1;
        public NationalInstruments.UI.WindowsForms.Graph graph1;
        private System.Windows.Forms.Button StreamButton;
        private System.Windows.Forms.Button SnapshotButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.BindingSource bindingSource1;
    }
}