using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.IO;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.DialogForms
{
    public class PrestartChecker
    {
        public static void CodecPrestartCheck(System.Drawing.Icon icon, string programPageUrlSubstring)
        {
            int codecCheckResult = Functions.LaunchTool(Programs.ToolID.CodecPack);
            if (codecCheckResult != 0)
            {
                ErrorReportForm errorForm = new ErrorReportForm(null, null);
                errorForm.Icon = icon;
                errorForm.ProgramPageUrlSubstring = programPageUrlSubstring;

                errorForm.ShowModal(null);
                throw new CaughtAndHandledException(CaughtAndHandledException.ErrorType.Codec, codecCheckResult);
            }
        }

        public static bool ShowCodecsDownloadQuestionDialog()
        {
            System.Windows.Forms.DialogResult res = MessageBox.Show(Resources.CommonData.SomeCodecsAreMissingTheyWillBeDownloaded,
                                                                    Resources.CommonData.CodecsDownload,
                                                                    MessageBoxButtons.YesNo,
                                                                    MessageBoxIcon.Question);
            return res == DialogResult.Yes;
        }

        /// <summary>
        /// Downloads the DVS CodecPack.
        /// </summary>
        /// <param name="saveToPath">Directory to save the installer executable to.</param>
        public static void DownloadCodecPack(string saveToPath)
        {
            string fileNameOnly = Path.GetFileName(DvsUrls.DownloadUrl + Programs.ToolID.CodecPack + ".exe");
            string saveToFullFileName = (saveToPath.EndsWith("\\") ? saveToPath : saveToPath + "\\") + fileNameOnly;
            ProcessStartInfo info = new ProcessStartInfo(FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonBin, Programs.ToolID.DVSUpdate + ".exe"));
            info.UseShellExecute = false;
            info.Arguments = Programs.ToolID.CodecPack.ToString().ToLower() + " /exec /lang:" + Thread.CurrentThread.CurrentUICulture.Name;
            Process.Start(info);
        }
    }
}
