using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;
using System.Threading;
using System.Globalization;

using DVDVideoSoft.Utils;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.AppFx
{
    public class Updater
    {
        public event EventHandler<System.ComponentModel.ProgressChangedEventArgs> ProgressChanged;
        public event EventHandler<EventArgs> Complete;

        private Form parent;
        private WebClient webClient = new WebClient();

        private string url;
        private string saveToPath;
        private string fileNameToDownload;
        private CultureInfo culture;
        //private bool cancelled;
        private bool succeeded;

        private Thread thread;

        public string FileName { get { return this.succeeded ? this.fileNameToDownload : ""; } }

        public Updater(Form parent, string saveToPath)
        {
            this.parent = parent;
            this.culture = Thread.CurrentThread.CurrentUICulture;

            this.saveToPath = saveToPath;
            if (!this.saveToPath.EndsWith("\\"))
                this.saveToPath += "\\";

            this.thread = new Thread(ThreadFunc);
        }

        public void DownloadAsync(string url)
        {
            this.succeeded = false;

            this.url = url;
            this.fileNameToDownload = this.saveToPath + Path.GetFileName(url);
            
            this.thread.Start();
        }

        public string Culture
        {
            get { return this.culture.Name; }
            set
            {
                CultureInfo old = this.culture;
                try
                {
                    this.culture = new CultureInfo(value);
                }
                catch
                {
                    this.culture = old;
                }
            }
        }

        private void ThreadFunc()
        {
            Thread.CurrentThread.CurrentUICulture = this.culture;

            if (File.Exists(this.fileNameToDownload))
            {
                try
                {
                    File.Delete(this.fileNameToDownload);
                }
                catch { }
            }

            this.webClient.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            this.webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);

            try
            {
                Directory.CreateDirectory(this.saveToPath);
            }
            catch { }

            try
            {
                this.webClient.DownloadFileAsync(new Uri(url), this.fileNameToDownload);
            }
            catch (WebException) { }
            catch { }
        }

        void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
                this.ProgressChanged(this, new System.ComponentModel.ProgressChangedEventArgs(e.ProgressPercentage, null));
        }

        void webClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.succeeded = true;

            if (this.Complete != null)
                this.Complete(this, new EventArgs());
        }

        public void Cancel()
        {
            //this.cancelled = true;
            this.webClient.CancelAsync();
        }
    }
}
