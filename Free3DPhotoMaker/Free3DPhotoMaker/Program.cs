using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;

using DVDVideoSoft.Utils;

namespace Free3DPhotoMaker
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

            Main main = null;
            Splash splash = null;

            try {
                splash = Splash.ShowSplash(appName, new System.Drawing.Bitmap(typeof(DVDVideoSoft.Resources.CommonData).Assembly.GetManifestResourceStream("DVDVideoSoft.Resources.Resources.Splash.png")));
            } catch { }

            try
            {
                main = new Main();
                main.SetSplash(splash);
            }
            catch (DVDVideoSoft.AppFxApi.CaughtAndHandledException)
            {
                if (splash != null) splash.Close();
                main = null;
            }
            catch (Exception ex) //InitException, ModuleMissingException
            {
                if (splash != null) splash.Close();
                DVDVideoSoft.DialogForms.ErrorReportForm errorForm = new DVDVideoSoft.DialogForms.ErrorReportForm(Programs.GetHumanName(appName), ex.Message);
                errorForm.ProgramPageUrlSubstring = Programs.GetProgramPageUrlSubstring(appName);
                errorForm.Icon = Properties.Resources.MainIcon;
                errorForm.ShowModal(null);
            }

            if (main != null)
                Application.Run(main);
            else if (splash != null)
                splash.Close();
        }
    }
}
