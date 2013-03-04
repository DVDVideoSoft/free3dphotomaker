using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.LoggerBridge
{
    public class LoggerBridge : IDisposable
    {
        private IntPtr Handle;

        [DllImport("tier0-pinv-1.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.SysInt)]
        public static extern IntPtr LoggerCreate();
        [DllImport("tier0-pinv-1.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void LoggerRelease( IntPtr hLogger );

        [DllImport("tier0-pinv-1.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void LoggerDrawLine(IntPtr hLogger, UInt32 LogLevel, String module, String msg);

        [DllImport("tier0-pinv-1.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 LoggerGetReportLevel(IntPtr Logger);

        [DllImport("tier0-pinv-1.dll", CharSet = CharSet.Unicode, ExactSpelling = true, CallingConvention = CallingConvention.StdCall)]
        public static extern void LoggerSetReportLevel(IntPtr Logger, UInt32 LogLevel);

        public LoggerBridge()
        {
            try
            {
                SysUtils.SetEnvPathAndDllPath();
                Handle = LoggerCreate();

            } catch (Exception) {
            }
        }


        #region General Logger Functions

        public LoggerLevel GetLoggerLevel()
        {
            return (LoggerLevel)(LoggerGetReportLevel(Handle));

        }

        public bool IsLoggerLevelEnabled(LoggerLevel loggerLevel)
        {
            if (GetLoggerLevel() >= loggerLevel)
                return true;
            return false;

        }

        public bool IsOffLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Off);

        }

        public bool IsFatalLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Fatal);

        }

        public bool IsErrorLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Error);

        }

        public bool IsWarningLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Warning);

        }

        public bool IsInfoLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Info);

        }

        public bool IsDebugLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Debug);

        }

        public bool IsTraceLevelEnabled()
        {
            return IsLoggerLevelEnabled(LoggerLevel.Trace);

        }

        public void WriteLoggerLine(
                        LoggerLevel loggerLevel,
                        string module,
                        string message,
                        ulong tag)
        {
            if (module.Length == 0)
                module = ".NET";
            LoggerDrawLine(Handle, ((UInt32)loggerLevel) , module, message);

        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            try
            {
                LoggerRelease(Handle);

            }
            catch (Exception)
            {
            }
        }

        #endregion
    }
}