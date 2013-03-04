using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace DVDVideoSoft.Utils
{
    public static class SystemHelper
    {
        public enum ETranscoderAvailability
        {
            kUnknown = -1,
            kReady = 0,
            kNoDriverFound = 1,
            kNoGPUsFound,
            kUnsupported,
            kNoAmdCodecs,
            kNoKLite,
            kReserved,
        };

        //////////////////////////////////////////////////////////////////////
        [DllImport("wbrhelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        private static extern int getDefaultBrowser();

        [DllImport("wbrhelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern bool getCookiesFolder([MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszOut);

        [DllImport("wbrhelper.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
        public static extern int getProxySettings([MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszValue, ref int nAutoConfigurationMode);

        [DllImport("dvssyshelper.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern int testNvGPU(ref GPUInfo info);

        [DllImport("dvssyshelper.dll", CallingConvention = CallingConvention.StdCall, ExactSpelling=true)]
        public static extern int testAtiGPU(ref GPUInfo info);

        //////////////////////////////////////////////////////////////////////

        public const int DVS_GENERIC_STRING_MAX = 4096;
        public const int DVS_LONG_STRING_MAX = 256;
        public const int DVS_SHORT_STRING_MAX = 64;

        private static string s_strLastError;

        public static string GetLastError()
        {
            return s_strLastError;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct GPUInfo
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = DVS_SHORT_STRING_MAX)]
            public string Name;
            public int Cores;
        }

        private static ETranscoderAvailability GetCudaAvailability()
        {
            ETranscoderAvailability transcoderAvailability = ETranscoderAvailability.kUnknown;
            GPUInfo info = new GPUInfo();
            try
            {
                transcoderAvailability = (ETranscoderAvailability)SystemHelper.testNvGPU(ref info);
            }
            catch (Exception ex)
            {
                s_strLastError = "Failed to testNvGPU(): " + ex.Message;
            }
            return transcoderAvailability;
        }

        private static ETranscoderAvailability GetAtiAvailability(ILogWriter ilog = null)
        {
            ETranscoderAvailability transcoderAvailability = ETranscoderAvailability.kUnknown;
            GPUInfo info = new GPUInfo();
            try
            {
                transcoderAvailability = (ETranscoderAvailability)SystemHelper.testAtiGPU(ref info);
            }
            catch (Exception ex)
            {
                s_strLastError = "Failed to testAtiGPU(): " + ex.Message;
            }

            if (transcoderAvailability == ETranscoderAvailability.kReady)
                transcoderAvailability = CheckCodeckPackForAti();

            return transcoderAvailability;
        }

        public static ETranscoderAvailability CheckCodeckPackForAti()
        {
            //ATIDrivers MUST be installed;
            string atiDriversKey = @"CLSID\{758C0F02-DF95-11D2-8E75-00104B93CF06}";
            //This is optional componets;
            string kLiteKey      = @"CLSID\{04FE9017-F873-410E-871E-AB91661A4EF7}";
            string lavFilterKey  = @"CLSID\{EE30215D-164F-4A92-A4EB-9D4C13390F9F}";

            //Check for the codecs: if (atiDriversKey exist and (kLiteKey exist or lavFilterKey exist))
            Regedit reg;
            try
            {
                reg = new Regedit(Regedit.HKEY.HKEY_CLASSES_ROOT, atiDriversKey, false);
                if (reg.Open(false))
                {
                    reg.Close();
                    try
                    {
                        reg = new Regedit(Regedit.HKEY.HKEY_CLASSES_ROOT, kLiteKey, false);
                        if (reg.Open(false))
                            return ETranscoderAvailability.kReady;
                    }
                    catch (Exception)
                    {
                        reg = new Regedit(Regedit.HKEY.HKEY_CLASSES_ROOT, lavFilterKey, false);
                        if (reg.Open(false))
                            return ETranscoderAvailability.kReady;
                    }
                    return ETranscoderAvailability.kNoKLite;
                }

            }
            catch { }
            return ETranscoderAvailability.kNoAmdCodecs;
        }

        public static ETranscoderAvailability GetTranscoderAvailability(DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder transcoder)
        {
            s_strLastError = "";

            if (transcoder == DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_ATI)
                return GetAtiAvailability();
            else if (transcoder == DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_CUDA)
                return GetCudaAvailability();

            return ETranscoderAvailability.kUnknown;
        }

        public static BrowserType GetDefaultBrowser()
        {
            BrowserType nRet = BrowserType.UnknownBrowser;
            try
            {
                nRet = (BrowserType)getDefaultBrowser();
            }
            catch (Exception ex)
            {
                string s = ex.Message;
            }
            return nRet;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetCookiesFolder()
        {
            StringBuilder sb = new StringBuilder(1000);
            try
            {
                bool ret = getCookiesFolder(sb);
                return sb.ToString();
            }
            catch (Exception /*ex*/)
            {
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static int GetProxySettings(out string value, out int nAutoConfigurationMode)
        {
            int ret = -1;
            StringBuilder sb = new StringBuilder(80);
            nAutoConfigurationMode = 0;
            try
            {
                ret = getProxySettings(sb, ref nAutoConfigurationMode);
                value = sb.ToString();
            }
            catch (Exception /*ex*/)
            {
                //string s = ex.Message;
                ret = -3;
                value = null;
            }
            return ret;
        }
    }
}
