using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DVDVideoSoft.Utils
{
    public class IniFileEditor
    {
        public string FileName { get { return this.FileName; } }

        private string fileName;

        public IniFileEditor(string fileName)
        {
            this.fileName = fileName;
        }

        public IList<string> GetSectionNames()
        {
            IList<string> sections = new List<string>();

            string[] lines = File.ReadAllLines(this.fileName);

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("[") && trimmedLine.EndsWith("]"))
                    sections.Add(trimmedLine.Substring(1, trimmedLine.Length - 2));
            }

            return sections;
        }

        public string Read(string section, string key, string defaultValue)
        {
            int MAXFILENAME = 256;
            StringBuilder sb = new StringBuilder(MAXFILENAME);
            uint retval = NativeMethods.GetPrivateProfileString(section, key, defaultValue, sb, MAXFILENAME, this.fileName);
            return sb.ToString();
        }

        //public string Write()
        //{
        //    //this.FileName
        //}
    }
}
