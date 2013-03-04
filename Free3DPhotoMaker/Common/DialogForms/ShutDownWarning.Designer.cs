namespace DVDVideoSoft.DialogForms
{
    partial class ShutDownWarningForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShutDownWarningForm));
            this.messageLabel = new System.Windows.Forms.Label();
            this.pictureBoxMsgIcon = new System.Windows.Forms.PictureBox();
            this.pnlDown = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMsgIcon)).BeginInit();
            this.pnlDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageLabel
            // 
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(58, 38);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(182, 13);
            this.messageLabel.TabIndex = 4;
            this.messageLabel.Text = "Computer will be shut down in 60 sec";
            // 
            // pictureBoxMsgIcon
            // 
            this.pictureBoxMsgIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMsgIcon.Image")));
            this.pictureBoxMsgIcon.Location = new System.Drawing.Point(21, 32);
            this.pictureBoxMsgIcon.Name = "pictureBoxMsgIcon";
            this.pictureBoxMsgIcon.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxMsgIcon.TabIndex = 3;
            this.pictureBoxMsgIcon.TabStop = false;
            // 
            // pnlDown
            // 
            this.pnlDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDown.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDown.Controls.Add(this.btnOK);
            this.pnlDown.Controls.Add(this.btnCancel);
            this.pnlDown.Location = new System.Drawing.Point(0, 89);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(289, 49);
            this.pnlDown.TabIndex = 6;
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(95, 12);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(88, 26);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(192, 12);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 26);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // ShutDownWarningForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(289, 137);
            this.Controls.Add(this.pnlDown);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.pictureBoxMsgIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(295, 165);
            this.Name = "ShutDownWarningForm";
            this.ShowInTaskbar = false;
            this.Text = "Information";
            this.Load += new System.EventHandler(this.ShutDownWarningForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMsgIcon)).EndInit();
            this.pnlDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.PictureBox pictureBoxMsgIcon;
        private System.Windows.Forms.Panel pnlDown;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
    }
}