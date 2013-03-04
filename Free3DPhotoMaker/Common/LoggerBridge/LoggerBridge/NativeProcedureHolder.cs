using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using DVDVideoSoft.LoggerBridge.Win32Interop;

namespace DVDVideoSoft.LoggerBridge
{
    internal class NativeProcedureHolder<T>
    {
        private IntPtr procPtr;
        private System.Delegate procDelegate;
        private string procName;

        public NativeProcedureHolder(string procName)
        {
            this.procPtr = new IntPtr(0);
            this.procDelegate = null;
            this.procName = procName;
        }

        public void Init(IntPtr moduleDll)
        {
            if (moduleDll.ToInt64() == 0)
                throw new ArgumentException("Dll module not defined");
            if (String.IsNullOrEmpty(this.procName))
                throw new ArgumentException("Procedure name not defined");

            this.procPtr = Win32SysUtils.GetProcAddress(moduleDll, this.procName);
            if (this.procPtr.ToInt64() == 0)
                throw new LoggerBridgeException((UInt64)Marshal.GetLastWin32Error(),
                    LoggerBridgeException.DllMethodNotLoaded, "Dll procedure '" + this.procName + "' not loaded");

            Type type = typeof(T);
            this.procDelegate = Marshal.GetDelegateForFunctionPointer(this.procPtr, type);
            if (this.procDelegate == null)
                throw new LoggerBridgeException((UInt64)Marshal.GetLastWin32Error(),
                    LoggerBridgeException.DllMethodDelegateNotCreated, "Delegate for function '" + this.procName + "' not created");
        }

        public T GetProc()
        {
            return (T)(object)this.procDelegate;
        }

        public string GetProcName()
        {
            return this.procName;
        }

        public void Clear()
        {
            this.procPtr = new IntPtr(0);
            this.procDelegate = null;
        }
    }
}