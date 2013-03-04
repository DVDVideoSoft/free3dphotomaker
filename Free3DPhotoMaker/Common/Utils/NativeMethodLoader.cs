using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.Utils
{
    public class NativeMethodLoader<T>
    {
        private IntPtr procPtr;
        private System.Delegate procDelegate;
        private string procName;

        public NativeMethodLoader(string procName)
        {
            this.procPtr = new IntPtr(0);
            this.procDelegate = null;
            this.procName = procName;
        }

        public void Init(IntPtr moduleDll)
        {
            if (moduleDll.ToInt64() == 0)
                throw new ArgumentException("DLL module not defined");
            if (String.IsNullOrEmpty(this.procName))
                throw new ArgumentException("Procedure name not defined");

            this.procPtr = WinApi.GetProcAddress(moduleDll, this.procName);
            if (this.procPtr.ToInt64() == 0)
                throw new NativeMethodException((UInt64)Marshal.GetLastWin32Error(), NativeMethodException.DllMethodNotLoaded, "Failed to load DLL function " + this.procName);

            Type type = typeof(T);
            this.procDelegate = Marshal.GetDelegateForFunctionPointer(this.procPtr, type);
            if (this.procDelegate == null)
                throw new NativeMethodException((UInt64)Marshal.GetLastWin32Error(), NativeMethodException.DllMethodDelegateNotCreated, "Failed to create a delegate for '" + this.procName + "' function");
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
