using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.DialogForms
{
    public interface IPreviewCtrl
    {
        void ShowPreview(string FileName);
        System.Drawing.Rectangle AdjustBounds(System.Windows.Forms.Control Parent, System.Drawing.Rectangle AvailableArea);
        System.Windows.Forms.Control GetControl();
        void ShutDown();
    }
}
