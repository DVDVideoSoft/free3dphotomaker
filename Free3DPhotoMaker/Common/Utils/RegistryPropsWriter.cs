using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public class RegistryPropsWriter : IPropsWriter
    {
        private Regedit storage;
        public RegistryPropsWriter(string regPath)
        {
            try
            {
                storage = new Regedit(regPath);
                storage.Open(true);
            }
            catch { }
        }

        public void Close()
        {
            if (storage != null)
                storage.Close();
        }


        public void Write(string key, object data)
        {
            if (data.GetType() == typeof(string))
            {
                Write(key, (string)data);
                return;
            }

            if (data.GetType() == typeof(int))
            {
                Write(key, (int)data);
                return;
            }

            if (data.GetType() == typeof(long))
            {
                Write(key, (long)data);
                return;
            }

            if (data.GetType() == typeof(bool))
            {
                Write(key, (bool)data);
                return;
            }
        }
        public void Write(string key, string data)
        {
            if (storage != null)
                storage.SetValue(key, data);
        }

        public void Write(string key, int data)
        {
            if (storage != null)
                storage.SetValue(key, data);
        }

        public void Write(string key, long data)
        {
            if (storage != null)
                storage.SetValue(key, data);
        }

        public void Write(string key, bool data)
        {
            if (storage != null)
                storage.SetValue(key, data);
        }
    }
}
