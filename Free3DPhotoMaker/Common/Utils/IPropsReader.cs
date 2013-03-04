using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.Utils
{
    public interface IPropsReader
    {
        bool ContainsKey(string key);
        bool ContainsKey(string key, Type typeOf);

        object this[string key]
        {
            get;
        }

        bool Retrieve<T>(string key, ref T value);

        string  GetString(string key);
        uint    GetUint(string key);
        int     GetInt(string key);
        bool    GetBool(string key);

        T       Get<T>(string key, T defaultVal);
        T       Get<T>(string key);
    }

    public interface IPropsCache
    {
        void Set(string key, object value);
        void Set(string key, string value);

        void Remove(string key);
        void Clear();
    }

    public interface IPropsSaveToHelper
    {
        void AddStorableValue(string name, object defaultVal);
        void SaveTo(IPropsWriter storageWriter);
    }

    public interface IPropsWriter
    {
        void Write(string key, object data);
        void Write(string key, string data);
        void Write(string key, int data);
        void Write(string key, bool data);
    }
}
