using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DVDVideoSoft.Utils
{
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int X;
        public int Y;

        public POINT(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public static implicit operator System.Drawing.Point(POINT p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        public static implicit operator POINT(System.Drawing.Point p)
        {
            return new POINT(p.X, p.Y);
        }
    }
        
    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINT ptReserved;
        public POINT ptMaxSize;
        public POINT ptMaxPosition;
        public POINT ptMinTrackSize;
        public POINT ptMaxTrackSize;
    }

    public class WinApiGraphics
    {
        public const uint MONITOR_DEFAULTTONULL = 0;
        public const uint MONITOR_DEFAULTTOPRIMARY = 1;
        public const uint MONITOR_DEFAULTTONEAREST = 2;

        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);

        [DllImport("user32.dll")]
        public static extern bool GetMonitorInfo(IntPtr hMonitor, ref WinApiGraphics.MONITORINFO lpmi);

        static public Size GetScreenSize()
        {
            User32.DEVMODE dm = new User32.DEVMODE();
            dm.dmDeviceName = new String(new char[32]);
            dm.dmFormName = new String(new char[32]);
            dm.dmSize = (short)Marshal.SizeOf(dm);

            if (0 != User32.EnumDisplaySettings(null, User32.ENUM_CURRENT_SETTINGS, ref dm))
                return new Size(dm.dmPelsWidth, dm.dmPelsHeight);

            return new Size(0, 0);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MONITORINFO
        {
            /// <summary>
            /// The size, in bytes, of the structure. Set this member to sizeof(MONITORINFO) (40) before calling the GetMonitorInfo function. 
            /// Doing so lets the function determine the type of structure you are passing to it.
            /// </summary>
            public int Size;

            /// <summary>
            /// A RECT structure that specifies the display monitor rectangle, expressed in virtual-screen coordinates. 
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT Monitor;

            /// <summary>
            /// A RECT structure that specifies the work area rectangle of the display monitor that can be used by applications, 
            /// expressed in virtual-screen coordinates. Windows uses this rectangle to maximize an application on the monitor. 
            /// The rest of the area in rcMonitor contains system windows such as the task bar and side bars. 
            /// Note that if the monitor is not the primary display monitor, some of the rectangle's coordinates may be negative values.
            /// </summary>
            public RECT WorkArea;

            /// <summary>
            /// The attributes of the display monitor.
            /// 
            /// This member can be the following value:
            ///   1 : MONITORINFOF_PRIMARY
            /// </summary>
            public uint Flags;

            public void Init()
            {
                this.Size = 40;
            }
        }

        ///// <summary>
        ///// The RECT structure defines the coordinates of the upper-left and lower-right corners of a rectangle.
        ///// </summary>
        ///// <see cref="http://msdn.microsoft.com/en-us/library/dd162897%28VS.85%29.aspx"/>
        ///// <remarks>
        ///// By convention, the right and bottom edges of the rectangle are normally considered exclusive. 
        ///// In other words, the pixel whose coordinates are ( right, bottom ) lies immediately outside of the the rectangle. 
        ///// For example, when RECT is passed to the FillRect function, the rectangle is filled up to, but not including, 
        ///// the right column and bottom row of pixels. This structure is identical to the RECTL structure.
        ///// </remarks>
        //[StructLayout(LayoutKind.Sequential)]
        //public struct RectStruct
        //{
        //    /// <summary>
        //    /// The x-coordinate of the upper-left corner of the rectangle.
        //    /// </summary>
        //    public int Left;

        //    /// <summary>
        //    /// The y-coordinate of the upper-left corner of the rectangle.
        //    /// </summary>
        //    public int Top;

        //    /// <summary>
        //    /// The x-coordinate of the lower-right corner of the rectangle.
        //    /// </summary>
        //    public int Right;

        //    /// <summary>
        //    /// The y-coordinate of the lower-right corner of the rectangle.
        //    /// </summary>
        //    public int Bottom;
        //}
    }
}
