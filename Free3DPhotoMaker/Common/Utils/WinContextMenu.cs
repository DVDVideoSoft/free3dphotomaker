using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Diagnostics;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.Utils
{
    public class WinContextMenu
    {
        // file types to convert with Programs.ID
        private Dictionary<Programs.ID, ProgrammMenuParams> ProgrammsMenuSettings = new Dictionary<Programs.ID, ProgrammMenuParams>();
        private Programs.ID appID;
        private string iconsLib = "\\Common Files\\DVDVideoSoft\\lib\\DVSResources.dll";

        public WinContextMenu(Programs.ID id)
        {
            ProgrammsMenuSettings.Add(Programs.ID.FreeAudioConverter, new ProgrammMenuParams(new string[] { "mp3", "wav" }, 1));
            appID = id;
        }

        /// <summary>
        /// Install Context menu to programm with admin rights request
        /// </summary>
        /// <param name="adminRights"> if true, then run external process</param>
        /// <returns></returns>
        public bool Install(bool adminRights)
        {
            bool result = false;
            if (adminRights)
            {
                if (AdjustContextMenu(true, (int)appID, true) == 0)
                {
                    result = true;
                }
            }
            else
            {
                result = true;
                if (!ProgrammsMenuSettings.ContainsKey(appID))
                {
                    throw new Exception(string.Format("Programm with id \"{0}\" do not have supported file extensions. ", appID));
                }
                else
                {
                    string strAppPath = Programs.GetAppPath(appID.ToString());
                    FileInfo appFile = new FileInfo(strAppPath);
                    string iconLibValue = appFile.Directory.Parent.Parent.FullName + iconsLib + "," + ProgrammsMenuSettings[appID].iconID.ToString();
                    for (int i = 0; i < ProgrammsMenuSettings[appID].FileTypes.Length; i++)
                    {
                        string fType = ProgrammsMenuSettings[appID].FileTypes[i];
                        RegistryKey ClassesRoot = Registry.ClassesRoot;
                        RegistryKey fTypeKey = ClassesRoot.CreateSubKey("." + fType);
                        if (fTypeKey != null)
                        {
                            string fTypeReg = (string)fTypeKey.GetValue("");
                            RegistryKey fTypeRegKey = ClassesRoot.CreateSubKey(fTypeReg);
                            if (fTypeRegKey != null)
                            {
                                RegistryKey fTypeRegKeyShell = fTypeRegKey.CreateSubKey("shell");

                                if (fTypeRegKeyShell != null)
                                {
                                    // convert menu item
                                    RegistryKey fTypeRegKeyShellProgramm = fTypeRegKeyShell.CreateSubKey(appID.ToString() + "_convert");
                                    if (fTypeRegKeyShellProgramm != null)
                                    {

                                        fTypeRegKeyShellProgramm.SetValue("", DVDVideoSoft.Resources.CommonData.ConvertWith + " " + Programs.GetHumanName(appID));
                                        fTypeRegKeyShellProgramm.SetValue("Icon", iconLibValue);
                                        RegistryKey comm = fTypeRegKeyShellProgramm.CreateSubKey("command");
                                        if (comm != null)
                                        {
                                            comm.SetValue("", strAppPath + " %1 -c");
                                            comm.Close();
                                        }
                                        fTypeRegKeyShellProgramm.Close();
                                    }
                                    // add to list menu item
                                    RegistryKey fTypeRegKeyShellProgrammList = fTypeRegKeyShell.CreateSubKey(appID.ToString() + "_addlist");
                                    if (fTypeRegKeyShellProgrammList != null)
                                    {

                                        fTypeRegKeyShellProgrammList.SetValue("", DVDVideoSoft.Resources.CommonData.AddToConvertList + " " + Programs.GetHumanName(appID));
                                        fTypeRegKeyShellProgrammList.SetValue("Icon", iconLibValue);
                                        RegistryKey comm = fTypeRegKeyShellProgrammList.CreateSubKey("command");
                                        if (comm != null)
                                        {
                                            comm.SetValue("", strAppPath + " %1");
                                            comm.Close();
                                        }
                                        fTypeRegKeyShellProgrammList.Close();
                                    }
                                    fTypeRegKeyShell.Close();
                                }
                                fTypeRegKey.Close();
                            }
                            fTypeKey.Close();
                        }
                        ClassesRoot.Close();
                    }
                }
            }
            return (result);
        }

        /// <summary>
        /// Install Context menu to programm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Install()
        {
            return (Install(false));
        }

        /// <summary>
        /// Uninstall Context menu to programm
        /// </summary>
        /// <param name="adminRights"> if true, then run external process </param>
        /// <returns></returns>
        public bool Uninstall(bool adminRights)
        {
            bool result = true;
            if (adminRights)
            {
                result = false;
                if (AdjustContextMenu(false, (int)appID, true) == 0)
                {
                    result = true;
                }
            }
            else
            {
                if (!ProgrammsMenuSettings.ContainsKey(appID))
                {
                    throw new Exception(string.Format("Programm with id \"{0}\" do not have supported file extensions. ", appID));
                }
                else
                {
                    string strAppPath = Programs.GetAppPath(appID.ToString());
                    for (int i = 0; i < ProgrammsMenuSettings[appID].FileTypes.Length; i++)
                    {
                        string fType = ProgrammsMenuSettings[appID].FileTypes[i];
                        RegistryKey ClassesRoot = Registry.ClassesRoot;
                        RegistryKey fTypeKey = ClassesRoot.CreateSubKey("." + fType);
                        if (fTypeKey != null)
                        {
                            string fTypeReg = (string)fTypeKey.GetValue("");
                            RegistryKey fTypeRegKey = ClassesRoot.CreateSubKey(fTypeReg);
                            if (fTypeRegKey != null)
                            {
                                RegistryKey fTypeRegKeyShell = fTypeRegKey.CreateSubKey("shell");
                                if (fTypeRegKeyShell == null)
                                {
                                    fTypeRegKeyShell = fTypeRegKey.CreateSubKey("shell");
                                }

                                if (fTypeRegKeyShell != null)
                                {
                                    RegistryKey fTypeRegKeyShellProgramm = fTypeRegKeyShell.OpenSubKey(appID.ToString() + "_convert");
                                    if (fTypeRegKeyShellProgramm != null)
                                    {
                                        fTypeRegKeyShell.DeleteSubKeyTree(appID.ToString() + "_convert");
                                    }

                                    RegistryKey fTypeRegKeyShellProgrammList = fTypeRegKeyShell.OpenSubKey(appID.ToString() + "_addlist");
                                    if (fTypeRegKeyShellProgrammList != null)
                                    {
                                        fTypeRegKeyShell.DeleteSubKeyTree(appID.ToString() + "_addlist");
                                    }

                                    fTypeRegKeyShell.Close();
                                }
                                fTypeRegKey.Close();
                            }
                            fTypeKey.Close();
                        }
                        ClassesRoot.Close();
                    }
                }
            }
            return (result);
        }

        /// <summary>
        /// Uninstall Context menu to programm with admin rights request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public bool Uninstall()
        {
            return (Uninstall(false));
        }

        public bool isContextMenuEnabled
        {
            get
            {
                return (ProgrammsMenuSettings.ContainsKey(appID));
            }
        }

        private int AdjustContextMenu(bool installOrUninstall, int applicationId, bool waitForExit)
        {
            string fileName = Path.Combine(FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonBin), Programs.ToolID.ContextMenuHelper.ToString() + ".exe");
            string arguments = (installOrUninstall ? "/i /aid " : "/u /aid ");
            arguments += applicationId.ToString();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = fileName;
            startInfo.Arguments = arguments;
            Process process = Process.Start(startInfo);
            int exitCode = 0;
            if (waitForExit)
            {
                process.WaitForExit();
                exitCode = process.ExitCode;
            }
            return exitCode;
        }

        public bool isInstalled
        {
            get
            {
                bool result = false;
                if (!ProgrammsMenuSettings.ContainsKey(appID))
                {
                    throw new Exception(string.Format("Program '{0}' doesn't support file extensions.", appID));
                }
                else
                {
                    string strAppPath = Programs.GetAppPath(appID.ToString());
                    for (int i = 0; i < ProgrammsMenuSettings[appID].FileTypes.Length; i++)
                    {
                        string fType = ProgrammsMenuSettings[appID].FileTypes[i];
                        RegistryKey ClassesRoot = Registry.ClassesRoot;
                        RegistryKey fTypeKey = ClassesRoot.OpenSubKey("." + fType, false);
                        if (fTypeKey != null)
                        {
                            string fTypeReg = (string)fTypeKey.GetValue("");
                            RegistryKey fTypeRegKey = ClassesRoot.OpenSubKey(fTypeReg, false);
                            if (fTypeRegKey != null)
                            {
                                RegistryKey fTypeRegKeyShell = fTypeRegKey.OpenSubKey("shell", false);
                                if (fTypeRegKeyShell == null)
                                {
                                    fTypeRegKeyShell = fTypeRegKey.OpenSubKey("shell", false);
                                }

                                if (fTypeRegKeyShell != null)
                                {
                                    RegistryKey fTypeRegKeyShellProgramm = fTypeRegKeyShell.OpenSubKey(appID.ToString() + "_convert");
                                    if (fTypeRegKeyShellProgramm != null)
                                    {
                                        result = true;
                                    }
                                    fTypeRegKeyShell.Close();
                                }
                                fTypeRegKey.Close();
                            }
                            fTypeKey.Close();
                        }
                        ClassesRoot.Close();
                    }
                }
                return (result);
            }
        }

        private class ProgrammMenuParams
        {
            public string[] FileTypes;
            public int iconID;

            public ProgrammMenuParams(string[] ext, int icon)
            {
                FileTypes = ext;
                iconID = icon;
            }
        }
    }
}
