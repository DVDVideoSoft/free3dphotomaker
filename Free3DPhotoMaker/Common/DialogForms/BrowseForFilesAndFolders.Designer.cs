namespace DVDVideoSoft.DialogForms
{
    partial class BrowseForFilesAndFolders
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.checkBoxInclSubfolders = new System.Windows.Forms.CheckBox();
            this.textBoxSearchPattern = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.panelPreview = new System.Windows.Forms.Panel();
            this.timerPreview = new System.Windows.Forms.Timer(this.components);
            this.explorerTreeViewWnd = new DVDVideoSoft.DialogForms.ExplorerTreeViewWnd();
            this.timerFilter = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(0, 473);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(409, 2);
            this.label1.TabIndex = 1;
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Location = new System.Drawing.Point(329, 485);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(248, 485);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 3;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // checkBoxInclSubfolders
            // 
            this.checkBoxInclSubfolders.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBoxInclSubfolders.AutoSize = true;
            this.checkBoxInclSubfolders.Checked = true;
            this.checkBoxInclSubfolders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxInclSubfolders.Location = new System.Drawing.Point(6, 489);
            this.checkBoxInclSubfolders.Name = "checkBoxInclSubfolders";
            this.checkBoxInclSubfolders.Size = new System.Drawing.Size(112, 17);
            this.checkBoxInclSubfolders.TabIndex = 4;
            this.checkBoxInclSubfolders.Text = "Include subfolders";
            this.checkBoxInclSubfolders.UseVisualStyleBackColor = true;
            this.checkBoxInclSubfolders.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // textBoxSearchPattern
            // 
            this.textBoxSearchPattern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxSearchPattern.Location = new System.Drawing.Point(6, 399);
            this.textBoxSearchPattern.Name = "textBoxSearchPattern";
            this.textBoxSearchPattern.Size = new System.Drawing.Size(209, 20);
            this.textBoxSearchPattern.TabIndex = 13;
            this.textBoxSearchPattern.Text = "*";
            this.textBoxSearchPattern.TextChanged += new System.EventHandler(this.textBoxSearchPattern_TextChanged);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(3, 383);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(38, 13);
            this.labelName.TabIndex = 12;
            this.labelName.Text = "Name:";
            // 
            // comboBoxFilter
            // 
            this.comboBoxFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilter.FormattingEnabled = true;
            this.comboBoxFilter.Location = new System.Drawing.Point(6, 436);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(209, 21);
            this.comboBoxFilter.TabIndex = 11;
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
            // 
            // panelPreview
            // 
            this.panelPreview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panelPreview.Location = new System.Drawing.Point(221, 383);
            this.panelPreview.Name = "panelPreview";
            this.panelPreview.Size = new System.Drawing.Size(183, 87);
            this.panelPreview.TabIndex = 14;
            // 
            // timerPreview
            // 
            this.timerPreview.Interval = 1000;
            this.timerPreview.Tick += new System.EventHandler(this.timerPreview_Tick);
            // 
            // explorerTreeViewWnd
            // 
            this.explorerTreeViewWnd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.explorerTreeViewWnd.CurFilter = -1;
            this.explorerTreeViewWnd.IncludeSubFolders = true;
            this.explorerTreeViewWnd.Indent = 19;
            this.explorerTreeViewWnd.Location = new System.Drawing.Point(0, 0);
            this.explorerTreeViewWnd.MultiSelect = true;
            this.explorerTreeViewWnd.Name = "explorerTreeViewWnd";
            this.explorerTreeViewWnd.SearchPattern = "*";
            this.explorerTreeViewWnd.ShowFiles = true;
            this.explorerTreeViewWnd.Size = new System.Drawing.Size(404, 380);
            this.explorerTreeViewWnd.TabIndex = 0;
            // 
            // timerFilter
            // 
            this.timerFilter.Interval = 1000;
            this.timerFilter.Tick += new System.EventHandler(this.timerFilter_Tick);
            // 
            // BrowseForFilesAndFolders
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancel;
            this.ClientSize = new System.Drawing.Size(406, 527);
            this.Controls.Add(this.panelPreview);
            this.Controls.Add(this.textBoxSearchPattern);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.explorerTreeViewWnd);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBoxInclSubfolders);
            this.MinimumSize = new System.Drawing.Size(350, 200);
            this.Name = "BrowseForFilesAndFolders";
            this.ShowIcon = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Browse for Files and Folders";
            this.Load += new System.EventHandler(this.BrowseForFilesAndFolders_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BrowseForFilesAndFolders_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DVDVideoSoft.DialogForms.ExplorerTreeViewWnd explorerTreeViewWnd;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.CheckBox checkBoxInclSubfolders;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.TextBox textBoxSearchPattern;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Panel panelPreview;
        private System.Windows.Forms.Timer timerPreview;
        private System.Windows.Forms.Timer timerFilter;
    }
}

