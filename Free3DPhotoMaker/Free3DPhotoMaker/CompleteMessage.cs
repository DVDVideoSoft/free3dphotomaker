using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Free3DPhotoMaker
{
    public partial class CompleteMessage : Form
    {
        DVDVideoSoft.Utils.PropsProvider data;

        public CompleteMessage( DVDVideoSoft.Utils.PropsProvider prov, bool showOpen)
        {
            InitializeComponent();
            data = prov;
            Text = prov.GetString(Configuration.MainFormName);
            pictureBox1.Image = SystemIcons.Information.ToBitmap();
            labelMessage.Text = DVDVideoSoft.Resources.CommonData.ProcessCompletedSuccessfully;
            buttonOpen.Visible = showOpen;
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            string argument = data.GetString(Configuration.OutputFile);
            string strPath = System.IO.Path.GetDirectoryName(argument);
            System.Diagnostics.Process.Start("explorer.exe", strPath);
        }
    }
}
