using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.Utils
{
    public static class PrerequisiteChecker
    {
        public static bool Check(string strAppId)
        {
            Programs.ID appId = Programs.GetProgramID(strAppId);

            switch (appId) {
                case Programs.ID.FreeAudioCDBurner:
                    return WinComponentsUtils.CheckForImapi();
            }
            return true;
        }
    }

    public static class WinComponentsUtils
    {
        public static string GetMSDownloadLang(string strSystemLang)
        {
            string strRet = strSystemLang.Substring(0, 2);
            string strLangInLower = strSystemLang.ToLower();
            if (strRet == "zh") {
                strRet = strSystemLang;
            }
            if (strRet == "pt") {
                strRet = strSystemLang;
            }
            return strRet;
        }

        public static string GetSystemType()
        {
            string strRetType = null;
            if (SysUtils.IsOS64 == true) {
                strRetType = "_SRV2003_x64.exe";
            } else {
                strRetType = "_XP_SRV2003_x86.exe";
            }
            return strRetType;
        }

        public static string GetImapiUrl(string strLang)
        {
            switch (strLang) {
                case "en": return "http://www.microsoft.com/downloads/info.aspx?na=41&srcfamilyid=63ab51ea-99c9-45c0-980a-c556746fcf05&srcdisplaylang=en&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f4%2fD%2fC%2f4DC0907E-38FF-4CF6-A155-B45C4EF39C35%2fIMAPI";
                case "de": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=de&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f0%2f8%2f7%2f0879B127-A35C-432D-8EAF-4DCB04D08DB3%2fIMAPI";
                case "jp": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=ja&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fC%2f9%2f5%2fC9522458-04B1-46CE-AD31-A0F68F74C6D6%2fIMAPI";
                case "fr": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=fr&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f3%2fE%2fA%2f3EA862DA-6CEE-48E8-A152-04A38F5EA0C6%2fIMAPI";
                case "es": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=es&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f7%2f4%2f0%2f7409803F-CB95-48F9-BA1C-4AE0F103FCF2%2fIMAPI";
                case "pt-PT": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=pt-pt&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f3%2fD%2f7%2f3D732B36-F673-4A72-9D88-BAF39979F929%2fIMAPI";
                case "pt-BR": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=pt-br&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f3%2fD%2f7%2f3D732B36-F673-4A72-9D88-BAF39979F929%2fIMAPI";
                case "ru": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=ru&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2fA%2f7%2f9%2fA7989D90-5375-425D-A4AD-434DF2E4B018%2fIMAPI";
                case "zh-CN": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=zh-cn&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f1%2fC%2f4%2f1C41F662-BBAF-4126-A55B-15E5E7B5A566%2fIMAPI";
                case "zh-TW": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=zh-tw&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f1%2fC%2f4%2f1C41F662-BBAF-4126-A55B-15E5E7B5A566%2fIMAPI";
                case "it": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=it&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f3%2fD%2fF%2f3DFCD425-A0E9-4DC8-B7A8-DDA3BDEDA94B%2fIMAPI";
                case "nl": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=nl&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f3%2f9%2f5%2f3956E01B-F860-45B0-BDEF-DFBC12888399%2fIMAPI";
                case "pl": return "http://www.microsoft.com/downloads/info.aspx?na=46&SrcFamilyId=63AB51EA-99C9-45C0-980A-C556746FCF05&SrcDisplayLang=pl&u=http%3a%2f%2fdownload.microsoft.com%2fdownload%2f9%2f6%2f6%2f9665E6D0-8BB0-4BCF-B948-5FA7DBCB17D6%2fIMAPI";
                default: return null;
            }
        }

        public static bool IsImapiPresent()
        {
            if (Environment.OSVersion.Version.Major >= 5 && Environment.OSVersion.Version.Major < 6) {
                if (Regedit.DoesKeyExist(Regedit.HKEY.HKEY_LOCAL_MACHINE, @"SOFTWARE\Classes\CLSID\{2735412E-7F64-5B0F-8F00-5D77AFBE261E}") == false) {
                    return false;
                } else {
                    return true;
                }
            }
            else return true;
        }

        public static bool CheckForImapi()
        {
            if (!WinComponentsUtils.IsImapiPresent()) {
                if (MessageBox.Show(DVDVideoSoft.Resources.CommonData.RequiresImapi, CommonData.Error, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK) {
                    System.Diagnostics.Process.Start(WinComponentsUtils.GetImapiUrl(WinComponentsUtils.GetMSDownloadLang(System.Globalization.CultureInfo.InstalledUICulture.Name)) + WinComponentsUtils.GetSystemType());
                    return false;
                } else {
                    return false;
                }
            } else return true;
        }
    }
}
