using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DVDVideoSoft.Utils;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.DialogForms
{
    public partial class ShutDownWarningForm : Form
    {
        Form parent;
        Timer shutDownTm = new Timer();
        int secCounter = 60;

        public ShutDownWarningForm(Form parent)
        {
            InitializeComponent();
            this.parent = parent;
            shutDownTm.Interval = 1000;
            shutDownTm.Tick += new EventHandler(shutDownTm_Tick);
        }

        void shutDownTm_Tick(object sender, EventArgs e)
        {
            Message = string.Format(CommonData.ShutDownWarning, secCounter--);
            if (secCounter == 0)
            {
                shutDownTm.Stop();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            if (this.DialogResult == System.Windows.Forms.DialogResult.Cancel || this.DialogResult == System.Windows.Forms.DialogResult.OK)
                shutDownTm.Stop();
        }

        public string Caption
        {
            set { this.Text = value; }
        }

        public string Message
        {
            set { messageLabel.Text = value; }
        }

        public DialogResult ShowModal()
        {
            shutDownTm.Start();
            Caption = CommonData.Information;
            Message = string.Format(CommonData.ShutDownWarning, secCounter--);
            DialogResult res = ShowDialog(parent);
            if (res != System.Windows.Forms.DialogResult.OK)
                shutDownTm.Stop();
            return res;
        }

        private void ShutDownWarningForm_Load(object sender, EventArgs e)
        {
            WindowUtils.CenterToParent(this, this.parent);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
