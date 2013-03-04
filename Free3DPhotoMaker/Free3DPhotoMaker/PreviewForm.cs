using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Free3DPhotoMaker
{
    public partial class PreviewForm : Form
    {
        public PreviewForm(int img)
        {
            InitializeComponent();
            Activate();
            gdViewerPreview.DisplayFromGdPictureImage(img);
        }

        private void PreviewForm_KeyDown(object sender, KeyEventArgs e)
        {
            Close();
            Dispose();
        }

        private void gdViewerPreview_MouseClick(object sender, MouseEventArgs e)
        {
            Close();
            Dispose();
        }
    }
}
