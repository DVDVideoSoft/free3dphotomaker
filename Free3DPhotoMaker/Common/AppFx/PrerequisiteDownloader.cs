using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Resources;
using System.Threading;
using System.Diagnostics;
using System.Globalization;

using DVDVideoSoft.Utils;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.AppFx
{
    public class PrerequisiteDownloader
    {
        private Form parent;

        private string downloadUrl = "";
        private string destPath = "";
        private string downloadedFile = "";
        private string culture = "en-US";
        private bool cancelled = false;

        private Thread thread;
        private EventWaitHandle completeEvent = new EventWaitHandle(false, EventResetMode.AutoReset);

        public PrerequisiteDownloader(Form parent, string url, string culture, bool showMessages)
        {
            this.parent = parent;
            this.downloadUrl = url;

            this.thread = new Thread(ThreadFunc);
        }

        public bool Download(string destPath)
        {
            this.destPath = destPath;
            if (!this.destPath.EndsWith("\\"))
                this.destPath += "\\";
            this.thread.Start();
            completeEvent.WaitOne();
            return !string.IsNullOrEmpty(this.downloadedFile);
        }

        public void ExecDownload()
        {
            if (!string.IsNullOrEmpty(this.downloadedFile))
                Process.Start(this.downloadedFile);
        }

        private void ThreadFunc()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            DownloadRedist(this.destPath + Path.GetFileName(downloadUrl));

            this.completeEvent.Set();
        }

        public void ShowResult(bool newVersionIsAvailable)
        {
            if (this.cancelled)
                return;

            //if (newVersionIsAvailable)
            //{
            //    if ((DialogResult)parent.Invoke((DialogResultFormDelegate)delegate(Form p){ return MessageBox.Show(p, CommonData.ThereIsNewVersion, CommonData.Confirm, MessageBoxButtons.YesNo, MessageBoxIcon.Question); }, parent) == DialogResult.Yes)
            //    {
            //        System.Diagnostics.Process p = new System.Diagnostics.Process();
            //        string urlSubstring = Programs.GetProgramPageUrlSubstring((Programs.ID)programID);
            //        string url = (string.IsNullOrEmpty(language)) ? Programs.CompanyRootUrl + urlSubstring :
            //                                              Programs.CompanyRootUrl + language + @"/" + urlSubstring;
            //        p.StartInfo.FileName = url;
            //        p.Start();
            //    }
            //}
            //else
            //{
            //    parent.Invoke((DialogResultFormDelegate)delegate(Form p){ return MessageBox.Show(p, CommonData.VersionIsUpToDate, CommonData.Information, MessageBoxButtons.OK, MessageBoxIcon.Information); }, parent);
            //}
         }

        private bool DownloadRedist(string saveToFileName)
        {
            if (File.Exists(saveToFileName))
            {
                try
                {
                    File.Delete(saveToFileName);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            WebClient myWebClient = new WebClient();
            try
            {
                myWebClient.DownloadFile(downloadUrl, saveToFileName);
                this.downloadedFile = saveToFileName;
            }
            catch (WebException)
            {
                return false;
            }
            return true;
        }

        public void Cancel()
        {
            this.cancelled = true;
        }
    }
}
