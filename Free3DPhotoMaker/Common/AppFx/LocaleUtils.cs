using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Threading;

namespace DVDVideoSoft.AppFx
{
    public class LocaleUtils
    {
        public static void ApplyCulture(ContextMenuStrip cm,Form parent,string culture)
        {
            ComponentResourceManager res = new ComponentResourceManager(parent.GetType());
            CultureInfo ci = new CultureInfo(
                        string.IsNullOrEmpty(culture) ? Thread.CurrentThread.CurrentUICulture.Name : culture);
            try
            {
                ApplyCulture(cm, res, ci);
            }
            catch (Exception e)
            {
            }
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
            catch (Exception e)
            {
            }
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

            form.SuspendLayout();
            res.ApplyResources(form, form.Name, ci);
            form.Text = res.GetString("$this.Text");

            try
            {
                foreach (Control c in form.Controls)
                {
                    ApplyCulture(c, ci, res);
                }
                if (form.MainMenuStrip != null)
                    ApplyCulture((ToolStrip)form.MainMenuStrip, res, ci);
            }
            catch (Exception e)
            {
            }
            form.ResumeLayout(false);
        }

        public static void SetThreadCulture(string culture)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);

            switch (culture)
            {
                case "zh-CHS":
                    culture = "zh-CN";
                    break;
            }

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture);
        }
    }
}
