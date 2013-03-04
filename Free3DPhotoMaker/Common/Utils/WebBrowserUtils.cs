using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DVDVideoSoft.Utils
{
    public interface IBrowserWrapper
    {
        string GetExtensionsDir();
        string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar);
    }

    public enum BrowserType
    {
        NoValue = -1,
        UnknownBrowser = 0,
        FirefoxBrowser = 1,
        ChromeBrowser = 2,
        IEBrowser = 3,
        SafaryBrowser = 4,
        OperaBrowser = 5,
        MaxthonBrowser = 6,
    }

    public class Chrome: IBrowserWrapper
    {
        public string GetExtensionsDir()
        {
            //c:\Users\ibmm.VIMPEL\AppData\Local\Google\Chrome\User Data\Default\Extensions\aaaaaakfopmidbfddimafofbdngbkidf\7.14.1.0_0\
            string browserProfileDir = string.Format(@"{0}\{1}", Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Google\Chrome\User Data\Default\Extensions");
            StringBuilder sb = new StringBuilder(browserProfileDir);
            return sb.ToString();
        }

        public string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar = ToolbarHelper.ToolbarType.ask)
        {
            return string.Format("{0}\\{1}", this.GetExtensionsDir(), ToolbarHelper.GetDvsBrowserExtensionName(BrowserType.ChromeBrowser, ttToolbar));
        }
    }

    public class IE: IBrowserWrapper
    {
        public string GetExtensionsDir() { return ""; }
        public string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar = ToolbarHelper.ToolbarType.ask) { return ""; }
    }

    public class Safary: IBrowserWrapper
    {
        public string GetExtensionsDir() { return ""; }
        public string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar = ToolbarHelper.ToolbarType.ask) { return ""; }
    }

    public class Opera: IBrowserWrapper
    {
        public string GetExtensionsDir() { return ""; }
        public string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar = ToolbarHelper.ToolbarType.ask) { return ""; }
    }

    public class Firefox: IBrowserWrapper
    {
        public static readonly string UserProfileExtensionsDirName = "extensions";
        public static readonly string UserProfileSearchpluginsDirName = "searchplugins";
        public static readonly string UserPreferencesFileName = "prefs.js";
        //c:\Users\ibmm.VIMPEL\AppData\Roaming\Mozilla\Firefox\Profiles\m9z41a0v.default\extensions\

        public static string GetUserProfilesPath()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string profilesPath = Path.Combine(appDataPath, "Mozilla\\Firefox\\Profiles");
            return profilesPath;
        }

        public static string GetDefaultProfileName(string browserProfileDir)
        {
            string strRet = null;
            try
            {
                IniFileEditor profilesIni = new IniFileEditor(browserProfileDir + "\\" + "profiles.ini");
                IList<string> sections = profilesIni.GetSectionNames();

                string defaultProfileSectionName = "Profile0";
                foreach (string section in sections)
                {
                    string name = profilesIni.Read(section, "Name", null);
                    if (section.StartsWith("Profile") && !string.IsNullOrEmpty(name) && string.Compare(name, "default", true) == 0)
                    {
                        defaultProfileSectionName = section;
                        break;
                    }
                }
                strRet = profilesIni.Read(defaultProfileSectionName, "Path", null).Replace('/', '\\');
            }
            catch { }
            return strRet;
        }

        public string GetExtensionsDir()
        {
            // Roaming\Mozilla\Firefox\Profiles\%PROFILE%.default\extensions\<toolbarGuid>
            string browserProfileDir = string.Format(@"{0}\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox");
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(@"{0}\{1}\extensions", browserProfileDir, GetDefaultProfileName(browserProfileDir));
            //if ( !string.IsNullOrEmpty( extensionGuid )
            //    sb.AppendFormat( @"{0}\{1}", "extensions", extensionGuid );
            return sb.ToString();
        }

        public string GetDVSExtensionDir(ToolbarHelper.ToolbarType ttToolbar)
        {
            return string.Format("{0}\\{1}", this.GetExtensionsDir(), ToolbarHelper.GetDvsBrowserExtensionName(BrowserType.FirefoxBrowser, ttToolbar));
            //if ( ttToolbar == ToolbarHelper.ToolbarType.AskToolbar )
        }

        private static bool IsDefaultFolder(string path)
        {
            string defaultDirSuffix = "default";

            StringBuilder def = new StringBuilder();
            try
            {
                int DEF_COUNT = defaultDirSuffix.Length;

                for (int count = 0; count < DEF_COUNT; count++)
                    def.Append(path[path.Length - DEF_COUNT + count]);
            }
            catch
            {
                return false;
            }

            return def.ToString() == defaultDirSuffix;
        }

        public static string FindDefaultFolder(string region)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(region);
            if (dir == null || !dir.Exists)
                return null;

            System.IO.DirectoryInfo[] dirs = dir.GetDirectories();

            for (int i = 0; i < dirs.Length; i++)
            {
                if (IsDefaultFolder(dirs[i].Name))
                    return dirs[i].FullName;
            }

            return null;
        }
    }

    #region
    #endregion

    public static class ToolbarHelper
    {
        public enum ToolbarType
        {
            ask,
            conduit,
            conduitDE,
        }

        private static IDictionary<BrowserType, IBrowserWrapper> m_mapBrowsers;

        static ToolbarHelper()
        {
            m_mapBrowsers = new Dictionary<BrowserType, IBrowserWrapper>();
        }

        public static string GetDvsBrowserExtensionName(BrowserType btBrowser, ToolbarType ttToolbar)
        {
            if (ttToolbar == ToolbarType.ask)
            {
                if (btBrowser == BrowserType.FirefoxBrowser)
                    return "toolbar@ask.com";
                if (btBrowser == BrowserType.ChromeBrowser)
                    return "aaaaaakfopmidbfddimafofbdngbkidf";
            }
            else if (ttToolbar == ToolbarType.conduit)
            {
                if (btBrowser == BrowserType.FirefoxBrowser)
                    return "{872b5b88-9db5-4310-bdd0-ac189557e5f5}";
                if (btBrowser == BrowserType.ChromeBrowser)
                    return "plmlpkfpkijnlijgalnjaacllnjmoamo";
            }
            else if (ttToolbar == ToolbarType.conduitDE)
            {
                if (btBrowser == BrowserType.FirefoxBrowser)
                    return "{0027da2d-c9f2-4b0b-ae05-e2cd1bdb6cff}";
                if (btBrowser == BrowserType.ChromeBrowser)
                    return "bhphemoobgnikcoofkgackkaimpfmenm";
            }
            return "";
        }

        public static void CreateConduitSetupIni(bool setSearch, bool setHome)
        {
            StringBuilder sb = new StringBuilder("[default]");
            sb.AppendFormat("\r\n_START_PAGE_={0}", setHome ? "TRUE" : "FALSE");
            sb.AppendFormat("\r\n_SET_DEFAULT_SEARCH_={0}", setSearch ? "TRUE" : "FALSE");
            sb.AppendFormat("\r\n_OPEN_UNINSTALL_PAGE_={0}", "FALSE");
            sb.AppendFormat("\r\n_OPEN_WELCOME_DIALOG_={0}", "FALSE");
            sb.AppendFormat("\r\n_OPEN_THANKYOU_PAGE_={0}", "FALSE");
            sb.Append("\r\n");

            File.WriteAllText((new Firefox()).GetDVSExtensionDir(ToolbarHelper.ToolbarType.conduit) + "\\" + "setup.ini", sb.ToString());
        }

        public static IBrowserWrapper GetBrowser(BrowserType bt)
        {
            switch (bt)
            {
                case BrowserType.FirefoxBrowser:
                    if (!m_mapBrowsers.ContainsKey(BrowserType.FirefoxBrowser))
                        m_mapBrowsers.Add(BrowserType.FirefoxBrowser, new Firefox());
                    return m_mapBrowsers[bt];
                case BrowserType.ChromeBrowser:
                    if (!m_mapBrowsers.ContainsKey(BrowserType.ChromeBrowser))
                        m_mapBrowsers.Add(BrowserType.ChromeBrowser, new Chrome());
                    return m_mapBrowsers[bt];
                case BrowserType.IEBrowser:
                    if (!m_mapBrowsers.ContainsKey(BrowserType.IEBrowser))
                        m_mapBrowsers.Add(BrowserType.IEBrowser, new IE());
                    return m_mapBrowsers[bt];
                case BrowserType.SafaryBrowser:
                    if (!m_mapBrowsers.ContainsKey(BrowserType.SafaryBrowser))
                        m_mapBrowsers.Add(BrowserType.SafaryBrowser, new Safary());
                    return m_mapBrowsers[bt];
                case BrowserType.OperaBrowser:
                    if (!m_mapBrowsers.ContainsKey(BrowserType.OperaBrowser))
                        m_mapBrowsers.Add(BrowserType.OperaBrowser, new Opera());
                    return m_mapBrowsers[bt];
            }
            return null;
        }

        public static bool IsSupportedBrowser(BrowserType btBrowser)
        {
            return btBrowser == BrowserType.FirefoxBrowser ||
                   btBrowser == BrowserType.ChromeBrowser ||
                   btBrowser == BrowserType.IEBrowser;
        }

        public static bool IsToolbarInstalled(BrowserType btBrowser, ToolbarHelper.ToolbarType ttToolbar)
        {
            if (!IsSupportedBrowser(btBrowser))
                return false;
            DirectoryInfo diToolbarDir = null;
            string strToolbarDir = GetBrowser(btBrowser).GetDVSExtensionDir(ttToolbar);
            if (!string.IsNullOrEmpty(strToolbarDir))
                diToolbarDir = new DirectoryInfo(strToolbarDir);

            bool bToolbarIsPresent = diToolbarDir != null && diToolbarDir.Exists;

            if (btBrowser == BrowserType.IEBrowser)
            {
                bToolbarIsPresent = SysUtils.CheckInprocServer("{872B5B88-9DB5-4310-BDD0-AC189557E5F5}");
                return bToolbarIsPresent;
            }

            if (ttToolbar == ToolbarHelper.ToolbarType.ask)
            {
                ///////////////////// ASK ////////////////////////
                bToolbarIsPresent &= bToolbarIsPresent && diToolbarDir.GetDirectories().Length > 0;
                if (btBrowser == BrowserType.FirefoxBrowser)
                {
                    // Look into extensions.ini to ensure the toolbar is not disabled
                    string strExtensionsIniFileName = Path.GetDirectoryName(GetBrowser(btBrowser).GetExtensionsDir()) + "\\" + "extensions.ini";
                    bool bToolbarEnabled = File.Exists(strExtensionsIniFileName)
                        && File.OpenText(strExtensionsIniFileName).ReadToEnd().Contains(ToolbarHelper.GetDvsBrowserExtensionName(btBrowser, ttToolbar));
                    bToolbarIsPresent &= bToolbarEnabled;
                    //File.Exists( strToolbarDir + "" );
                }
            }
            else if (btBrowser == BrowserType.ChromeBrowser && (ttToolbar == ToolbarHelper.ToolbarType.conduit || ttToolbar == ToolbarHelper.ToolbarType.conduitDE)) 
            {
                if (bToolbarIsPresent && diToolbarDir.GetDirectories().Length == 1)
                {
                    DirectoryInfo di = diToolbarDir.GetDirectories()[0];
                    bToolbarIsPresent &= System.IO.File.Exists(Path.Combine(di.FullName, "CT2269050.txt"));
                }
            }
            return bToolbarIsPresent;
        }

        public static bool IsAnyDvsToolbarInstalled()
        {
            //IE
            if (IsToolbarInstalled(BrowserType.IEBrowser, ToolbarType.conduitDE))
                return true;
            if (IsToolbarInstalled(BrowserType.IEBrowser, ToolbarType.conduit))
                return true;
            if (IsToolbarInstalled(BrowserType.IEBrowser, ToolbarType.ask))
                return true;
            //FF
            if (IsToolbarInstalled(BrowserType.FirefoxBrowser, ToolbarType.conduitDE))
                return true;
            if (IsToolbarInstalled(BrowserType.FirefoxBrowser, ToolbarType.conduit))
                return true;
            if (IsToolbarInstalled(BrowserType.FirefoxBrowser, ToolbarType.ask))
                return true;            
            //CH
            if (IsToolbarInstalled(BrowserType.ChromeBrowser, ToolbarType.conduitDE))
                return true;
            if (IsToolbarInstalled(BrowserType.ChromeBrowser, ToolbarType.conduit))
                return true;
            if (IsToolbarInstalled(BrowserType.ChromeBrowser, ToolbarType.ask))
                return true;            
            return false;
        }
    }

    public static class BrowserHelper
    {
        private static string sm_strBrowserHelperExe;
        private static ILogWriter Log = null;

        static BrowserHelper()
        {
            sm_strBrowserHelperExe = (new Regedit(Programs.APP_PATHS_REG_KEY, Regedit.HKEY.HKEY_LOCAL_MACHINE, false)).GetValue(Programs.ToolID.BrowserHelpersInstaller.ToString(), string.Empty);
        }

        public static void SetLogger(ILogWriter log)
        {
            Log = log;
        }

        public static string GetBrowserExtensionDir(BrowserType btBrowser)
        {
            return ToolbarHelper.GetBrowser(btBrowser).GetExtensionsDir();
        }

        public static string GetBHIKeyForBrowser(BrowserType type)
        {
            string strBrowserId = "";
            if (type == BrowserType.FirefoxBrowser)
                strBrowserId = "ff";
            else if (type == BrowserType.IEBrowser)
                strBrowserId = "ie";
            else if (type == BrowserType.ChromeBrowser)
                strBrowserId = "ch";

            if (!string.IsNullOrEmpty(strBrowserId))
                return "/" + strBrowserId;
            else
                return "";
        }

        public static void AddOrRemoveToBrowserCtxMenu(BrowserType btBrowser, string appId, bool add)
        {
            string strBrowserCmdKey = GetBHIKeyForBrowser(btBrowser);
            
            if (string.IsNullOrEmpty(strBrowserCmdKey))
            {
                if (Log != null) Log.Error("Unsupported browser");
                return;
            }

            if (!File.Exists(sm_strBrowserHelperExe))
            {
                if (Log != null) Log.Error("BrowserHelpersInstaller not found");
                return;
            }

            // Run BrowserHelpersIntaller
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = sm_strBrowserHelperExe;
            string strArgs =
                strBrowserCmdKey + " " +
                (add ? "/i" : "/u") + " " + appId;
            p.StartInfo.Arguments = strArgs;

            try
            {
                p.Start();
            }
            catch (Exception ex)
            {
                if (Log != null) Log.Error("Failed to start process: " + ex.Message);
            }
        }
    }
}
