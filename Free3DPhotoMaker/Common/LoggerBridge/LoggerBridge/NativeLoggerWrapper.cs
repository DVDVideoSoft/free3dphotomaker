using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

using DVDVideoSoft.Utils;
using DVDVideoSoft.LoggerBridge.Win32Interop;

namespace DVDVideoSoft.LoggerBridge
{
    internal class NativeLoggerWrapper
    {
        //////////////////////////////////////////////////////////////////////////////////////////////////
        // Start of delegate's definition
        //////////////////////////////////////////////////////////////////////////////////////////////////

        private delegate int GetLoggerLevelInvoker();
        private delegate bool IsLoggerLevelEnabledInvoker(int logLevel);
        private delegate bool IsLoggerStartedInvoker();
        private delegate bool InitLoggerInvoker();
        private delegate void FreeLoggerInvoker();
        private delegate void WriteLoggerLineInvoker(
                                int logLevel,
                                [MarshalAs(UnmanagedType.LPWStr)]string module,
                                [MarshalAs(UnmanagedType.LPWStr)]string message,
                                ulong tag);

        //////////////////////////////////////////////////////////////////////////////////////////////////
        // End of delegate's definition
        //////////////////////////////////////////////////////////////////////////////////////////////////

        private volatile object serverStartedLocker = new object();

        private IntPtr hDllModule;
        private bool serverStarted;
        private string loggerDllPath;

        // Native delegates
        private NativeProcedureHolder<GetLoggerLevelInvoker> getLoggerLevelDelegate;
        private NativeProcedureHolder<IsLoggerLevelEnabledInvoker> isLoggerLevelEnabledDelegate;
        private NativeProcedureHolder<IsLoggerStartedInvoker> isLoggerStartedDelegate;
        private NativeProcedureHolder<InitLoggerInvoker> initLoggerDelegate;
        private NativeProcedureHolder<FreeLoggerInvoker> freeLoggerDelegate;
        private NativeProcedureHolder<WriteLoggerLineInvoker> writeLoggerLineDelegate;

        public NativeLoggerWrapper()
        {
            this.hDllModule = new IntPtr(0);
            this.serverStarted = false;
            this.loggerDllPath = null;

            this.getLoggerLevelDelegate = new NativeProcedureHolder<GetLoggerLevelInvoker>("GetLoggerLevel");
            this.isLoggerLevelEnabledDelegate = new NativeProcedureHolder<IsLoggerLevelEnabledInvoker>("IsLoggerLevelEnabled");
            this.isLoggerStartedDelegate = new NativeProcedureHolder<IsLoggerStartedInvoker>("IsLoggerStarted");
            this.initLoggerDelegate = new NativeProcedureHolder<InitLoggerInvoker>("InitLogger");
            this.freeLoggerDelegate = new NativeProcedureHolder<FreeLoggerInvoker>("FreeLogger");
            this.writeLoggerLineDelegate = new NativeProcedureHolder<WriteLoggerLineInvoker>("WriteLoggerLine");
        }

        #region General Logger Server Functions

        public LoggerLevel GetLoggerLevel()
        {
            if (!IsStarted())
                return LoggerLevel.Off;
            GetLoggerLevelInvoker proc = this.getLoggerLevelDelegate.GetProc();
            return ConvertToLoggerLevel(proc());
        }

        public bool IsLoggerLevelEnabled(LoggerLevel loggerLevel)
        {
            if (!IsStarted())
                return false;
            IsLoggerLevelEnabledInvoker proc = this.isLoggerLevelEnabledDelegate.GetProc();
            return proc((int)loggerLevel);
        }

        public bool IsLoggerServerStarted()
        {
            if (!IsStarted())
                return false;
            IsLoggerStartedInvoker proc = this.isLoggerStartedDelegate.GetProc();
            return proc();
        }

        public bool InitLoggerServer()
        {
            if (!IsStarted())
                return false;
            InitLoggerInvoker proc = this.initLoggerDelegate.GetProc();
            return proc();
        }

        public void FreeLoggerServer()
        {
            if (!IsStarted())
                return;
            FreeLoggerInvoker proc = this.freeLoggerDelegate.GetProc();
            proc();
        }

        public void WriteLoggerLine(
                        LoggerLevel loggerLevel,
                        string module,
                        string message,
                        ulong tag)
        {
            if (!IsStarted())
                return;
            WriteLoggerLineInvoker proc = this.writeLoggerLineDelegate.GetProc();
            proc((int)loggerLevel, module, message, tag);
        }

        #endregion

        public bool IsStarted()
        {
            bool val = false;
            lock (this.serverStartedLocker)
                val = this.serverStarted;
            return val;
        }

        public void Start()
        {
            if (IsStarted())
                return;

            this.DefineLoggerDllPath();
            CheckDllExistence();

            try
            {
                LoadDll();
            }
            catch (LoggerBridgeException ex)
            {
                FreeDll();
                throw ex;
            }
            catch (Exception ex)
            {
                FreeDll();
                throw new LoggerBridgeException(LoggerBridgeException.Success, LoggerBridgeException.StartingOfServerFailed,
                    "Failed to start logging server. " + ex.Message);
            }

            lock (this.serverStartedLocker)
                this.serverStarted = true;
        }

