using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public interface ILoggable
    {
        void SetLogger(ILogWriter log);
    }
}
