namespace Free3DPhotoMaker
{
    partial class PreviewForm
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
            this.gdViewerPreview = new GdPicture.GdViewer();
            this.SuspendLayout();
            // 
            // gdViewerPreview
            // 
            this.gdViewerPreview.AnimateGIF = false;
            this.gdViewerPreview.BackColor = System.Drawing.Color.Black;
            this.gdViewerPreview.BackgroundImage = null;
            this.gdViewerPreview.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.gdViewerPreview.ContinuousViewMode = true;
            this.gdViewerPreview.Cursor = System.Windows.Forms.Cursors.Default;
            this.gdViewerPreview.DisplayQuality = GdPicture.DisplayQuality.DisplayQualityBicubicHQ;
            this.gdViewerPreview.DisplayQualityAuto = false;
            this.gdViewerPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdViewerPreview.DocumentAlignment = GdPicture.ViewerDocumentAlignment.DocumentAlignmentMiddleCenter;
            this.gdViewerPreview.DocumentPosition = GdPicture.ViewerDocumentPosition.DocumentPositionMiddleCenter;
            this.gdViewerPreview.EnableMenu = true;
            this.gdViewerPreview.EnableMouseWheel = true;
            this.gdViewerPreview.ForceScrollBars = false;
            this.gdViewerPreview.ForceTemporaryModeForImage = false;
            this.gdViewerPreview.ForceTemporaryModeForPDF = false;
            this.gdViewerPreview.ForeColor = System.Drawing.Color.Black;
            this.gdViewerPreview.Gamma = 1F;
            this.gdViewerPreview.IgnoreDocumentResolution = false;
            this.gdViewerPreview.KeepDocumentPosition = false;
            this.gdViewerPreview.Location = new System.Drawing.Point(0, 0);
            this.gdViewerPreview.LockViewer = false;
            this.gdViewerPreview.MouseButtonForMouseMode = GdPicture.MouseButton.MouseButtonLeft;
            this.gdViewerPreview.MouseMode = GdPicture.ViewerMouseMode.MouseModeDefault;
            this.gdViewerPreview.MouseWheelMode = GdPicture.ViewerMouseWheelMode.MouseWheelModeZoom;
            this.gdViewerPreview.Name = "gdViewerPreview";
            this.gdViewerPreview.OptimizeDrawingSpeed = false;
            this.gdViewerPreview.PdfDisplayFormField = true;
            this.gdViewerPreview.PDFShowDialogForPassword = true;
            this.gdViewerPreview.RectBorderColor = System.Drawing.Color.Black;
            this.gdViewerPreview.RectBorderSize = 1;
            this.gdViewerPreview.RectIsEditable = true;
            this.gdViewerPreview.RegionsAreEditable = true;
            this.gdViewerPreview.ScrollBars = true;
            this.gdViewerPreview.ScrollLargeChange = ((short)(50));
            this.gdViewerPreview.ScrollSmallChange = ((short)(1));
            this.gdViewerPreview.SilentMode = true;
            this.gdViewerPreview.Size = new System.Drawing.Size(284, 262);
            this.gdViewerPreview.TabIndex = 0;
            this.gdViewerPreview.Zoom = 0.001D;
            this.gdViewerPreview.ZoomCenterAtMousePosition = false;
            this.gdViewerPreview.ZoomMode = GdPicture.ViewerZoomMode.ZoomModeFitToViewer;
            this.gdViewerPreview.ZoomStep = 25;
            this.gdViewerPreview.KeyDown += new System.Windows.Forms.KeyEventHandler(this.PreviewForm_KeyDown);
            this.gdViewerPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.gdViewerPreview_MouseClick);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.gdViewerPreview);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PreviewForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PreviewForm";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private GdPicture.GdViewer gdViewerPreview;
    }
}