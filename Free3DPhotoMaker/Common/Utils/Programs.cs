using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Win32;

namespace DVDVideoSoft.Utils
{
    public class Programs
    {
        public static readonly string CompanyName = "DVDVideoSoft";
        public static readonly string ProductName = "Free Studio";

        public static readonly string DVS_REG_ROOT = "SOFTWARE\\DVDVideoSoft\\";
        public static readonly string LINKS_REG_KEY = DVS_REG_ROOT + "Manager\\Links";

        public static readonly string DVS_REG_KEY = DVS_REG_ROOT.Remove(DVS_REG_ROOT.Length - 1, 1);
        public static readonly string UNINSTALL_REG_KEY = DVS_REG_ROOT + "UninstallPaths";
        public static readonly string APP_PATHS_REG_KEY = DVS_REG_ROOT + "AppPaths";
        public static readonly string STARTUP_REG_KEY = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";

        public static readonly string LogsSubDirName = "logs";

        #region Registry value names to avoid hardcoding constants in many places

        public enum RVN
        {
            SharedDir,
            SharedLibVer,
            ShowInBrowserCtxMenu,
        }

        #endregion

        public static ID GetProgramID(string idString)
        {
            if (string.IsNullOrEmpty(idString))
                return ID.Unknown;

            for (int id = (int)ID.Manager; id < 999; id++)
            {
                if (string.Compare(((ID)id).ToString(), idString, true) == 0)
                    return (ID)id;
            }

            return ID.Unknown;
        }

        public static string GetRegKey(Programs.ID id)
        {
            string appName = id.ToString();
            return DVS_REG_ROOT + appName;
        }

        public static string GetRegKey(string appName)
        {
            if (!string.IsNullOrEmpty(appName))
                return DVS_REG_ROOT + appName;
            return null;
        }

        public static string GetHumanName(string strProgId)
        {
            ID progId = GetProgramID(strProgId);
            if (progId == ID.Unknown)
                return strProgId;
            return GetHumanName(progId);
        }

        public static string GetHumanName(Programs.ID id)
        {
            switch (id)
            {
                case ID.Manager:
                    return "Free Studio Manager";
                case ID.FreeAudioCDBurner:
                    return "Free Audio CD Burner";
                case ID.FreeAudioConverter:
                    return "Free Audio Converter";
                case ID.FreeAudioDub:
                    return "Free Audio Dub";
                case ID.FreeDiscBurner:
                    return "Free Disc Burner";
                case ID.FreeDVDDecrypter:
                    return "Free DVD Decrypter";
                case ID.FreeDVDVideoBurner:
                    return "Free DVD Video Burner";
                case ID.Free3GPVideoConverter:
                    return "Free 3GP Video Converter";
                case ID.FreeVideoDub:
                    return "Free Video Dub";
                case ID.FreeVideoFlipAndRotate:
                    return "Free Video Flip and Rotate";
                case ID.FreeVideoToDVDConverter:
                    return "Free Video to DVD Converter";
                case ID.FreeVideoToFlashConverter:
                    return "Free Video to Flash Converter";
                case ID.FreeVideoToiPhoneConverter:
                    return "Free Video to iPhone Converter";
                case ID.FreeVideoToiPodConverter:
                    return "Free Video to iPod Converter";
                case ID.FreeVideoToJPGConverter:
                    return "Free Video to JPG Converter";
                case ID.FreeVideoToMP3Converter:
                    return "Free Video to MP3 Converter";
                case ID.FreeYTVDownloader:
                    return "Free YouTube Download";
                case ID.FreeYouTubeToiPhoneConverter:
                    return "Free YouTube to iPhone Converter";
                case ID.FreeYouTubeToiPodConverter:
                    return "Free YouTube to iPod Converter";
                case ID.FreeYouTubeToMP3Converter:
                    return "Free YouTube to MP3 Converter";
                case ID.FreeYouTubeUploader:
                    return "Free YouTube Uploader";
                case ID.FreeAudioCDToMP3Converter:
                    return "Free Audio CD to MP3 Converter";
                case ID.FreeDVDVideoConverter:
                    return "Free DVD Video Converter";
                case ID.FreeAudioToFlashConverter:
                    return "Free Audio to Flash Converter";
                case ID.FreeYouTubeToDVDConverter:
                    return "Free YouTube to DVD Converter";
                case ID.FreeScreenVideoRecorder:
                    return "Free Screen Video Recorder";
                case ID.FreeImageConvertAndResize:
                    return "Free Image Convert and Resize";
                case ID.Free3DPhotoMaker:
                    return "Free 3D Photo Maker";
                case ID.FreeVideoToAndroidConverter:
                    return "Free Video to Android Converter";
                case ID.FreeVideoToXboxConverter:
                    return "Free Video to Xbox Converter";
                case ID.FreeVideoToiPadConverter:
                    return "Free Video to iPad Converter";
                case ID.FreeVideoToSonyPSPConverter:
                    return "Free Video to Sony PSP Converter";
                case ID.FreeVideoToHTCPhonesConverter:
                    return "Free Video to HTC Phones Converter";
                case ID.FreeVideoToMotorolaPhonesConverter:
                    return "Free Video to Motorola Phones Converter";
                case ID.FreeVideoToNokiaPhonesConverter:
                    return "Free Video to Nokia Phones Converter";
                case ID.FreeVideoToSamsungPhonesConverter:
                    return "Free Video to Samsung Phones Converter";
                case ID.FreeVideoToAppleTVConverter:
                    return "Free Video to Apple TV Converter";
                case ID.FreeVideoToBlackBerryConverter:
                    return "Free Video to BlackBerry Converter";
                case ID.FreeVideoToLGPhonesConverter:
                    return "Free Video to LG Phones Converter";
                case ID.FreeVideoToCMobilePhonesConverter:
                    return "Free Video to Mobile Phones Converter";
                case ID.FreeVideoToJMobilePhonesConverter:
                    return "Free Video to Mobile Phones Converter";
                case ID.FreeVideoToNintendoConverter:
                    return "Free Video to Nintendo Converter";
                case ID.FreeVideoToSonyPlayStationConverter:
                    return "Free Video to Sony PlayStation Converter";
                case ID.Free3DVideoMaker:
                    return "Free 3D Video Maker";
                case ID.FreeVideoToSonyPhonesConverter:
                    return "Free Video to Sony Phones Converter";
                case ID.FreeWebMVideoConverter:
                    return "Free WebM Video Converter";
                case ID.FreeMP4VideoConverter:
                    return "Free MP4 Video Converter";
                case ID.FreeAVIVideoConverter:
                    return "Free AVI Video Converter";
                case ID.FreeVideoToTabletPCConverter:
                    return "Free Video to Tablet PC Converter";
				case ID.FreeHTML5VideoPlayerAndConverter:
                    return "Free HTML5 Video Player and Converter";
            }
            return "";
        }

