using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DVDVideoSoft.AppFxApi
{
    public delegate void VoidDelegate();
    public delegate void VoidIntDelegate(int value);
    public delegate void VoidStringDelegate(string value);
    public delegate DialogResult DialogResultFormDelegate(Form value);
    public delegate void VoidExceptionHandler(Exception value);

    public class Defs
    {
        #region String IDs in the form of pseudo enums

        /// <summary>
        /// AppExtender actions
        /// </summary>
        public enum ACT
        {
            _None,
            ModifyForm,
            ModifyOptionsForm,
            SetProgressForm,
            OnCultureChanged,
            OnFinalCultureChanged,
            OnAppThemeChanged,
            OnShown,
            SaveState,
            BeforeConfigurationChanged,
            ConfigurationChanged,
            OnMainAction,
            PostConvert,
            ItemsChanged,
            CheckButtonsState,
            ThemeChanged,
            OnItemComplete,
            OnProceedToNextItem,
            OnAllItemsComplete,
            OnProcessingCancelled,
            OnPresetChanged,
            OnProgressChanged,
            AddItem,
            OnCreateConversionItems,
            VerifyCategory,
            UpdateSize,
            GetConverter,
            BeforeConvert,
            CheckForPrerequisites
        }

        /// <summary>
        /// Property names ('PNs')
        /// </summary>
        public enum PN
        {
            AccelerateEncoding,
            ActivationData,
            AddArtwork,
            //ShowInBrowserCtxMenu,
            AllowMultipleInstances,
            AppId,
            AppPath,
            AppTitle,
            AppTitleWithVersion,
            AsmName,
            AutoPasteUrl,
            AutoStart,
            AutoNumberTracks,
            AutoCompleteTags,
            AvailableThemes,
            Cancel,
            CheckForUpdates,
            CheckUpdateResult,
            Completed,
            CreateITunesPlaylist,
            CreateM3UPlaylist,
            CurrentCulture,
            CurrentTheme,
            DefaultComment,
            DesiredVideoQuality,
            DontShowSubscriptionExpiredMsg,
            ExpireDate,
            EnableM3UOption,
            EnableSounds,
            ExitAfterStartupProcessing,
            ForbidAccelerateEncodingOption,
            ForbidITunesOption,
            TagsEditingEnabled,
            FormSize,
            Id,
            IncludeDate,
            IncludeDownloadDate,
            IncludeUploadDate,
            IncludeOriginalFileName,
            InputFolder,
            IsActivated,
            IsRelocatingControlsEnabled,
            ItemsToHide,
            LastCategoryIndex,
            LastPresetIndex,
            LastTime,
            LogLevel,
            Logger,
            NamePostfix,
            NamePrefix,
            Next, //rather a strange name for the update check duration
            OutputFolder,
            OutputNamePattern,
            Password,
            ProxyAddress,
            ProxyPassword,
            ProxyPort,
            ProxyUserName,
            Registry,
            RecentlyActivated,
            RelocatedControls,
            SaveOriginalFiles,
            SeparatorIndex,
            ShowPrerequisiteForm,
            ShutDown,
            StartupProcessingMode,
            Timeout,
            TranscoderAvailability,
            TranscoderAvailableType,
            UseAlternateAlgorithm,
            UseCookies,
            UseManualProxySettings,
            UseProxy,
            UserName,
            VersionStr,
            WindowState,
            WindowX,
            WindowY,
            WriteCurrentYearToTags,
            LaunchBurnerIndex,
        }

        public static Defs.ACT StringToAction(string value)
        {
            Defs.ACT result = ACT._None;

            try {
                result = DVDVideoSoft.Utils.Helper<Defs.ACT>.StringToEnum(value);
            } catch { }

            return result;
        }

        public enum CommonCmdArgKeyNames
        {
            lang,
            tb
        }

        #endregion

        #region Messages and strings

        public const string LoggerCannotBeNull_STR = "Logger cannot be null";
        
        #endregion
    }
}
