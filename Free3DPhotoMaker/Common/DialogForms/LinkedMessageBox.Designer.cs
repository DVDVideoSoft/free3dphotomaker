namespace DVDVideoSoft.DialogForms
{
    partial class LinkedMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LinkedMessageBox));
            this.pnlDown = new System.Windows.Forms.Panel();
            this.OkButton = new System.Windows.Forms.Button();
            this.messageLabel = new System.Windows.Forms.Label();
            this.iconPictureBox = new System.Windows.Forms.PictureBox();
            this.yourLinkLabel = new System.Windows.Forms.LinkLabel();
            this.pnlDown.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlDown
            // 
            this.pnlDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDown.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDown.Controls.Add(this.OkButton);
            this.pnlDown.Location = new System.Drawing.Point(0, 100);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(427, 49);
            this.pnlDown.TabIndex = 6;
            // 
            // OkButton
            // 
            this.OkButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.OkButton.Location = new System.Drawing.Point(330, 12);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(88, 26);
            this.OkButton.TabIndex = 4;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(65, 32);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(127, 13);
            this.messageLabel.TabIndex = 9;
            this.messageLabel.Text = "This site is not supported.";
            // 
            // iconPictureBox
            // 
            this.iconPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("iconPictureBox.Image")));
            this.iconPictureBox.Location = new System.Drawing.Point(26, 26);
            this.iconPictureBox.Name = "iconPictureBox";
            this.iconPictureBox.Size = new System.Drawing.Size(32, 32);
            this.iconPictureBox.TabIndex = 7;
            this.iconPictureBox.TabStop = false;
            // 
            // yourLinkLabel
            // 
            this.yourLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.yourLinkLabel.AutoSize = true;
            this.yourLinkLabel.Location = new System.Drawing.Point(65, 61);
            this.yourLinkLabel.Name = "yourLinkLabel";
            this.yourLinkLabel.Size = new System.Drawing.Size(94, 13);
            this.yourLinkLabel.TabIndex = 10;
            this.yourLinkLabel.TabStop = true;
            this.yourLinkLabel.Text = "Contact to support";
            this.yourLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.yourLinkLabel_LinkClicked);
            // 
            // LinkedMessageBox
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.OkButton;
            this.ClientSize = new System.Drawing.Size(427, 149);
            this.Controls.Add(this.yourLinkLabel);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.iconPictureBox);
            this.Controls.Add(this.pnlDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(208, 135);
            this.Name = "LinkedMessageBox";
            this.ShowInTaskbar = false;
            this.Text = "Information";
            this.Load += new System.EventHandler(this.LinkedMessageBox_Load);
            this.pnlDown.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.iconPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlDown;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.PictureBox iconPictureBox;
        private System.Windows.Forms.LinkLabel yourLinkLabel;
    }
}