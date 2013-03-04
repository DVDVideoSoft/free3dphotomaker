using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public interface IAVConverterCapability
    {
        IDictionary<int, string> SupportedContainers { get; }
        IDictionary<int, string> SupportedVideoCodecs { get; }        
        /*IDictionary<int, int> FormatToCodecDict { get; }
        IDictionary<int, string> VideoExtensions { get; }
        string GetCodecByFormat(int format);
        string GetCodecName(int codec);*/
    }
}
