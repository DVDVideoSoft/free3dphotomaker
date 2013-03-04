namespace DVDVideoSoft.Controls
{
    partial class ChoupBox
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
            this.checkBoxTitle = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // checkBoxTitle
            // 
            this.checkBoxTitle.AutoSize = true;
            this.checkBoxTitle.Location = new System.Drawing.Point(3, 3);
            this.checkBoxTitle.Name = "checkBoxTitle";
            this.checkBoxTitle.Size = new System.Drawing.Size(46, 17);
            this.checkBoxTitle.TabIndex = 0;
            this.checkBoxTitle.Text = "Title";
            this.checkBoxTitle.UseVisualStyleBackColor = true;
            this.checkBoxTitle.CheckedChanged += new System.EventHandler(this.checkBoxTitle_CheckedChanged);
            // 
            // ChoupBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxTitle);
            this.Name = "ChoupBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxTitle;
    }
}
