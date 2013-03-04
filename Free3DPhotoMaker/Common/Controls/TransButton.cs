using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class TransButton : UserControl
    {
        Image img1 = Bitmap.FromFile("c:\\DownloadCompleteState.png");
        Image img2 = Bitmap.FromFile("c:\\DownloadCompleteState_s.png");

        public TransButton()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.None;
        }

        private bool alternateState;
        public bool AlternateState
        {
            get { return this.alternateState; }
            set
            {
                this.alternateState = value;
                this.BackgroundImage = value ? img2 : img1;
            }
        }

        private void TransButton_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Aquamarine, 3, 3, 6, 9);
        }
    }
}
