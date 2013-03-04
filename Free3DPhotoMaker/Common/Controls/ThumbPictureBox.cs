using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.Controls
{
    public partial class ThumbPictureBox : UserControl
    {
        private string time;

        private Graphics graphics = null;

        private int timeRenderingPanelWidth = 30;
        private static Font timeFont;
        
        private Graphics GetGraphics()
        {
            if (this.graphics == null)
                this.graphics = CreateGraphics();
            return this.graphics;
        }

        /// <summary>
        /// The radius for the button corners. The  greater this value is, the more 'smooth' the corners are.
        /// This property should not be greater than half of the controls height.
        /// </summary>
        [Category("Appearance"),
         DefaultValue("1:00")]
        public string Time
        {
            get { return this.time; }

            set
            {
                this.time = value ?? "0:00";
                if (this.time.StartsWith("00:"))
                    this.time = value.Substring(3, value.Length - 3);
                this.timeRenderingPanelWidth = (int)GetGraphics().MeasureString(this.time, this.Font).Width + 4;
            }
        }

        public Font TimeFont
        {
            get { return timeFont; }
            set 
            { 
                timeFont = (value == null) ? new Font("Arial", 7.5f, FontStyle.Bold) : value; 
            }
        }

        public ThumbPictureBox()
        {
            this.time = "1:00";
            TimeFont = new Font("Arial", 7.5f, FontStyle.Bold);

            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = Color.WhiteSmoke;
            this.BackgroundImageLayout = ImageLayout.Zoom;
        }

        public Image Image
        {
            get { return this.BackgroundImage; }
            set { this.BackgroundImage = value; }
        }

        private void ThumbPictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (this.BackgroundImage != null)
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(120, 0, 0, 0)),
                                         new Rectangle(this.Width - timeRenderingPanelWidth,
                                                           this.Height - 16,
                                                           timeRenderingPanelWidth,
                                                           16));
                e.Graphics.DrawString(this.Time, TimeFont, Brushes.White, this.Width - timeRenderingPanelWidth + 3, this.Height - 16 + 2);
            }
        }
    }
}
