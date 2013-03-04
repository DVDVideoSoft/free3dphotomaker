using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    //public enum LoggerLevel
    //{
    //    Off = 0,
    //    Fatal = 1,
    //    Error = 2,
    //    Warning = 3,
    //    Info = 4,
    //    Debug = 5,
    //    Trace = 6
    //}

    public enum LoggerLevel
    {
        Off = 0,
        Fatal = 1,
        Crit = 2,
        Error = 3,
        Warning = 4,
        Notice = 5,
        Info = 6,
        Debug = 7,
        Trace = 8
    }
          
    public interface ILogWriter
    {
        LoggerLevel GetLoggerLevel();
        bool IsLoggerLevelEnabled(LoggerLevel loggerLevel);
        bool IsOffEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsWarningEnabled { get; }
        bool IsInfoEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsTraceEnabled { get; }
        void WriteLoggerLine(LoggerLevel loggerLevel, string message, ulong tag);
        void Fatal(string message);
        void Crit(string message);
        void Error(string message);
        void Warning(string message);
        void Notice(string message);
        void Info(string message);
        void Debug(string message);
        void Trace(string message);
    }
}
