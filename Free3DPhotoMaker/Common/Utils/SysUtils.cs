using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace DVDVideoSoft.Utils
{
    public class SysUtils
    {
        [DllImport("kernel32", EntryPoint = "GetVolumeInformationW", CharSet = CharSet.Ansi, SetLastError = true)]
        private static extern bool GetVolumeInformationW(
                [MarshalAs(UnmanagedType.LPWStr)]string rootPathName,
                [MarshalAs(UnmanagedType.LPArray)]ushort[] volumeNameBuffer,
                UInt32 volumeNameSize,
                [MarshalAs(UnmanagedType.U4)]ref UInt32 volumeSerialNumber,
                [MarshalAs(UnmanagedType.U4)]ref UInt32 maximumComponentLength,
                [MarshalAs(UnmanagedType.U4)]ref UInt32 fileSystemFlags,
                [MarshalAs(UnmanagedType.LPArray)]ushort[] fileSystemNameBuffer,
                uint fileSystemNameSize);

        public static ulong GetDiskSerialNumber(string path)
        {
            ulong serialNumber = 0;

            UInt32 bufferSize = 1024;
            ushort[] volumeNameBuffer = new ushort[bufferSize];
            ushort[] fileSystemNameBuffer = new ushort[bufferSize];
            UInt32 volumeSerialNumber = 0;
            UInt32 maximumComponentLength = 0;
            UInt32 fileSystemFlags = 0;

            bool res = GetVolumeInformationW(
                                    Path.GetPathRoot(path),
                                    volumeNameBuffer,
                                    bufferSize,
                                    ref volumeSerialNumber,
                                    ref maximumComponentLength,
                                    ref fileSystemFlags,
                                    fileSystemNameBuffer,
                                    bufferSize);
            if (res)
                serialNumber = volumeSerialNumber;

            return serialNumber;
        }

        #region OS related

        public static bool IsUacEnabled()
        {
            try {
                RegistryKey key = Registry.LocalMachine;
                key = key.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", false);
                return (int)key.GetValue("EnableLUA") == 1;
            } catch { }

            return true;
        }

        public static bool IsOS64
        {
            get
            {
                string pa = Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE", EnvironmentVariableTarget.Machine);
                if (!string.IsNullOrEmpty(pa))
                    return string.Compare(pa, 0, "x86", 0, 3, true) != 0;
                return false;
            }
        }

        #endregion

        #region Hardware related

        public static string GetCpuInfo()
        {
            RegistryKey key = Registry.LocalMachine;
            key = key.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor\\0", false);
            Object vendor = key.GetValue("VendorIdentifier");
            Object cpuName = key.GetValue("ProcessorNameString");
            Object cpuIdentifier = key.GetValue("Identifier");
            Object cpuSpeed = key.GetValue("~MHz");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} [{1}] ({2} MHz)", cpuName, cpuIdentifier, cpuSpeed);

            key = Registry.LocalMachine.OpenSubKey("HARDWARE\\DESCRIPTION\\System\\CentralProcessor", false);
            if (key.SubKeyCount > 1)
                sb.AppendFormat(" - {0} units", key.SubKeyCount);
            return sb.ToString();
        }

        #endregion

        #region Software related

        public static void SetEnvPathAndDllPath()
        {
            // Set dvs common dll path
            string strPathsToAdd = FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonLib);

            try
            {
                string sBuf = Regedit.ReadString(true, Programs.DVS_REG_KEY, FileUtils.DvsFolderType.SharedDir.ToString(), "");
                if (!string.IsNullOrEmpty(sBuf))
                    strPathsToAdd = Path.Combine(sBuf, "lib");
                WinApi.SetDllDirectory(strPathsToAdd.Split(';')[0]);
            }
            catch { }

            if (!strPathsToAdd.EndsWith(";"))
                strPathsToAdd += ";";
            string strPath = Environment.GetEnvironmentVariable("PATH");
            Environment.SetEnvironmentVariable("PATH", strPathsToAdd + strPath, EnvironmentVariableTarget.Process);
        }

        public static string GetBrowsersInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("ff {0} | chr {1} | ie {2}", GetMozillaInfo(), GetChromeInfo(), GetIEInfo());
            return sb.ToString();
        }

        private static string GetMozillaInfo()
        {
            try {
                RegistryKey key = Registry.LocalMachine;
                key = key.OpenSubKey("SOFTWARE\\Mozilla\\Mozilla Firefox", false);
                //key = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Mozilla\\Mozilla Firefox", false);
                Object version = key.GetValue("CurrentVersion");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", version);
                return sb.ToString();
            } catch (Exception) {
                return "";
            }
        }

        private static string GetIEInfo()
        {
            try {
                RegistryKey key = Registry.LocalMachine;
                key = key.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer", false);
                //key = key.OpenSubKey("SOFTWARE\\Wow6432Node\\Mozilla\\Mozilla Firefox", false);
                Object version = key.GetValue("Version");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", version);
                return sb.ToString();
            } catch (Exception) {
                return "";
            }
        }

        private static string GetChromeInfo()
        {
            try {
                RegistryKey key = Registry.CurrentUser;
                key = key.OpenSubKey("SOFTWARE\\MICROSOFT\\Windows\\CurrentVersion\\Uninstall\\Google Chrome", false);
                Object displayName = key.GetValue("DisplayName");
                Object version = key.GetValue("Version");
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", version);
                return sb.ToString();
            } catch (Exception) {
                return "";
            }
        }

        public static bool CheckForNetFx35()
        {
            Regedit reg30 = new Regedit(Regedit.HKEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0", false);
            string netFx30Version = Regedit.ReadString(true, @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.0", "Version", "");
            string netFx35Version = Regedit.ReadString(true, @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v3.5", "Version", "");
            int subVersion;

            if (!string.IsNullOrEmpty(netFx30Version)) {
                int.TryParse(netFx30Version.Substring(4, 5), out subVersion);
                if (subVersion >= 30729)
                    return true;
            }
            if (!string.IsNullOrEmpty(netFx35Version)) {
                int.TryParse(netFx35Version.Substring(4, 5), out subVersion);
                if (subVersion == 30729)
                    return true;
            }
            return false;
        }
        #endregion

        #region COM related

        public static bool CheckInprocServer(string Clsid)
        {
            string Key;
            Key = "CLSID\\" + Clsid + "\\InprocServer32";

            try {
                Microsoft.Win32.RegistryKey hKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(Key, false);

                if (hKey != null) {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(hKey.GetValue("") as string);
                    hKey.Close();
                    return fileInfo.Exists;
                }
            } catch { }

            return false;
        }

        #endregion
    }
}
