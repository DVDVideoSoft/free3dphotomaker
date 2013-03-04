using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DVDVideoSoft.Controls
{
    public static class ControlExtensions
    {
        public static void AdjustOnBackgroundChange(PictureBox pictureBox, bool finalStage)
        {
            pictureBox.Visible = finalStage;
            if (finalStage)
                pictureBox.BackColor = Color.Transparent;
            else
                pictureBox.BackColor = pictureBox.Parent.BackColor;
        }
    }
}
