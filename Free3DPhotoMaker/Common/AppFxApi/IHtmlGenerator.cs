using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DVDVideoSoft.HtmlGeneratorApi
{
    public class HtmlGenerationSettings
    {
        private IDictionary<string, Color> colors = new Dictionary<string, Color>();
        private IDictionary<string, string> strings = new Dictionary<string, string>();
        public static readonly string Fore_PropID = "Fore";
        public static readonly string Back_PropID = "Back";

        public HtmlGenerationSettings()
        {
        }

        public Color GetColor(string id)
        {
            if (this.colors.ContainsKey(id))
                return this.colors[id];
            return Color.Transparent;
        }

        public void SetColor(string id, Color value)
        {
            this.colors[id] = value;
        }

        public string GetString(string id)
        {
            return this.strings[id];
        }

        public void SetString(string id, string value)
        {
            this.strings[id] = value;
        }
    }

    public interface IHtmlGenerator
    {
        void Init(string outputPath, HtmlGenerationSettings settings);
        void Init(string outputPath);
        void Add(object file);
        void Generate(int playerColor);
    }
}