        public enum ID
        {
            Unknown = -1,
            Manager = 0,
            FreeAudioCDBurner = 1,
            FreeAudioConverter = 2,
            FreeAudioDub = 3,
            FreeDiscBurner = 4,
            FreeDVDDecrypter = 5,
            FreeDVDVideoBurner = 6,
            Free3GPVideoConverter = 7,
            FreeVideoDub = 8,
            FreeVideoFlipAndRotate = 9,
            FreeVideoToDVDConverter = 10,
            FreeVideoToFlashConverter = 11,
            FreeVideoToiPhoneConverter = 12,
            FreeVideoToiPodConverter = 13,
            FreeVideoToJPGConverter = 14,
            FreeVideoToMP3Converter = 15,
            FreeYTVDownloader = 16,
            FreeYouTubeToiPhoneConverter = 17,
            FreeYouTubeToiPodConverter = 18,
            FreeYouTubeToMP3Converter = 19,
            FreeYouTubeUploader = 20,
            FreeAudioCDToMP3Converter = 21,
            FreeDVDVideoConverter = 22,
            FreeAudioToFlashConverter = 23,
            FreeYouTubeToDVDConverter = 24,
            FreeScreenVideoRecorder = 25,
            FreeUploaderForFacebook = 26,
            FreeImageConvertAndResize = 27,
            Free3DPhotoMaker = 28,
            FreeVideoToAndroidConverter = 29,
            FreeVideoToXboxConverter = 30,
            FreeVideoToiPadConverter = 31,
            FreeVideoToSonyPSPConverter = 32,
            FreeVideoToHTCPhonesConverter = 33,
            FreeVideoToMotorolaPhonesConverter = 34,
            FreeVideoToNokiaPhonesConverter = 35,
            FreeVideoToSamsungPhonesConverter = 36,
            FreeVideoToAppleTVConverter = 37,
            FreeVideoToBlackBerryConverter = 38,
            FreeVideoToLGPhonesConverter = 39,
            FreeVideoToCMobilePhonesConverter = 40,
            FreeVideoToJMobilePhonesConverter = 41,
            FreeVideoToNintendoConverter = 42,
            FreeVideoToSonyPlayStationConverter = 43,
            Free3DVideoMaker = 44,
            FreeVideoToSonyPhonesConverter = 45,
            FreeWebMVideoConverter = 46,
            FreeMP4VideoConverter = 47,
            FreeAVIVideoConverter = 48,
            FreeVideoToTabletPCConverter = 49,
			FreeHTML5VideoPlayerAndConverter = 50,
        };

        public static IList<string> AppNames
        {
            get
            {
                List<string> appNames = new List<string>();
                //Don't have time to write a func since we're moving out of .net
                Array appIds = Enum.GetValues(typeof(Programs.ID));
                int lastAppId = (int)appIds.GetValue(0);
                foreach (Programs.ID id in appIds)
                {
                    if ((int)id > lastAppId)
                        lastAppId = (int)id;
                }
                for (int i = 1; i < lastAppId; i++)
                {
                    Programs.ID id = (Programs.ID)i;
                    appNames.Add(id.ToString());
                }
                return appNames;
            }
        }

        public enum ToolID
        {
            SubscriptionOffer,
            DVSUpdate,
            CodecPack,
            BrowserHelpersInstaller,
            DVSSysReport,
            ContextMenuHelper,
        }

        public static string GetAppPath(string appName)
        {
            return Regedit.ReadString(true, Programs.APP_PATHS_REG_KEY, appName, "");
        }

        public static string GetProgramPageUrlSubstring(ID id)
        {
            if (id == 0)
                return "free-dvd-video-software.htm";

            return "products/dvd/" + GetHumanName(id).Replace(' ', '-') + ".htm";
        }

        public static string GetProgramPageUrlSubstring(string appID)
        {
            return GetProgramPageUrlSubstring(GetProgramID(appID));
        }
    }
}
