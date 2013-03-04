using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class ProductRegistrationStatus
    {
        private bool isRegistrationSuccess;
        private uint errorCode;
        private string errorMessage;

        public ProductRegistrationStatus(bool isRegistrationSuccess, uint errorCode, string errorMessage)
        {
            this.isRegistrationSuccess = isRegistrationSuccess;
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }

        public bool IsRegistrationSuccess { get { return this.isRegistrationSuccess; } }
        public uint ErrorCode { get { return this.errorCode; } }
        public string ErrorMessage { get { return this.errorMessage; } }
    }
}
