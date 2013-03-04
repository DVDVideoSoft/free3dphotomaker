using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DVDVideoSoft.DialogForms
{
    public static class FormUtils
    {
        public static Image DownloadImage(string artworkUrl)
        {
            PictureBox pbx = new PictureBox();
            try
            {
                pbx.Load(artworkUrl);
                return pbx.Image;
            }
            catch (Exception ex)
            {
                //TODO: never show msg box from a worker function
                MessageBox.Show("Failed to download image\n" + ex.Message);
            }
            return null;
        }
    }
}