        public void Stop()
        {
            if (!IsStarted())
                return;

            FreeDll();

            lock (this.serverStartedLocker)
                this.serverStarted = false;
        }

        private void LoadDll()
        {
            UInt32 error = 0;

            this.hDllModule = Win32SysUtils.LoadLibrary(GetLoggerDllPath());
            if (this.hDllModule.ToInt64() == 0)
            {
                error = (uint)Marshal.GetLastWin32Error();
                FreeDll();
                throw new LoggerBridgeException(error, LoggerBridgeException.DllModuleNotLoaded, "Dll '" + GetLoggerDllPath() + "' not loaded");
            }

            this.getLoggerLevelDelegate.Init(this.hDllModule);
            this.isLoggerLevelEnabledDelegate.Init(this.hDllModule);
            this.isLoggerStartedDelegate.Init(this.hDllModule);
            this.initLoggerDelegate.Init(this.hDllModule);
            this.freeLoggerDelegate.Init(this.hDllModule);
            this.writeLoggerLineDelegate.Init(this.hDllModule);
        }

        private void FreeDll()
        {
            if (this.hDllModule.ToInt64() != 0)
            {
                Win32SysUtils.FreeLibrary(this.hDllModule);
                this.hDllModule = new IntPtr(0);
            }

            this.loggerDllPath = null;

            this.getLoggerLevelDelegate.Clear();
            this.isLoggerLevelEnabledDelegate.Clear();
            this.isLoggerStartedDelegate.Clear();
            this.initLoggerDelegate.Clear();
            this.freeLoggerDelegate.Clear();
            this.writeLoggerLineDelegate.Clear();
        }

        private LoggerLevel ConvertToLoggerLevel(int loggerLevel)
        {
            switch (loggerLevel)
            {
                case (int)LoggerLevel.Off: return LoggerLevel.Off;
                case (int)LoggerLevel.Fatal: return LoggerLevel.Fatal;
                case (int)LoggerLevel.Error: return LoggerLevel.Error;
                case (int)LoggerLevel.Warning: return LoggerLevel.Warning;
                case (int)LoggerLevel.Info: return LoggerLevel.Info;
                case (int)LoggerLevel.Debug: return LoggerLevel.Debug;
                case (int)LoggerLevel.Trace: return LoggerLevel.Trace;
            }
            return LoggerLevel.Off;
        }

        private string GetNativeLoggerDllName()
        {
            return "DvsServiceBridge.dll";
        }

        private string GetDllPathByCurrentProcess()
        {
            string startupPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            return Path.Combine(startupPath, GetNativeLoggerDllName());
        }

        //private string GetDllPathByCommon()
        //{
        //    string dllPath = null;
        //    try
        //    {
        //        string commonPath = null;
        //        using (Regedit registry = new Regedit(Programs.DVS_REG_KEY, Regedit.HKEY.HKEY_LOCAL_MACHINE, false))
        //        {
        //            registry.Open(false);
        //            commonPath = registry.GetValue(Programs.RVN.SharedDir.ToString(), string.Empty);
        //        }
        //        if (!string.IsNullOrEmpty(commonPath))
        //        {
        //            if (Directory.Exists(commonPath))
        //            {
        //                dllPath = Path.Combine(
        //                    FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonLib),
        //                    GetNativeLoggerDllName());
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //    return dllPath;
        //}

        private string GetDllPathByNativeCommon()
        {
            string dllPath = null;
            try
            {
                string commonProgramFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles);
                string commonPath = Path.Combine(commonProgramFilesPath, Programs.CompanyName);
                commonPath = Path.Combine(commonPath, "lib");
                dllPath = Path.Combine(commonPath, GetNativeLoggerDllName());
            }
            catch (Exception)
            {
                return null;
            }
            return dllPath;
        }

        private void DefineLoggerDllPath()
        {
            do
            {
                string dllPath = null;
                //
                dllPath = Path.Combine(FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonLib),
                                        GetNativeLoggerDllName()); //GetDllPathByCommon();
                if (!string.IsNullOrEmpty(dllPath)) {
                    if (File.Exists(dllPath)) {
                        this.loggerDllPath = dllPath;
                        break;
                    }
                }
                //
                dllPath = GetDllPathByNativeCommon();
                if (!string.IsNullOrEmpty(dllPath)) {
                    if (File.Exists(dllPath)) {
                        this.loggerDllPath = dllPath;
                        break;
                    }
                }
                //
                dllPath = GetDllPathByCurrentProcess();
                if (!string.IsNullOrEmpty(dllPath)) {
                    if (File.Exists(dllPath)) {
                        this.loggerDllPath = dllPath;
                        break;
                    }
                }
            }
            while (false);
        }

        private string GetLoggerDllPath()
        {
            return this.loggerDllPath;
        }

        private void CheckDllExistence()
        {
            string dllPath = GetLoggerDllPath();
            if (!File.Exists(dllPath))
                throw new LoggerBridgeException(LoggerBridgeException.Success, LoggerBridgeException.DllModuleNotFound,
                    "Dll file '" + dllPath + "' not found");
        }
    }
}