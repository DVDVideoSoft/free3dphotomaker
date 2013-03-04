using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Threading;

namespace DVDVideoSoft.Utils
{
    public class LocaleUtils
    {
        public static IList<string> CulturesSupported = new List<string>() { "en-US", "en-GB", "de-DE", "el-GR", "es-ES", "fr-FR", "it-IT", "hu-HU", "ja-JP", "nl-NL", "pl-PL", "pt-BR", "pt-PT", "ru-RU", "zh-CHS", "zh-CHT", "tr-TR" };
        public static string[] LongLangNames = new string[] { "Deutsch", "English", "Español", "Français", "Italiano", "Magyar", "日本語", "Dutch", "Polski", "Português", "Русский", "中文（简体）", "Chinese Traditional", "Greek", "Turkish", "Portuguese Brazil" };
        public static IList<string> CulturesSupportedOnSite = new List<string>() { "de-DE", "es-ES", "ja-JP", "fr-FR", "it-IT", "pt-PT", "pl-PL", "ru-RU", "zh-CHS", "zh-CHT" };
        public static readonly string DefaultLocaleName = "en-US";
        public const string ChineseSimplifiedLocaleName = "zh-CHS";
        public const string ChineseNeutralLocaleName    = "zh-CN";
        public const string ChineseTraditionalLocaleName = "zh-CHT";

        public static string GetDvsLocaleId(string culture)
        {
            return GetDvsLocaleId(culture, false);
        }

        public static string GetDvsLocaleId(string culture, bool forceEvenForNonsupported = false)
        {
            string ret = "";

            if (string.IsNullOrEmpty(culture))
                return ret;

            else if (culture.StartsWith("en-") || !CulturesSupported.Contains(culture)) //forceEvenForNonsupported
                return "";
            else if (culture == "zh-CHS")
                return "cn";

            int nPos = culture.IndexOf('-') + 1;
            ret =
                culture.Substring(nPos, culture.Length - nPos)
                .ToLower();

            return ret;
        }

        public static bool IsSupportedCulture(string culture)
        {
            bool ret = false;

            foreach (string str in CulturesSupported)
            {
                if (string.Compare(str, culture, true) == 0)
                    ret = true;
            }

            return ret;
        }

        /// <summary>
        /// Creates culture-dependent DVS url like dvdvideosoft/de/guides/ taking a template string as an argument.
        /// </summary>
        /// <param name="fmtString"></param>
        /// <param name="forceEvenForNonsupported"></param>
        /// <param name="addUnderscore">0 - don't add, '-1' - prepend underscore, 1 - append</param>
        /// <returns></returns>
        public static string FormatDvsStringWithCultureName(string fmtString, bool forceEvenForNonsupported, int addUnderscore)
        {
            return FormatDvsStringWithCultureName(fmtString, Thread.CurrentThread.CurrentUICulture.Name, forceEvenForNonsupported, addUnderscore);
        }

        public static string FormatDvsStringWithCultureName(string fmtString, string culture, bool forceEvenForNonsupported, int addUnderscore)
        {
            string strToAdd;

            if (addUnderscore < 0)
            {
                strToAdd = "_" + GetDvsLocaleId(culture, forceEvenForNonsupported);
            }
            else if (addUnderscore > 0)
            {
                string sCultureID = GetDvsLocaleId(culture, forceEvenForNonsupported);
                strToAdd = (string.IsNullOrEmpty(sCultureID) ? "" : sCultureID + "_");
            }
            else
            {
                strToAdd = GetDvsLocaleId(culture, forceEvenForNonsupported);
            }

            return string.Format(fmtString, strToAdd);
        }

        public static string FormatCultureDependentUrl(string fmtString, int addUnderscore)
        {
            string strToAdd;
            string dvsLocale = GetDvsLocaleId(Thread.CurrentThread.CurrentUICulture.Name);

            if (addUnderscore < 0)
                strToAdd = "_" + dvsLocale;
            else if (addUnderscore > 0)
                strToAdd = dvsLocale + "_";
            else
                strToAdd = dvsLocale;

            return string.Format(fmtString, ((dvsLocale == "") ? "" : "/") + strToAdd);
        }

        public static void ApplyCulture(ContextMenuStrip cm, Form parent, string culture)
        {
            ComponentResourceManager res = new ComponentResourceManager(parent.GetType());
            CultureInfo ci = new CultureInfo(
                        string.IsNullOrEmpty(culture) ? Thread.CurrentThread.CurrentUICulture.Name : culture);
            try
            {
                ApplyCulture(cm, res, ci);
            }
            catch { }
        }

        public static void ApplyCulture(DataGridView cm, Form parent, string culture)
        {
            ComponentResourceManager res = new ComponentResourceManager(parent.GetType());
            CultureInfo ci = new CultureInfo(
                        string.IsNullOrEmpty(culture) ? Thread.CurrentThread.CurrentUICulture.Name : culture);
            try
            {

                for (int i = 0; i < cm.Columns.Count; i++)
                {
                    res.ApplyResources(cm.Columns[i], cm.Columns[i].Name, ci);
                }
            }
            catch { }
        }

        public static void ApplyCulture(ToolStrip cm, Form parent, string culture)
        {
            ComponentResourceManager res = new ComponentResourceManager(parent.GetType());
            CultureInfo ci = new CultureInfo(
                        string.IsNullOrEmpty(culture) ? Thread.CurrentThread.CurrentUICulture.Name : culture);
            try
            {
                ApplyCulture(cm, res, ci);
            }
            catch { }
        }

