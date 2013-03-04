using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DVDVideoSoft.Utils;
using DVDVideoSoft.Resources;

namespace DVDVideoSoft.DialogForms
{
    public static class MessageBoxes
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <returns>Succeeded/failed</returns>
        public static bool ShowRegistrationResultMessage(IWin32Window parent, ProductRegistrationStatus status)
        {
            bool result = false;
            /*
            if (status == null)
                return false;
            string caption = CommonData.Registration;
            string text = "";
            if (status.IsRegistrationSuccess)
            {
                caption = CommonData.RegistrationIsComplete;
                text = CommonData.ThankYouForRegistering;
            }
            else
            {
                caption = CommonData.RegistrationFailed;
                switch (status.ErrorCode)
                {
                    case SubscriptionException.ErrorCodes.UnknownError:
                    case SubscriptionException.ErrorCodes.InternalError:
                    case SubscriptionException.ErrorCodes.RegServerAddressNotDefined:
                    case SubscriptionException.ErrorCodes.ApplicationLicensePathNotDefined:
                        text = CommonData.AnErrorOccured;
                        break;
                    case SubscriptionException.ErrorCodes.LicenseFileNameNotDefined:
                    case SubscriptionException.ErrorCodes.LicenseFileNotFound:
                        text = CommonData.LicenseFileNotFound;
                        break;
                    case SubscriptionException.ErrorCodes.OpenLicenseFileFailed:
                    case SubscriptionException.ErrorCodes.ReadLicenseFileFailed:
                    case SubscriptionException.ErrorCodes.DecryptLicenseFileFailed:
                    case SubscriptionException.ErrorCodes.InvalidLicenseData:
                    case SubscriptionException.ErrorCodes.EmptyLicenseFile:
                        text = CommonData.LicenseFileCorrupted;
                        break;
                    case SubscriptionException.ErrorCodes.ThisMachineIsNotRegistered:
                        text = CommonData.ThisMachineIsNotRegistered;
                        break;
                    case SubscriptionException.ErrorCodes.LicenseHasExpired:
                        text = CommonData.LicenseHasExpired;
                        break;
                    case SubscriptionException.ErrorCodes.CreateHttpConnectionFailed:
                        text = CommonData.FailedToConnectToTheInternet;
                        break;
                    case SubscriptionException.ErrorCodes.ReadHttpResponseFailed:
                        text = CommonData.FailedToGetResponseFromServer;
                        break;
                    case SubscriptionException.ErrorCodes.SaveLicenseFileFailed:
                        text = CommonData.FailedToSaveLicenseFile;
                        break;
                    case SubscriptionException.ErrorCodes.NotDefinedSubscriptionKey:
                    case SubscriptionException.ErrorCodes.WrongSubscriptionKey:
                    case SubscriptionException.ErrorCodes.ActivationDataNotDefined:
                    case SubscriptionException.ErrorCodes.HardwareIdNotDefined:
                    case SubscriptionException.ErrorCodes.GetHardwareIdFailed:
                    case SubscriptionException.ErrorCodes.EncryptDataToSendFailed:
                    case SubscriptionException.ErrorCodes.RegServerEmptyResponse:
                    case SubscriptionException.ErrorCodes.RegServerRefusedRegistration:
                    case SubscriptionException.ErrorCodes.RegServerReceivedDocumentDecryptionFailed:
                    case SubscriptionException.ErrorCodes.RegServerActivationStateNotDefined:
                    case SubscriptionException.ErrorCodes.RegServerEmptyHardwareId:
                    case SubscriptionException.ErrorCodes.DecryptHardwareIdFromResponseFailed:
                    case SubscriptionException.ErrorCodes.EncryptDocumentFromResponseFailed:
                    case SubscriptionException.ErrorCodes.InvalidRegistrationData:
                        text = CommonData.WrongSubscriptionKey;
                        break;
                    default:
                        text = CommonData.WrongSubscriptionKey;
                        break;
                }
            }

            DialogResult result = MessageBox.Show(parent, text, caption, MessageBoxButtons.OK);

             * */
            return (result);
        }

        public static void TryToReadSubscriptionInfo(IWin32Window parent, ILogWriter Log, AppFxApi.VoidDelegate func, AppFxApi.VoidDelegate closeSplashFunc)
        {
            /*
            try
            {
                func();
            }
            catch (SubscriptionException ex)
            {
                string msg = "";
                switch (ex.ErrorCode)
                {
                    case SubscriptionException.ErrorCodes.InvalidLicenseData:
                        msg = CommonData.LicenseFileCorrupted;
                        break;
                    case SubscriptionException.ErrorCodes.OpenLicenseFileFailed:
                        break;
                    case SubscriptionException.ErrorCodes.LicenseHasExpired:
                        msg = CommonData.LicenseHasExpired;
                        break;
                    case SubscriptionException.ErrorCodes.ThisMachineIsNotRegistered:
                        msg = CommonData.ThisMachineIsNotRegistered;
                        break;
                }

                if (!string.IsNullOrEmpty(msg))
                {
                    if (closeSplashFunc != null)
                        closeSplashFunc();
                    MessageBoxEx.Show(parent, msg, CommonData.Information, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Log.Info("Failed to activate subscription features. Code: " + ex.ErrorCode.ToString() + ". " + ex.Message);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled)
                    Log.Error("Subscription check failed: " + ex.ToString());
            }
             * */
        }
    }
}
