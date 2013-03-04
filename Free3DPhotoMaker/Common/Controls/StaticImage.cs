using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class StaticImage : UserControl
    {
        //Image img1 = Bitmap.FromFile("c:\\DownloadCompleteState.png");
        //Image img2 = Bitmap.FromFile("c:\\DownloadCompleteState_s.png");

        private Image image;
        private Image imageAlt;
        private bool alternateState;

        public StaticImage()
        {
            InitializeComponent();
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.Transparent;
            this.BackgroundImageLayout = ImageLayout.None;
        }

        [Category("Image"), DefaultValue(null),
         Description("To draw the normal or an alternate state image."),
         System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        public bool AlternateState
        {
            get { return this.alternateState; }
            set
            {
                this.alternateState = value;
                this.BackgroundImage = value ? this.imageAlt : this.image;
            }
        }

        [Category("Image"), DefaultValue(null),
         Description("The normal state image."),
         System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        public Image Image
        {
            get { return this.image; }
            set
            {
                this.image = value;
                this.Invalidate();
            }
        }

        [Category("Image"), DefaultValue(null),
         Description("The alternate state image."),
         System.ComponentModel.RefreshProperties(RefreshProperties.Repaint)]
        public Image ImageAlt
        {
            get { return this.imageAlt; }
            set
            {
                this.imageAlt = value;
                this.Invalidate();
            }
        }

        #region Public members

                public void X(bool finalStage)
                {
                    this.Visible = finalStage;
                    if (finalStage)
                        this.BackColor = Color.Transparent;
                    else
                        this.BackColor = this.Parent.BackColor;
                }

        #endregion

        private void TransButton_Paint(object sender, PaintEventArgs e)
        {
            //e.Graphics.FillRectangle(Brushes.Aquamarine, 3, 3, 6, 9);
        }
    }
}
