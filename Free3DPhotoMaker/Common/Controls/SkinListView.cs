using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.Utils;

namespace DVDVideoSoft.Controls
{
    public partial class SkinListView : ListView, IDvsControl
    {
        // Event declaration
        public delegate void SkinListViewScrollDelegate(object Sender, SkinListViewScrollArgs e);
        public event SkinListViewScrollDelegate Scroll;

        private bool osIsLessThanVista;
        private Bitmap background;
        private Bitmap emptyBackground = new Bitmap(1, 1);

        protected bool hasBackgroundImage = false;
        public bool HasBackgroundImage { get { return this.hasBackgroundImage; } }

        protected bool drawBackgroundImage = true;
        public bool DrawBackgroundImage
        {
            get { return this.drawBackgroundImage; }
            set
            {
                if (!value && this.HasBackgroundImage)
                {
                    this.SetBackground(this.emptyBackground);
                }
                else if (value && this.background != null)
                {
                    this.SetBackground(this.background);
                }

                this.drawBackgroundImage = value;
            }
        }

        protected override void WndProc(ref Message m)
        {
            // Trap the WM_VSCROLL message to generate the Scroll event
            base.WndProc(ref m);
            if (m.Msg == WinApi.WM_VSCROLL)
            {
                int nfy = m.WParam.ToInt32() & 0xFFFF;
                if (Scroll != null && (nfy == WinApi.SB_THUMBTRACK || nfy == WinApi.SB_ENDSCROLL))
                    Scroll(this, new SkinListViewScrollArgs(nfy == WinApi.SB_THUMBTRACK));
            }
            else if (m.Msg == WinApi.WM_MOUSEWHEEL)
            {
                //Scroll(this, new SkinListViewScrollArgs(false));
            }
        }
        public class SkinListViewScrollArgs
        {
            // Scroll event argument
            private bool mTracking;
            public SkinListViewScrollArgs(bool tracking)
            {
                mTracking = tracking;
            }

            public bool Tracking { get { return mTracking; } }
        }

        //Messages To load background Image
        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref int lParam);

        [DllImport("User32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, ref LVBKIMAGE lParam);

        // Structure to hold the background image information that will be loaded
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct LVBKIMAGE
        {
            public ListViewBkImageFlags ulFlags;
            public IntPtr hbm;
            public IntPtr pszImage;
            public int cchImageMax;
            public int xOffsetPercent;
            public int yOffsetPercent;
        }

        // flags that are used to set what kind of background am I loading
        [Flags]
        public enum ListViewBkImageFlags
        {
            LVBKIF_SOURCE_NONE = 0x00000000,
            LVBKIF_SOURCE_HBITMAP = 0x00000001,
            LVBKIF_SOURCE_URL = 0x00000002,
            LVBKIF_SOURCE_MASK = 0x00000003,
            LVBKIF_STYLE_NORMAL = 0x00000000,
            LVBKIF_STYLE_TILE = 0x00000010,
            LVBKIF_STYLE_MASK = 0x00000010,
            LVBKIF_FLAG_TILEOFFSET = 0x00000100,
            LVBKIF_TYPE_WATERMARK = 0x10000000,
            LVBKIF_FLAG_ALPHABLEND = 0x20000000
        }

        // message flags for the system to know what event am I hooking into
        enum Messages
        {
            CLR_NONE = -1,
            LVM_FIRST = 0x1000,
            LVM_SETBKIMAGEW = LVM_FIRST + 138,
            LVM_GETBKIMAGEW = LVM_FIRST + 139,
            LVM_SETTEXTBKCOLOR = (LVM_FIRST + 38),
            LVM_SETBKIMAGE = (LVM_FIRST + 68)
        }

        // Constructor to make listview double buffered so that there is no flicker on resize
        public SkinListView()
        {
            //Activate double buffering if possible. There is a some rendering trouble on XP
            this.osIsLessThanVista = System.Environment.OSVersion.Version.Major < 6;

            if (this.osIsLessThanVista)
                this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            else
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = false; //!xpCompatible; //TODO
        }

        public void Refresh(bool force)
        {
            if ((this.osIsLessThanVista && this.hasBackgroundImage) || force)
            {
                this.Refresh();
            }
        }

        public void SetBackground(Bitmap background)
        {
            if (background == null)
            {
                this.background = background;
                //this.hasBackgroundImage = false;
                this.DrawBackgroundImage = false;
                return;
            }
            
            // creates API item and loads it into listview's background
            LVBKIMAGE apiItem = new LVBKIMAGE();
            apiItem.ulFlags = ListViewBkImageFlags.LVBKIF_STYLE_NORMAL;//ListViewBkImageFlags.LVBKIF_TYPE_WATERMARK;

            this.hasBackgroundImage = background != null;
            if (this.hasBackgroundImage && background != this.emptyBackground) // we shall not remember an empty background
                this.background = background;

            apiItem.ulFlags = ListViewBkImageFlags.LVBKIF_SOURCE_HBITMAP | ListViewBkImageFlags.LVBKIF_STYLE_NORMAL;
            //				the following would be used if you wanted to load a background image that is not fixed
            //				apiItem.ulFlags = ListViewBkImageFlags.LVBKIF_SOURCE_HBITMAP | ListViewBkImageFlags.LVBKIF_STYLE_NORMAL;
            apiItem.hbm = background.GetHbitmap();
            apiItem.xOffsetPercent = 100;
            apiItem.yOffsetPercent = 100;

            // Set the background color of the ListView to 0XFFFFFFFF (-1) so it will be transparent
            int clear = (int)Messages.CLR_NONE;
            SendMessage(this.Handle, (int)Messages.LVM_SETTEXTBKCOLOR, 0, ref clear);
            SendMessage(this.Handle, (int)Messages.LVM_SETBKIMAGE, 0, ref apiItem);
        }

        public void OnSystemThemeChanged(bool visualStylesEnabled)
        {
        }
    }
}
