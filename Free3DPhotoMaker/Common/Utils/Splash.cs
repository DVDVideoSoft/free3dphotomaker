using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DVDVideoSoft.Utils
{
    public class Splash : Form
    {
        //private static Color textColor = Color.FromArgb(0x69, 0x69, 0x69);
        private static Color textColor = Color.White;
        public static Splash ShowSplash(string appID, Bitmap src)
        {
            Splash splash;
            try
            {
                //Bitmap src = DVDVideoSoft.Resources.Properties.Resources.ResourceManager.GetObject(appID + "Splash") as Bitmap;
                Bitmap img = new Bitmap(src);
                Graphics g = Graphics.FromImage(img);
                //g.DrawString(appHumanName.ToUpper(), new Font("Tahoma", 10.0f), new SolidBrush(Color.Wheat), new PointF(111, 106));
                g.DrawString("Loading components ...", new Font("Tahoma", 8.0f), new SolidBrush(textColor), new PointF(20, 204));
                splash = new Splash(appID, img);
                splash.BackgroundImage = img;
                splash.SetBits(img,
                               (Screen.PrimaryScreen.Bounds.Width - splash.BackgroundImage.Width) / 2,
                               (Screen.PrimaryScreen.Bounds.Height - splash.BackgroundImage.Height) / 2 - 10);
                //splash.CreateGraphics().DrawString("Free Video to Flash Converter", new Font("Tahoma", 14.7f), Brushes.Wheat, new PointF(20, 20));
            }
            catch { return null; }

            splash.Show();
            return splash;
        }

        public static Splash ShowSplash(string appID, Bitmap src, Color txtColor)
        {
            textColor = txtColor;
            return ShowSplash(appID, src);
        }

        private Splash(string appID, Bitmap img)
        {
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.Black;
            this.ShowInTaskbar = false;
            this.ShowIcon = false;
            this.TopMost = true;
            this.Size = img.Size;
            this.Paint += new PaintEventHandler(Splash_Paint);
        }

        public void AddProgress()
        {
            try
            {
                Bitmap src = this.BackgroundImage as Bitmap;
                Bitmap img = new Bitmap(src);
                Graphics g = Graphics.FromImage(img);
                g.DrawString("...", new Font("Tahoma", 8.0f), new SolidBrush(textColor), new PointF(135, 204));
                this.SetBits(img,
                               (Screen.PrimaryScreen.Bounds.Width - this.BackgroundImage.Width) / 2,
                               (Screen.PrimaryScreen.Bounds.Height - this.BackgroundImage.Height) / 2 - 10);
            }
            catch { }
        }

        void Splash_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Brushes.White, new Rectangle(0, 0, 300, 300));
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.UserPaint, true);
            UpdateStyles();

            base.OnHandleCreated(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParms = base.CreateParams;
                cParms.ExStyle |= 0x00080000; // WS_EX_LAYERED
                return cParms;
            }
        }

        public void SetBits(Bitmap bitmap, int x, int y)
        {
            if (!Bitmap.IsCanonicalPixelFormat(bitmap.PixelFormat) || !Bitmap.IsAlphaPixelFormat(bitmap.PixelFormat))
                throw new ApplicationException("The picture must be 32bit picture with alpha channel.");

            IntPtr oldBits = IntPtr.Zero;
            IntPtr screenDC = WinApi.GetDC(IntPtr.Zero);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr memDc = WinApi.CreateCompatibleDC(screenDC);

            try
            {
                WinApi.Point topLoc = new WinApi.Point(x, y);
                WinApi.Size bitMapSize = new WinApi.Size(bitmap.Width, bitmap.Height);
                WinApi.BLENDFUNCTION blendFunc = new WinApi.BLENDFUNCTION();
                WinApi.Point srcLoc = new WinApi.Point(0, 0);

                hBitmap = bitmap.GetHbitmap(Color.FromArgb(0));
                oldBits = WinApi.SelectObject(memDc, hBitmap);

                blendFunc.BlendOp = WinApi.AC_SRC_OVER;
                blendFunc.SourceConstantAlpha = 255;
                blendFunc.AlphaFormat = WinApi.AC_SRC_ALPHA;
                blendFunc.BlendFlags = 0;

                WinApi.UpdateLayeredWindow(Handle, screenDC, ref topLoc, ref bitMapSize, memDc, ref srcLoc, 0, ref blendFunc, WinApi.ULW_ALPHA);
            }
            finally
            {
                if (hBitmap != IntPtr.Zero)
                {
                    WinApi.SelectObject(memDc, oldBits);
                    WinApi.DeleteObject(hBitmap);
                }
                WinApi.ReleaseDC(IntPtr.Zero, screenDC);
                WinApi.DeleteDC(memDc);
            }
        }
    }
}
