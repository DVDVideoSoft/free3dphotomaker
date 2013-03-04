using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DVDVideoSoft.LoggerBridge.Win32Interop
{
    internal class Win32SysUtils
    {
        [DllImport("kernel32", EntryPoint = "LoadLibrary", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32.dll", EntryPoint = "FreeLibrary", SetLastError = true)]
        public static extern bool FreeLibrary(IntPtr module);

        [DllImport("kernel32", EntryPoint = "GetProcAddress", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetProcAddress(IntPtr module, string procName);
    }
}