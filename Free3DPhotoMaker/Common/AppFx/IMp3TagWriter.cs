using System;
using System.Collections.Generic;
using System.Text;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.AppFxApi
{
    public interface IMp3TagWriter : ILoggable
    {
        void Init(string pk); // DVS private key
        bool Initialized { get; }
        void SetTempPath(string value);
        void SetVideoConverter(object converter);
        void Write(string fileName, ConversionInfo info);
    }
}
