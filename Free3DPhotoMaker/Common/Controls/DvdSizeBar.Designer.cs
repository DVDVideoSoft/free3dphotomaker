namespace DVDVideoSoft.Controls
{
    partial class DvdSizeBar
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
            this.backPictureBox = new System.Windows.Forms.PictureBox();
            this.outputSizePanel = new System.Windows.Forms.Panel();
            this.outputSizeLabel = new System.Windows.Forms.Label();
            this.dvdTypesBoundsPictureBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.backPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvdTypesBoundsPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // backPictureBox
            // 
            this.backPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backPictureBox.Location = new System.Drawing.Point(0, 0);
            this.backPictureBox.Name = "backPictureBox";
            this.backPictureBox.Size = new System.Drawing.Size(338, 36);
            this.backPictureBox.TabIndex = 0;
            this.backPictureBox.TabStop = false;
            // 
            // outputSizePanel
            // 
            this.outputSizePanel.BackColor = System.Drawing.SystemColors.Highlight;
            this.outputSizePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.outputSizePanel.Location = new System.Drawing.Point(0, 0);
            this.outputSizePanel.Name = "outputSizePanel";
            this.outputSizePanel.Size = new System.Drawing.Size(42, 36);
            this.outputSizePanel.TabIndex = 3;
            // 
            // outputSizeLabel
            // 
            this.outputSizeLabel.AutoSize = true;
            this.outputSizeLabel.BackColor = System.Drawing.Color.Transparent;
            this.outputSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.outputSizeLabel.Location = new System.Drawing.Point(43, 21);
            this.outputSizeLabel.Name = "outputSizeLabel";
            this.outputSizeLabel.Size = new System.Drawing.Size(57, 12);
            this.outputSizeLabel.TabIndex = 5;
            this.outputSizeLabel.Text = "Output size: ";
            // 
            // dvdTypesBoundsPictureBox
            // 
            this.dvdTypesBoundsPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dvdTypesBoundsPictureBox.Location = new System.Drawing.Point(127, 0);
            this.dvdTypesBoundsPictureBox.Name = "dvdTypesBoundsPictureBox";
            this.dvdTypesBoundsPictureBox.Size = new System.Drawing.Size(107, 36);
            this.dvdTypesBoundsPictureBox.TabIndex = 6;
            this.dvdTypesBoundsPictureBox.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.label2.Location = new System.Drawing.Point(129, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "DVD";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F);
            this.label3.Location = new System.Drawing.Point(240, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "DVD DL";
            // 
            // DvdSizeBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.outputSizeLabel);
            this.Controls.Add(this.outputSizePanel);
            this.Controls.Add(this.dvdTypesBoundsPictureBox);
            this.Controls.Add(this.backPictureBox);
            this.Name = "DvdSizeBar";
            this.Size = new System.Drawing.Size(338, 36);
            this.SizeChanged += new System.EventHandler(this.DvdSizeBar_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.backPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvdTypesBoundsPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox backPictureBox;
        private System.Windows.Forms.Panel outputSizePanel;
        private System.Windows.Forms.Label outputSizeLabel;
        private System.Windows.Forms.PictureBox dvdTypesBoundsPictureBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}
