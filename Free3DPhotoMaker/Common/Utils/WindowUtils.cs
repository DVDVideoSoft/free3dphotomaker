using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace DVDVideoSoft.Utils
{
    public class WindowUtils
    {
        public static void CenterToParent(Form thisForm, Form parent)
        {
            thisForm.StartPosition = FormStartPosition.Manual;

            Point pt = parent.Location;
            pt.X = pt.X + (parent.Width - thisForm.Width) / 2;
            pt.Y = pt.Y + (parent.Height - thisForm.Height) / 2;
            thisForm.Location = pt;
        }

        public static void FillComboWithGenres(ComboBox combobox, IList<string> genres)
        {
            combobox.Items.Clear();

            foreach (string genre in genres)
                combobox.Items.Add(genre);
        }

        /// <summary>
        /// Creates a new font based on the given font des and prototype
        /// </summary>
        /// <param name="prototype"></param>
        /// <param name="fontDef">Font def in the "Family name|size_in_pt|styles" syntax</param>
        /// <remarks>sample def string: Times New Roman|19|BoldItalicUnderline</remarks>
        /// <returns></returns>
        public static Font CreateFont(Font prototype, string fontDef)
        {
            if (string.IsNullOrEmpty(fontDef))
                return prototype;

            FontStyle fs = prototype.Style;
            string family = null;
            float size = -1;

            string[] fontDefStrings = fontDef.Split(new char[] { '|' });
            if (fontDefStrings.Length >= 3)
            {
                if (fontDefStrings[2].Contains("Bold"))
                    fs |= FontStyle.Bold;
                if (fontDefStrings[2].Contains("Italic"))
                    fs |= FontStyle.Italic;
                if (fontDefStrings[2].Contains("Underline"))
                    fs |= FontStyle.Underline;
                if (fontDefStrings[2].Contains("Strikeout"))
                    fs |= FontStyle.Strikeout;
            }
            if (fontDefStrings.Length >= 1)
            {
                family = fontDefStrings[0];
            }
            if (fontDefStrings.Length >= 2)
            {
                if (!float.TryParse(fontDefStrings[1], out size))
                    size = -1;
            }

            if (!string.IsNullOrEmpty(family))
            {
                if (size > -1)
                    return new Font(family, size, fs);
                else
                    return new Font(family, prototype.Size, fs);
            }
            else
            {
                return new Font(prototype, fs);
            }
        }

        public static bool CompareFontsAreEqual(Font font1, Font font2)
        {
            return font1.Size == font2.Size &&
                    font1.FontFamily.ToString() == font2.FontFamily.ToString() &&
                    font1.Style == font2.Style;
        }

        public static void ResetFontToDefault(Control control, ComponentResourceManager componentResMgr)
        {
            Font oldFont = control.Font;
            componentResMgr.ApplyResources(control, control.Name);
            if (CompareFontsAreEqual(oldFont, control.Font) && !WindowUtils.CompareFontsAreEqual(control.Parent.Font, control.Font))
                control.Font = control.Parent.Font;
        }
    }
}
