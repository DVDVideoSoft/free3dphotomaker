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
    public partial class LinkedMessageBox : Form
    {
        private Form parent;
        private bool m_bCenterToParent = true;
        private const int MaxStringLength = 80;

        public LinkedMessageBox()
        {
            InitializeComponent();
        }

        public LinkedMessageBox(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
        }

        #region Initialization

        private void SetFormSize()
        {
            int width = messageLabel.Left + messageLabel.Width + 39;
            this.Width = Math.Max(messageLabel.Left + messageLabel.Width + 39, this.yourLinkLabel.Left + this.yourLinkLabel.Width + 39);
        }

        private void SetButtonsText()
        {
            this.OkButton.Text = CommonData.OK;
        }

        private void LinkedMessageBox_Load(object sender, EventArgs e)
        {
            if (this.CenterToParentForm)
                WindowUtils.CenterToParent(this, this.parent);
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
            set 
            {
                this.messageLabel.Text =  FormatMessage(value);
                this.Height += (this.messageLabel.Height - 13);
            }
        }

        private string FormatMessage(string text)
        {
            string result = string.Empty;

            List<string> separators =  new List<string>() { "\r\n", "\n" };

            foreach (string str in text.Split(separators.ToArray(), StringSplitOptions.None))//.RemoveEmptyEntries))
            {
                if (string.IsNullOrEmpty(str))
                {
                    result += "\r\n";
                    continue;
                }
                if (!string.IsNullOrEmpty(result))
                    result += "\r\n";
                result += ParseMessageString(str);
            }
            return result;
        }

        private string ParseMessageString(string messageStr)
        {
            string resultString = string.Empty;

            IList<string> wordsInMessageStr = messageStr.Split(' ');
            string tempStr = string.Empty;
            foreach (string word in wordsInMessageStr)
            {
                if (!string.IsNullOrEmpty(tempStr))
                   tempStr += " ";
                if (tempStr.Length + word.Length < LinkedMessageBox.MaxStringLength)
                {
                    tempStr += word;
                }
                else   //add part in resultString
                {
                    if (!string.IsNullOrEmpty(resultString))
                        resultString += "\r\n";
                    resultString += tempStr;
                    tempStr = word;
                }

                if (wordsInMessageStr.IndexOf(word) == wordsInMessageStr.Count - 1)
                {
                    if (!string.IsNullOrEmpty(resultString))
                        resultString += "\r\n";
                        resultString += tempStr;
                }
            }
            return resultString;
        }

        public string LinkName
        {
            set { this.yourLinkLabel.Text = value; }
        }

        public string Link
        {
            set 
            {
                string url = value as string;
                this.yourLinkLabel.Links.Add(0, this.yourLinkLabel.Text.Length, url);
            }
        }

        public object MessageIcon
        {
            set
            {
                Image img = ((value as Icon) != null) ? (value as Icon).ToBitmap() : (value as Image);
                if (img != null) this.iconPictureBox.Image = img;
            }
        }

        #endregion

        #region Show methods

        public DialogResult ShowModal()
        {
            SetFormSize();

            SetButtonsText();

            return ShowDialog();
        }

        public DialogResult Show(string caption, string message, bool withoutLink)
        {
            if (withoutLink)
            {
                this.yourLinkLabel.Visible = false;
                //todo: remove hardcode
                this.Height -= (this.yourLinkLabel.Height + 25);
            }
            return this.Show(caption, message, string.Empty, string.Empty);
        }

        public DialogResult Show(string caption, string message, string linkName, string link, object icon)
        {
            this.Caption = caption;

            this.Message = message;

            this.LinkName = linkName;

            this.Link = link;

            this.MessageIcon = icon;

            return this.ShowModal();
        }

        public DialogResult Show(string caption, string message, string linkName, string link)
        {
            Caption = caption;

            Message = message;

            this.LinkName = linkName;

            this.Link = link;

            return ShowModal();
        }

        private void yourLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start(target);
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        #endregion

    }
}
