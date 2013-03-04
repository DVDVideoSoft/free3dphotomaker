using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using System.Windows.Forms;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public class ProcessingString
    {
        public enum StringKind
        {
            Ordinal,
            InProgress,
            Success,
            Error
        }

        public string ProcessString = "";
        public Color StringColor = SystemColors.WindowText;
        public Image StringIcon = null;
        public string Key = null;
        public int Shift = 0;
        public bool WithLoadingIcon = false;
        public StringKind Kind = StringKind.Ordinal;

        public ProcessingString(string processString, Image stringIcon, string key, Color stringColor, int shift)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
            this.StringIcon = stringIcon;
            this.Key = key;
            this.Shift = shift;
        }

        public ProcessingString(string processString, Icon stringIcon, string key,  Color stringColor, int shift)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
            this.StringIcon = stringIcon.ToBitmap();
            this.Key = key;
            this.Shift = shift;
        }

        public ProcessingString(string processString, Image stringIcon, string key, Color stringColor)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
            this.StringIcon = stringIcon;
            this.Key = key;
        }

        public ProcessingString(string processString, Icon stringIcon, string key, Color stringColor)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
            this.StringIcon = stringIcon.ToBitmap();
            this.Key = key;
        }

        public ProcessingString(string processString, Color stringColor)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
        }

        public ProcessingString(string processString)
        {
            ProcessString = processString;
        }

        public ProcessingString(string processString, Color stringColor, bool withLoadingIcon)
        {
            this.ProcessString = processString;
            this.StringColor = stringColor;
            this.WithLoadingIcon = withLoadingIcon;
        }

        public ProcessingString(ProcessingString.StringKind kind)
        {
            this.Kind = kind;
        }
    }
}