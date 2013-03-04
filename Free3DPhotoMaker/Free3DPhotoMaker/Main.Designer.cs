namespace Free3DPhotoMaker
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.leftGDViewer = new GdPicture.GdViewer();
            this.rightGDViewer = new GdPicture.GdViewer();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.labelMerging = new System.Windows.Forms.Label();
            this.showPreviewCheckBox = new System.Windows.Forms.CheckBox();
            this.btnOptions = new System.Windows.Forms.Button();
            this.algorithmCombo = new System.Windows.Forms.ComboBox();
            this.labelAlgorithm = new System.Windows.Forms.Label();
            this.labelOutputFile = new System.Windows.Forms.Label();
            this.dividerLabel2 = new System.Windows.Forms.Label();
            this.dividerLabel1 = new System.Windows.Forms.Label();
            this.browseButton = new System.Windows.Forms.Button();
            this.swapButton = new System.Windows.Forms.Button();
            this.outputFolderText = new System.Windows.Forms.TextBox();
            this.convertButton = new System.Windows.Forms.Button();
            this.openRightButton = new System.Windows.Forms.Button();
            this.openLeftButton = new System.Windows.Forms.Button();
            this.advancedButton = new System.Windows.Forms.Button();
            this.advancedPanel = new System.Windows.Forms.Panel();
            this.numericUpDownAngle = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.applyButton = new System.Windows.Forms.Button();
            this.resetButton = new System.Windows.Forms.Button();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelUp = new System.Windows.Forms.Label();
            this.numericUpDownVert = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHorz = new System.Windows.Forms.NumericUpDown();
            this.rightImageRadio = new System.Windows.Forms.RadioButton();
            this.leftImageRadio = new System.Windows.Forms.RadioButton();
            this.transparentGDViewer = new GdPicture.GdViewer();
            this.buttonBasic = new System.Windows.Forms.Button();
            this.useSingleSourceCheckBox = new System.Windows.Forms.CheckBox();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.advancedPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorz)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.leftGDViewer);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rightGDViewer);
            // 
            // leftGDViewer
            // 
            this.leftGDViewer.AnimateGIF = false;
            this.leftGDViewer.BackColor = System.Drawing.Color.Transparent;
            this.leftGDViewer.BackgroundImage = null;
            resources.ApplyResources(this.leftGDViewer, "leftGDViewer");
            this.leftGDViewer.ContinuousViewMode = true;
            this.leftGDViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.leftGDViewer.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityLow;
            this.leftGDViewer.DisplayQualityAuto = false;
            this.leftGDViewer.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.leftGDViewer.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.leftGDViewer.EnableMenu = true;
            this.leftGDViewer.EnableMouseWheel = true;
            this.leftGDViewer.ForceScrollBars = false;
            this.leftGDViewer.ForceTemporaryModeForImage = false;
            this.leftGDViewer.ForceTemporaryModeForPDF = false;
            this.leftGDViewer.ForeColor = System.Drawing.Color.Black;
            this.leftGDViewer.Gamma = 1F;
            this.leftGDViewer.IgnoreDocumentResolution = false;
            this.leftGDViewer.KeepDocumentPosition = false;
            this.leftGDViewer.LockViewer = false;
            this.leftGDViewer.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.leftGDViewer.MouseMode = GdPicture.ViewerMouseMode.MouseModeDefault;
            this.leftGDViewer.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.leftGDViewer.Name = "leftGDViewer";
            this.leftGDViewer.OptimizeDrawingSpeed = true;
            this.leftGDViewer.PdfDisplayFormField = true;
            this.leftGDViewer.PDFShowDialogForPassword = true;
            this.leftGDViewer.RectBorderColor = System.Drawing.Color.Black;
            this.leftGDViewer.RectBorderSize = 1;
            this.leftGDViewer.RectIsEditable = true;
            this.leftGDViewer.RegionsAreEditable = true;
            this.leftGDViewer.ScrollBars = true;
            this.leftGDViewer.ScrollLargeChange = ((short)(50));
            this.leftGDViewer.ScrollSmallChange = ((short)(1));
            this.leftGDViewer.SilentMode = true;
            this.leftGDViewer.Zoom = 0.001D;
            this.leftGDViewer.ZoomCenterAtMousePosition = false;
            this.leftGDViewer.ZoomMode = GdPicture.ViewerZoomMode.ZoomModeFitToViewer;
            this.leftGDViewer.ZoomStep = 25;
            // 
            // rightGDViewer
            // 
            this.rightGDViewer.AnimateGIF = false;
            this.rightGDViewer.BackColor = System.Drawing.Color.Transparent;
            this.rightGDViewer.BackgroundImage = null;
            resources.ApplyResources(this.rightGDViewer, "rightGDViewer");
            this.rightGDViewer.ContinuousViewMode = true;
            this.rightGDViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.rightGDViewer.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityLow;
            this.rightGDViewer.DisplayQualityAuto = false;
            this.rightGDViewer.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.rightGDViewer.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.rightGDViewer.EnableMenu = true;
            this.rightGDViewer.EnableMouseWheel = true;
            this.rightGDViewer.ForceScrollBars = false;
            this.rightGDViewer.ForceTemporaryModeForImage = false;
            this.rightGDViewer.ForceTemporaryModeForPDF = false;
            this.rightGDViewer.ForeColor = System.Drawing.Color.Black;
            this.rightGDViewer.Gamma = 1F;
            this.rightGDViewer.IgnoreDocumentResolution = false;
            this.rightGDViewer.KeepDocumentPosition = false;
            this.rightGDViewer.LockViewer = false;
            this.rightGDViewer.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.rightGDViewer.MouseMode = GdPicture.ViewerMouseMode.MouseModeDefault;
            this.rightGDViewer.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.rightGDViewer.Name = "rightGDViewer";
            this.rightGDViewer.OptimizeDrawingSpeed = true;
            this.rightGDViewer.PdfDisplayFormField = true;
            this.rightGDViewer.PDFShowDialogForPassword = true;
            this.rightGDViewer.RectBorderColor = System.Drawing.Color.Black;
            this.rightGDViewer.RectBorderSize = 1;
            this.rightGDViewer.RectIsEditable = true;
            this.rightGDViewer.RegionsAreEditable = true;
            this.rightGDViewer.ScrollBars = true;
            this.rightGDViewer.ScrollLargeChange = ((short)(50));
            this.rightGDViewer.ScrollSmallChange = ((short)(1));
            this.rightGDViewer.SilentMode = true;
            this.rightGDViewer.Zoom = 0.001D;
            this.rightGDViewer.ZoomCenterAtMousePosition = false;
            this.rightGDViewer.ZoomMode = GdPicture.ViewerZoomMode.ZoomModeFitToViewer;
            this.rightGDViewer.ZoomStep = 25;
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // labelMerging
            // 
            resources.ApplyResources(this.labelMerging, "labelMerging");
            this.labelMerging.BackColor = System.Drawing.Color.Transparent;
            this.labelMerging.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.labelMerging.Name = "labelMerging";
            // 
            // showPreviewCheckBox
            // 
            resources.ApplyResources(this.showPreviewCheckBox, "showPreviewCheckBox");
            this.showPreviewCheckBox.BackColor = System.Drawing.Color.Transparent;
            this.showPreviewCheckBox.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.showPreviewCheckBox.Name = "showPreviewCheckBox";
            this.showPreviewCheckBox.UseVisualStyleBackColor = false;
            this.showPreviewCheckBox.CheckedChanged += new System.EventHandler(this.checkBoxShowPreview_CheckedChanged);
            // 
            // btnOptions
            // 
            resources.ApplyResources(this.btnOptions, "btnOptions");
            this.btnOptions.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.btnOptions.Name = "btnOptions";
            this.btnOptions.UseVisualStyleBackColor = true;
            this.btnOptions.Click += new System.EventHandler(this.buttonOptions_Click);
            // 
            // algorithmCombo
            // 
            resources.ApplyResources(this.algorithmCombo, "algorithmCombo");
            this.algorithmCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algorithmCombo.FormattingEnabled = true;
            this.algorithmCombo.Name = "algorithmCombo";
            this.algorithmCombo.SelectedIndexChanged += new System.EventHandler(this.comboBoxAlgorithm_SelectedIndexChanged);
            // 
            // labelAlgorithm
            // 
            resources.ApplyResources(this.labelAlgorithm, "labelAlgorithm");
            this.labelAlgorithm.BackColor = System.Drawing.Color.Transparent;
            this.labelAlgorithm.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.labelAlgorithm.Name = "labelAlgorithm";
            // 
            // labelOutputFile
            // 
            resources.ApplyResources(this.labelOutputFile, "labelOutputFile");
            this.labelOutputFile.BackColor = System.Drawing.Color.Transparent;
            this.labelOutputFile.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.labelOutputFile.Name = "labelOutputFile";
            // 
            // dividerLabel2
            // 
            resources.ApplyResources(this.dividerLabel2, "dividerLabel2");
            this.dividerLabel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dividerLabel2.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.dividerLabel2.Name = "dividerLabel2";
            // 
            // dividerLabel1
            // 
            resources.ApplyResources(this.dividerLabel1, "dividerLabel1");
            this.dividerLabel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dividerLabel1.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.dividerLabel1.Name = "dividerLabel1";
            // 
            // browseButton
            // 
            resources.ApplyResources(this.browseButton, "browseButton");
            this.browseButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.browseButton.Name = "browseButton";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // swapButton
            // 
            resources.ApplyResources(this.swapButton, "swapButton");
            this.swapButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.swapButton.Name = "swapButton";
            this.swapButton.UseVisualStyleBackColor = true;
            this.swapButton.Click += new System.EventHandler(this.buttonSwap_Click);
            // 
            // outputFolderText
            // 
            resources.ApplyResources(this.outputFolderText, "outputFolderText");
            this.outputFolderText.Name = "outputFolderText";
            this.outputFolderText.TextChanged += new System.EventHandler(this.textboxOutputFolder_TextChanged);
            // 
            // convertButton
            // 
            resources.ApplyResources(this.convertButton, "convertButton");
            this.convertButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.convertButton.Name = "convertButton";
            this.convertButton.UseVisualStyleBackColor = true;
            this.convertButton.Click += new System.EventHandler(this.buttonConvert_Click);
            // 
            // openRightButton
            // 
            resources.ApplyResources(this.openRightButton, "openRightButton");
            this.openRightButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.openRightButton.Name = "openRightButton";
            this.openRightButton.UseVisualStyleBackColor = true;
            this.openRightButton.Click += new System.EventHandler(this.buttonOpenRight_Click);
            // 
            // openLeftButton
            // 
            resources.ApplyResources(this.openLeftButton, "openLeftButton");
            this.openLeftButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.openLeftButton.Name = "openLeftButton";
            this.openLeftButton.UseVisualStyleBackColor = true;
            this.openLeftButton.Click += new System.EventHandler(this.buttonOpenLeft_Click);
            // 
            // advancedButton
            // 
            resources.ApplyResources(this.advancedButton, "advancedButton");
            this.advancedButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.advancedButton.Name = "advancedButton";
            this.advancedButton.UseVisualStyleBackColor = true;
            this.advancedButton.Click += new System.EventHandler(this.buttonAdvanced_Click);
            // 
            // advancedPanel
            // 
            resources.ApplyResources(this.advancedPanel, "advancedPanel");
            this.advancedPanel.BackColor = System.Drawing.Color.Transparent;
            this.advancedPanel.Controls.Add(this.numericUpDownAngle);
            this.advancedPanel.Controls.Add(this.label1);
            this.advancedPanel.Controls.Add(this.applyButton);
            this.advancedPanel.Controls.Add(this.resetButton);
            this.advancedPanel.Controls.Add(this.labelLeft);
            this.advancedPanel.Controls.Add(this.labelUp);
            this.advancedPanel.Controls.Add(this.numericUpDownVert);
            this.advancedPanel.Controls.Add(this.numericUpDownHorz);
            this.advancedPanel.Controls.Add(this.rightImageRadio);
            this.advancedPanel.Controls.Add(this.leftImageRadio);
            this.advancedPanel.Controls.Add(this.transparentGDViewer);
            this.advancedPanel.Name = "advancedPanel";
            // 
            // numericUpDownAngle
            // 
            resources.ApplyResources(this.numericUpDownAngle, "numericUpDownAngle");
            this.numericUpDownAngle.Maximum = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.numericUpDownAngle.Minimum = new decimal(new int[] {
            90,
            0,
            0,
            -2147483648});
            this.numericUpDownAngle.Name = "numericUpDownAngle";
            this.numericUpDownAngle.ValueChanged += new System.EventHandler(this.numericUpDownAngle_ValueChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.label1.Name = "label1";
            // 
            // applyButton
            // 
            resources.ApplyResources(this.applyButton, "applyButton");
            this.applyButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.applyButton.Name = "applyButton";
            this.applyButton.UseVisualStyleBackColor = true;
            this.applyButton.Click += new System.EventHandler(this.button2_Click);
            // 
            // resetButton
            // 
            resources.ApplyResources(this.resetButton, "resetButton");
            this.resetButton.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.resetButton.Name = "resetButton";
            this.resetButton.UseVisualStyleBackColor = true;
            this.resetButton.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // labelLeft
            // 
            resources.ApplyResources(this.labelLeft, "labelLeft");
            this.labelLeft.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.labelLeft.Name = "labelLeft";
            // 
            // labelUp
            // 
            resources.ApplyResources(this.labelUp, "labelUp");
            this.labelUp.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.labelUp.Name = "labelUp";
            // 
            // numericUpDownVert
            // 
            resources.ApplyResources(this.numericUpDownVert, "numericUpDownVert");
            this.numericUpDownVert.Name = "numericUpDownVert";
            this.numericUpDownVert.ValueChanged += new System.EventHandler(this.numericUpDownVert_ValueChanged);
            // 
            // numericUpDownHorz
            // 
            resources.ApplyResources(this.numericUpDownHorz, "numericUpDownHorz");
            this.numericUpDownHorz.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.numericUpDownHorz.Name = "numericUpDownHorz";
            this.numericUpDownHorz.ValueChanged += new System.EventHandler(this.numericUpDownHorz_ValueChanged);
            // 
            // rightImageRadio
            // 
            resources.ApplyResources(this.rightImageRadio, "rightImageRadio");
            this.rightImageRadio.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.rightImageRadio.Name = "rightImageRadio";
            this.rightImageRadio.UseVisualStyleBackColor = true;
            this.rightImageRadio.CheckedChanged += new System.EventHandler(this.radioButtonRight_CheckedChanged);
            // 
            // leftImageRadio
            // 
            resources.ApplyResources(this.leftImageRadio, "leftImageRadio");
            this.leftImageRadio.Checked = true;
            this.leftImageRadio.ImageKey = global::Free3DPhotoMaker.Properties.Resources.OptimizedAlg;
            this.leftImageRadio.Name = "leftImageRadio";
            this.leftImageRadio.TabStop = true;
            this.leftImageRadio.UseVisualStyleBackColor = true;
            this.leftImageRadio.CheckedChanged += new System.EventHandler(this.radioButtonLeft_CheckedChanged);
            // 
            // transparentGDViewer
            // 
            resources.ApplyResources(this.transparentGDViewer, "transparentGDViewer");
            this.transparentGDViewer.AnimateGIF = false;
            this.transparentGDViewer.BackColor = System.Drawing.Color.Transparent;
            this.transparentGDViewer.BackgroundImage = null;
            this.transparentGDViewer.ContinuousViewMode = true;
            this.transparentGDViewer.Cursor = System.Windows.Forms.Cursors.Default;
            this.transparentGDViewer.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityLow;
            this.transparentGDViewer.DisplayQualityAuto = false;
            this.transparentGDViewer.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.transparentGDViewer.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.transparentGDViewer.EnableMenu = true;
            this.transparentGDViewer.EnableMouseWheel = false;
            this.transparentGDViewer.ForceScrollBars = false;
            this.transparentGDViewer.ForceTemporaryModeForImage = false;
            this.transparentGDViewer.ForceTemporaryModeForPDF = false;
            this.transparentGDViewer.ForeColor = System.Drawing.Color.Black;
            this.transparentGDViewer.Gamma = 1F;
            this.transparentGDViewer.IgnoreDocumentResolution = false;
            this.transparentGDViewer.KeepDocumentPosition = false;
            this.transparentGDViewer.LockViewer = false;
            this.transparentGDViewer.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.transparentGDViewer.MouseMode = GdPicture.ViewerMouseMode.MouseModeDefault;
            this.transparentGDViewer.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.transparentGDViewer.Name = "transparentGDViewer";
            this.transparentGDViewer.OptimizeDrawingSpeed = true;
            this.transparentGDViewer.PdfDisplayFormField = true;
            this.transparentGDViewer.PDFShowDialogForPassword = true;
            this.transparentGDViewer.RectBorderColor = System.Drawing.Color.Black;
            this.transparentGDViewer.RectBorderSize = 1;
            this.transparentGDViewer.RectIsEditable = true;
            this.transparentGDViewer.RegionsAreEditable = true;
            this.transparentGDViewer.ScrollBars = true;
            this.transparentGDViewer.ScrollLargeChange = ((short)(50));
            this.transparentGDViewer.ScrollSmallChange = ((short)(1));
            this.transparentGDViewer.SilentMode = true;
            this.transparentGDViewer.Zoom = 0.001D;
            this.transparentGDViewer.ZoomCenterAtMousePosition = false;
            this.transparentGDViewer.ZoomMode = GdPicture.ViewerZoomMode.ZoomModeFitToViewer;
            this.transparentGDViewer.ZoomStep = 25;
            // 
            // buttonBasic
            // 
            resources.ApplyResources(this.buttonBasic, "buttonBasic");
            this.buttonBasic.Name = "buttonBasic";
            this.buttonBasic.UseVisualStyleBackColor = true;
            this.buttonBasic.Click += new System.EventHandler(this.buttonBasic_Click);
            // 
            // useSingleSourceCheckBox
            // 
            resources.ApplyResources(this.useSingleSourceCheckBox, "useSingleSourceCheckBox");
            this.useSingleSourceCheckBox.Name = "useSingleSourceCheckBox";
            this.useSingleSourceCheckBox.UseVisualStyleBackColor = true;
            this.useSingleSourceCheckBox.CheckedChanged += new System.EventHandler(this.checkSingleImage_CheckedChanged);
            // 
            // Main
            // 
            this.AcceptButton = this.convertButton;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.useSingleSourceCheckBox);
            this.Controls.Add(this.buttonBasic);
            this.Controls.Add(this.advancedPanel);
            this.Controls.Add(this.advancedButton);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.labelMerging);
            this.Controls.Add(this.showPreviewCheckBox);
            this.Controls.Add(this.btnOptions);
            this.Controls.Add(this.algorithmCombo);
            this.Controls.Add(this.labelAlgorithm);
            this.Controls.Add(this.labelOutputFile);
            this.Controls.Add(this.dividerLabel2);
            this.Controls.Add(this.dividerLabel1);
            this.Controls.Add(this.browseButton);
            this.Controls.Add(this.swapButton);
            this.Controls.Add(this.outputFolderText);
            this.Controls.Add(this.convertButton);
            this.Controls.Add(this.openRightButton);
            this.Controls.Add(this.openLeftButton);
            this.Name = "Main";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResizeBegin += new System.EventHandler(this.Main_ResizeBegin);
            this.ResizeEnd += new System.EventHandler(this.Main_ResizeEnd);
            this.HelpRequested += new System.Windows.Forms.HelpEventHandler(this.Main_HelpRequested);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.advancedPanel.ResumeLayout(false);
            this.advancedPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAngle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownVert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHorz)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label labelMerging;
        private System.Windows.Forms.CheckBox showPreviewCheckBox;
        private System.Windows.Forms.Button btnOptions;
        private System.Windows.Forms.ComboBox algorithmCombo;
        private System.Windows.Forms.Label labelAlgorithm;
        private System.Windows.Forms.Label labelOutputFile;
        private System.Windows.Forms.Label dividerLabel2;
        private System.Windows.Forms.Label dividerLabel1;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.Button swapButton;
        private System.Windows.Forms.TextBox outputFolderText;
        private System.Windows.Forms.Button convertButton;
        private System.Windows.Forms.Button openRightButton;
        private System.Windows.Forms.Button openLeftButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private GdPicture.GdViewer leftGDViewer;
        private GdPicture.GdViewer rightGDViewer;
        private System.Windows.Forms.Button advancedButton;
        private System.Windows.Forms.Panel advancedPanel;
        private System.Windows.Forms.Button resetButton;
        private System.Windows.Forms.Label labelLeft;
        private System.Windows.Forms.Label labelUp;
        private System.Windows.Forms.NumericUpDown numericUpDownVert;
        private System.Windows.Forms.NumericUpDown numericUpDownHorz;
        private System.Windows.Forms.RadioButton rightImageRadio;
        private System.Windows.Forms.RadioButton leftImageRadio;
        private GdPicture.GdViewer transparentGDViewer;
        private System.Windows.Forms.Button applyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownAngle;
        private System.Windows.Forms.Button buttonBasic;
        private System.Windows.Forms.CheckBox useSingleSourceCheckBox;
    }
}

