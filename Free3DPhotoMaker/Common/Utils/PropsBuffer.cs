using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class PropsBuffer
    {
        private IDictionary<string, string> rememberedDict = new Dictionary<string, string>();

        public PropsBuffer(PropsProvider provider)
        {
            foreach (string key in provider.Keys)
                this.rememberedDict[key] = provider[key].ToString();
            foreach (string key in provider.StorableKeys)
            {
                if (provider.GetTypeOfStorableValue(key) == typeof(string))
                    this.rememberedDict[key] = provider.GetString(key);
                else if (provider.GetTypeOfStorableValue(key) == typeof(int))
                    this.rememberedDict[key] = provider.Get<int>(key).ToString();
            }
        }

        public string this[string key]
        {
            get
            {
                if (this.rememberedDict.ContainsKey(key))
                    return this.rememberedDict[key];
                return null;
            }
        }
    }
}
