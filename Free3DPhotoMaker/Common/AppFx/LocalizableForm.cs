using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.AppFx
{
    public class LocalizableForm : Form, ILocalizable
    {
        public delegate void SystemThemeChangedHandler(bool visualStylesEnabled);

        #region Protected members

        protected ILocalizedFormList localizedFormList;

        protected Graphics graphics = null;

        #endregion

        #region Protected methods

        /// <summary>
        /// Avoid multiple CreateGraphics().
        /// </summary>
        protected Graphics GetGraphics()
        {
            if (this.graphics == null)
                this.graphics = this.CreateGraphics();

            return this.graphics;
        }

        /// <summary>
        /// DONT get this property before the form is shown!
        /// </summary>
        protected float dpiX { get { return GetGraphics().DpiX; } }

        #endregion

        public virtual void SetLocFormList(ILocalizedFormList localizedFormList)
        {
            this.localizedFormList = localizedFormList;
        }

        public virtual void BeforeSetCulture()
        {
            // Override this method to call SuspendLayout() if necessary
        }

        public virtual void SetCulture(string culture)
        {
            // Override this method to perform custom localization actions in derived classes
            // if necessary
        }

        public virtual void AfterSetCulture()
        {
            // Override this method to call ResumeLayout() if BeforeSetCulture was overriden
        }

        public virtual void OnSystemThemeChanged(bool visualStylesEnabled)
        {
            foreach (Control c in this.Controls)
            {
                IDvsControl idvsControl = c as IDvsControl;
                if (idvsControl != null)
                {
                    idvsControl.OnSystemThemeChanged(visualStylesEnabled);
                }
            }
        }
    }
}
