using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{

    public class Preset : System.Collections.ObjectModel.Collection<string>, IList<string>
    {
        public static readonly string DefaultCategoryString = "Standard";
        
        // Default values
        private static int defaultAudioFormat = 0;
        private static int defaultAudioBitrate = 128000;
        private static int defaultAudioSampleRate = 44100;
        private static int defaultAudioChannels = 2;
        private static int defaultVideoFormat = 0;
        private static int defaultVideoWidth = 0;
        private static int defaultVideoHeight = 0;
        private static int defaultVideoFrameRate = 30;
        private static int defaultVideoBitrate = 750000;

        public Preset()
        {
            this.Name = "";
            this.Description = "";
            this.Extension = "";
            this.AudioFormat = defaultAudioFormat;
            this.AudioBitrate = defaultAudioBitrate;
            this.AudioSampleRate = defaultAudioSampleRate;
            this.AudioChannels = defaultAudioChannels;
            this.VideoFormat = defaultVideoFormat;
            this.VideoWidth = defaultVideoWidth;
            this.VideoHeight = defaultVideoHeight;
            this.VideoFrameRate = defaultVideoFrameRate;
            this.VideoBitrate = defaultVideoBitrate;
            this.AdditionalParams = "";
        }

        public Preset(string name, string description, string extension,
                        int audiobitrate, int audioSampleRate, int audiochannels, int audioformat,
                        int videoWidth, int videoHeight, double videoFrameRate, int videoBitrate, int videoformat,
                        string category = null, string additionalParams = "")
            : this()
        {
            this.Name = name;
            this.Description = description;
            this.Extension = extension;
            this.AudioFormat = audioformat;
            this.AudioBitrate = audiobitrate;
            this.AudioSampleRate = audioSampleRate;
            this.AudioChannels = audiochannels;
            this.VideoFormat = videoformat;
            this.VideoWidth = videoWidth;
            this.VideoHeight = videoHeight;
            this.VideoFrameRate = videoFrameRate;
            this.VideoBitrate = videoBitrate;
            this.Category = string.IsNullOrEmpty(category) ? DefaultCategoryString : category;
            this.AdditionalParams = additionalParams;
        }


        public Preset(int audiobitrate, int audioSampleRate,
                      int audiochannels, int audioformat,
                      string description, string name,
                      string extension, string additionalParams, string category)
            : this()
        {
            this.Name = name;
            this.Description = description;
            this.Extension = extension;
            this.AudioFormat = audioformat;
            this.AudioBitrate = audiobitrate;
            this.AudioSampleRate = audioSampleRate;
            this.AudioChannels = audiochannels;
            this.VideoFormat = -1;
            this.VideoWidth = 0;
            this.VideoHeight = 0;
            this.VideoFrameRate = 0;
            this.VideoBitrate = 0;
            this.AdditionalParams = "";
            this.Category = string.IsNullOrEmpty(category) ? DefaultCategoryString : category;
        }

        public Preset(int audioFormat, int audioBitrate, int audioSampleRate, int audioChannels)
        {
            this.AudioFormat = audioFormat;
            this.AudioBitrate = audioBitrate;
            this.AudioSampleRate = audioSampleRate;
            this.AudioChannels = audioChannels;
        }

        public Preset(Preset other)
        {
            this.Name = other.Name;
            this.Description = (string)other.Description.Clone();
            this.Extension = (string)other.Extension.Clone();
            this.AudioFormat = other.AudioFormat;
            this.AudioBitrate = other.AudioBitrate;
            this.AudioSampleRate = other.AudioSampleRate;
            this.AudioChannels = other.AudioChannels;
            this.VideoFormat = other.VideoFormat;
            this.VideoWidth = other.VideoWidth;
            this.VideoHeight = other.VideoHeight;
            this.VideoFrameRate = other.VideoFrameRate;
            this.VideoBitrate = other.VideoBitrate;
            this.AdditionalParams = (string)other.AdditionalParams.Clone();
            this.Category = (string)other.Category.Clone();
        }

        public static bool operator ==(Preset a, Preset b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return b.Name == a.Name && b.Description == a.Description && b.Extension == a.Extension &&
                    b.AudioFormat == a.AudioFormat && b.AudioBitrate == a.AudioBitrate && b.AudioSampleRate == a.AudioSampleRate &&
                    b.AudioChannels == a.AudioChannels && b.VideoFormat == a.VideoFormat && b.VideoWidth == a.VideoWidth &&
                    b.VideoWidth == a.VideoWidth && b.VideoHeight == a.VideoHeight && b.VideoFrameRate == a.VideoFrameRate && b.VideoBitrate ==
                    a.VideoBitrate && b.AdditionalParams == a.AdditionalParams && b.Category == a.Category;
        }

        public static bool operator !=(Preset a, Preset b)
        {
            return !(a == b);
        }

        public override bool Equals(System.Object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Preset return false.
            Preset p = obj as Preset;
            if ((System.Object)p == null)
            {
                return false;
            }

            // Return true if the fields match:
            return this == p;
        }

        public override int GetHashCode()
        {
            return (int)((this.AudioBitrate * this.AudioSampleRate * this.AudioChannels * this.VideoFormat * this.VideoWidth * this.VideoHeight) + this.VideoFrameRate);
        }

        public object Clone()
        {
            return new Preset(this);
        }

        public new string this[int index]
        {
            get
            {
                if (index == 0)
                    return this.Description;
                else
                    return this.Name;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Extension { get; set; }
        public int AudioFormat { get; set; }
        public int AudioBitrate { get; set; }
        public int AudioSampleRate { get; set; }
        public int AudioChannels { get; set; }
        public int VideoFormat { get; set; }
        public int VideoWidth { get; set; }
        public int VideoHeight { get; set; }
        public int VideoBitrate { get; set; }
        public double VideoFrameRate { get; set; }
        public string Category { get; set; }
        public string AdditionalParams { get; set; }

        public static List<Preset> LoadPresets(string path)
        {
            PresetCollection profile = new PresetCollection();
            try {
                profile.Load(path);
            } catch { }

            return (List<Preset>)profile.Presets;
        }

        public bool IsAudioOnly()
        {
            return (this.VideoFormat == -1);
        }

        public string AsString()
        {
            Preset p = this;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}[{1}] *{2} = video: [{3}] {4}fps {5:0.#} Kbit/s {6}x{7} audio: [{8}] {9:0.#} KHz {10:0.#} Kbit/s {11}ch", p.Name, p.Description, p.Extension, p.VideoFormat, p.VideoFrameRate, (float)p.VideoBitrate / 1000, p.VideoWidth, p.VideoHeight,
                            p.AudioFormat, (float)p.AudioSampleRate / 1000, (float)p.AudioBitrate / 1000, p.AudioChannels);
            return sb.ToString();
        }
    }
}
