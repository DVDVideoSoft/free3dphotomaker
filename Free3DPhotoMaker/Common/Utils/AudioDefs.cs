using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class AudioDefs
    {
        public static readonly string AllAudioFormatsFilter = "*.mp3;*.wav;*.aac;*.m4a;*m4b;*.wma;*.ogg;*.flac;*.alac;*.shn;*.ra;*.ram;*.rm;*.rmm;*.rmvb;*.amr;*.mka;*.tta;*.aiff;*.aif;*.au;*.mpc;*.spx;*.ac3;*.asf;";
        public static string AllAudioFormatsFileDialogFilter;

        private static readonly string defaultAudioFileFilterTemplateFmtString = @"All Audio files (*.mp3, ...)|{0}
|MP3 - MPEG Audio Layer 3 (*.mp3)|*.mp3;
|WAV - Waveform Audio (*.wav)|*.wav;
|AAC - Advanced Audio Coding (*.aac)|*.aac;
|M4A - MPEG-4 Audio (*.m4a, *.m4b)|*.m4a;*.m4b;
|WMA - Windows Media Audio (*.wma)|*.wma;
|ASF - Advanced Systems Format (*.asf)|*.asf;
|OGG - Ogg Vorbis Audio (*.ogg)|*.ogg;
|FLAC - Free Loseless Audio (*.flac)|*.flac;
|ALAC - Apple Loseless Audio (*.alac)|*.alac;
|SHN - Shorten Audio (*.shn)|*.shn;
|RA_ - RealAudio (*.ra, *.ram)|*.ra;*.ram;*.rm;*.rmm;*.rmvb;
|AMR - Adaptive Multi-Rate (*.amr)|*.amr;
|MKA - Matroska Audio (*.mka)|*.mka;
|TTA - True Audio (*.tta)|*.tta;
|AIFF - Audio Interchange File Format (*.aiff, *.aif)|*.aiff;*.aif;
|AU - Sun Microsystems Audio Format (*.au)|*.au;
|MPC - Musepack Audio (*.mpc)|*.mpc;
|SPX - Speex Audio (*.spx)|*.spx;
|AC3 - Dolby Digital Audio (*.ac3)|*.ac3;
|CUE - CUE List Audio (*.cue)|*.cue;
|MP2 - MPEG Audio Layer 2 (*.mp2)|*.mp2;
|All Files (*.*)|*.*;";
        
        static AudioDefs()
        {
            AllAudioFormatsFileDialogFilter = string.Format(defaultAudioFileFilterTemplateFmtString, AllAudioFormatsFilter);
        }

        public class AudioTags
        {
            public static readonly string album_PN    = "Album";
            public static readonly string artist_PN   = "Artist";
            public static readonly string title_PN    = "Title";
            public static readonly string genre_PN    = "Genre";
            public static readonly string comment_PN  = "Comment";
            public static readonly string track_PN    = "Track";
            public static readonly string year_PN     = "Year";
        }

        public static class SampleRates
        {
            public static int[] MP3_SampleRate = { 8000, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000 };
            public static int[] MP2_SampleRate = { 16000, 22050, 24000, 32000, 44100, 48000 };
            public static int[] AAC_SampleRate = { 8000, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000 };
            public static int[] WMA_SampleRate = { 8000, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000 };
            public static int[] OGG_SampleRate = { 8000, 11025, 12000, 16000, 22050, 24000, 32000, 44100, 48000 };
            public static int[] AMR_SampleRate = { 8000 };
            public static string unit = "Hz";
        }

        public static class AudioBitrates
        {
            public const int DEFAULT_AUDIOBITRATE_INDEX = 7;
            public static int[] MP3_Bitrate = { 32, 44, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320 };
            public static int[] MP2_Bitrate = { 32, 48, 56, 64, 96, 112, 128, 160, 192, 224, 256, 320, 384 };
            public static int[] AAC_M4A_Bitrate = { 12, 16, 24, 32, 44, 48, 64, 80, 96, 112, 128, 160, 192, 256, 320 };
            public static int[] WMA_Bitrate = { 32, 44, 48, 59, 64, 80, 96, 112, 128, 160, 192, 256, 320, 440 };
            public static int[] OGG_Bitrate = { 44, 48, 56, 64, 80, 96, 112, 128, 160, 192, 224, 256, 320, 384, 448, 500 };
            public static int[] PCM_Bitrate = { 44, 48, 64, 80, 96, 112, 128, 160, 192, 256, 320, 448, 640, 1400 };
            public static float[] AMR_Bitrate = { 6.6f, 7.4f, 7.95f, 8.85f, 10.2f, 12.2f, 12.65f, 12.8f };
        }

        public static class Channels
        {
            public static readonly string channelOriginal = "Original";
            public static readonly string channelMono     = "Mono";
            public static readonly string channelStereo   = "Stereo";

            private static Dictionary<string, int> dictChannels;

            static Channels()
            {
                dictChannels = new Dictionary<string, int>();
                dictChannels[channelOriginal] = 0;
                dictChannels[channelMono] = 1;
                dictChannels[channelStereo] = 2;
            }

            public static string ToStr(int channelInt)
            { 
                foreach (KeyValuePair<string, int> kvp in dictChannels)
                {
                    if (kvp.Value == channelInt)
                        return kvp.Key; 
                }
                return null;
            }

            public static int ToInt(string channelStr)
            {
                return dictChannels[channelStr];
            }
        }
    }

    public class AudioFormat
    {
        private static readonly IList<string> playableFormats = new List<string> { ".wav", ".mp3", ".wma", ".aac", ".ac3", ".m4a", ".m4b", ".ogg", ".mka", ".tta", ".flac", ".ogg", ".aiff", ".amr", ".mp2" };

        public enum ID
        {
            SameAsInput = -3,
            Raw         = -2,
            Unknown     = -1,
            AAC         = 0,
            MP3         = 1,
            MP2         = 2,
            AMR_NB      = 3,
            AAC_2       = 4,
            OGG         = 8,
            WAV         = 9,
            WMAV2       = 10,
            MP4         = 11,
            FLAC        = 12,
            ALAC        = 13,
            AC3         = 14,
            PCM_S16LE   = 20,
            PCM_S32LE   = 21,
            PCM_U8      = 22,
            VORBIS      = 23,
            M4A         = 24,
        }

        private static AudioFormat.ID[] notBitrateChangeableFormats = { AudioFormat.ID.WAV, AudioFormat.ID.FLAC, AudioFormat.ID.ALAC };

        public static Dictionary<int, string> formatNames;

        static AudioFormat()
        {
            formatNames = new Dictionary<int, string>();
            formatNames[(int)ID.AAC] = "AAC";
            formatNames[(int)ID.MP3] = "MP3";
            formatNames[(int)ID.AMR_NB] = "AMR";
            formatNames[(int)ID.MP4] = "MP4";
            formatNames[(int)ID.WAV] = "WAV";
            formatNames[(int)ID.WMAV2] = "WMA";
            formatNames[(int)ID.OGG] = "OGG";
            formatNames[(int)ID.ALAC] = "ALAC";
            formatNames[(int)ID.FLAC] = "FLAC";
            formatNames[(int)ID.PCM_S16LE] = "PCM 16";
            formatNames[(int)ID.PCM_S32LE] = "PCM 32";
            formatNames[(int)ID.PCM_U8] = "PCM U8";
            formatNames[(int)ID.VORBIS] = "VORBIS";
            formatNames[(int)ID.M4A] = "M4A";
            formatNames[(int)ID.MP2] = "MP2";
            formatNames[(int)ID.AC3] = "AC3";
        }

        public static string GetFormatExt(AudioFormat.ID id)
        {
            try {
                return ("." + formatNames[(int)id]).ToLower();
            } catch { }

            return null;
        }

        public static string GetFormatName(int formatInt)
        {
            try {
                return formatNames[formatInt];
            } catch { }

            return null;
        }

        public static bool IsFormatBitrateChangeable(int formatID)
        {
            foreach (AudioFormat.ID fmt in notBitrateChangeableFormats)
            {
                if (fmt == (AudioFormat.ID)formatID)
                    return false;
            }
            return true;
        }

        public static bool IsPlayable(string fileName)
        {
            bool ret = false;
                
            string lowerFileName = fileName.ToLower();
            foreach (string s in playableFormats)
            {
                ret |= lowerFileName.ToLower().EndsWith(s);
            }
            return ret;
        }
    }
}
