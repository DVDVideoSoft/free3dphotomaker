using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DVDVideoSoft.Utils
{
    public static class ProcUtils
    {
        // VideoAccelerator proc names:
        private static string p = "";

        static ProcUtils()
        {
            
        }

        public static bool Is3rdPartyRunning()
        {
            return IsProcRunning(p);
        }

        public static bool IsProcRunning(string strProcName)
        {
            foreach (Process p in Process.GetProcesses())
            {
                if (string.Compare(p.ProcessName, strProcName, true) == 0)
                    return true;
            }
            return false;
        }

        public static void StartOrStopSvc(string strSvcName, bool bStart)
        {
            string strCommand = bStart ? "start" : "stop";
            try
            {
                {
                    ProcessStartInfo info = new ProcessStartInfo("net.exe");
                    info.Arguments = strCommand + " " + strSvcName;
                    info.WindowStyle = ProcessWindowStyle.Hidden;
                    Process process = Process.Start(info);
                    process.WaitForExit(0);
                }
            }
            catch (Exception)
            {
                //Log.Error("Exception in StartOrStopVA(): " + ex.Message);
            }
        }

        public static void KillProc(string strName)
        {
            foreach (Process p in System.Diagnostics.Process.GetProcessesByName(strName))
            {
                try
                {
                    p.Kill();
                    p.WaitForExit(); // possibly with a timeout
                }
                catch (Win32Exception)
                {
                    //Log.Error("Failed to terminate the process, " + ex.Message);
                }
                catch (InvalidOperationException)
                {
                    // process has already exited - might be able to let this one go
                }
            }
        }
    }
}
