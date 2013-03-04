using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DVDVideoSoft.Utils
{
    public interface IDvsControl
    {
        void SetBackground(Bitmap bitmap);
        void OnSystemThemeChanged(bool visualStylesEnabled);
    }
}
