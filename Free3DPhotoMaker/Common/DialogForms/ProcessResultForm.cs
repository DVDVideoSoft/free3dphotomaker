using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.DialogForms
{
    public partial class ProcessResultForm : DVDVideoSoft.AppFx.LocalizableForm
    {
        private Font resultBoxFontFileName;
        private Font resultBoxFontURL;

        private string serviceUrl;
        private static ILogWriter Log = null;

        public ProcessResultForm(string serviceUrl, ILogWriter log)
        {
            Log = log;

            InitializeComponent();

            this.serviceUrl = serviceUrl;

            this.serviceUrlLinkLabel.Text = serviceUrl;

            resultBoxFontFileName = new Font((Font)resultRichText.Font, FontStyle.Bold);
            resultBoxFontURL = new Font((Font)resultRichText.Font, FontStyle.Regular);

            this.serviceUrlLinkLabel.Links.Add(0, this.serviceUrlLinkLabel.Text.Length, this.serviceUrlLinkLabel.Text);
        }

        public bool DoDialog(IWin32Window parent,
                            ref List<UploadTypes.UploadResultInfo> results,
                            UploadTypes.StringUploadResultDelegate translateUploaderErrorFunc,
                            string serviceSpecificMessage)
        {
            string s = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
            Clean();
            StringBuilder urlsBuilder = new StringBuilder();

            this.serviceMessageLabel.Text = serviceSpecificMessage;

            foreach (UploadTypes.UploadResultInfo uplRes in results)
            {
                string verboseResult = null;
                StringBuilder sb = new StringBuilder();

                if (uplRes.Result != UploadTypes.UploadResult.Success)
                {
                    verboseResult = translateUploaderErrorFunc(uplRes.Result);

                    if (uplRes.Result != UploadTypes.UploadResult.Cancelled)
                        sb.AppendFormat(" - {0}", CommonData.UploadFailed);
                    else
                        sb.AppendFormat(" - {0}", verboseResult);
                }

                this.AddResultString(uplRes, verboseResult);

                if (!string.IsNullOrEmpty(uplRes.URL))
                    urlsBuilder.AppendFormat("{0}{1}", uplRes.URL, Environment.NewLine);
            }

            if (urlsBuilder.Length > 0)
            {
                urlsBuilder.Remove(urlsBuilder.Length - Environment.NewLine.Length, Environment.NewLine.Length);
                // Use the menu item tag to store the list of URLs ready to by copied to the clipboard
                this.copyUrlsItem.Tag = urlsBuilder.ToString();
            }
            this.copyUrlsItem.Enabled = urlsBuilder.Length > 0;

            return this.ShowDialog(parent) == System.Windows.Forms.DialogResult.OK;
        }

        public override void SetCulture(string culture)
        {
            base.SetCulture(culture);

            //this.Text = this.caption;
            //this.serviceMessageLabel.Text = this.serviceMessage;//this.commonDataResMgr.GetString(this.serviceMessage);
            this.serviceUrlLinkLabel.Text = this.serviceUrl;
        }

        private void Clean()
        {
            resultRichText.Clear();
            resultRichText.Text = "";
        }

        private void serviceUrlLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string target = e.Link.LinkData as string;
            System.Diagnostics.Process.Start(target);
        }

        public void AddResultString(UploadTypes.UploadResultInfo uplRes, string verboseResult)
        {
            int iSelectionStart = resultRichText.Text.Length;

            resultRichText.AppendText(uplRes.FileName);
            resultRichText.SelectionStart = iSelectionStart;

            resultRichText.SelectionLength = uplRes.FileName.Length;
            resultRichText.SelectionColor = uplRes.Result == 0 ? SystemColors.ControlText : Color.Red;
            resultRichText.SelectionFont = resultBoxFontFileName;

            StringBuilder sb = new StringBuilder();

            if (uplRes.Result == UploadTypes.UploadResult.Success)
            {
                verboseResult = uplRes.URL;
            }
            else
            {
                if (uplRes.Result != UploadTypes.UploadResult.Cancelled)
                    sb.AppendFormat(" - {0}", CommonData.UploadFailed);
                else
                    sb.AppendFormat(" - {0}", verboseResult);
            }

            if (!string.IsNullOrEmpty(verboseResult) && uplRes.Result != UploadTypes.UploadResult.Cancelled)
                sb.AppendFormat("{0}   {1}", Environment.NewLine, verboseResult);

            iSelectionStart = resultRichText.Text.Length;
            resultRichText.AppendText(sb.ToString());
            resultRichText.SelectionStart = iSelectionStart;
            resultRichText.SelectionLength = sb.ToString().Length;
            resultRichText.SelectionColor = Color.Black;
            resultRichText.SelectionFont = resultBoxFontURL;

            resultRichText.AppendText(Environment.NewLine);

            Application.DoEvents();
        }

        private void resultRichText_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }

        private void resultRichText_SelectionChanged(object sender, EventArgs e)
        {
           copyMenuItem.Enabled = ((RichTextBox)sender).SelectedText != "";
        }

        private void setClipboardData(object value)
        {
            DataObject data = new DataObject();
            data.SetData(DataFormats.Text, true, value);
            try
            {
                Clipboard.SetDataObject(data, true);
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                if (Log != null)
                    Log.Error("Failed to Clipboard.SetDataObject(): " + ex.Message);
                MessageBox.Show(this, CommonData.ClipboardIsInUse, CommonData.Error, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void copyMenuItem_Click(object sender, EventArgs e)
        {
            setClipboardData(resultRichText.SelectedText);
        }

        private void copyUrlsItem_Click(object sender, EventArgs e)
        {
            setClipboardData(((ToolStripMenuItem)sender).Tag);
        }
    }
}
