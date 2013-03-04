namespace DVDVideoSoft.DialogForms
{
    partial class ProcessResultForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessResultForm));
            this.btnOK = new System.Windows.Forms.Button();
            this.divLabel = new System.Windows.Forms.Label();
            this.resultUrlLabel = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.serviceMessageLabel = new System.Windows.Forms.Label();
            this.serviceUrlLinkLabel = new System.Windows.Forms.LinkLabel();
            this.resultRichText = new System.Windows.Forms.RichTextBox();
            this.resultsCtxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUrlsItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.resultsCtxMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // divLabel
            // 
            this.divLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            resources.ApplyResources(this.divLabel, "divLabel");
            this.divLabel.Name = "divLabel";
            // 
            // resultUrlLabel
            // 
            resources.ApplyResources(this.resultUrlLabel, "resultUrlLabel");
            this.resultUrlLabel.Name = "resultUrlLabel";
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // serviceMessageLabel
            // 
            this.serviceMessageLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            resources.ApplyResources(this.serviceMessageLabel, "serviceMessageLabel");
            this.serviceMessageLabel.Name = "serviceMessageLabel";
            // 
            // serviceUrlLinkLabel
            // 
            this.serviceUrlLinkLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(225)))));
            resources.ApplyResources(this.serviceUrlLinkLabel, "serviceUrlLinkLabel");
            this.serviceUrlLinkLabel.Name = "serviceUrlLinkLabel";
            this.serviceUrlLinkLabel.TabStop = true;
            this.serviceUrlLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.serviceUrlLinkLabel_LinkClicked);
            // 
            // resultRichText
            // 
            this.resultRichText.ContextMenuStrip = this.resultsCtxMenu;
            this.resultRichText.DetectUrls = false;
            resources.ApplyResources(this.resultRichText, "resultRichText");
            this.resultRichText.Name = "resultRichText";
            this.resultRichText.SelectionChanged += new System.EventHandler(this.resultRichText_SelectionChanged);
            this.resultRichText.KeyDown += new System.Windows.Forms.KeyEventHandler(this.resultRichText_KeyDown);
            // 
            // resultsCtxMenu
            // 
            this.resultsCtxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyMenuItem,
            this.copyUrlsItem});
            this.resultsCtxMenu.Name = "contextMenuStrip1";
            resources.ApplyResources(this.resultsCtxMenu, "resultsCtxMenu");
            // 
            // copyMenuItem
            // 
            this.copyMenuItem.Name = "copyMenuItem";
            resources.ApplyResources(this.copyMenuItem, "copyMenuItem");
            this.copyMenuItem.Click += new System.EventHandler(this.copyMenuItem_Click);
            // 
            // copyUrlsItem
            // 
            this.copyUrlsItem.Name = "copyUrlsItem";
            resources.ApplyResources(this.copyUrlsItem, "copyUrlsItem");
            this.copyUrlsItem.Click += new System.EventHandler(this.copyUrlsItem_Click);
            // 
            // ProcessResultForm
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            resources.ApplyResources(this, "$this");
            this.CancelButton = this.btnOK;
            this.Controls.Add(this.resultRichText);
            this.Controls.Add(this.serviceUrlLinkLabel);
            this.Controls.Add(this.serviceMessageLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.resultUrlLabel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.divLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ProcessResultForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.resultsCtxMenu.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label divLabel;
        private System.Windows.Forms.Label resultUrlLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label serviceMessageLabel;
        private System.Windows.Forms.LinkLabel serviceUrlLinkLabel;
        private System.Windows.Forms.RichTextBox resultRichText;
        private System.Windows.Forms.ContextMenuStrip resultsCtxMenu;
        private System.Windows.Forms.ToolStripMenuItem copyMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyUrlsItem;
    }
}