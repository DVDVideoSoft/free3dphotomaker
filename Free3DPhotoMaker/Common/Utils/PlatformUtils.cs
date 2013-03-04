using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text;

namespace DVDVideoSoft.Utils
{
    [Serializable]
    public enum ImageType
    {
        Bitmap = 0,
        Icon = 1,
        Cursor = 2,
        EnhMetafile = 3,
    }

    [Serializable, Flags]
    public enum LoadImageFlags
    {
        DefaultColor = 0x0,
        Monochrome = 0x1,
        Color = 0x2,
        CopyReturnOriginal = 0x4,
        CopyDeleteOriginal = 0x8,
        LoadFromFile = 0x10,
        LoadTransparent = 0x20,
        DefaultSize = 0x40,
        VgaColor = 0x80,
        LoadMap3DColors = 0x1000,
        CreateDibSection = 0x2000,
        CopyFromResource = 0x4000,
        Shared = 0x8000,
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        //[MarshalAs(UnmanagedType.TBStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        //[MarshalAs(UnmanagedType.LPStr, SizeConst = 80)]
        //public byte[] szTypeName;
        public string szTypeName;
    };

    public class DVDVideoSoftMutex : IDisposable
    {
        public static readonly string baseName = "Global\\DVDVIDEOSOFT";
        private string name;
        private IntPtr mutexHandle;

        public DVDVideoSoftMutex(string name)
        {
            this.name = baseName;
            if (!string.IsNullOrEmpty(name))
                this.name = this.name + "_" + name.ToUpper();

            this.mutexHandle = WinApi.CreateMutex(new IntPtr(0), true, this.name);
        }

        public void Dispose()
        {
            if (this.mutexHandle != (IntPtr)0)
            {
                WinApi.ReleaseMutex(this.mutexHandle);
                this.mutexHandle = (IntPtr)0;
            }
        }
    }
}
