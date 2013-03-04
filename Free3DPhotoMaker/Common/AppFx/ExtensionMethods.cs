using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFx
{
    public static class ExtensionMethods
    {
        public static bool GetBool(Utils.PropsProvider _this, AppFxApi.Defs.PN key)
        {
            return _this.GetBool(key.ToString());
        }

        public static T Get<T>(Utils.PropsProvider _this, AppFxApi.Defs.PN key)
        {
            return _this.Get<T>(key.ToString());
        }

        public static T Get<T>(Utils.PropsProvider _this, AppFxApi.Defs.PN key, T defaultVal)
        {
            return _this.Get<T>(key.ToString(), defaultVal);
        }

        public static string GetString(Utils.PropsProvider _this, AppFxApi.Defs.PN key)
        {
            return _this.GetString(key.ToString());
        }

        public static int GetInt(Utils.PropsProvider _this, AppFxApi.Defs.PN key)
        {
            return _this.GetInt(key.ToString());
        }

        public static void Set(Utils.PropsProvider _this, AppFxApi.Defs.PN key, string value)
        {
            _this.Set(key.ToString(), value);
        }

        public static void Set(Utils.PropsProvider _this, AppFxApi.Defs.PN key, object value)
        {
            _this.Set(key.ToString(), value);
        }

        public static void AddStorableValue(Utils.IPropsSaveToHelper _this, AppFxApi.Defs.PN key, object defaultVal)
        {
            _this.AddStorableValue(key.ToString(), defaultVal);
        }

        public static T Get<T>(PropMan _this, AppFxApi.Defs.PN key) where T: class
        {
            return _this.Get<T>(key.ToString());
        }
    }
}
