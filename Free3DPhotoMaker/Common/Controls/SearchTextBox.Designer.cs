namespace DVDVideoSoft.Controls
{
    partial class SearchTextBox
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
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearchDownloads = new System.Windows.Forms.Label();
            this.searchPictureBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.searchPictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // txtSearch
            // 
            this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSearch.Location = new System.Drawing.Point(2, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(113, 13);
            this.txtSearch.TabIndex = 0;
            this.txtSearch.TextChanged += new System.EventHandler(this.SearchTextBox_TextChanged);
            // 
            // lblSearchDownloads
            // 
            this.lblSearchDownloads.AutoSize = true;
            this.lblSearchDownloads.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblSearchDownloads.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(157)))), ((int)(((byte)(157)))), ((int)(((byte)(157)))));
            this.lblSearchDownloads.Location = new System.Drawing.Point(4, 3);
            this.lblSearchDownloads.Name = "lblSearchDownloads";
            this.lblSearchDownloads.Size = new System.Drawing.Size(94, 13);
            this.lblSearchDownloads.TabIndex = 71;
            this.lblSearchDownloads.Text = "Search downloads";
            this.lblSearchDownloads.Click += new System.EventHandler(this.lblSearchDownloads_Click);
            // 
            // searchPictureBox
            // 
            this.searchPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.searchPictureBox.Image = global::DVDVideoSoft.Controls.Properties.Resources.search;
            this.searchPictureBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.searchPictureBox.Location = new System.Drawing.Point(116, 2);
            this.searchPictureBox.Name = "searchPictureBox";
            this.searchPictureBox.Size = new System.Drawing.Size(15, 15);
            this.searchPictureBox.TabIndex = 70;
            this.searchPictureBox.TabStop = false;
            // 
            // SearchTextBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.lblSearchDownloads);
            this.Controls.Add(this.searchPictureBox);
            this.Controls.Add(this.txtSearch);
            this.MinimumSize = new System.Drawing.Size(40, 22);
            this.Name = "SearchTextBox";
            this.Size = new System.Drawing.Size(135, 22);
            this.Load += new System.EventHandler(this.SearchTextBox_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SearchTextBox_Paint);
            this.Enter += new System.EventHandler(this.SearchTextBox_Enter);
            this.Leave += new System.EventHandler(this.SearchTextBox_Leave);
            ((System.ComponentModel.ISupportInitialize)(this.searchPictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.PictureBox searchPictureBox;
        private System.Windows.Forms.Label lblSearchDownloads;
    }
}
