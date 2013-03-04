using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public class CaughtAndHandledException : ApplicationException
    {
        public enum ErrorType
        {
            Unknown = 0,
            Application,
            Init,
            System,
            Network,
            Codec
        }

        public CaughtAndHandledException()
        {
        }

        public CaughtAndHandledException(ErrorType errorType, int errorCode)
        {
            base.Data["ErrorType"] = errorType;
            base.Data["ErrorCode"] = errorCode;
        }
    }

    public class InitException : ApplicationException
    {
        public InitException(string message)
            : base(message)
        {
        }
    }

    public class ModuleMissingException : ApplicationException
    {
        private static string messageFmtString = "Necessary module \"{0}\" is missing, the program cannot start working";

        protected string moduleName;

        public ModuleMissingException(string mofuleName)
            : base(string.Format(messageFmtString, mofuleName))
        {
        }
    }

    public class ControllerException : ApplicationException
    {
        public ControllerException(string message)
            : base(message)
        {
        }
    }

    public class CodecsMissedException : ApplicationException
    {
        public string Locale;
        public string ProgramPage;

        public CodecsMissedException(string message, string locale, string programPage)
            : base(message)
        {
            Locale = locale;
            ProgramPage = programPage;
        }
    }
    
    public class NativeComponentException : ApplicationException
    {
        public NativeComponentException(string message)
            : base(message)
        {
        }
    }

    public enum ConversionError
    {
        Success,
        Unknown,
        UnsupportedCodec,
        CodecIsMissing,
        DecodingError,
        EncodingError,
        InvalidVideoParameters,
        InvalidAudioParameters,
        UnsupportedVideoParameters,
        UnsupportedAudioParameters,
    }

    public class ConversionException : ApplicationException
    {
        public ConversionError Code;

        public ConversionException(ConversionError code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }

    public class MediaConverterException : ApplicationException
    {
        public int Code;

        public MediaConverterException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }

    public class FileAdditionException : ApplicationException
    {
        public enum ErrorTypeEnum
        {
            Unknown,
            OpenFailed,
            AlreadyExist,
            InvalidName
        }

        public ErrorTypeEnum ErrorType;

        public FileAdditionException(string aMessage, ErrorTypeEnum errorType)
            : base(aMessage)
        {
            this.ErrorType = errorType;
        }
    }

    public class UploadException : ApplicationException
    {
        public int Code;

        public UploadException()
            : base("")
        {
        }

        public UploadException(int code)
            : base("")
        {
            this.Code = code;
        }

        public UploadException(int code, string message)
            : base(message)
        {
            this.Code = code;
        }
    }
}
