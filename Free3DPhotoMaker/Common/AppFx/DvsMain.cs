using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.IO;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.LoggerBridge;

namespace DVDVideoSoft.AppFx
{
    public partial class DvsMain : LocalizableForm
    {
        #region Protected members

        protected event SystemThemeChangedHandler SystemThemeChangedEvent;

        protected static ILogWriter Log;

        protected LocalizationManager locMgr;
        protected DVDVideoSoftMutex commonMutex = null;
        protected DVDVideoSoftMutex assemblyMutex = null;

        protected Splash splash;

        // The Controller
        protected Controller controller = null;

        // Reference to the PropMan, just for convenience
        protected IPropMan propman = null;

        private PropsProvider provider;

        // Property IDs
        protected static readonly string AVConverter_PN = "AVConverter";
        protected static readonly string PresetEditor_PN = "PresetEditor";
        protected static readonly string CurrentTheme_RN = "CurrentTheme";
        protected static readonly string DefaultThemeId = "Default";
        protected static readonly string ProfileXmlBaseName = "Profile.xml";
        protected static readonly string DefaultPredefinedID = "Default";


        protected static readonly string id_EN = "Id";
        protected static readonly string title_EN = "Title";
        protected static readonly string profileXml_EN = "ProfileXml";
        protected static readonly string inputFilter_EN = "InputFilter";
        protected static readonly string outputFolder_EN = "OutputFolder";
        protected static readonly string playingEnabled_EN = "PlayingEnabled";
        protected static readonly string tagsEditingEnabled_EN = "TagsEditingEnabled";
        protected static readonly string forbidAccelerateEncodingOption_EN = "ForbidAccelerateEncodingOption";
        protected static readonly string defaultCategoryIndex_EN = "DefaultCategoryIndex";
        protected static readonly string defaultPresetIndex_EN = "DefaultPresetIndex";
        protected static readonly string enableAutoTagging_EN = "EnableAutoTagging";

        protected static string App_Id_PN;
        protected static string App_Title_PN;
        protected static string App_PlayingEnabled_EN;
        protected static string App_TagsEditingEnabled_EN;
        protected static string App_ForbidAccelerateEncodingOption_EN;
        protected static string UI_InputFilter_PN;
        protected static string UI_OutputFolder_PN;
        protected static string UI_DefaultCategoryIndex_PN;
        protected static string UI_DefaultPresetIndex_PN;
        protected static string UI_EnableAutoTagging_PN;


        // Key infrastructure members
        protected string Id;
        protected Programs.ID legacyDvsProgId;
        // The progId must be known to show the correct error page in the case of the DvsMain/Main .ctor crash. So store it in the static member.
        //protected static Programs.ID LegacyProgId = Programs.ID.Manager; // for the case constructor raises an exception
        protected string appRegKey;
        protected string appAsmName;
        protected string appTitle;
        protected string versionStr;

        protected DateTime expireDate;
        protected bool m_bIsActivated;
        protected bool m_bHasJustExpired = false;
        protected bool m_bShowOfferScreen;
        protected bool m_bOfferShallInstallConduit;

        protected Regedit registry;

        protected string appRoamingPath;
        protected string defaultOutputFolder = null;

        // Theme support
        protected string themesPath;
        protected string currentTheme;
        protected List<string> availableThemes;
        protected ThemeLoader themeLoader;

        //protected UpdateChecker updateChecker;

        // Misc
        protected bool m_bVisualStylesEnabled;
        protected bool m_bSuppressWndProc;
        protected bool m_bHasShown;
        protected bool m_bFirstTime = true;

        //protected static ITaskbarManager s_iTaskbarMgr = null;
       // protected UIProgressState lastProgressState = UIProgressState.Unknown;

        // UI defaults
        protected Color originalBackColor = Color.Transparent;
        protected Dictionary<string, Font> uiDefaultFonts = new Dictionary<string, Font>();
        protected Dictionary<string, Color> uiDefaultColors = new Dictionary<string, Color>();

        #endregion

        #region Private members

        private const int WM_THEMECHANGED = 0x031A;
        private bool initialized;
        // Hardcode: Skip check point date for subscription
        private const bool skipCheckPointDate = false;

        #endregion

        #region Initiliazation

