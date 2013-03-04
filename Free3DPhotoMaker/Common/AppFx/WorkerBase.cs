using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Resources;
using System.ComponentModel;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.AppFx
{
    public class WorkerBase : BackgroundWorker
    {
        protected static ILogWriter Log;

        public new event EventHandler<ProgressChangedEventArgs> ProgressChanged;
        public event EventHandler<ItemProcessedEventArgs> ItemProcessed;
        public event EventHandler<RunWorkerCompletedEventArgs> Complete;

        protected BackgroundWorker bgw;
        protected IProgressDlg iprogress;
        protected ResourceManager resman;
        protected ResourceManager commonResman;

        protected bool cancelled = false;

        public WorkerBase()
        {
            this.bgw = this;
            this.bgw.DoWork +=              new DoWorkEventHandler(bgw_DoWork);
            this.bgw.RunWorkerCompleted +=  new RunWorkerCompletedEventHandler(bgw_WorkerCompleted);
            this.bgw.ProgressChanged +=     new ProgressChangedEventHandler(bgw_ProgressChanged);
        }

        public void SetLogger(ILogWriter log)
        {
            if (log == null)
                throw new ArgumentNullException(Defs.LoggerCannotBeNull_STR);
            Log = log;
        }

        public virtual void Cancel()
        {
            this.cancelled = true;
            this.bgw.CancelAsync();
        }

        public void SetResourceManagers(ResourceManager resman, ResourceManager commonResman)
        {
            this.resman = resman;
            this.commonResman = commonResman;
        }

        public void SetIProgress(IProgressDlg iprogress)
        {
            this.iprogress = iprogress;
        }

        protected virtual void bgw_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
        }

        protected void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (this.ProgressChanged != null)
                this.ProgressChanged(null, e);
        }

        protected void bgw_WorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (this.iprogress != null)
                this.iprogress.SetTotalProgress(100);

            if (Complete != null)
            {
                RunWorkerCompletedEventArgs eOut = new RunWorkerCompletedEventArgs(null, null, this.cancelled);
                Complete(null, eOut);
            }
        }

        protected void itemProcessedHandler(object sender, ItemProcessedEventArgs e)
        {
            if (this.ItemProcessed != null)
                this.ItemProcessed(null, e);
        }
    }
}
