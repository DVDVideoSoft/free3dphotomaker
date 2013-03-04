using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;

using DVDVideoSoft.AppFxApi;
using DVDVideoSoft.Utils;

namespace DVDVideoSoft.AppFx
{
    public class ThemeLoader : IThemeLoader
    {
        private static ILogWriter Log;

        private static string schemaFileName = "Theme.xml";
        private static readonly string images_EN= "Images";
        private static readonly string image_EN = "Image";
        private static readonly string colors_EN = "Colors";
        private static readonly string color_EN = "Color";
        private static readonly string properties_EN = "Properties";
        private static readonly string property_EN = "Property";

        protected Dictionary<string, Image> images = new Dictionary<string, Image>();
        protected Dictionary<string, Color> colors = new Dictionary<string, Color>();
        protected Dictionary<string, string> propertyDefs = new Dictionary<string, string>();

        protected string themesPath;
        protected string name;
        protected bool loaded;

        public static readonly string Background_EN = "Background";

        public bool Loaded { get { return this.loaded; } }
        
        public ThemeLoader(string path, ILogWriter log)
        {
            this.themesPath = path;
            if (!string.IsNullOrEmpty(this.themesPath) && this.themesPath[this.themesPath.Length - 1] != '\\' && this.themesPath[this.themesPath.Length - 1] != '/')
                this.themesPath += '\\';
            Log = log;
        }

        protected void Clear()
        {
            this.images.Clear();
            this.colors.Clear();
            this.propertyDefs.Clear();
        }

        public bool IsBorderless
        {
            get
            {
                if (!this.loaded)
                    return false;

                string buf = this.GetProperty("UI.BorderlessControls");
                return !string.IsNullOrEmpty(buf) && FormattingFunctions.IsTrueValue(buf);
            }
        }

        public bool Load(string themeName)
        {
            this.loaded = false;

            string dir = this.themesPath + themeName;
            if (!Directory.Exists(dir))
                return false;

            this.Clear();
            this.name = System.IO.Path.GetFileNameWithoutExtension(dir);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(dir + "\\" + schemaFileName);
                XmlNode main = doc.SelectSingleNode("Theme");
                if (main != null)
                {
                    XmlNode imagesNode = main.SelectSingleNode(images_EN);
                    if (imagesNode != null)
                    {
                        XmlNodeList image = imagesNode.SelectNodes(image_EN);
                        if (image != null)
                        {
                            for (int i = 0; i < image.Count; i++)
                            {
                                string name = image[i].Attributes[0].Value;
                                string val = image[i].Attributes[1].Value;
                                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(val))
                                {
                                    try {
                                        Image bmp = Bitmap.FromFile(dir + "\\" + val);
                                        if (bmp != null)
                                            this.images.Add(name, bmp);
                                    }
                                    catch
                                    {
                                        if (Log.IsErrorEnabled)
                                            Log.Error("Failed to load " + val);
                                    }
                                }
                            }
                        }
                    }

                    XmlNode colorsNode = main.SelectSingleNode(colors_EN);
                    if (colorsNode != null)
                    {
                        XmlNodeList color = colorsNode.SelectNodes(color_EN);
                        if (color != null)
                        {
                            for (int i = 0; i < color.Count; i++)
                            {
                                string name = color[i].Attributes[0].Value;
                                string val = color[i].Attributes[1].Value;
                                if (name != null && val != null)
                                {
                                    Color c = FormattingFunctions.ParseColor(val);
                                    this.colors.Add(name, c);
                                }
                            }
                        }
                    }

                    XmlNode propsNode = main.SelectSingleNode(properties_EN);
                    if (propsNode != null)
                    {
                        XmlNodeList prop = propsNode.SelectNodes(property_EN);
                        if (prop != null)
                        {
                            for (int i = 0; i < prop.Count; i++)
                            {
                                string name = prop[i].Attributes[0].Value;
                                string val = prop[i].Attributes[1].Value;
                                if (name != null && val != null)
                                {
                                    this.propertyDefs.Add(name, val);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                if (Log.IsErrorEnabled)
                    Log.Error("ThemeLoader.Load fails with error: " + e.Message);
                return false;
            }

            this.loaded = true;
            return true;
        }

        public string Path { get { return this.themesPath; } }
        public string Name { get { return this.name; } }

        public bool HasColor(string elementName) { return this.colors.ContainsKey(elementName); }
        public bool HasImage(string elementName) { return this.images.ContainsKey(elementName); }

        public Color GetColor(string elementName, Color defaultValue)
        {
            if (!this.colors.ContainsKey(elementName))
                return defaultValue;

            return this.colors[elementName];
        }

        public Image GetImage(string elementName)
        {
            if (!this.images.ContainsKey(elementName))
                return null;
    
            return this.images[elementName];
        }

        public string GetProperty(string elementName)
        {
            if (!this.propertyDefs.ContainsKey(elementName))
                return null;

            return this.propertyDefs[elementName];
        }

        public static List<string> GetAvailableThemes(string path)
        {
            if (!Directory.Exists(path))
                return new List<string>();

            List<string> availableThemes = new List<string>();
            DirectoryInfo dirInf = new DirectoryInfo(path);
            DirectoryInfo[] themes=dirInf.GetDirectories();
            for (int i = 0; i < themes.Length; i++)
            {
                if (File.Exists(themes[i].FullName + "\\" + schemaFileName))
                    availableThemes.Add(themes[i].Name);
            }

            return availableThemes;
        }
    }
}
