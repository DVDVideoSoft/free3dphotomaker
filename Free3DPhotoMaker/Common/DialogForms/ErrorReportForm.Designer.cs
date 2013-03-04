namespace DVDVideoSoft.DialogForms
{
    partial class ErrorReportForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pleaseVisitSiteLabel = new System.Windows.Forms.Label();
            this.siteLinkLabel = new System.Windows.Forms.LinkLabel();
            this.messageLabel = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pleaseVisitSiteLabel);
            this.panel1.Controls.Add(this.siteLinkLabel);
            this.panel1.Location = new System.Drawing.Point(12, 57);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(381, 45);
            this.panel1.TabIndex = 3;
            // 
            // pleaseVisitSiteLabel
            // 
            this.pleaseVisitSiteLabel.AutoSize = true;
            this.pleaseVisitSiteLabel.Location = new System.Drawing.Point(0, 0);
            this.pleaseVisitSiteLabel.Name = "pleaseVisitSiteLabel";
            this.pleaseVisitSiteLabel.Size = new System.Drawing.Size(270, 13);
            this.pleaseVisitSiteLabel.TabIndex = 4;
            this.pleaseVisitSiteLabel.Text = "Please install the latest version directly from our website:";
            // 
            // siteLinkLabel
            // 
            this.siteLinkLabel.AutoSize = true;
            this.siteLinkLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.AlwaysUnderline;
            this.siteLinkLabel.Location = new System.Drawing.Point(127, 23);
            this.siteLinkLabel.Name = "siteLinkLabel";
            this.siteLinkLabel.Size = new System.Drawing.Size(118, 13);
            this.siteLinkLabel.TabIndex = 3;
            this.siteLinkLabel.TabStop = true;
            this.siteLinkLabel.Text = "www.dvdvideosoft.com";
            this.siteLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.siteLinkLabel_LinkClicked);
            // 
            // messageLabel
            // 
            this.messageLabel.Location = new System.Drawing.Point(12, 0);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(367, 54);
            this.messageLabel.TabIndex = 3;
            this.messageLabel.Text = "The program cannot continue working as a required module is missing.";
            this.messageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(159, 112);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // ErrorReportForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(391, 147);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ErrorReportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Free Studio";
            this.Shown += new System.EventHandler(this.ErrorReportForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label pleaseVisitSiteLabel;
        private System.Windows.Forms.LinkLabel siteLinkLabel;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Button btnOK;
    }
}