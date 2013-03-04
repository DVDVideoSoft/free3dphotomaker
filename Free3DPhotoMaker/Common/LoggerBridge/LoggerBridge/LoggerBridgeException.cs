using System;

namespace DVDVideoSoft.LoggerBridge
{
    public class LoggerBridgeException : Exception
    {
        public const UInt64 Success = 0;
        public const UInt64 StartingOfServerFailed = 10;
        public const UInt64 DllModuleNotFound = 100;
        public const UInt64 DllModuleNotLoaded = 101;
        public const UInt64 DllMethodNotLoaded = 102;
        public const UInt64 DllMethodDelegateNotCreated = 103;

        private UInt64 systemError;
        private UInt64 customError;

        public LoggerBridgeException(UInt64 systemError, UInt64 customError)
            : base()
        {
            this.systemError = systemError;
            this.customError = customError;
        }

        public LoggerBridgeException(UInt64 systemError, UInt64 customError, string errorMessage)
            : base(errorMessage)
        {
            this.systemError = systemError;
            this.customError = customError;
        }

        public UInt64 CustomError
        {
            get
            {
                return this.customError;
            }
        }

        public UInt64 SystemError
        {
            get
            {
                return this.systemError;
            }
        }
    }
}