        static DvsMain()
        {
            // Just a workaround
            DVDVideoSoft.Utils.SysUtils.SetEnvPathAndDllPath();

            // Exception handling
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            App_Id_PN = Controller.App_EN + "." + id_EN;
            App_Title_PN = Controller.App_EN + "." + title_EN;
            App_TagsEditingEnabled_EN = Controller.App_EN + "." + tagsEditingEnabled_EN;
            App_ForbidAccelerateEncodingOption_EN = Controller.App_EN + "." + forbidAccelerateEncodingOption_EN;

            UI_InputFilter_PN = Controller.UI_EN + "." + inputFilter_EN;
            UI_OutputFolder_PN = Controller.UI_EN + "." + outputFolder_EN;
            UI_DefaultCategoryIndex_PN = Controller.UI_EN + "." + defaultCategoryIndex_EN;
            UI_DefaultPresetIndex_PN = Controller.UI_EN + "." + defaultPresetIndex_EN;
            UI_EnableAutoTagging_PN = Controller.UI_EN + "." + enableAutoTagging_EN;
            App_PlayingEnabled_EN = Controller.UI_EN + "." + playingEnabled_EN;


            Assembly entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                try
                {
                    Log = Logger.GetLogger("");
                }
                catch { }
                
                if (Log == null)
                    try
                    {
                        string logFName = Path.Combine(FileUtils.GetDvsPath(FileUtils.DvsFolderType.AppData, "logs"), "emergencyLog.txt");
                        File.AppendAllText(logFName, "\n" + Assembly.GetEntryAssembly().GetName().Name + " LogInitFailed " + DateTime.Now.ToString());
                    }
                    catch { }
            }

        }

        public DvsMain()
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();
            this.appAsmName = Controller.BaseAppName;

            if (entryAssembly != null)
            {
                this.commonMutex = new DVDVideoSoftMutex(null);
                this.assemblyMutex = new DVDVideoSoftMutex(this.appAsmName);
                this.versionStr = FormattingFunctions.GetVerboseVersion(entryAssembly.GetName().Version.ToString());
            }

            // Init logging (runtime only)
            if (entryAssembly != null)
            {
                //try { // Detect if the logger was loaded normally
                //    bool b = Log.IsFatalEnabled;
                //} catch (Exception ex) {
                //    throw ex;
                //}

                if (Log != null)
                {
                    Log.Notice(string.Format("============ {0} {1} ==========================", this.appAsmName, this.versionStr, DateTime.Now.Date.ToShortDateString()));
                    Log.Info(Environment.OSVersion.Version.ToString() + (SysUtils.IsUacEnabled() ? "" : " LUA0"));
                    Log.Info(SysUtils.GetCpuInfo());
                }

                this.Shown += new System.EventHandler(this.DvsMain_Shown);
            }

            controller = new Controller(Log);

            try
            {
                //s_iTaskbarMgr = this.controller.GetTaskbarManager();
            }
            catch { }

            //if (s_iTaskbarMgr == null)
            //    s_iTaskbarMgr = new TaskbarManagerDummy();

            if (Log != null && Log.IsTraceEnabled)
                Log.Trace("Try to DvsMain.InitializeComponent()");

            InitializeComponent();

            this.FormClosing += new FormClosingEventHandler(DvsMain_FormClosing);

