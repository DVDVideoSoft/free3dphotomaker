using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;

namespace DVDVideoSoft.Utils
{
    public class Regedit : IDisposable
    {
        enum State
        {
            Null = 0,
            Editing = 1,
            Reading = 2,
            Failed = 0xFF,
        };

        public enum HKEY
        {
            HKEY_CLASSES_ROOT,
            HKEY_LOCAL_MACHINE,
            HKEY_CURRENT_USER,
            HKEY_USERS,
            HKEY_CURRENT_CONFIG
        };

        #region Private members

        string regPath;
        RegistryKey regKey;
        RegistryKey hKey;
        State state;

        private bool disposed = false;

        #endregion

        #region Initialization

        public Regedit(string regPath)//TODO: exception handling
        {
            try
            {
                this.regPath = regPath;
                this.regKey = Registry.CurrentUser.CreateSubKey(this.regPath);
                this.hKey = Registry.CurrentUser;
                if (this.regKey != null)
                    this.regKey.Close();
                this.state = State.Null;
            }
            catch
            {
                this.state = State.Failed;
            }
        }

        public Regedit(string regPath, HKEY hKey, bool create)
        {
            this.regPath = regPath;

            switch (hKey)
            {
                case HKEY.HKEY_CLASSES_ROOT:
                    this.regKey = Registry.ClassesRoot.CreateSubKey(this.regPath); this.hKey = Registry.ClassesRoot; break;
                case HKEY.HKEY_LOCAL_MACHINE:
                    if (create)
                        this.regKey = Registry.LocalMachine.CreateSubKey(this.regPath);
                    else
                        this.regKey = Registry.LocalMachine.OpenSubKey(this.regPath, false);
                    this.hKey = Registry.LocalMachine;
                    break;
                case HKEY.HKEY_CURRENT_USER:
                    this.regKey = Registry.CurrentUser.CreateSubKey(this.regPath); this.hKey = Registry.CurrentUser; break;
                case HKEY.HKEY_USERS:
                    this.regKey = Registry.Users.CreateSubKey(this.regPath); this.hKey = Registry.Users; break;
                case HKEY.HKEY_CURRENT_CONFIG:
                    this.regKey = Registry.CurrentConfig.CreateSubKey(this.regPath); this.hKey = Registry.CurrentConfig; break;
                default:
                    this.regKey = Registry.LocalMachine.CreateSubKey(this.regPath); this.hKey = Registry.LocalMachine; break;
            }

            if (this.regKey != null)
                this.regKey.Close();
            state = State.Null;
        }

        public Regedit(string regPath, HKEY hKey)
            : this(regPath, hKey, true)
        {
        }

        public Regedit(HKEY hKey, string regPath, bool create)
            : this(hKey)
        {
            this.regPath = regPath;

            if (create)
            {
                this.regKey = Registry.CurrentUser.CreateSubKey(this.regPath);

                if (this.regKey != null)
                    this.regKey.Close();
            }
        }

        public Regedit(HKEY hKey)
        {
            switch (hKey)
            {
                case HKEY.HKEY_CLASSES_ROOT:
                    this.hKey = Registry.ClassesRoot;
                    break;
                case HKEY.HKEY_LOCAL_MACHINE:
                    this.hKey = Registry.LocalMachine;
                    break;
                case HKEY.HKEY_CURRENT_USER:
                    this.hKey = Registry.CurrentUser;
                    break;
                case HKEY.HKEY_USERS:
                    this.hKey = Registry.Users;
                    break;
                case HKEY.HKEY_CURRENT_CONFIG:
                    this.hKey = Registry.CurrentConfig;
                    break;
                default:
                    this.hKey = Registry.LocalMachine;
                    break;
            }

            this.state = State.Null;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Close();
                }

                this.state = State.Null;
            }
            disposed = true;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Opens registry for reading/writing depending of @writable parameter.
        /// Call this function before reading from and writing to registry, if you
        /// want to read/write many variables
        /// </summary>
        /// <returns></returns>
        public bool Open(bool writable)
        {
            this.regKey = this.hKey.OpenSubKey(this.regPath, writable);
            state = writable ? State.Editing : State.Reading;
            if (this.regKey == null)
                state = State.Null;
            return this.regKey != null;
        }

