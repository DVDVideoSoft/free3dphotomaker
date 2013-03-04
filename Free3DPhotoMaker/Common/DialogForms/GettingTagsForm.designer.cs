namespace DVDVideoSoft.DialogForms
{
    partial class GettingTagsForm
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
            if (disposing && (components != null)) {
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
            this.lblFileIsScanned = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // lblFileIsScanned
            // 
            this.lblFileIsScanned.AutoSize = true;
            this.lblFileIsScanned.Location = new System.Drawing.Point(29, 21);
            this.lblFileIsScanned.Name = "lblFileIsScanned";
            this.lblFileIsScanned.Size = new System.Drawing.Size(227, 14);
            this.lblFileIsScanned.TabIndex = 0;
            this.lblFileIsScanned.Text = "Your file is being scanned. Please wait...";
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(29, 48);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(98, 14);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "MyVideoFile.mp4";
            // 
            // timer1
            // 
            this.timer1.Interval = 60000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // GettingTagsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 85);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.lblFileIsScanned);
            this.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GettingTagsForm";
            this.Text = "Getting tags...";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GettingTagsForm_FormClosing);
            this.Load += new System.EventHandler(this.GettingTagsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileIsScanned;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.Timer timer1;
    }
}