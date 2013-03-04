using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using DVDVideoSoft.Resources;
using DVDVideoSoft.Utils;

namespace DVDVideoSoft.DialogForms
{
    public partial class CheckedMessageBox : Form
    {
        private bool m_bCenterToParent = true;
        private Form parent;
        private bool m_bOnlyOkMode;

        public CheckedMessageBox()
        {
            InitializeComponent();
            WithLink = false;
        }

        public CheckedMessageBox(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            WithLink = false;
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Yes;
        }


        #region Initialization

        private void UpdateControlsLocation()
        {
            int messageWidth  = lblMessage.Left + lblMessage.Width + 39;
            int checkBoxWidth = chbDontAskAgain.Left + chbDontAskAgain.Width + 39;
            this.Width = Math.Max(messageWidth, checkBoxWidth);

            if (!this.WithLink)
            {
                this.sukaBlyatLinkLabel.Visible = false;
            }
            else
            {
                this.sukaBlyatLinkLabel.Top = this.lblMessage.Top + this.lblMessage.Height + 16;
            }

            this.chbDontAskAgain.Top = (!this.WithLink) ? this.lblMessage.Top + this.lblMessage.Height + 16 : this.sukaBlyatLinkLabel.Top + this.sukaBlyatLinkLabel.Height + 16;
            int clientHeight =// (this.WithLink) ? this.sukaBlyatLinkLabel.Top + this.sukaBlyatLinkLabel.Height + 22 + this.pnlDown.Height :
                                                 this.chbDontAskAgain.Top + this.chbDontAskAgain.Height + 22 + this.pnlDown.Height;
            this.ClientSize = new Size(this.ClientSize.Width, clientHeight);
        }

        private void CheckedMessageBox_Load(object sender, EventArgs e)
        {
            if (this.CenterToParentForm)
                WindowUtils.CenterToParent(this, this.parent);
        }

        private void SetButtonsText()
        {
            if (m_bOnlyOkMode)
            {
                this.btnNo.Hide();
                this.btnYes.Location = this.btnNo.Location;
                this.btnYes.Text = CommonData.OK;
            }
            else
            {
                btnYes.Text = CommonData.Yes;
            }

            btnNo.Text = CommonData.No;
        }

        #endregion


        #region Properties

        public bool CenterToParentForm
        {
            get { return m_bCenterToParent; }
            set { m_bCenterToParent = value; }
        }

        public string Caption
        {
            set { this.Text = value; } 
        }

        public string Message
        {
            set { this.lblMessage.Text = value; }
        }

        public string CheckBoxMessage
        {
            set { this.chbDontAskAgain.Text = value; }
        }

        public string LinkName
        {
            set { this.sukaBlyatLinkLabel.Text = value; }
        }

        public string Link
        {
            set
            {
                string url = value as string;
                this.sukaBlyatLinkLabel.Links.Clear();
                this.sukaBlyatLinkLabel.Links.Add(0, this.sukaBlyatLinkLabel.Text.Length, url);
            }
        }

        public bool WithLink
        { get; set; }

        //public bool CheckBoxChecked
        //{
        //    set { this.chbDontAskAgain.Checked = value; }
        //}

        public object MessageIcon
        {
            set
            {
                Image img = ((value as Icon) != null) ? (value as Icon).ToBitmap() : (value as Image);
                if (img != null)
                    this.pictureBoxMsgIcon.Image = img;
            }
        }

        #endregion


        #region Show methods

        public DialogResult ShowModal()
        {
            UpdateControlsLocation();

            SetButtonsText();

            return ShowDialog(parent);
        }

        public DialogResult ShowModal(ref bool bDontAskAgain)
        {
            UpdateControlsLocation();

            SetButtonsText();
            
            this.chbDontAskAgain.Checked = bDontAskAgain;
            DialogResult result = ShowDialog(parent);
            bDontAskAgain = this.chbDontAskAgain.Checked;

            return result;
        }

        public DialogResult Show(string caption, string message, string checkBoxMessage, object icon, ref bool bDontAskAgain)
        {
            Caption = caption;

            Message = message;

            CheckBoxMessage = checkBoxMessage;

            this.chbDontAskAgain.Checked = bDontAskAgain;

            MessageIcon = icon;

            return ShowModal(ref bDontAskAgain);
        }

        public DialogResult Show(string caption, string message, string checkBoxMessage, ref bool bDontAskAgain, bool bOnlyOk = false)
        {
            Caption = caption;

            Message = message;

            CheckBoxMessage = checkBoxMessage;

            this.chbDontAskAgain.Checked = bDontAskAgain;

            m_bOnlyOkMode = bOnlyOk;

            return ShowModal(ref bDontAskAgain);
        }

        public DialogResult Show(string caption, string message, string checkBoxMessage, ref bool bDontAskAgain)
        {
            return Show(caption, message, checkBoxMessage, ref bDontAskAgain, false);
        }

        #endregion

        private void sukaBlyatLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start(target);
        }
    }
}
