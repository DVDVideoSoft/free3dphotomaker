using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Resources;

using DVDVideoSoft.Utils;


namespace DVDVideoSoft.AppFxApi
{
    public interface IMediaConverter : ILoggable
    {
        event EventHandler<RunWorkerCompletedEventArgs> Complete;
        event EventHandler<ProgressChangedEventArgs>    ProgressChanged;
        event EventHandler<ItemProcessedEventArgs>      ItemProcessed;

        bool Init(object obj, string value);
        void SetPreset(object preset);
        void SetIProgress(IProgressDlg iprogress);
        void Convert(IList<ConversionItem> items, bool createPlayList);
        void Open(string fileName);
        void Close();
        void Cancel();
        bool IsOpened { get; }
        double Duration { get; }
        double OriginalFrameRate { get; }
        int FirstVideoTrackIndex { get; }
        object GetAudioTracks();

        IList<BasicErrorInfo> ErrorInfos { get; }
        void SetResourceManagers(ResourceManager resman, ResourceManager commonResman);
        void SetTempPath(string value);

        /// <summary>
        /// 'Static' methods, can be used without prior call to Open()
        /// </summary>
        IDictionary<int, string> SupportedVideoFormats { get; }
        IDictionary<int, string> SupportedVideoCodecs { get; }
        IDictionary<int, string> VideoExtensions { get; }
    }
}
