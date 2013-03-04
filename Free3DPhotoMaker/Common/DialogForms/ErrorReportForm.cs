using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.DialogForms
{
    public partial class ErrorReportForm : Form
    {
        public string ProgramPageUrlSubstring { get; set; }

        public ErrorReportForm(string caption, string message)
        {
            InitializeComponent();

            this.Text = string.IsNullOrEmpty(caption) ? Assembly.GetEntryAssembly().GetName().Name : caption;

            if (!string.IsNullOrEmpty(message))
                this.messageLabel.Text = message;
            else
                this.messageLabel.Text = Resources.CommonData.RequiredModuleMissing;

            this.pleaseVisitSiteLabel.Text = Resources.CommonData.PleaseInstallTheLatest;
        }

        public void ShowModal(IWin32Window parent)
        {
            base.ShowDialog(parent);
        }

        protected new DialogResult ShowDialog(IWin32Window owner)
        {
            return base.ShowDialog(owner);
        }

        private void ErrorReportForm_Shown(object sender, EventArgs e)
        {
            BringToFront();
        }

        private void siteLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            StringBuilder sb = new StringBuilder(DvsUrls.CompanyRootUrl);

            string localeSpecificUrlSubstring =  LocaleUtils.GetDvsLocaleId(Thread.CurrentThread.CurrentUICulture.ToString(), false);
            if (!string.IsNullOrEmpty(localeSpecificUrlSubstring))
                sb.AppendFormat("{0}/", localeSpecificUrlSubstring);

            sb.Append(this.ProgramPageUrlSubstring);

            System.Diagnostics.Process.Start(sb.ToString());

            Close();
        }
    }
}
