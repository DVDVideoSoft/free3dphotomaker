namespace DVDVideoSoft.Controls
{
    partial class DliHistoryView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DliHistoryView));
            this.templatePanel = new System.Windows.Forms.Panel();
            this.formatButton = new DVDVideoSoft.Controls.IconButton();
            this.thumbPictureBox = new DVDVideoSoft.Controls.ThumbPictureBox();
            this.btnRemove = new DVDVideoSoft.Controls.IconButton();
            this.pauseButton = new DVDVideoSoft.Controls.IconButton();
            this.titleText = new System.Windows.Forms.TextBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.scroller = new DVDVideoSoft.Controls.Scroller();
            this.lblDateDivider = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.pnl = new System.Windows.Forms.Panel();
            this.iconButton1 = new DVDVideoSoft.Controls.IconButton();
            this.thumbPictureBox1 = new DVDVideoSoft.Controls.ThumbPictureBox();
            this.iconButton2 = new DVDVideoSoft.Controls.IconButton();
            this.iconButton3 = new DVDVideoSoft.Controls.IconButton();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.templatePanel.SuspendLayout();
            this.pnl.SuspendLayout();
            this.SuspendLayout();
            // 
            // templatePanel
            // 
            this.templatePanel.BackColor = System.Drawing.Color.White;
            this.templatePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.templatePanel.Controls.Add(this.formatButton);
            this.templatePanel.Controls.Add(this.thumbPictureBox);
            this.templatePanel.Controls.Add(this.btnRemove);
            this.templatePanel.Controls.Add(this.pauseButton);
            this.templatePanel.Controls.Add(this.titleText);
            this.templatePanel.Controls.Add(this.progressLabel);
            this.templatePanel.Location = new System.Drawing.Point(110, 0);
            this.templatePanel.Name = "templatePanel";
            this.templatePanel.Size = new System.Drawing.Size(630, 65);
            this.templatePanel.TabIndex = 3;
            this.templatePanel.Visible = false;
            // 
            // formatButton
            // 
            this.formatButton.AlternateState = false;
            this.formatButton.AutoSize = true;
            this.formatButton.BackColor = System.Drawing.Color.Transparent;
            this.formatButton.DrawComboButton = true;
            this.formatButton.DrawFocusOnPressed = false;
            this.formatButton.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.formatButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.formatButton.HighlightColor = System.Drawing.Color.Transparent;
            this.formatButton.Highlighted = false;
            this.formatButton.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(144)))), ((int)(((byte)(182)))));
            this.formatButton.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.formatButton.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.formatButton.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.formatButton.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.formatButton.ImageSize = new System.Drawing.Size(13, 15);
            this.formatButton.Location = new System.Drawing.Point(95, 34);
            this.formatButton.Name = "formatButton";
            this.formatButton.Padding = new System.Windows.Forms.Padding(7, 2, 0, 0);
            this.formatButton.Size = new System.Drawing.Size(197, 19);
            this.formatButton.TabIndex = 35;
            this.formatButton.Text = "MP4 320p ~28Mb";
            this.formatButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.formatButton.TextHighlightColor = System.Drawing.Color.White;
            this.formatButton.TextHOffset = 6;
            // 
            // thumbPictureBox
            // 
            this.thumbPictureBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.thumbPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.thumbPictureBox.Image = null;
            this.thumbPictureBox.Location = new System.Drawing.Point(21, 13);
            this.thumbPictureBox.Name = "thumbPictureBox";
            this.thumbPictureBox.Size = new System.Drawing.Size(70, 40);
            this.thumbPictureBox.TabIndex = 34;
            this.thumbPictureBox.TabStop = false;
            // 
            // btnRemove
            // 
            this.btnRemove.AlternateState = false;
            this.btnRemove.BackColor = System.Drawing.Color.Transparent;
            this.btnRemove.DrawFocusOnPressed = false;
            this.btnRemove.Highlighted = false;
            this.btnRemove.HoverColor = System.Drawing.Color.Empty;
            this.btnRemove.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.Remove_down_new;
            this.btnRemove.ImageDownA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_down_s_new;
            this.btnRemove.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.Remove_norm_new;
            this.btnRemove.ImageNormA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_norm_s_new;
            this.btnRemove.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.Remove_over_new;
            this.btnRemove.ImageOverA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_over_s_new;
            this.btnRemove.ImageSize = new System.Drawing.Size(12, 12);
            this.btnRemove.Location = new System.Drawing.Point(723, 6);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(12, 12);
            this.btnRemove.TabIndex = 30;
            this.btnRemove.TextHighlightColor = System.Drawing.Color.Empty;
            this.btnRemove.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // pauseButton
            // 
            this.pauseButton.AlternateState = false;
            this.pauseButton.BackColor = System.Drawing.Color.Transparent;
            this.pauseButton.DrawFocusOnPressed = false;
            this.pauseButton.Highlighted = false;
            this.pauseButton.HoverColor = System.Drawing.Color.Empty;
            this.pauseButton.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.Pause_down;
            this.pauseButton.ImageDownA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_down_s;
            this.pauseButton.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.Pause_norm;
            this.pauseButton.ImageNormA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_norm_s;
            this.pauseButton.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.Pause_over;
            this.pauseButton.ImageOverA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_over_s;
            this.pauseButton.ImageSize = new System.Drawing.Size(22, 22);
            this.pauseButton.Location = new System.Drawing.Point(687, 23);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(22, 22);
            this.pauseButton.TabIndex = 17;
            this.pauseButton.TextHighlightColor = System.Drawing.Color.Empty;
            this.pauseButton.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // titleText
            // 
            this.titleText.BackColor = System.Drawing.Color.White;
            this.titleText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titleText.ForeColor = System.Drawing.Color.White;
            this.titleText.Location = new System.Drawing.Point(4, 4);
            this.titleText.Name = "titleText";
            this.titleText.Size = new System.Drawing.Size(0, 14);
            this.titleText.TabIndex = 4;
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.BackColor = System.Drawing.Color.Transparent;
            this.progressLabel.Location = new System.Drawing.Point(527, 9);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(0, 14);
            this.progressLabel.TabIndex = 1;
            // 
            // scroller
            // 
            this.scroller.ChannelColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.scroller.DownArrowImage = ((System.Drawing.Image)(resources.GetObject("scroller.DownArrowImage")));
            this.scroller.Enabled = false;
            this.scroller.FrameSize = 5;
            this.scroller.LargeChange = 20F;
            this.scroller.Location = new System.Drawing.Point(740, 0);
            this.scroller.Maximum = 100F;
            this.scroller.MinimumSize = new System.Drawing.Size(16, 88);
            this.scroller.Name = "scroller";
            this.scroller.Size = new System.Drawing.Size(16, 443);
            this.scroller.SmallChange = 1F;
            this.scroller.TabIndex = 4;
            this.scroller.ThumbBottomImage = ((System.Drawing.Image)(resources.GetObject("scroller.ThumbBottomImage")));
            this.scroller.ThumbBottomSpanImage = ((System.Drawing.Image)(resources.GetObject("scroller.ThumbBottomSpanImage")));
            this.scroller.ThumbMiddleImage = ((System.Drawing.Image)(resources.GetObject("scroller.ThumbMiddleImage")));
            this.scroller.ThumbTopImage = ((System.Drawing.Image)(resources.GetObject("scroller.ThumbTopImage")));
            this.scroller.ThumbTopSpanImage = ((System.Drawing.Image)(resources.GetObject("scroller.ThumbTopSpanImage")));
            this.scroller.UpArrowImage = ((System.Drawing.Image)(resources.GetObject("scroller.UpArrowImage")));
            this.scroller.Value = 0F;
            // 
            // lblDateDivider
            // 
            this.lblDateDivider.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblDateDivider.Location = new System.Drawing.Point(0, 241);
            this.lblDateDivider.Name = "lblDateDivider";
            this.lblDateDivider.Size = new System.Drawing.Size(740, 1);
            this.lblDateDivider.TabIndex = 5;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Location = new System.Drawing.Point(15, 255);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(71, 14);
            this.lblData.TabIndex = 6;
            this.lblData.Text = "01.01.2012";
            // 
            // pnl
            // 
            this.pnl.BackColor = System.Drawing.Color.White;
            this.pnl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pnl.Controls.Add(this.iconButton1);
            this.pnl.Controls.Add(this.thumbPictureBox1);
            this.pnl.Controls.Add(this.iconButton2);
            this.pnl.Controls.Add(this.iconButton3);
            this.pnl.Controls.Add(this.textBox1);
            this.pnl.Controls.Add(this.label2);
            this.pnl.Location = new System.Drawing.Point(109, 252);
            this.pnl.Name = "pnl";
            this.pnl.Size = new System.Drawing.Size(630, 65);
            this.pnl.TabIndex = 36;
            this.pnl.Visible = false;
            // 
            // iconButton1
            // 
            this.iconButton1.AlternateState = false;
            this.iconButton1.AutoSize = true;
            this.iconButton1.BackColor = System.Drawing.Color.Transparent;
            this.iconButton1.DrawComboButton = true;
            this.iconButton1.DrawFocusOnPressed = false;
            this.iconButton1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Underline);
            this.iconButton1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(102)))), ((int)(((byte)(153)))));
            this.iconButton1.HighlightColor = System.Drawing.Color.Transparent;
            this.iconButton1.Highlighted = false;
            this.iconButton1.HoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(144)))), ((int)(((byte)(182)))));
            this.iconButton1.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.iconButton1.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.iconButton1.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.iconButton1.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.SingleVideo;
            this.iconButton1.ImageSize = new System.Drawing.Size(13, 15);
            this.iconButton1.Location = new System.Drawing.Point(95, 34);
            this.iconButton1.Name = "iconButton1";
            this.iconButton1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 0);
            this.iconButton1.Size = new System.Drawing.Size(197, 19);
            this.iconButton1.TabIndex = 35;
            this.iconButton1.Text = "MP4 320p ~28Mb";
            this.iconButton1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.iconButton1.TextHighlightColor = System.Drawing.Color.White;
            this.iconButton1.TextHOffset = 6;
            // 
            // thumbPictureBox1
            // 
            this.thumbPictureBox1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.thumbPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.thumbPictureBox1.Image = null;
            this.thumbPictureBox1.Location = new System.Drawing.Point(21, 13);
            this.thumbPictureBox1.Name = "thumbPictureBox1";
            this.thumbPictureBox1.Size = new System.Drawing.Size(70, 40);
            this.thumbPictureBox1.TabIndex = 34;
            this.thumbPictureBox1.TabStop = false;
            // 
            // iconButton2
            // 
            this.iconButton2.AlternateState = false;
            this.iconButton2.BackColor = System.Drawing.Color.Transparent;
            this.iconButton2.DrawFocusOnPressed = false;
            this.iconButton2.Highlighted = false;
            this.iconButton2.HoverColor = System.Drawing.Color.Empty;
            this.iconButton2.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.Remove_down_new;
            this.iconButton2.ImageDownA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_down_s_new;
            this.iconButton2.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.Remove_norm_new;
            this.iconButton2.ImageNormA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_norm_s_new;
            this.iconButton2.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.Remove_over_new;
            this.iconButton2.ImageOverA = global::DVDVideoSoft.Controls.Properties.Resources.Remove_over_s_new;
            this.iconButton2.ImageSize = new System.Drawing.Size(12, 12);
            this.iconButton2.Location = new System.Drawing.Point(723, 6);
            this.iconButton2.Name = "iconButton2";
            this.iconButton2.Size = new System.Drawing.Size(12, 12);
            this.iconButton2.TabIndex = 30;
            this.iconButton2.TextHighlightColor = System.Drawing.Color.Empty;
            this.iconButton2.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // iconButton3
            // 
            this.iconButton3.AlternateState = false;
            this.iconButton3.BackColor = System.Drawing.Color.Transparent;
            this.iconButton3.DrawFocusOnPressed = false;
            this.iconButton3.Highlighted = false;
            this.iconButton3.HoverColor = System.Drawing.Color.Empty;
            this.iconButton3.ImageDown = global::DVDVideoSoft.Controls.Properties.Resources.Pause_down;
            this.iconButton3.ImageDownA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_down_s;
            this.iconButton3.ImageNorm = global::DVDVideoSoft.Controls.Properties.Resources.Pause_norm;
            this.iconButton3.ImageNormA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_norm_s;
            this.iconButton3.ImageOver = global::DVDVideoSoft.Controls.Properties.Resources.Pause_over;
            this.iconButton3.ImageOverA = global::DVDVideoSoft.Controls.Properties.Resources.Pause_over_s;
            this.iconButton3.ImageSize = new System.Drawing.Size(22, 22);
            this.iconButton3.Location = new System.Drawing.Point(687, 23);
            this.iconButton3.Name = "iconButton3";
            this.iconButton3.Size = new System.Drawing.Size(22, 22);
            this.iconButton3.TabIndex = 17;
            this.iconButton3.TextHighlightColor = System.Drawing.Color.Empty;
            this.iconButton3.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.White;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(0, 14);
            this.textBox1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(527, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 14);
            this.label2.TabIndex = 1;
            // 
            // DliHistoryView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.pnl);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.lblDateDivider);
            this.Controls.Add(this.scroller);
            this.Controls.Add(this.templatePanel);
            this.Font = new System.Drawing.Font("Tahoma", 8.4F);
            this.MaximumSize = new System.Drawing.Size(756, 0);
            this.MinimumSize = new System.Drawing.Size(756, 276);
            this.Name = "DliHistoryView";
            this.Size = new System.Drawing.Size(756, 444);
            this.templatePanel.ResumeLayout(false);
            this.templatePanel.PerformLayout();
            this.pnl.ResumeLayout(false);
            this.pnl.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel templatePanel;
        private ThumbPictureBox thumbPictureBox;
        private IconButton btnRemove;
        private IconButton pauseButton;
        private System.Windows.Forms.TextBox titleText;
        private System.Windows.Forms.Label progressLabel;
        private Scroller scroller;
        private IconButton formatButton;
        private System.Windows.Forms.Label lblDateDivider;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Panel pnl;
        private IconButton iconButton1;
        private ThumbPictureBox thumbPictureBox1;
        private IconButton iconButton2;
        private IconButton iconButton3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
    }
}
