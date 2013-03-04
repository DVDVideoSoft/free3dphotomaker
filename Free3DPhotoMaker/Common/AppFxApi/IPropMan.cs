using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public interface IPropMan
    {
        string this[string name]
        {
            get;
            set;
        }

        bool ContainsKey(string key);
        object GetObject(string name);
        void SetObject(string name, object value);

        bool ContainsCodeObject(string name);
        object GetCodeObject(string name);
    }
}
