using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public static class VideoDefs
    {
        public static readonly string AllVideoFormatsFilter = "*.avi;*.ivf;*.div;*.divx;*.mpg;*.mpeg;*.mpe;*.mp4;*.m4v;*.webm;*.wmv;*.asf;*.mov;*.qt;*.mts;*.m2t;*.m2ts;*.mod;*.tod;*.vro;*.dat;*.3gp2;*.3gpp;*.3gp;*.3g2;*.dvr-ms;*.flv;*.f4v;*.amv;*.rm;*.rmm;*.rv;*.rmvb;*.ogv;*.mkv;*.ts;*.vob;*.trp;*.wtv;";
        public static string AllVideoFormatsFileDialogFilter;

        private static readonly string defaultVideoFileFilterTemplateFmtString = @"All video files (*.mp4, *.avi ...)|{0}
|AVI files (*.avi, *.ivf, *.div, *.divx)|*.avi;*.ivf;*.div;*.divx;
|MPEG files (*.mpg, *.mpeg, *.mpe, *.mp4, *.m4v)|*.mpg; *.mpeg;*.mpe;*.mp4;*.m4v;
|WMV files (*.wmv, *.asf)|*.wmv;*.asf;
|WebM files (*.webm)|*.webm;
|Matroska files (*.mkv)|*.mkv;
|QuickTime files (*.mov, *.qt)|*.mov;*.qt;
|HD Video files (*.ts, *.mts, *.m2t, *.m2ts, *.mod, *.tod, *.vro)|*.ts;*.mts;*.m2t;*.m2ts;*.mod;*.tod;*.vro;*.trp;
|DVD Video files (*.vob)|*.vob;
|VCD Compact Disc digital video (View CD) files (*.dat)|*.dat;
|Mobile video files (*.3gp2, *.3gpp, *.3gp, *.3g2)|*.3gp2;*.3gpp;*.3gp;*.3g2;
|DVR-MS files (*.dvr-ms)|*.dvr-ms;
|FLV files (*.flv,*.f4v)|*.flv;*.f4v;
|AMV files (*.amv)|*.amv;
|RealVideo files (*.rm, *.rmm, *.rv, *.rmvb)|*.rm;*.rmm;*.rv;*.rmvb;
|Theora video (*.ogv)|*.ogv;
|MXF files (*.mxf)|*.mxf;
|WTV Windows Recorded TV Show files (*.wtv)|*.wtv;
|All files (*.*)|*.*;";

        // Key MUST be synchronized with VideoFileToIPOD enums
        public static Dictionary<int, string> formatExts;

        static VideoDefs()
        {
            AllVideoFormatsFileDialogFilter = string.Format(defaultVideoFileFilterTemplateFmtString, AllVideoFormatsFilter);

            // Format extensions
            formatExts = new Dictionary<int, string>();
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_SAME_AS_INPUT]      = "";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_MP4] = ".mp4";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_AVI] = ".avi";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_MPG] = ".mpg";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_3GP] = ".3gp";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_FLV] = ".flv";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_SWF] = ".swf";
            //formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.DVS_VIDEO_FORMAT_MP4_H264_MAIN] = ".mp4";
            //formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.DVS_VIDEO_FORMAT_MP4_H264_BASELINE] = ".mp4";
            //formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.DVS_VIDEO_FORMAT_MP4_H264_HIGH] = ".mp4";
            //formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.DVS_VIDEO_FORMAT_AVI_FOR_WII] = ".avi";
            //formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.DVS_VIDEO_FORMAT_NINTENDO_DS] = ".avi";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_MOV] = ".mov";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_WEBM] = ".webm";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_WMV] = ".wmv";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_MKV] = ".mkv";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_M2TS] = ".m2ts";
            formatExts[(int)DVDVideoSoft.VideoFileToIPOD_EXTERN.EMediaContainerType.K_MEDIA_CONTAINER_MPEG] = ".mpg";
        }

        public static string GetContainerName(int format)
        {
            if (formatExts.ContainsKey(format))
                return formatExts[format];

            return null;
        }

        public static string GetFormatExt(int format)
        {
            try
            {
                if (formatExts.ContainsKey(format))
                    return (formatExts[format]).ToLower();
            }
            catch { }

            return null;
        }

        public class VideoWidth
        {
            public static readonly int[] videoWidth = { 176, 320, 220, 352, 480, 512, 640, 704, 720, 854, 960, 1280, 1920, 2048, 3840, 4096, 4520, 7680 };
        }

        public class VideoHeight
        {
            public static readonly int[] videoHeight = { 144, 176, 220, 240, 288, 320, 360, 384, 480, 540, 576, 640, 720, 1080, 1536, 2160, 2540, 3072 };
        }

        public class VideoBitrate
        {
            public static readonly double[] Values = { 0, 32000, 60000, 64000, 119000, 120000, 128000, 144000, 146000, 160000, 192000, 200000, 224000, 256000,
                                                         320000, 400000, 500000, 600000, 700000, 750000, 760000, 900000,
                                                         1000000, 1200000, 1300000, 1500000, 1600000, 1700000, 1800000,
                                                         2000000, 2300000, 2500000, 2800000, 3000000, 3500000, 4000000,
                                                         5000000, 6500000, 7000000, 8000000, 10000000, 20000000, 24000000, 36000000 };
            public static readonly string[] Units = { "Kbit/s", "Mbit/s" };
        }

        public class VideoFrameRate
        {
            public static readonly int[] videoFrameRate = { 0, 12, 15, 20, 24, 25, 30 };
            public static readonly string unit = "fps";
        }

        public static bool IsVideoFormat(string fileName)
        {
            return AllVideoFormatsFilter.Contains("*" + System.IO.Path.GetExtension(fileName).ToLower() + ";");
        }

        public static IList<string> TaggableFormats = new List<string>() { "mp4", "mp3", "m4a", "ape", "ogg", "flac", "wma", "mpc", "asf", "aiff", "wav", "tta" };

        public static bool IsTaggableFormat(string fileName)
        {
            //IList<string> taggableFormatExts = new List<string> { ".mp4", ".mp3", ".m4a", ".ape", ".ogg", ".flac", ".wma", ".mpc", ".asf", ".aiff", ".wav", ".tta" };
            //TODO: Xiph, WavPack

            string formatID = System.IO.Path.GetExtension(fileName).ToLower().Remove(0, 1);
            return TaggableFormats.Contains(formatID);
        }

        public static bool IsSupportedTaggableFormat(string fileName)
        {
            //IList<string> currentlyUnsupportedAudioExts = new List<string>() { ".wma" };

            return IsTaggableFormat(fileName);// && !currentlyUnsupportedAudioExts.Contains(System.IO.Path.GetExtension(fileName).ToLower());
        }
    }
}
