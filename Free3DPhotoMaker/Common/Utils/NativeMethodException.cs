using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    class NativeMethodException : Exception
    {
        public const UInt64 Success = 0;
        public const UInt64 ServerBusy = 1;
        public const UInt64 ServerNotStarted = 2;
        public const UInt64 InternalError = 3;
        public const UInt64 GetServerStatusFailed = 4;
        public const UInt64 StartingOfServerFailed = 5;
        public const UInt64 ServerBindFailed = 6;
        public const UInt64 LoadingOfFilterListFailed = 7;
        public const UInt64 LoadingOfCompressorListFailed = 8;
        public const UInt64 GetFiltersCountFailed = 9;
        public const UInt64 GetFilterFailed = 10;
        public const UInt64 ActivateFilterFailed = 11;
        public const UInt64 FirstDllModuleNotFound = 12;
        public const UInt64 SecondDllModuleNotFound = 13;
        public const UInt64 UnauthorizedAccessException = 14;
        public const UInt64 CopyOfDllModuleFailed = 15;
        public const UInt64 MediaFileNotDefined = 16;
        public const UInt64 MediaFileNotFound = 17;
        public const UInt64 OpenMediaFileFailed = 18;
        public const UInt64 MediaFileAlreadyOpened = 19;
        public const UInt64 MediaFileConversionFailed = 20;
        public const UInt64 CompressorNotSupported = 21;
        public const UInt64 DllModuleNotLoaded = 22;
        public const UInt64 DllMethodNotLoaded = 23;
        public const UInt64 DllMethodDelegateNotCreated = 24;
        public const UInt64 MediaFileNotOpened = 25;
        public const UInt64 FirstMediaFileNotOpened = 26;
        public const UInt64 SecondMediaFileNotOpened = 27;
        public const UInt64 MoveFilterPositionFailed = 28;
        public const UInt64 UpdateFilterOptionsFailed = 29;
        public const UInt64 DeactivateFilterFailed = 30;
        public const UInt64 GetFilterParameterFailed = 31;
        public const UInt64 GetActiveFiltersCountFailed = 32;
        public const UInt64 GetActiveFilterFailed = 33;
        public const UInt64 QueryIsServerStartedFailed = 34;
        public const UInt64 QueryIsServerBusyFailed = 35;
        public const UInt64 StopServerFailed = 36;

        private UInt64 systemError;
        private UInt64 customError;

        public NativeMethodException(UInt64 systemError, UInt64 customError)
            : base()
        {
            this.systemError = systemError;
            this.customError = customError;
        }

        public NativeMethodException(UInt64 systemError, UInt64 customError, string errorMessage)
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
