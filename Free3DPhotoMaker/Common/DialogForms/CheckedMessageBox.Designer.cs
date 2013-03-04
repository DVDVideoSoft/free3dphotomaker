namespace DVDVideoSoft.DialogForms
{
    partial class CheckedMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckedMessageBox));
            this.pictureBoxMsgIcon = new System.Windows.Forms.PictureBox();
            this.chbDontAskAgain = new System.Windows.Forms.CheckBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnYes = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.pnlDown = new System.Windows.Forms.Panel();
            this.sukaBlyatLinkLabel = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMsgIcon)).BeginInit();
            this.pnlDown.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxMsgIcon
            // 
            this.pictureBoxMsgIcon.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxMsgIcon.Image")));
            this.pictureBoxMsgIcon.Location = new System.Drawing.Point(10, 26);
            this.pictureBoxMsgIcon.Name = "pictureBoxMsgIcon";
            this.pictureBoxMsgIcon.Size = new System.Drawing.Size(32, 32);
            this.pictureBoxMsgIcon.TabIndex = 0;
            this.pictureBoxMsgIcon.TabStop = false;
            // 
            // chbDontAskAgain
            // 
            this.chbDontAskAgain.AutoSize = true;
            this.chbDontAskAgain.Location = new System.Drawing.Point(50, 79);
            this.chbDontAskAgain.Name = "chbDontAskAgain";
            this.chbDontAskAgain.Size = new System.Drawing.Size(100, 17);
            this.chbDontAskAgain.TabIndex = 1;
            this.chbDontAskAgain.Text = "Don\'t ask again";
            this.chbDontAskAgain.UseVisualStyleBackColor = true;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(47, 32);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(50, 13);
            this.lblMessage.TabIndex = 2;
            this.lblMessage.Text = "Message";
            // 
            // btnYes
            // 
            this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnYes.Location = new System.Drawing.Point(233, 12);
            this.btnYes.Name = "btnYes";
            this.btnYes.Size = new System.Drawing.Size(88, 26);
            this.btnYes.TabIndex = 3;
            this.btnYes.Text = "Yes";
            this.btnYes.UseVisualStyleBackColor = true;
            this.btnYes.Click += new System.EventHandler(this.btnYes_Click);
            // 
            // btnNo
            // 
            this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnNo.Location = new System.Drawing.Point(330, 12);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(88, 26);
            this.btnNo.TabIndex = 4;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // pnlDown
            // 
            this.pnlDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDown.BackColor = System.Drawing.SystemColors.Control;
            this.pnlDown.Controls.Add(this.btnYes);
            this.pnlDown.Controls.Add(this.btnNo);
            this.pnlDown.Location = new System.Drawing.Point(0, 109);
            this.pnlDown.Name = "pnlDown";
            this.pnlDown.Size = new System.Drawing.Size(427, 49);
            this.pnlDown.TabIndex = 5;
            // 
            // sukaBlyatLinkLabel
            // 
            this.sukaBlyatLinkLabel.AutoSize = true;
            this.sukaBlyatLinkLabel.Location = new System.Drawing.Point(48, 53);
            this.sukaBlyatLinkLabel.Name = "sukaBlyatLinkLabel";
            this.sukaBlyatLinkLabel.Size = new System.Drawing.Size(47, 13);
            this.sukaBlyatLinkLabel.TabIndex = 6;
            this.sukaBlyatLinkLabel.TabStop = true;
            this.sukaBlyatLinkLabel.Text = "Click me";
            this.sukaBlyatLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sukaBlyatLinkLabel_LinkClicked);
            // 
            // CheckedMessageBox
            // 
            this.AcceptButton = this.btnYes;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btnNo;
            this.ClientSize = new System.Drawing.Size(427, 158);
            this.Controls.Add(this.sukaBlyatLinkLabel);
            this.Controls.Add(this.pnlDown);
            this.Controls.Add(this.chbDontAskAgain);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pictureBoxMsgIcon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CheckedMessageBox";
            this.ShowInTaskbar = false;
            this.Text = "Confirm";
            this.Load += new System.EventHandler(this.CheckedMessageBox_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMsgIcon)).EndInit();
            this.pnlDown.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxMsgIcon;
        private System.Windows.Forms.CheckBox chbDontAskAgain;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnYes;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Panel pnlDown;
        private System.Windows.Forms.LinkLabel sukaBlyatLinkLabel;
    }
}