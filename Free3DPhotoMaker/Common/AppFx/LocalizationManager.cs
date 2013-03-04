using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Threading;
using System.IO;

using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.Utils;

namespace DVDVideoSoft.AppFx
{
    public class LocalizationManager : ILocalizedFormList
    {
        private static ILogWriter Log;
        private List<ILocalizable> forms;
        private string currentCulture = null;
        private string regPath;

        public LocalizationManager(string registryPath, ILogWriter log)
        {
            if (log == null)
                throw new ArgumentNullException(Defs.LoggerCannotBeNull_STR);
            Log = log;
            forms = new List<ILocalizable>();
            regPath = registryPath;
            ReadCurrentCulture();
        }

        #region ILocalizedFormContainer members

        public void Add(ILocalizable form)
        {
            ILocalizable able = form as ILocalizable;
            if (able != null)
            {
                able.SetLocFormList(this);
                forms.Add(form);
            }
        }

        public void Remove(ILocalizable form)
        {
            forms.Remove(form);
        }

        #endregion

        public string RegPath { get { return regPath; } set { regPath = value; } }

        public string CurrentCulture
        {
            get { return currentCulture; }
            set { currentCulture = value; }
        }

        public void WriteCurrentCulture()
        {
            Regedit.WriteValue(false, this.regPath, Defs.PN.CurrentCulture.ToString(), this.CurrentCulture);
        }

        public string ReadCurrentCulture()
        {
            Regedit reg = new Regedit(regPath);
            reg.Open(false);
            this.CurrentCulture = reg.GetValue(Defs.PN.CurrentCulture.ToString(), LocaleUtils.DefaultLocaleName);
            reg.Close();

            return currentCulture;
        }

        public static IList<string> GetAvailableLanguages(System.Reflection.Assembly asm)
        {
            if (asm == null)
                asm = System.Reflection.Assembly.GetEntryAssembly();

            List<string> availableResources = new List<string>();
            DirectoryInfo[] dirs = new DirectoryInfo(Path.GetDirectoryName(asm.Location)).GetDirectories();
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
            
            availableResources.Add(LocaleUtils.DefaultLocaleName);
            foreach (DirectoryInfo dir in dirs)
            {
                string dirName = (string.Compare(dir.Name, "zh-CHT") == 0) ? "zh-CHT" : dir.Name;
                for (int j = 0; j < cultures.Length; j++)
                {
                    if (cultures[j].Name == dirName)
                    {
                        string searchStr = asm.GetName().Name + "*";
                        FileInfo[] found = dir.GetFiles(searchStr);
                        if (found.Length > 0)
                            availableResources.Add(cultures[j].Name);
                    }
                }
            }

            ChangeLanguagesOver("pt-PT", "pt-BR", availableResources);
            ChangeLanguagesOver("hu-HU", "it-IT", availableResources);
            return availableResources;
        }

        private static void ChangeLanguagesOver(string cultName1, string cultName2, IList<string> availableResources)
        {
            int ptPtResourceIndex = availableResources.IndexOf(cultName1);
            int ptBrResourceIndex = availableResources.IndexOf(cultName2);
            if (ptPtResourceIndex >= 0 && ptBrResourceIndex >= 0)
            {
                availableResources[ptPtResourceIndex] = cultName2;
                availableResources[ptBrResourceIndex] = cultName1;
            }
        }


        public void ApplyCultureForAll(string culture)
        {
            if (!string.IsNullOrEmpty(culture))
                this.currentCulture = culture;

            LocaleUtils.SetThreadCulture(this.currentCulture);

            StringBuilder sb;
            for (int i = 0; i < forms.Count; i++)
            {
                ILocalizable able = forms[i] as ILocalizable;
                if (able != null)
                {
                    able.BeforeSetCulture();
                    try
                    {
                        LocaleUtils.ApplyCulture(able as LocalizableForm, this.currentCulture);
                    }
                    catch (Exception ex) {
                        sb = new StringBuilder();
                        sb.AppendFormat("Failed to LocaleUtils.ApplyCulture({0}) : {1}", (able as LocalizableForm).Name, ex.Message);
                        if (Log.IsErrorEnabled)
                            Log.Error(sb.ToString());
                    }
                    try {
                        able.SetCulture(this.currentCulture);
                    }
                    catch (Exception ex) {
                        sb = new StringBuilder();
                        sb.AppendFormat("Failed to {0}.SetCulture() : {1}", (able as LocalizableForm).Name, ex.Message);
                        if (Log.IsErrorEnabled)
                            Log.Error(sb.ToString());
                    }
                    able.AfterSetCulture();
                }
            }
        }
    }
}
