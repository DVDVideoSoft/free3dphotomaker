using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public static class WebDefs
    {
        public static int kDefaultProxyPort = 80;
    }

    public static class WebUtils
    {
        public static string GetMimeByExt(string ext)
        {
            string mediaType = "";

            switch (ext.ToLower())
            {
                case ".avi":
                    mediaType = "video/avi";
                    break;
                case ".mpeg":
                case ".mts":
                case ".mpg":
                case ".m4v":
                case ".mov":
                    mediaType = "video/quicktime";
                    break;
                case ".mp4":
                    mediaType = "video/mp4";
                    break;
                case ".wmv":
                    mediaType = "video/x-ms-wmv";
                    break;
                case ".flv":
                    mediaType = "flv-application/octet-stream";
                    break;
                case ".3gp":
                    mediaType = "video/3gpp";
                    break;
                case ".mkv":
                    mediaType = "video/x-matroska";
                    break;
                case ".ogg":
                    mediaType = "application/ogg";
                    break;
                case ".divx":
                    mediaType = "video/divx";
                    break;
                case ".swf":
                    mediaType = "application/x-shockwave-flash";
                    break;
            }

            return mediaType;
        }
    }
}
