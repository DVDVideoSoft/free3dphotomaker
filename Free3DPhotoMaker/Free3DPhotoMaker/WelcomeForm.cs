using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DVDVideoSoft.AppFx;
using DVDVideoSoft.Utils;

namespace Free3DPhotoMaker
{
    public partial class WelcomeForm : LocalizableForm
    {
        public WelcomeForm()
        {
            InitializeComponent();
        }

        public void ShowModal(Form parent, PropsProvider provider)
        {
            this.chbDoNotShow.Checked=provider.GetBool(Configuration.DoNotShowWelcomeScreen);
            ShowDialog(parent);
            provider.Set(Configuration.DoNotShowWelcomeScreen, this.chbDoNotShow.Checked);
        }
    }
}
