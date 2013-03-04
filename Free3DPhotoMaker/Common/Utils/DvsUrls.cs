using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Microsoft.Win32;

namespace DVDVideoSoft.Utils
{
    public static class DvsUrls
    {
        public static string CompanyRootUrl = "http://dvdvideosoft.com/";
        public static string CompanyRootUrlLangTemplate = "http://dvdvideosoft.com/{0}";
        public static string CompanyToolsUrl = "http://tools.dvdvideosoft.com/";
        public static string DvsCultureUrlTemplate = "http://dvdvideosoft.com{0}/";
        public static string CompanyWwwRoot = "www.dvdvideosoft.com";
        public static string UpdateDirectoryUrl = CompanyToolsUrl + "updates/";
        public static string DownloadUrl = CompanyRootUrl + "download/";
        public static string CompanyRedirectUrlTemplate = CompanyRootUrl + "r/{0}dvdvideosoft.htm";
        public static string FacebookRedirectUrlTemplate = CompanyRootUrl + "r/{0}Facebook.htm";
        public static string TwitterRedirectUrlTemplate = CompanyRootUrl + "r/{0}Twitter.htm";
        public static string SupportUrlTemplate = CompanyRootUrl + "r/support{0}.htm";
        public static string GuideUrlTemplate = DvsCultureUrlTemplate + "guides/";
        public static string ForumsUrlPart = "forums/";
        public static string GuidesUrlPart = "guides/";
        public static string MoreFreeSoftDownloadUrlPart = "free-dvd-video-software-download.htm";
        public static readonly string AboutCuda = CompanyRootUrl + "converter/technology/nvidia-video-converter.htm";
        public static readonly string AboutAti  = CompanyRootUrl + "converter/technology/ATI-stream-technology.htm";

        public static string ImapiRedistUrl = "http://download.microsoft.com/download/4/D/C/4DC0907E-38FF-4CF6-A155-B45C4EF39C35/IMAPI_XP_SRV2003_x86.exe";
        public static string FacebookDVSPage = "http://www.facebook.com/dvdvideosoft";
    }
}