            if (Log != null && Log.IsTraceEnabled) // There is no log at design time, so this check is necessary
                Log.Trace("Exit DvsMain.ctor");
        }

        ~DvsMain()
        {
            Dispose(false);
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (this.commonMutex != null)
            {
                this.commonMutex.Dispose();
                this.commonMutex = null;
            }

            if (this.assemblyMutex != null)
            {
                this.assemblyMutex.Dispose();
                this.assemblyMutex = null;
            }

            if (Log != null)
            {
                if (Log.IsTraceEnabled)
                    Log.Trace("DvsMain.Dispose(" + disposing + ")");

                if (disposing)
                {
                    Log = null;
                }
            }

            base.Dispose(disposing);
        }

        public virtual void Init(bool configurationRequired, bool needToCreateDirs = true)
        {
            if (Log.IsTraceEnabled)
                Log.Trace("==> DvsMain::Init");

            // Just a workaround
            if (needToCreateDirs)
                try
                {
                    Directory.CreateDirectory(FileUtils.GetDvsPath(FileUtils.DvsFolderType.AppData, Programs.LogsSubDirName));
                }
                catch { }

            m_bVisualStylesEnabled = NativeMethods.IsThemeActive() != 0;

            // It's too early so the current culture hasn't been set by the LocMgr, do it manually.
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Regedit.ReadString(false, Programs.GetRegKey(Programs.ID.Manager), Defs.PN.CurrentCulture.ToString(), "en-US"));

            if (!controller.Init() && configurationRequired)
            {
                Log.Error("Cannot load the program configuration / failed to initialize the controller");
                throw new InitException(StringResources.CannotLoadTheProgramsConfiguration);
            }

            this.propman = this.controller.PropMan;

            if (string.IsNullOrEmpty(this.appRegKey))
                this.appRegKey = Programs.GetRegKey(this.appAsmName);

            SysUtils.SetEnvPathAndDllPath();

            ReadSubscriptionInfo();

            if (!CheckForPrerequisites())
                throw new CaughtAndHandledException(CaughtAndHandledException.ErrorType.Codec, 0xFFFFFF);

            if (this.propman.ContainsKey(App_Id_PN))
                this.Id = this.propman[App_Id_PN];
            else
                this.Id = this.appAsmName;
            this.legacyDvsProgId = Programs.GetProgramID(this.Id);

            this.appRegKey = Programs.GetRegKey(this.Id);

            GetProvider();

            // Themes
            this.appRoamingPath = FileUtils.GetDvsPath(FileUtils.DvsFolderType.AppData, this.appAsmName) + "\\";
            this.themesPath = this.appRoamingPath + "Themes\\";
            if (needToCreateDirs)
                try
                {
                    Directory.CreateDirectory(this.themesPath);
                }
                catch { }
            this.availableThemes = ThemeLoader.GetAvailableThemes(this.themesPath);

            // Registry
            this.registry = new Regedit(Regedit.HKEY.HKEY_CURRENT_USER, this.appRegKey, true);
            this.registry.Open(true);

            // The loc manager
            this.locMgr = new LocalizationManager(Programs.GetRegKey(Programs.ID.Manager), Log);
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.locMgr.CurrentCulture);
            // Add the form itself to the loc manager
            this.locMgr.Add(this);

            this.themeLoader = new ThemeLoader(this.themesPath, Log);

            // If it is specified
            //string s = GetProfileXmlFileName();
            if (this.propman.ContainsKey(Controller.App_EN + "." + profileXml_EN))
            {
                if (!this.LoadPresets())
                    throw new InitException(StringResources.CannotLoadPresetsPleaseReinstall);
            }

            this.initialized = true;
            if (Log.IsTraceEnabled)
                Log.Trace("<== DvsMain::Init");
        }

        public static void Application_ThreadException(object sender, ThreadExceptionEventArgs ex)
        {
            Log.Error(new string('+', 80));
            Log.Error(ex.Exception.ToString());
            string strMsg = "";
            if (ex.Exception != null && !string.IsNullOrEmpty(ex.Exception.Message))
            {
                strMsg = "[Error: " + ex.Exception.Message + ex.Exception.Message + "]\r\n\r\n";
            }
            strMsg += StringResources.PleaseSendTheLogFile;
            DialogResult res = MessageBox.Show(strMsg, StringResources.ErrorHasOccured, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (res == DialogResult.Yes)
            {
                string sExePath = FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonBin, Programs.ToolID.DVSSysReport.ToString() + ".exe");
                if (File.Exists(sExePath))
                {
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(sExePath);
                    System.Diagnostics.Process.Start(info);
                }
                //FileUtils.OpenDirectory(FileUtils.GetDvsPath(FileUtils.DvsFolderType.AppData, Programs.LogsSubDirName) + "\\");
            }
            Application.Exit();
        }

        protected virtual void PlayFinishSound(Functions.FinishSound sound)
        {
            if (GetProvider().Get<bool>(Defs.PN.EnableSounds.ToString()))
                Functions.PlayFinishSound(sound);
        }

        protected virtual bool CheckForPrerequisites()
        {
            return true;
        }

        public virtual void SetSplash(Splash splash)
        {
            this.splash = splash;
        }

        public virtual void CloseSplash()
        {
            if (this.splash != null)
                this.splash.Close();
            this.splash = null;
        }

        #endregion

        #region Form's event handlers

        protected override void WndProc(ref Message m)
        {
            if (!(m_bSuppressWndProc && this.GetType().Name == "DvsMain"))
            {
                switch (m.Msg)
                {
                    case WM_THEMECHANGED:
                        base.WndProc(ref m);
                        this.OnSystemThemeChanged(true/*dummy, in DvsMain this argument is ignored*/);
                        return;
                }
            }
            base.WndProc(ref m);
        }

        private DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder DetectAvailablEGpuTranscoder(ref SystemHelper.ETranscoderAvailability transcoderAvailability)
        {
            SystemHelper.ETranscoderAvailability eAtiAvailability = SystemHelper.GetTranscoderAvailability(DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_ATI);
            if (!string.IsNullOrEmpty(SystemHelper.GetLastError()))
                Log.Error(SystemHelper.GetLastError());

            // ATI has a priority
            if (eAtiAvailability == SystemHelper.ETranscoderAvailability.kReady || eAtiAvailability == SystemHelper.ETranscoderAvailability.kNoKLite)
            {
                transcoderAvailability = SystemHelper.ETranscoderAvailability.kReady;
                return DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_ATI;
            }

            SystemHelper.ETranscoderAvailability eNVidiaAvailability = SystemHelper.GetTranscoderAvailability(DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_CUDA);

            if (eNVidiaAvailability != SystemHelper.ETranscoderAvailability.kReady && eAtiAvailability == SystemHelper.ETranscoderAvailability.kNoDriverFound)
            {
                // ATI is present, but drivers not found
                transcoderAvailability = eAtiAvailability;
                return DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_ATI;
            }
            else if (eNVidiaAvailability == SystemHelper.ETranscoderAvailability.kReady || eNVidiaAvailability == SystemHelper.ETranscoderAvailability.kNoDriverFound)
            {
                transcoderAvailability = eNVidiaAvailability;
                return DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_CUDA;
            }

            transcoderAvailability = SystemHelper.ETranscoderAvailability.kUnknown;
            return DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_None;
        }

        protected virtual void InitProperties()
        {
            IPropsSaveToHelper h = GetProvider() as IPropsSaveToHelper;

            // App title
            this.appTitle = this.propman[DvsMain.App_Title_PN];
            if (string.IsNullOrEmpty(this.appTitle))
                this.appTitle = Programs.GetHumanName(this.Id);
            GetProvider().Set(Defs.PN.AppTitle.ToString(), this.appTitle);
            GetProvider().Set(Defs.PN.AppId.ToString(), this.legacyDvsProgId);

            // Transcoder acceleration related
            DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder transcoderType = DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_None;
            SystemHelper.ETranscoderAvailability               transcoderAvailability = SystemHelper.ETranscoderAvailability.kUnknown;

            bool bForbidAccelerateEncodingOption = this.propman != null && this.propman.ContainsKey(App_ForbidAccelerateEncodingOption_EN) && FormattingFunctions.IsTrueValue(this.propman[App_ForbidAccelerateEncodingOption_EN]);
            if (bForbidAccelerateEncodingOption)
            {
                GetProvider().Set(Defs.PN.ForbidAccelerateEncodingOption.ToString(), bForbidAccelerateEncodingOption);
            }
            else
            {
                transcoderType = DetectAvailablEGpuTranscoder(ref transcoderAvailability);
            }

            GetProvider().Set(Defs.PN.TranscoderAvailableType.ToString(), transcoderType);
            GetProvider().Set(Defs.PN.TranscoderAvailability.ToString(), transcoderAvailability);

            // Storable values
            h.AddStorableValue(Defs.PN.AccelerateEncoding.ToString(),
                               (bool)(!bForbidAccelerateEncodingOption && transcoderAvailability == SystemHelper.ETranscoderAvailability.kReady));
        }

        private void DvsMain_Load(object sender, EventArgs e)
        {
            //int mfid = Thread.CurrentThread.ManagedThreadId;
            GetProvider().Set(Defs.PN.IsActivated.ToString(), m_bIsActivated);
        }

        private void DvsMain_Activated(object sender, EventArgs e)
        {
            if (m_bFirstTime)
            {
                m_bFirstTime = false;
                if (!m_bIsActivated && m_bHasJustExpired)
                {
                    this.provider.Set(Defs.PN.DontShowSubscriptionExpiredMsg.ToString(), true);
                    CloseSplash();
                    MessageBox.Show( StringResources.LicenseHasExpired, StringResources.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void DvsMain_Shown(object sender, EventArgs e)
        {
            Log.Info("SharedLib: " +
                Regedit.ReadString(true, Programs.DVS_REG_ROOT, Programs.RVN.SharedLibVer.ToString(), "") + "/" +
                Regedit.ReadString(true, "SOFTWARE\\" + Programs.ToolID.CodecPack.ToString(), "Version", ""));
            if (this.dpiX != 96.0)
                Log.Info(((int)this.dpiX).ToString() + " DPI mode");

            if (this.initialized)
                this.CheckForUpdates(true);

            this.CloseSplash();

            m_bHasShown = true;
        }

        private void DvsMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (this.updateChecker != null)
            //    this.updateChecker.Cancel();
            //OnFormClosing(sender, e);
        }

        #endregion

        #region Protected methods

        protected virtual string GetLocalizedString(string id)
        {
            return "";
        }

        protected virtual void ReadSubscriptionInfo()
        {/*
            try
            {
                ReadSubscription();
            }
            catch (SubscriptionException ex)
            {
                string msg = "";
                switch (ex.ErrorCode)
                {
                    case SubscriptionException.ErrorCodes.InvalidLicenseData:
                        msg = GetLocalizedString("LicenseFileCorrupted");
                        break;
                    case SubscriptionException.ErrorCodes.OpenLicenseFileFailed:
                        break;
                    case SubscriptionException.ErrorCodes.LicenseHasExpired:
                        msg = GetLocalizedString("LicenseHasExpired");
                        break;
                    case SubscriptionException.ErrorCodes.ThisMachineIsNotRegistered:
                        msg = GetLocalizedString("ThisMachineIsNotRegistered");
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    CloseSplash();
                    MessageBoxEx.Show(null, msg, GetLocalizedString("Information"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Log.Info("Failed to activate subscription features. Code: " + ex.ErrorCode.ToString() + ". " + ex.Message);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled)
                    Log.Error("Subscription check failed: " + ex.ToString());
            }
          * */
        }

        protected void ShowPrerequisiteForm()
        {
            int showForm = Regedit.ReadInt(false, Programs.GetRegKey(Programs.ID.Manager), Defs.PN.ShowPrerequisiteForm.ToString(), 1);
            if (showForm != 1)
                return;

            DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder eGpuTranscoder = (DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder)this.provider.Get<int>(Defs.PN.TranscoderAvailableType.ToString(), (int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EGpuTranscoder.kGpu_None);
            SystemHelper.ETranscoderAvailability eRealTranscoderAvailability = SystemHelper.GetTranscoderAvailability(eGpuTranscoder);
            if (eRealTranscoderAvailability == SystemHelper.ETranscoderAvailability.kNoDriverFound ||
                eRealTranscoderAvailability == SystemHelper.ETranscoderAvailability.kNoAmdCodecs ||
                eRealTranscoderAvailability == SystemHelper.ETranscoderAvailability.kNoKLite)
            {
                string prerequisiteCheckExe = FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonBin, "PrerequisiteCheck.exe");
                if (File.Exists(prerequisiteCheckExe))
                {
                    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(prerequisiteCheckExe);
                    System.Diagnostics.Process.Start(info);
                }
            }
        }

        protected void ReadSubscription()
        {
           
        }

        protected virtual bool LoadPresets()
        {
            return false;
        }

        protected virtual string TypeName
        {
            get { return "Main"; }
        }

        protected string GetProfileXmlFileName()
        {
            string profileXmlFileName = DefaultPredefinedID;
            string profileXmlKeyName = Controller.App_EN + "." + profileXml_EN;

            if (this.propman.ContainsKey(profileXmlKeyName))
                profileXmlFileName = this.propman[profileXmlKeyName];

            if (string.Compare(profileXmlFileName, DefaultPredefinedID, true) == 0)
                profileXmlFileName = this.appAsmName + ProfileXmlBaseName;

            return profileXmlFileName;
        }

        protected virtual string GetAppTitleWithVersion()
        {
            if (string.IsNullOrEmpty(this.appTitle))
            {
                this.appTitle = this.propman[DvsMain.App_Title_PN];
                if (string.IsNullOrEmpty(this.appTitle))
                    this.appTitle = Programs.GetHumanName(this.Id);
            }
            return this.appTitle + " " + this.versionStr;
        }

        protected void DisplayProgress(float value, int numCompletedFiles = 0, int totalNumFiles = 0)
        {
            if (value < 0)
                return;
            StringBuilder sb = new StringBuilder();
            if (numCompletedFiles > 0 && totalNumFiles > 0)
                sb.AppendFormat("{0:F2}% ({1}/{2}) {3} - ", value, numCompletedFiles, totalNumFiles, DVDVideoSoft.Resources.CommonData.Completed);
            else
                sb.AppendFormat("{0:F2}% {1} - ", value, DVDVideoSoft.Resources.CommonData.Completed);
            //if (s_iTaskbarMgr != null)
            //{
            //    s_iTaskbarMgr.SetProgressValue((int)value, 100);
            //    if (this.lastProgressState != UIProgressState.Normal)
            //        s_iTaskbarMgr.SetProgressState(DVDVideoSoft.Utils.UIProgressState.Normal);
            //    this.lastProgressState = UIProgressState.Normal;
            //}

            sb.Append(GetAppTitleWithVersion());
            this.Text = sb.ToString();
        }


        protected void ChangeProgressState()
        {

        }

        /*
        protected void ChangeProgressState(UIProgressState state)
        {
            if (state == UIProgressState.Unknown)
                return;
            if (state == UIProgressState.NoProgress && !(this.lastProgressState == UIProgressState.Unknown && s_iTaskbarMgr != null))
            {
                this.Text = GetAppTitleWithVersion();
                if (s_iTaskbarMgr != null)
                    s_iTaskbarMgr.SetProgressState(DVDVideoSoft.Utils.UIProgressState.NoProgress);
            }
            else if (s_iTaskbarMgr != null)
            {
                DVDVideoSoft.Utils.UIProgressState taskbarState = DVDVideoSoft.Utils.UIProgressState.NoProgress;
                switch (state)
                {
                    case UIProgressState.NoProgress:
                        taskbarState = DVDVideoSoft.Utils.UIProgressState.NoProgress;
                        break;
                    case UIProgressState.Normal:
                        taskbarState = DVDVideoSoft.Utils.UIProgressState.Normal;
                        break;
                    case UIProgressState.Paused:
                        taskbarState = DVDVideoSoft.Utils.UIProgressState.Paused;
                        break;
                    case UIProgressState.Error:
                        taskbarState = DVDVideoSoft.Utils.UIProgressState.Error;
                        break;
                    case UIProgressState.Indeterminate:
                        taskbarState = DVDVideoSoft.Utils.UIProgressState.Indeterminate;
                        break;
                }

                s_iTaskbarMgr.SetProgressState(taskbarState);
                this.lastProgressState = state;
            }
        }
         * */

        protected void CheckForUpdates(bool startupCheck)
        {
            if (startupCheck)
            {
#if STARTUPCHECK_IS_OPTIONAL
                try
                {
                    Regedit reg = new Regedit(Regedit.HKEY.HKEY_LOCAL_MACHINE, Programs.DVS_REG_ROOT, false);
                    if ((reg.Open(false) && reg.GetValue("CheckForUpdates", 1) == 0))
                        return;
                }
                catch { }
#endif
            }
            // this.registry.SetValue(Defs.PN.LastTime.ToString(), 0L);
            if (Log.IsDebugEnabled)
                Log.Debug("Start UpdateChecker");
            //this.updateChecker = new UpdateChecker(this.Visible ? this : null,
            //        (int)this.legacyDvsProgId,
            //        this.registry, Thread.CurrentThread.CurrentCulture.Name,
            //        !startupCheck, Log);
            //this.updateChecker.Check();
        }

        protected virtual string GetOutputFolder()
        {
            //Dictionary<string, string> predefined = new Dictionary<string,string>();
            if (string.IsNullOrEmpty(this.defaultOutputFolder))
            {
                if (this.propman.ContainsKey(DvsMain.UI_OutputFolder_PN))
                {
                    this.defaultOutputFolder = this.propman[DvsMain.UI_OutputFolder_PN];
                    // Parse some predefined values
                    if (this.defaultOutputFolder.Contains("%MYMUSIC%"))
                        this.defaultOutputFolder = this.defaultOutputFolder.Replace("%MYMUSIC%", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic));
                    else if (this.defaultOutputFolder.Contains("%VIDEOS%"))
                        this.defaultOutputFolder = this.defaultOutputFolder.Replace("%VIDEOS%", FileUtils.GetMyVideoFolderPath());
                    else if (this.defaultOutputFolder.Contains("%MYPICTURES%"))
                        this.defaultOutputFolder = this.defaultOutputFolder.Replace("%MYPICTURES%", Environment.GetFolderPath(Environment.SpecialFolder.MyPictures));

                    if (this.defaultOutputFolder.Contains("%APPNAME%"))
                        this.defaultOutputFolder = this.defaultOutputFolder.Replace("%APPNAME%", this.appAsmName);

                    if (!this.defaultOutputFolder.EndsWith("\\"))
                        this.defaultOutputFolder += "\\";
                }
                else
                {
                    this.defaultOutputFolder = FileUtils.GetDvsPath(FileUtils.DvsFolderType.Personal, this.appAsmName);
                }
            }

            // return GetProvider().GetString( Defs.PN.OutputFolder.ToString() ) + ( this.outputFolderText.Text.EndsWith( "\\" ) ? "" : "\\" );
            if (GetProvider().ContainsKey(Defs.PN.OutputFolder.ToString()))
                return GetProvider().Get<string>(Defs.PN.OutputFolder.ToString(), this.defaultOutputFolder);
            return this.defaultOutputFolder;
        }

        protected virtual string GetInputFileFilter()
        {
            string filter = null;
            if (this.propman.ContainsKey(DvsMain.UI_InputFilter_PN))
            {
                filter = this.propman[DvsMain.UI_InputFilter_PN];
                // Parse some predefined values
                if (filter == "AUDIOFILES")
                    filter = AudioDefs.AllAudioFormatsFileDialogFilter;
                else if (filter == "VIDEOFILES")
                    filter = VideoDefs.AllVideoFormatsFileDialogFilter;
            }
            return filter;
        }

        protected void Restart(bool clearLog)
        {
            //if (clearLog)
            //    (Log as LogManager).ClearLog(); //TODO
            ProcessStartInfo info = new ProcessStartInfo(new Uri(Assembly.GetCallingAssembly().CodeBase).LocalPath);
            info.UseShellExecute = false;
            Process.Start(info);
            Application.Exit();
        }

        protected PropsProvider GetProvider()
        {
            if (this.provider == null)
                this.provider = new PropsProvider(new RegistryPropsReader(this.appRegKey));
            return this.provider;
        }

        protected IPropsSaveToHelper GetPropsSaver()
        {
            return GetProvider() as IPropsSaveToHelper;
        }

        protected virtual void WriteProviderValues()
        {
            (this.provider as IPropsSaveToHelper).SaveTo(new RegistryPropsWriter(this.appRegKey));
            if (this.provider.ContainsKey(Defs.PN.LogLevel.ToString()))
                Regedit.WriteValue(false, Programs.DVS_REG_ROOT + Defs.PN.Logger.ToString(), Defs.PN.LogLevel.ToString(), this.provider.GetInt(Defs.PN.LogLevel.ToString()));
        }

        protected virtual void OnFormClosing(object sender, FormClosingEventArgs e)
        {
            WriteProviderValues();

            if (m_bShowOfferScreen && !m_bIsActivated)
                LaunchSubscriptionOffer();
        }

        protected virtual void LaunchSubscriptionOffer()
        {
            string args = string.Format("-{0}=", Defs.CommonCmdArgKeyNames.tb.ToString());
            args += m_bOfferShallInstallConduit ? ToolbarHelper.ToolbarType.conduit.ToString() : ToolbarHelper.ToolbarType.ask.ToString();
            Functions.LaunchTool(Programs.ToolID.SubscriptionOffer, args);
        }
        #endregion

        #region Public methods

        //public static void LoadPresetsToComboBox(ComboBox combobox, IList<Preset> presets)
        //{
        //    combobox.Items.Clear();

        //    foreach (Preset preset in presets)
        //    {
        //        combobox.Items.Add(new ComboBoxItem<Preset>(preset, preset.Name));
        //    }
        //}

        public override void OnSystemThemeChanged(bool visualStylesEnabled /*dummy, the actual value is taken directly from the system*/)
        {
            m_bVisualStylesEnabled = NativeMethods.IsThemeActive() != 0;
            if (this.SystemThemeChangedEvent != null)
                this.SystemThemeChangedEvent.Invoke(visualStylesEnabled);
        }

        public virtual void SetTheme(bool createBackgrounds)
        {
            if (!this.themeLoader.Loaded)
                return;

            bool isThemeClassic = NativeMethods.IsThemeActive() == 0;

            // Back color
            if (this.originalBackColor == Color.Transparent)
                this.originalBackColor = this.BackColor;

            this.BackColor = this.themeLoader.GetColor("BackColor", this.originalBackColor);

            bool backColorChanged = this.BackColor != this.originalBackColor;

            // General colors (default for all controls)
            Color foreColor = this.themeLoader.GetColor("ForeColor", Color.Transparent);
            Color backColor = this.themeLoader.GetColor("BackColor", Color.Transparent);

            // Default font
            string defaultFontDef = this.themeLoader.GetProperty("Default.Font");

            // The background image
            this.BackgroundImage = this.themeLoader.GetImage(ThemeLoader.Background_EN);

            foreach (Control c in this.Controls)
            {
                if (createBackgrounds)
                {
                    IDvsControl idvsControl = c as IDvsControl;
                    if (idvsControl != null)
                    {
                        if (this.themeLoader.HasImage(c.Name))
                        {
                            idvsControl.SetBackground(this.themeLoader.GetImage(c.Name) as Bitmap);
                        }
                        else //if (this.BackgroundImage != null)
                        {
                            idvsControl.SetBackground(GraphicsUtils.GetBitmapClip((Bitmap)this.BackgroundImage, c.Left - 2, c.Top, c.Width, c.Height));
                        }
                    }
                }

                if (c is Button)
                {
                    c.BackColor = Color.FromKnownColor(isThemeClassic ? KnownColor.Control : KnownColor.Transparent);
                }
                else if (c is TextBox)
                {
                    // Set the fore color
                    if (!this.uiDefaultColors.ContainsKey(c.Name))
                        this.uiDefaultColors[c.Name] = c.ForeColor;
                    if (themeLoader.HasColor(c.Name))
                        c.ForeColor = themeLoader.GetColor(c.Name, this.uiDefaultColors[c.Name]);
                    else
                        c.ForeColor = foreColor != Color.Transparent ? foreColor : this.uiDefaultColors[c.Name];

                    string backColorPropName = c.Name + ".BackColor";
                    if (!this.uiDefaultColors.ContainsKey(backColorPropName))
                        this.uiDefaultColors[backColorPropName] = c.BackColor;

                    if (themeLoader.HasColor(backColorPropName))
                        c.BackColor = themeLoader.GetColor(backColorPropName, this.uiDefaultColors[backColorPropName]);
                    else
                        c.BackColor = this.uiDefaultColors[backColorPropName];
                }
                else if (c is Label || c is ListView || c is CheckBox || c is RadioButton)
                {
                    // Set the fore color
                    if (!this.uiDefaultColors.ContainsKey(c.Name))
                        this.uiDefaultColors[c.Name] = c.ForeColor;
                    if (themeLoader.HasColor(c.Name))
                        c.ForeColor = themeLoader.GetColor(c.Name, this.uiDefaultColors[c.Name]);
                    else
                        c.ForeColor = foreColor != Color.Transparent ? foreColor : this.uiDefaultColors[c.Name];

                    // Back color
                    string backColorId = c.Name + ".BackColor";

                    if (!this.uiDefaultColors.ContainsKey(backColorId))
                        this.uiDefaultColors[backColorId] = c.BackColor;

                    if (themeLoader.HasColor(backColorId))
                        c.BackColor = themeLoader.GetColor(backColorId, this.uiDefaultColors[backColorId]);
                    else
                        c.BackColor = backColor != Color.Transparent ? backColor : this.uiDefaultColors[backColorId];

                    // Set the font
                    string customFont = this.themeLoader.GetProperty(c.Name + ".Font");

                    if (!uiDefaultFonts.ContainsKey(c.Name))
                        uiDefaultFonts[c.Name] = c.Font;

                    if (string.IsNullOrEmpty(defaultFontDef) && string.IsNullOrEmpty(customFont))
                        c.Font = uiDefaultFonts[c.Name];
                    else
                        c.Font = WindowUtils.CreateFont(uiDefaultFonts[c.Name], string.IsNullOrEmpty(customFont) ? defaultFontDef : customFont);
                }
            }
        }

        #endregion
    }
}
