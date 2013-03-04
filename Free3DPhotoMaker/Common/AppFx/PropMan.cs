using System;
using System.Collections.Generic;
using System.Text;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.AppFx
{
    public class CodeObjectDef
    {
        public CodeObjectDef(string name,
                string assemblyName,
                string className)
        {
            this.name = name;
            this.assemblyName = assemblyName;
            this.className = className;
        }

        public string name;
        public string assemblyName;
        public string className;
        public bool immediateLoad;
    }

    public class PropMan : IPropMan
    {
        private static ILogWriter Log;

        public delegate object CreateCodeObjectDelegate(CodeObjectDef objectDef);

        protected Dictionary<string, object> props = new Dictionary<string, object>();

        protected Dictionary<string, object> codeObjects = new Dictionary<string, object>();

        public Dictionary<string, CodeObjectDef> CodeObjectDefs = new Dictionary<string, CodeObjectDef>();

        protected CreateCodeObjectDelegate codeObjectCreatorMethod;

        public PropMan(ILogWriter log, CreateCodeObjectDelegate codeObjectCreatorMethod)
        {
            Log = log;
            this.codeObjectCreatorMethod = codeObjectCreatorMethod;
        }

        public string this[string name]
        {
            get
            {
                return Get<string>(name);
            }

            set
            {
                this.props[name] = value;
            }
        }

        public T Get<T>(string name) where T : class
        {
            try
                {
                    if (this.props.ContainsKey(name))
                        return props[name] as T;
                }
                catch (Exception)
                {
                    if (Log.IsErrorEnabled)
                        Log.Error("Exception while trying to get the property \"" + name + "\"");
                }

                return null;
        }

        public object GetObject(string name)
        {
            if (this.props.ContainsKey(name))
                return this.props[name];

            return null;
        }

        public void SetObject(string name, object value)
        {
            this.props[name] = value;
        }

        public object GetCodeObject(string name)
        {
            try
            {
                if (!this.codeObjects.ContainsKey(name))
                {
                    if (this.CodeObjectDefs.ContainsKey(name) && this.codeObjectCreatorMethod != null)
                        this.codeObjects[name] = this.codeObjectCreatorMethod(this.CodeObjectDefs[name]);
                }

                return this.codeObjects[name];
            }
            catch (Exception)
            {
            }
            return null;
        }

        public void SetCodeObject(string name, object value)
        {
            this.codeObjects[name] = value;
        }

        public bool ContainsKey(string key)
        {
            return this.props.ContainsKey(key);
        }

        public bool ContainsCodeObject(string name)
        {
            return this.CodeObjectDefs.ContainsKey(name);
        }
    }
}
