using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DVDVideoSoft.Utils
{
    public class PresetCollection
    {
        static readonly string presetCollection_EN = "PresetCollection";
        static readonly string preset_EN = "Preset";
        static readonly string categories_EN = "categories";
        static readonly string category_EN = "category";
        static readonly string defaultCategory_EN = "defaultCategory";
        static readonly string defaultPreset_EN   = "defaultPreset";
        static readonly string useDescription_EN = "useDescription";
        static readonly string version_EN = "version";
        public static readonly string DefaultCategoriesString = "High|Standard|Economy";
        public static readonly string CurrentVersionString = "1.1";

        protected string fileName;
        public string FileName { get { return this.fileName; } }

        public IList<Preset> Presets { get { return this.presets; } }
        public IList<IList<string>> PresetsAsIListObjects
        {
            get
            {
                List<IList<string>> objects = new List<IList<string>>();
                foreach (Preset preset in this.presets)
                    objects.Add(preset as IList<string>);
                return objects;
            }
        }
        public IList<string> Categories {
            get { return this.categories; }
            set
            {
                this.categories.Clear();
                foreach (string s in value)
                    this.categories.Add(s);
            }
        }
        public string DefaultCategory;
        public int DefaultPreset = 0;
        public bool UseDescription = true;
        private Version version;

        List<Preset> presets = new List<Preset>();
        List<string> categories = new List<string>();

        public PresetCollection()
        {
            this.version = new Version("0.0");
        }

        public object Clone()
        {
            PresetCollection newObj = new PresetCollection();

            newObj.fileName = this.fileName;

            foreach (Preset item in this.presets)
            {
                newObj.Presets.Add(item.Clone() as Preset);
            }

            foreach(string grade in this.Categories)
            {
                newObj.Categories.Add(grade);
            }
            newObj.DefaultCategory = this.DefaultCategory;
            newObj.DefaultPreset = this.DefaultPreset;
            newObj.UseDescription = this.UseDescription;

            newObj.version = new Version(this.version.ToString());

            return newObj;
        }

        public void Load(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            this.fileName = fileName;

            XmlNode node = null;
            //Load categories list
            string categoriesString = null;
            try
            {
                node = doc.SelectSingleNode(PresetCollection.presetCollection_EN);
                categoriesString = node.Attributes.GetNamedItem(PresetCollection.categories_EN).Value;
            }
            catch (Exception) { }

            if (string.IsNullOrEmpty(categoriesString))
                categoriesString = DefaultCategoriesString;
            string[] grades = categoriesString.Split('|');
            foreach(string s in grades)
                this.categories.Add(s);

            // Default category
            try {
                node = doc.SelectSingleNode(PresetCollection.presetCollection_EN);
                this.DefaultCategory = node.Attributes.GetNamedItem(PresetCollection.defaultCategory_EN).Value;
            } catch { }

            // Default preset
            try {
                node = doc.SelectSingleNode(PresetCollection.presetCollection_EN);
                this.DefaultPreset = int.Parse(node.Attributes.GetNamedItem(PresetCollection.defaultPreset_EN).Value);
            } catch { }

            // UseDescription attribute
            try {
                if (node != null)
                    this.UseDescription = bool.Parse(node.Attributes.GetNamedItem(PresetCollection.useDescription_EN).Value.ToLower());
            } catch { }

            if (this.categories.Count > 0)
            {
                if (string.IsNullOrEmpty(this.DefaultCategory))
                {
                    if (this.categories.Contains(Preset.DefaultCategoryString))
                        this.DefaultCategory = Preset.DefaultCategoryString;
                    else
                        this.DefaultCategory = this.categories[0];
                }
            }

            // Version attribute
            //try {
                if (node != null)
                    this.version = new Version(node.Attributes.GetNamedItem(PresetCollection.version_EN).Value.ToLower());
            //} catch { }

            XmlNodeList profiles = doc.SelectNodes(PresetCollection.presetCollection_EN + "/" + PresetCollection.preset_EN);

            this.presets.Clear();

            for (int i = 0; i < profiles.Count; i++)
            {
                XmlNode profile = profiles[i];

                Preset preset = new Preset();

                try
                {
                    preset.Category = profile.Attributes.GetNamedItem(PresetCollection.category_EN).Value;
                }
                catch
                {
                    if (this.UseDescription)
                        throw new Exception("Preset category cannot be detected");
                }

                XmlNode presetProperty = profile.SelectSingleNode("Name");

                if (presetProperty != null)
                {
                    preset.Name = presetProperty.InnerText;
                }

                presetProperty = profile.SelectSingleNode("Description");
                if (presetProperty != null)
                {
                    preset.Description = presetProperty.InnerText;
                }

                presetProperty = profile.SelectSingleNode("VideoBitrate");
                if (presetProperty != null)
                {
                    preset.VideoBitrate = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("VideoFrameRate");
                if (presetProperty != null)
                {
                    System.Globalization.NumberFormatInfo ni = (System.Globalization.NumberFormatInfo)System.Threading.Thread.CurrentThread.CurrentCulture.NumberFormat.Clone();
                    char decimalSeparator = (ni.NumberDecimalSeparator.ToCharArray())[0];
                    preset.VideoFrameRate = double.Parse(presetProperty.InnerText.Replace('.', decimalSeparator));
                }

                presetProperty = profile.SelectSingleNode("VideoWidth");
                if (presetProperty != null)
                {
                    preset.VideoWidth = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("VideoHeight");
                if (presetProperty != null)
                {
                    preset.VideoHeight = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("VideoFormat");
                if (presetProperty != null)
                {
                    preset.VideoFormat = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("AudioFormat");
                if (presetProperty != null)
                {
                    preset.AudioFormat = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("AudioSampleRate");
                if (presetProperty != null)
                {
                    preset.AudioSampleRate = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("AudioChannels");
                if (presetProperty != null)
                {
                    preset.AudioChannels = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("AudioBitrate");
                if (presetProperty != null)
                {
                    preset.AudioBitrate = Int32.Parse(presetProperty.InnerText);
                }

                presetProperty = profile.SelectSingleNode("Extension");
                if (presetProperty != null)
                    preset.Extension = presetProperty.InnerText;

                if (!string.IsNullOrEmpty(preset.Extension))
                    preset.Extension = preset.Extension.Insert(0, ".");

                presetProperty = profile.SelectSingleNode("AdditionalParams");
                if (presetProperty != null)
                    preset.AdditionalParams = presetProperty.InnerText;

                this.presets.Add(preset);
            }
        }

        public bool IsAudioCollection()
        {
            foreach(Preset preset in this.presets)
            {
                char[] sep = {'.', ','};

                foreach (string format in AudioFormat.formatNames.Values)
                {
                    if (preset.Extension.Split(sep)[0] == format.ToLower())
                        return true;
                }
            }
            return false;
        }

        public void Save()
        {
            XmlTextWriter textWriter = new XmlTextWriter(this.fileName, null);
            textWriter.Formatting = Formatting.Indented;
            textWriter.Indentation = 4;
            textWriter.WriteStartDocument();

            textWriter.WriteStartElement(PresetCollection.presetCollection_EN);

            if (FormattingFunctions.ListToString(this.categories, "|") == PresetCollection.DefaultCategoriesString)
            {
                textWriter.WriteAttributeString(PresetCollection.categories_EN, "");
            }
            else
            {
                textWriter.WriteAttributeString(PresetCollection.categories_EN, FormattingFunctions.ListToString(this.categories, "|"));
            }

            textWriter.WriteAttributeString(PresetCollection.version_EN, PresetCollection.CurrentVersionString);
            textWriter.WriteAttributeString(PresetCollection.useDescription_EN, this.UseDescription ? "true" : "");
 
            foreach (Preset preset in this.presets)
            {
                textWriter.WriteStartElement(PresetCollection.preset_EN);
                textWriter.WriteAttributeString(PresetCollection.category_EN, preset.Category == null ? Preset.DefaultCategoryString : preset.Category);

                // Write next element
                textWriter.WriteStartElement("Name");
                textWriter.WriteString(preset.Name);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Description");
                textWriter.WriteRaw(preset.Description);
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("Extension");
                if (string.IsNullOrEmpty(preset.Extension))
                    textWriter.WriteRaw("");
                else
                    textWriter.WriteString(preset.Extension.Remove(0, 1));

                textWriter.WriteEndElement();

                textWriter.WriteStartElement("AudioFormat");
                textWriter.WriteRaw(preset.AudioFormat.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("AudioBitrate");
                textWriter.WriteRaw(preset.AudioBitrate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("AudioChannels");
                textWriter.WriteRaw(preset.AudioChannels.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("AudioSampleRate");
                textWriter.WriteRaw(preset.AudioSampleRate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VideoFormat");
                textWriter.WriteRaw(preset.VideoFormat.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VideoWidth");
                textWriter.WriteRaw(preset.VideoWidth.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VideoHeight");
                textWriter.WriteRaw(preset.VideoHeight.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VideoFrameRate");
                textWriter.WriteRaw(preset.VideoFrameRate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("VideoBitrate");
                textWriter.WriteRaw(preset.VideoBitrate.ToString());
                textWriter.WriteEndElement();

                textWriter.WriteStartElement("AdditionalParams");
                textWriter.WriteRaw(preset.AdditionalParams != null ? preset.AdditionalParams : "");
                textWriter.WriteEndElement();

                textWriter.WriteEndElement(); //Preset
            }

            textWriter.WriteEndElement();

            textWriter.WriteEndDocument();
            textWriter.Close();
        }

        public bool IsAudioOnly()
        {
            foreach (Preset preset in this.Presets)
            {
                if (preset.VideoFormat != -1)
                    return false;
            }
            return true;
        }
    }
}