        /// <summary>
        /// Closes registry after reading/writing depending of @writable parameter.
        /// Call this function if you called Open() and finished work with registry.
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            if (this.regKey != null)
                this.regKey.Close();
            state = State.Null;
        }

        public IList<string> GetSubkeyNames()
        {
            return (this.regKey != null) ? this.regKey.GetSubKeyNames() : null;
        }

        public IList<string> GetValueNames()
        {
            return (this.regKey != null) ? this.regKey.GetValueNames() : null;
        }

        public bool IsValueExists(string name)
        {
            try
            {
                if (state != State.Null)
                {
                    object t = this.regKey.GetValue(name);
                    if (t != null)
                        return true;
                }
                else
                {
                    this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                    if (this.regKey != null)
                    {
                        bool result = this.regKey.GetValue(name) != null;
                        this.regKey.Close();
                        return result;
                    }
                }
            }
            catch (Exception)
            {
                return false;
            }

            return false;
        }

        public void DeleteValue(string name, bool throwOnMissingValue)
        {
            try
            {
                this.regKey.DeleteValue(name, throwOnMissingValue);
            }
            catch (Exception)
            {
                return;
            }
        }

        #endregion

        #region Setters

        public void SetValue(string name, int value)
        {
            if (state == State.Editing)
            {
                this.regKey.SetValue(name, value, RegistryValueKind.DWord);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, true);
                if (this.regKey != null)
                {
                    this.regKey.SetValue(name, value, RegistryValueKind.DWord);
                    this.regKey.Close();
                }

                if (state == State.Reading)
                {
                    this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                }
            }
        }

        public void SetValue(string name, long value)
        {
            if (state == State.Editing)
            {
                this.regKey.SetValue(name, value, RegistryValueKind.QWord);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, true);
                if (this.regKey != null)
                {
                    this.regKey.SetValue(name, value, RegistryValueKind.QWord);
                    this.regKey.Close();
                }

                if (state == State.Reading)
                {
                    this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                }
            }
        }

        public bool SetValueSafe(string name, string value)
        {
            try
            {
                SetValue(name, value);
            }
            catch { return false; }
            return true;
        }

        public void SetValue(string name, string value)
        {
            if (state == State.Editing)
            {
                this.regKey.SetValue(name, value, RegistryValueKind.String);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, true);
                if (this.regKey != null)
                {
                    this.regKey.SetValue(name, value, RegistryValueKind.String);
                    this.regKey.Close();
                }

                if (state == State.Reading)
                {
                    this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                }
            }
        }

        public void SetValue(string name, bool value)
        {
            if (state == State.Editing)
            {
                this.regKey.SetValue(name, (value == true) ? 1 : 0, RegistryValueKind.DWord);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, true);

                if (this.regKey != null)
                {
                    this.regKey.SetValue(name, (value == true) ? 1 : 0, RegistryValueKind.DWord);
                    this.regKey.Close();
                }

                if (state == State.Reading)
                {
                    this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                }
            }
        }

        #endregion

        #region Getters

        public int GetValue(string name, int defaultValue)
        {
            if (state != State.Null)
            {
                return (int)this.regKey.GetValue(name, defaultValue);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                if (this.regKey != null)
                {
                    int result = (int)this.regKey.GetValue(name, defaultValue);
                    this.regKey.Close();
                    return result;
                }
            }

            return 0;
        }

        public uint GetValue(string name, uint defaultValue)
        {
            if (state != State.Null)
                return (uint)this.regKey.GetValue(name, defaultValue);

            this.regKey = this.hKey.OpenSubKey(this.regPath, false);
            if (this.regKey != null)
            {
                uint result = (uint)this.regKey.GetValue(name, defaultValue);
                this.regKey.Close();

                return result;
            }

            return 0;
        }

        public long GetValue(string name, long defaultValue)
        {
            if (state != State.Null)
            {
                return (long)this.regKey.GetValue(name, defaultValue);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                if (this.regKey != null)
                {
                    long result = (long)this.regKey.GetValue(name, defaultValue);
                    this.regKey.Close();
                    return result;
                }
            }

            return 0;
        }

        public string GetValue(string name, string defaultValue)
        {
            if (state != State.Null)
            {
                return this.regKey.GetValue(name, defaultValue) as string;
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                if (this.regKey != null)
                {
                    string result = (string)this.regKey.GetValue(name, defaultValue);
                    this.regKey.Close();
                    return result;
                }
            }

            return "";
        }

        public bool GetValue(string name, bool defaultValue)
        {

            if (state != State.Null)
            {
                return ((int)this.regKey.GetValue(name, (defaultValue == true) ? 1 : 0) != 0);
            }
            else
            {
                this.regKey = this.hKey.OpenSubKey(this.regPath, false);
                if (this.regKey != null)
                {
                    bool result = ((int)this.regKey.GetValue(name, (defaultValue == true) ? 1 : 0) != 0);
                    this.regKey.Close();
                    return result;
                }
            }

            return false;
        }

        #endregion

        #region Static methods

        /// <summary>
        /// Check if some registry value exists
        /// </summary>
        /// <param name="hive">Registry hive: 2 - HKCU, 3 - HKLM</param>
        /// <param name="regPath">The path, e.g. "SOFTWARE\\Classes</param>
        public static bool DoesKeyExist(HKEY hive, string regPath)
        {
            RegistryKey key;
            switch (hive)
            {
                case HKEY.HKEY_LOCAL_MACHINE:
                    key = Registry.LocalMachine.OpenSubKey(regPath);
                    break;
                default:
                    key = null;
                    break;
            }

            if (key != null)
                key.Close();

            return key != null;
        }

        /// <summary>
        /// Writes string value to some (not this instance root) registry key
        /// </summary>
        /// <param name="localMachine">Registry hive: HKCU/HKLM</param>
        /// <param name="regPath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void WriteValue(bool localMachine, string regPath, string name, string value)
        {
            RegistryKey root = localMachine ? Registry.LocalMachine : Registry.CurrentUser;

            RegistryKey regKey = root.OpenSubKey(regPath, true);
            if (regKey != null)
            {
                regKey.SetValue(name, value, RegistryValueKind.String);
                regKey.Close();
            }
        }

        /// <summary>
        /// Writes int value to any registry key
        /// </summary>
        /// <param name="localMachine">Registry hive: HKCU/HKLM</param>
        /// <param name="regPath"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        public static void WriteValue(bool localMachine, string regPath, string name, int value)
        {
            RegistryKey root = localMachine ? Registry.LocalMachine : Registry.CurrentUser;
            using (RegistryKey reg = root.OpenSubKey(regPath, true))
            {
                if (reg != null)
                    reg.SetValue(name, value, RegistryValueKind.DWord);
            }
        }

        public static string ReadString(bool localMachine, string regPath, string name, string defaultValue = null)
        {
            string result = defaultValue;
            RegistryKey root = localMachine ? Registry.LocalMachine : Registry.CurrentUser;

            using (RegistryKey reg = root.OpenSubKey(regPath, false))
            {
                if (reg != null)
                    result = (string)reg.GetValue(name, defaultValue);
            }
            return result;
        }

        public static int ReadInt(bool localMachine, string regPath, string name, int defaultValue = 0)
        {
            int result = defaultValue;
            RegistryKey root = localMachine ? Registry.LocalMachine : Registry.CurrentUser;

            using (RegistryKey reg = root.OpenSubKey(regPath, false))
            {
                if (reg != null)
                    result = (int)reg.GetValue(name, defaultValue);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hive">Registry hive. FIXME: only HKCU & HKLM are currently supported</param>
        /// <param name="regPath"></param>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <param name="force64Branch"></param>
        /// <returns></returns>
        public static uint ReadValue(HKEY hive, string regPath, string name, uint defaultValue = 0)
        {
            uint result = defaultValue;

            RegistryKey root = hive == HKEY.HKEY_CURRENT_USER ? Registry.CurrentUser : Registry.LocalMachine;

            using (RegistryKey reg = root.OpenSubKey(regPath, false))
            {
                if (reg != null)
                    result = (uint)reg.GetValue(name, defaultValue);
            }

            return result;
        }

        public static UIntPtr HkeyToUintPtr(HKEY hkey)
        {
            switch (hkey)
            {
                case HKEY.HKEY_CLASSES_ROOT:
                    return WinApi.HKEY_CLASSES_ROOT;
                case HKEY.HKEY_CURRENT_USER:
                    return WinApi.HKEY_CURRENT_USER;
                case HKEY.HKEY_LOCAL_MACHINE:
                    return WinApi.HKEY_LOCAL_MACHINE;
                case HKEY.HKEY_USERS:
                    return WinApi.HKEY_USERS;
            }
            return WinApi.HKEY_CURRENT_USER;
        }

        public static uint ReadRegDword(HKEY hive, string keyName, string valueName, bool force64Branch = false)
        {
            UIntPtr hkey = UIntPtr.Zero;
            WinApi.RegSAM regSam = 0;
            if (force64Branch)
                regSam = WinApi.RegSAM.WOW64_64Key;

            try
            {
                int lResult = WinApi.RegOpenKeyEx(HkeyToUintPtr(hive), keyName, 0, (int)WinApi.RegSAM.QueryValue | (int)regSam, out hkey);
                if (0 != lResult)
                    return uint.MaxValue;
                uint lpType = 0;
                uint lpcbData = 4;
                byte[] buf = new byte[4];
                WinApi.RegQueryValueEx(hkey, valueName, 0, out lpType, buf, ref lpcbData);
                return (uint)(buf[0] + buf[1] * 256u + buf[2] * (256u * 256u) + buf[3] * 256u * 256u * 256u);
            }
            finally
            {
                if (hkey != UIntPtr.Zero)
                    WinApi.RegCloseKey(hkey);
            }
        }
        
        /// <summary>
        /// This function is able to read from a 64bit registry from a 32bit app. FIXME: untested
        /// </summary>
        /// <param name="hive"></param>
        /// <param name="inKeyName"></param>
        /// <param name="name"></param>
        /// <param name="force64Branch"></param>
        /// <returns></returns>
        private static string GetRegString(HKEY hive, string inKeyName, string name, bool force64Branch = false)
        {
            UIntPtr hkey = UIntPtr.Zero;
            WinApi.RegSAM regSam = 0;
            if (force64Branch)
                regSam = WinApi.RegSAM.WOW64_64Key;
            try
            {
                int lResult = WinApi.RegOpenKeyEx(HkeyToUintPtr(hive), inKeyName, 0, (int)WinApi.RegSAM.QueryValue | (int)regSam, out hkey);
                if (0 != lResult)
                    return null;
                uint lpType = 0;
                uint lpcbData = 1024;
                StringBuilder buf = new StringBuilder(1024);
                WinApi.RegQueryValueEx(hkey, name, 0, out lpType, buf, ref lpcbData);
                return buf.ToString();
            }
            finally
            {
                if (hkey != UIntPtr.Zero)
                    WinApi.RegCloseKey(hkey);
            }
        }

        #endregion
    }
}
