using System;
using System.Collections.Generic;
using System.Text;

using DVDVideoSoft.Utils;
using DVDVideoSoft.LoggerBridge;

namespace DVDVideoSoft.AppFx
{
    public class Logger
    {
        private static LoggerBridge.LoggerBridge loggerBridge = new LoggerBridge.LoggerBridge();
        private static object loggerBridgeLocker = new object();
        private static UInt64 loggerBridgeCounter = 0;

        private class LogWriter : ILogWriter, IDisposable
        {
            private LoggerBridge.LoggerBridge logger;
            private string module;

            public LogWriter(LoggerBridge.LoggerBridge logger, string module)
            {
                this.logger = logger;
                this.module = module;
            }

            #region ILoggerWriter methods

            public LoggerLevel GetLoggerLevel()
            {
                if (logger == null)
                    return LoggerLevel.Off;
                return this.logger.GetLoggerLevel();
            }

            public bool IsLoggerLevelEnabled(LoggerLevel loggerLevel)
            {
                if (logger == null)
                    return false;
                return this.logger.IsLoggerLevelEnabled(loggerLevel);
            }

            public bool IsOffEnabled
            {
                get
                {
                    if (logger == null)
                        return true;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Off);
                }
            }

            public bool IsFatalEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Fatal);
                }
            }

            public bool IsErrorEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Error);
                }
            }

            public bool IsWarningEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Warning);
                }
            }

            public bool IsInfoEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Info);
                }
            }

            public bool IsDebugEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Debug);
                }
            }

            public bool IsTraceEnabled
            {
                get
                {
                    if (logger == null)
                        return false;
                    return this.logger.IsLoggerLevelEnabled(LoggerLevel.Trace);
                }
            }

            public void WriteLoggerLine(
                            LoggerLevel loggerLevel,
                            string message,
                            ulong tag)
            {
                if (logger == null)
                    return;
                this.logger.WriteLoggerLine(
                    loggerLevel,
                    this.module == null ? "" : this.module,
                    message == null ? "" : message,
                    tag);
            }

            public void Fatal(string message)
            {
                WriteLoggerLine(LoggerLevel.Fatal, message, 0);
            }

            public void Crit(string message)
            {
                WriteLoggerLine(LoggerLevel.Crit, message, 0);
            }

            public void Error(string message)
            {
                WriteLoggerLine(LoggerLevel.Error, message, 0);
            }

            public void Warning(string message)
            {
                WriteLoggerLine(LoggerLevel.Warning, message, 0);
            }

            public void Notice(string message)
            {
                WriteLoggerLine(LoggerLevel.Notice, message, 0);
            }

            public void Info(string message)
            {
                WriteLoggerLine(LoggerLevel.Info, message, 0);
            }

            public void Debug(string message)
            {
                WriteLoggerLine(LoggerLevel.Debug, message, 0);
            }

            public void Trace(string message)
            {
                WriteLoggerLine(LoggerLevel.Trace, message, 0);
            }

            #endregion

            #region IDisposable

            public void Dispose()
            {
                Logger.ReleaseLoggerBridge();
            }

            #endregion
        }

        private Logger()
        {
        }

        public static ILogWriter GetLogger(string module)
        {
            return new LogWriter(Logger.GetLoggerBridge(), module);
        }

        private static LoggerBridge.LoggerBridge GetLoggerBridge()
        {
            lock (Logger.loggerBridgeLocker)
            {
                if (Logger.loggerBridge == null)
                {
                    Logger.loggerBridge = new LoggerBridge.LoggerBridge();
                    Logger.loggerBridgeCounter = 1;
                }
                else
                {
                    Logger.loggerBridgeCounter += 1;
                }
            }
            return Logger.loggerBridge;
        }

        private static void ReleaseLoggerBridge()
        {
            lock (Logger.loggerBridgeLocker)
            {
                if (Logger.loggerBridge != null)
                {
                    Logger.loggerBridgeCounter -= 1;
                    if (Logger.loggerBridgeCounter == 0)
                    {
                        Logger.loggerBridge.Dispose();
                        Logger.loggerBridge = null;
                    }
                }
            }
        }
    }
}