        static void ApplyCulture(ContextMenu cm, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            foreach (MenuItem mi in cm.MenuItems)
            {
                ApplyCulture(mi, resManager, cInfo);                
            }
        }

        static void ApplyCulture(MenuItem mi, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            foreach (MenuItem item in mi.MenuItems)
            {
                resManager.ApplyResources(item, item.Name, cInfo);
                ApplyCulture(item, resManager, cInfo);
            }
        }

        static void ApplyCulture(ToolStrip ts, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            foreach (ToolStripItem tsi in ts.Items)
            {
                ToolStripDropDownItem tdi = tsi as ToolStripDropDownItem;
                if (tdi != null)
                    ApplyAllToolStripItems(tdi, resManager, cInfo);

                ToolStripComboBox tdc = tsi as ToolStripComboBox;
                if (tdc != null)
                    ApplyAllToolStripItems(tdc, resManager, cInfo);

                resManager.ApplyResources(tsi, tsi.Name, cInfo);
            }
            resManager.ApplyResources(ts, ts.Name, cInfo);
        }

        static void ApplyAllToolStripItems(ToolStripItem tsi, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            ToolStripDropDownItem tdi = tsi as ToolStripDropDownItem;
            if (tdi != null)
            {
                foreach (ToolStripItem tsi2 in tdi.DropDownItems)
                {
                    ToolStripDropDownItem tdi2 = tsi2 as ToolStripDropDownItem;
                    if (tdi2 != null && tdi2.DropDownItems.Count > 0)
                        ApplyAllToolStripItems(tdi2, resManager, cInfo);
                    resManager.ApplyResources(tsi2, tsi2.Name, cInfo);
                }
            }
            ToolStripComboBox tdc = tsi as ToolStripComboBox;
            if (tdc != null)
                for (int i = 0; i < tdc.Items.Count; i++)
                {
                    string itemStr = resManager.GetString(tdc.Name + ".Items" +
                      ((i == 0) ? "" : i.ToString()), cInfo);
                    if (itemStr != null)
                        tdc.Items[i] = itemStr;
                }
            resManager.ApplyResources(tsi, tsi.Name, cInfo);
        }

        public static void ApplyCulture(ContextMenuStrip ctx, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            foreach (ToolStripItem tsi in ctx.Items)
            {
                ApplyAllToolStripItems(tsi, resManager, cInfo);
            }
            resManager.ApplyResources(ctx, ctx.Name, cInfo);
        }

        public static void ApplyCulture(ComboBox combo, ComponentResourceManager resManager, CultureInfo cInfo)
        {
            if (string.IsNullOrEmpty(resManager.GetString(combo.Name + ".Items", cInfo)))
                return;

            int index = combo.SelectedIndex;
            string[] oldItems = new string[combo.Items.Count];
            combo.Items.CopyTo(oldItems, 0);
            combo.Items.Clear();
            for (int i = 0; i < oldItems.Length; i++)
            {
                combo.Items.Add(resManager.GetString(combo.Name + ".Items" + (i == 0 ? "" : i.ToString()), cInfo));
            }
            resManager.ApplyResources(combo, combo.Name, cInfo);
            combo.SelectedIndex = index;
        }

        public static void ApplyCulture(Control ctrl, CultureInfo ci, ComponentResourceManager res)
        {
            res.ApplyResources(ctrl, ctrl.Name, ci);

            foreach (Control c in ctrl.Controls)
            {
                if (c is ToolStrip)
                    ApplyCulture((ToolStrip)c, res, ci);
                else
                    ApplyCulture(c, ci, res);
            }

            if (ctrl.ContextMenuStrip != null)
                ApplyCulture(ctrl.ContextMenuStrip, res, ci);
        }

        public static void ApplyCulture(Form form, string culture)
        {
            ComponentResourceManager res = new ComponentResourceManager(form.GetType());
            CultureInfo ci = new CultureInfo(
                        string.IsNullOrEmpty(culture) ? Thread.CurrentThread.CurrentUICulture.Name : culture);

            res.ApplyResources(form, form.Name, ci);
            string locTitle = res.GetString("$this.Text");
            if (!string.IsNullOrEmpty(locTitle))
                form.Text = locTitle;

            foreach (Control c in form.Controls)
            {
                if (c is ComboBox)
                    ApplyCulture((ComboBox)c, res, ci);
                else if (c is DataGridView)
                    ApplyCulture(c as DataGridView, form, culture);
                else if (c is ToolStrip)
                    ApplyCulture(c as ToolStrip, form, culture);
                else
                    ApplyCulture(c, ci, res);
            }
            if (form.MainMenuStrip != null)
                ApplyCulture((ToolStrip)form.MainMenuStrip, res, ci);
        }

        public static void SetThreadCulture(string culture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            if (culture == ChineseSimplifiedLocaleName || culture == ChineseTraditionalLocaleName)
                    culture = ChineseNeutralLocaleName;

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
        }

        public static string GetLanguageNameNoCountry(string culture)
        {
            string langName = System.Globalization.CultureInfo.GetCultureInfo(culture).NativeName;
            
            if (culture != ChineseSimplifiedLocaleName && culture != ChineseTraditionalLocaleName)
            {
                int bracketPos = langName.IndexOf(" (");
                if (bracketPos != -1)
                    langName = langName.Substring(0, bracketPos);
            }
            //else
            //{
            //    langName = langName.Replace("(", "").Replace(")", "");
            //}

            langName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(langName);

            if (culture == "pt-BR")
            {
                langName = langName + " (Brasil)";
            }
            return langName;
        }
    }
}
