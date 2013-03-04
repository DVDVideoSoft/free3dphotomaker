using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class NativeMethods
    {
        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr OpenThemeData(IntPtr hWnd, String classList);

        [DllImport("uxtheme.dll", ExactSpelling = true)]
        public extern static int IsThemeActive();

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        public static extern int SetWindowTheme(IntPtr hWnd, String pszSubAppName, String pszSubIdList);

        public enum CSIDL
        {
            MYMUSIC = 13,
            MYVIDEO = 14,
            ADMINTOOLS = 0x30,
            ALTSTARTUP = 0x1d,
            APPDATA = 0x1a,
            BITBUCKET = 10,
            CDBURN_AREA = 0x3b, 
            COOKIES = 0x21,
            COMMON_ADMINTOOLS = 0x2f,
            COMMON_ALTSTARTUP = 30,
            COMMON_APPDATA = 0x23,
            COMMON_DESKTOPDIRECTORY = 0x19,
            COMMON_DOCUMENTS = 0x2e,
            COMMON_FAVORITES = 0x1f,
            COMMON_MUSIC = 0x35,
            COMMON_PICTURES = 0x36,
            COMMON_PROGRAMS = 0x17

        }

        public static string GetFolderPath(CSIDL folder)
        {
            StringBuilder sb = new StringBuilder(260);
            SHGetFolderPath(IntPtr.Zero, (int)folder, IntPtr.Zero, 0x0000, sb);
            return sb.ToString();
        }

        [DllImport("shfolder.dll", CharSet = CharSet.Auto)]
        internal static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, StringBuilder lpszPath);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool WritePrivateProfileString(string lpAppName, string lpKeyName, string lpString, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, int nSize, string lpFileName);
    }
}
