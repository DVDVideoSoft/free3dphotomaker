using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public interface IMediaHelper
    {
        //double GetDuration(string fileName);
        int GetVideoTrackId(string fileName);
        object GetAudioTracks(string fileName);
    }
}
