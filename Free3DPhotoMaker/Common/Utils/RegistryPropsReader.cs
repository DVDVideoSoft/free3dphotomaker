using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class RegistryPropsReader : IPropsReader
    {
        private Regedit storage;

        public RegistryPropsReader(string regPath)
        {
            try
            {
                storage = new Regedit(regPath);
                storage.Open(false);
            }
            catch (Exception)
            {
            }
        }

        public void Close()
        {
            if (storage != null)
                storage.Close();
        }

        public bool ContainsKey(string key)
        {
            if (storage != null)
                return storage.IsValueExists(key);
            return false;
        }

        public bool ContainsKey(string key, Type typeOf)
        {
            return ContainsKey(key);
        }

        public object this[string key]
        {
            get { return null; }
        }

        public string GetString(string key)
        {
            if (storage != null && storage.IsValueExists(key))
                return storage.GetValue(key, "");

            return null;
        }

        public uint GetUint(string key)
        {
            if (storage != null)
                return storage.GetValue(key, (uint)0);
            return 0;
        }

        public int GetInt(string key)
        {
            if (storage != null)
                return storage.GetValue(key, 0);
            return 0;
        }

        public bool GetBool(string key)
        {
            if (storage != null)
                return storage.GetValue(key, false);
            return false;
        }

        public bool Retrieve<T>(string key, ref T value)
        {
            if (this.ContainsKey(key))
            {
                value = Get<T>(key);
                return true;
            }
            return false;
        }

        public T Get<T>(string key)
        {
            return Get<T>(key, default(T));
        }

        public T Get<T>(string key, T defaultVal = default(T))
        {
            if (storage == null)
                return defaultVal;

            if (typeof(T) == typeof(int))
                return (T)(object)storage.GetValue(key, (int)(object)defaultVal);
            else if (typeof(T) == typeof(long))
                return (T)(object)storage.GetValue(key, (long)(object)defaultVal);
            else if (typeof(T) == typeof(uint))
                return (T)(object)storage.GetValue(key, (uint)(object)defaultVal);
            else if (typeof(T) == typeof(string))
                return (T)(object)storage.GetValue(key, (string)(object)defaultVal);
            else if (typeof(T) == typeof(bool))
                return (T)(object)storage.GetValue(key, (bool)(object)defaultVal);

            return defaultVal;
        }
    }
}
