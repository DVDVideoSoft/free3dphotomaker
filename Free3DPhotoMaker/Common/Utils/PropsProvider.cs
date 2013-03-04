using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public partial class PropsProvider : IPropsReader, IPropsCache, IPropsSaveToHelper
    {
        private IPropsReader reader;
        private Dictionary<string, object> propsCache;
        private Dictionary<string, object> storableValues; // keep the list of properties that shall be written on Save, and thier default values
        private Dictionary<string, bool> storableValueSetDict;

        public PropsProvider(IPropsReader reader)
        {
            this.reader = reader;
            this.propsCache = new Dictionary<string, object>();
            this.storableValues = new Dictionary<string, object>();
            this.storableValueSetDict = new Dictionary<string, bool>();
        }

        public bool ContainsKey(string key)
        {
            return this.propsCache.ContainsKey(key) ||
                    (this.reader != null && this.reader.ContainsKey(key));
        }

        public Type GetTypeOfStorableValue(string key)
        {
            if (this.storableValues.ContainsKey(key))
                return this.storableValues[key].GetType();

            return null;
        }

        public IList<string> Keys
        {
            get
            {
                string[] keys = new string[this.propsCache.Keys.Count];
                this.propsCache.Keys.CopyTo(keys, 0);
                return keys;
            }
        }

        public IList<string> StorableKeys
        {
            get
            {
                string[] keys = new string[this.storableValues.Keys.Count];
                this.storableValues.Keys.CopyTo(keys, 0);
                return keys;
            }
        }

        public bool ContainsKey(string key, Type typeOf)
        {
            try
            {
                object t = null;
                if (this.propsCache.ContainsKey(key))
                    t = propsCache[key];
                else if (this.reader != null && this.reader.ContainsKey(key))
                    t = reader[key];

                if (t != null)
                    return (t.GetType() == typeOf);
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public object this[string key]
        {
            get
            {
                if (propsCache.ContainsKey(key))
                    return this.propsCache[key];
                else
                {
                    if (this.reader != null && this.reader.ContainsKey(key))
                    {
                        object tmp = this.reader[key];
                        if (tmp != null)
                            Set(key, tmp);
                        return tmp;
                    }
                    else if (this.storableValues.ContainsKey(key))
                        return this.storableValues[key];
                }

                return null;
            }
        }

        public bool Retrieve<T>(string key, ref T value)
        {
            if (this.reader == null)
                return false;
            return this.reader.Retrieve<T>(key, ref value);
        }

        public string GetString(string key)
        {
            if (propsCache.ContainsKey(key))
                return this.propsCache[key] as string;
            else
            {
                if (this.reader != null && this.reader.ContainsKey(key))
                {
                    string tmp = this.reader.GetString(key);
                    if (tmp != null)
                        Set(key, tmp);
                    return tmp;
                }
                else if (this.storableValues.ContainsKey(key) && this.GetTypeOfStorableValue(key) == typeof(string))
                    return (string)this.storableValues[key];
            }
            return null;
        }

        public int GetInt(string key)
        {
            if (propsCache.ContainsKey(key))
                return (int)this[key];
            else
            {
                if (this.reader != null && this.reader.ContainsKey(key, typeof(int)))
                {
                    int tmp = this.reader.GetInt(key);
                    Set(key, tmp);
                    return tmp;
                }
                else if (this.storableValues.ContainsKey(key) && this.GetTypeOfStorableValue(key) == typeof(int))
                    return (int)this.storableValues[key];
            }
            return 0;
        }

        public bool GetBool(string key)
        {
            if (propsCache.ContainsKey(key))
                return (bool)this[key];
            else
            {
                if (this.reader != null && this.reader.ContainsKey(key, typeof(bool)))
                {
                    bool tmp = this.reader.GetBool(key);
                    Set(key, tmp);
                    return tmp;
                }
                else if (this.storableValues.ContainsKey(key) && this.GetTypeOfStorableValue(key) == typeof(bool))
                    return (bool)this.storableValues[key];
            }
            return false;
        }

        public uint GetUint(string key)
        {
            if (propsCache.ContainsKey(key))
                return (uint)this[key];
            else
            {
                if (this.reader != null && this.reader.ContainsKey(key, typeof(uint)))
                {
                    uint tmp = this.reader.GetUint(key);
                    Set(key, tmp);
                    return tmp;
                }
                else if (this.storableValues.ContainsKey(key) && this.GetTypeOfStorableValue(key) == typeof(uint))
                    return (uint)this.storableValues[key];
            }
            return 0;
        }

        public T Get<T>(string key)
        {
            return Get<T>(key, default(T));
        }

        public T Get<T>(string key, T defaultValue)
        {
            if (this.propsCache.ContainsKey(key))
                return (T)this[key];

            T val = defaultValue;
            if (this.reader == null || !this.reader.Retrieve<T>(key, ref val))
            {
                if (typeof(T) == typeof(int))
                {
                    int defaultStorableValue = (int)(object)defaultValue;
                    if (this.storableValues.ContainsKey(key))
                        defaultStorableValue = (int)this.storableValues[key];
                    val = (T)(object)defaultStorableValue;
                }
                else if (typeof(T) == typeof(bool))
                {
                    bool defaultStorableValue = (bool)(object)defaultValue;
                    if (this.storableValues.ContainsKey(key))
                        defaultStorableValue = (bool)this.storableValues[key];
                    return (T)(object)defaultStorableValue;
                }
            }

            this.propsCache[key] = val;
            return val;
        }

        public string Get(string key, string defaultValue)
        {
            return Get<string>(key, defaultValue);
        }

        public int Get(string key, int defaultValue)
        {
            return Get<int>(key, defaultValue);
        }

        public bool Get(string key, bool defaultValue)
        {
            return Get<bool>(key, defaultValue);
        }

        public uint Get(string key, uint defaultValue)
        {
            return Get<uint>(key, defaultValue);
        }

        public void Set(string key, object value)
        {
            propsCache[key] = value;
        }

        public void Set(string key, string value)
        {
            Set(key, (object)value);
        }

        public void Remove(string key)
        {
            propsCache.Remove(key);
        }

        public void Clear()
        {
            propsCache.Clear();
        }

        public PropsProvider Copy()
        {
            PropsProvider result = new PropsProvider(this.reader);

            foreach (string s in propsCache.Keys)
                result.Set(s, propsCache[s]);

            foreach (string s in storableValues.Keys)
                ((IPropsSaveToHelper)result).AddStorableValue(s, storableValues[s]);
            return result;
        }

        void IPropsSaveToHelper.AddStorableValue(string name, object defaultValue)
        {
            //if (!this.storableValues.ContainsKey(name))
            //    this.storableValues.Add(name, defaultValue);
            //else
            this.storableValues[name] = defaultValue;
        }

        void IPropsSaveToHelper.SaveTo(IPropsWriter storageWriter)
        {
            if (storageWriter == null)
                return;

            foreach (string s in this.propsCache.Keys)
            {
                try
                {
                    if (this.storableValues.ContainsKey(s) && this.storableValues[s].GetType() == this.propsCache[s].GetType())
                        storageWriter.Write(s, this.propsCache[s]);
                }
                catch { }
            }

            //foreach ( string s in this.storableValues.Keys ) {
            //    try {
            //        if ( !this.propsCache.ContainsKey( s ) )
            //            storageWriter.Write( s, this.storableValues[s] );
            //    } catch { }
            //}
        }
    }
}
