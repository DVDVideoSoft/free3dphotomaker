using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DVDVideoSoft.Utils
{
    public class LayoutAdjusterBase
    {
        protected Form form;

        public bool Initialized { get { return this.form != null; } }

        protected IDictionary<string, Point> initialLocations  = new Dictionary<string, Point>();
        protected IDictionary<string, int>   initialPositions  = new Dictionary<string, int>();
        protected IDictionary<string, int>   initialDimensions = new Dictionary<string, int>();
        protected IDictionary<int, object>   generalFlags      = new Dictionary<int, object>();
        protected IDictionary<int, object>   specificFlags     = new Dictionary<int, object>();

        public virtual void Init(Form form)
        {
            this.form = form;
        }

        public virtual void Adjust(bool borderlessMode, bool themeChanged)
        {
        }

        public virtual void AdjustOnCultureChange(bool borderlessMode)
        {
        }

        public virtual void AdjustOnDpiChange()
        {
        }

        public virtual bool ContainsKey(int keyId)
        {
            return this.generalFlags.ContainsKey(keyId) || this.specificFlags.ContainsKey(keyId);
        }

        public virtual object Get(int keyId)
        {
            return null;
        }

        protected virtual void AdjustOnThemeChange(bool borderlessMode)
        {
        }
    }
}
