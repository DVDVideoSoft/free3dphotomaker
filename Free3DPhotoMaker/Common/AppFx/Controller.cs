using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml;
using System.Xml.XPath;

using DVDVideoSoft.Utils;
using DVDVideoSoft.AppFxApi;

namespace DVDVideoSoft.AppFx
{
    public class Controller
    {
        // Predefined app config xml element and property names
        public static readonly string App_EN = "App";
        public static readonly string UI_EN  = "UI";

        private static ILogWriter Log;
        protected PropMan propMan;

        // Config XML file name based on the app name; if failed to read it in the Init(), this member is set to null.
        protected string configFileName;

        // Buffers
        protected StringBuilder tempSb = new StringBuilder();
        private Dictionary<string, string> tempDict = new Dictionary<string, string>();
        private List<string> tempObjectNames = new List<string>();

        protected char[] xmlvalueTrimChars = { '\r', '\n', '\t', ' ' };

        //static readonly string propsLoader_EN = "PropsLoader";
        static readonly string loadModeImmediate_AttrValue = "immediate";

        #region Methods

        public IPropMan PropMan
        {
            get { return this.propMan; }
        }

        public Controller(ILogWriter log)
        {
            Log = log;

            if (log != null && Log.IsTraceEnabled)
                Log.Trace("Enter Controller()");

            // Create the property manager
            this.propMan = new PropMan(Log, new PropMan.CreateCodeObjectDelegate(this.CodeObjectCreatorMethod));
            
            if (log != null && Log.IsTraceEnabled)
                Log.Trace("apppath: " + (string)this.propMan[Defs.PN.AppPath.ToString()]);
        }

        public static string BaseAppName
        {
            get
            {
                return Path.GetFileNameWithoutExtension(System.Windows.Forms.Application.ExecutablePath);
            }
        }

        public bool Init()
        {
            if (Log.IsTraceEnabled)
                Log.Trace("Enter Controller.Init");
            bool ret = false;

            this.propMan[Defs.PN.AsmName.ToString()] = Controller.BaseAppName;
            this.propMan[Defs.PN.AppPath.ToString()] = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\";

            // Read the app config
            this.configFileName = Path.Combine(this.propMan[Defs.PN.AppPath.ToString()], Controller.BaseAppName + ".xml");

            try
            {
                ReadAppConfig();

                // Read code object definitions. Instantiate 'immediate-load' ones.
                ReadCodeObjectsConfig();

                ret = true;
            }
            catch (FileNotFoundException)
            {
                this.configFileName = null;
                if (Log.IsDebugEnabled)
                    Log.Debug("App config xml not found, file: " + this.configFileName);
            }
            catch (System.Xml.XmlException ex)
            {
                this.configFileName = null;
                if (Log.IsErrorEnabled)
                    Log.Error("XmlException: " + ex.Message);
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled)
                    Log.Error("Exception in Controller.Init: " + ex.Message);
                ret = true; // The particular error may not prevent a general program flow
            }

            return ret;
        }

        public object CodeObjectCreatorMethod(CodeObjectDef objectDef)
        {
            object ret = null;
            try
            {
                ret = Activator.CreateInstanceFrom(
                        (string)this.propMan[Defs.PN.AppPath.ToString()] + objectDef.assemblyName,
                        objectDef.className).Unwrap();
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled && /*hack*/!ex.Message.Contains("TaskbarManager"))
                    Log.Error("Exception caught in CodeObjectCreatorMethod: " + ex.Message);
            }
            //if (ret != null && Log.IsTraceEnabled)
            //        Log.Trace(objectDef.name + " code object created (" + objectDef.assemblyName + "/" + objectDef.className + ")");

            return ret;
        }

        /*
        public ITaskbarManager GetTaskbarManager()
        {
            CodeObjectDef objectDef = new CodeObjectDef("TaskbarManager", "DVDVideoSoft.TaskbarManager.dll", "DVDVideoSoft.TaskbarManager.TaskbarManager");
            DVDVideoSoft.AppFxApi.ITaskbarManager o = null;
            try {
                o = (DVDVideoSoft.AppFxApi.ITaskbarManager)CodeObjectCreatorMethod(objectDef);
            }
            catch (Exception ex) {
                string s = ex.Message;
            }
            //TaskbarManager t = o as TaskbarManager;
            return o;
        }
        */

        #endregion

        void ReadAppConfig()
        {
            if (Log.IsTraceEnabled)
                Log.Trace("==> Controller.ReadAppConfig");
            XmlDocument doc = new XmlDocument();
            doc.Load(this.configFileName);

            // Read all elements to the tempDict buffer. Use a plain dotted notation syntax (<App><Name>.. => "App.Name")
            ReadNodeValue(doc.DocumentElement, "/");

            // and move them to the property manager
            foreach (string key in this.tempDict.Keys)
            {
                if (key.IndexOf("Objects.Object") == 0)
                {
                    if (key.EndsWith("Assembly"))
                    {
                        int pos = key.IndexOf("[");
                        this.tempObjectNames.Add(key.Substring(pos + 1, key.IndexOf("]") - pos - 1));
                    }
                }
                else
                {
                    this.propMan[key] = this.tempDict[key];
                }
            }
            if (Log.IsTraceEnabled)
                Log.Trace("<== Controller.ReadAppConfig");
        }

        void ReadCodeObjectsConfig()
        {
            //Log.Trace("<== Controller.ReadCodeObjectsConfig");
            XPathDocument doc = new XPathDocument(this.configFileName);
            XPathNavigator nav = doc.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("/Config/Objects/Object");
            XPathNodeIterator iterator = nav.Select(expr);

            try
            {
                StringBuilder sb = new StringBuilder("Code objects listing: ");
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    try
                    {
                        CodeObjectDef objDef = new CodeObjectDef(
                                        nav2.GetAttribute("name", ""),
                                        nav2.GetAttribute("assembly", ""),
                                        nav2.GetAttribute("class", ""));
                        sb.AppendFormat("{0}[{1}], ", objDef.name, objDef.className);

                        if (nav2.GetAttribute("loadMode", "") == Controller.loadModeImmediate_AttrValue)
                            objDef.immediateLoad = true;

                        this.propMan.CodeObjectDefs.Add(nav2.GetAttribute("name", ""), objDef);
                    }
                    catch(Exception ex)
                    {
                        if (Log.IsErrorEnabled)
                            Log.Error("Exception caught, error: " + ex.Message);
                    }
                }
                if (Log != null && this.propMan.CodeObjectDefs.Count > 0 && Log.IsTraceEnabled)
                    Log.Trace(sb.ToString());
            }
            catch (Exception ex)
            {
                if (Log.IsErrorEnabled)
                    Log.Error("Exception caught in ReadCodeObjectsConfig: \"" + ex.Message + "\"");
            }
            
            foreach (KeyValuePair<string, CodeObjectDef> objDefPair in this.propMan.CodeObjectDefs)
            {
                if (objDefPair.Value.immediateLoad)
                {
                    object buf = this.propMan.GetCodeObject(objDefPair.Key);
                }
            }
            //Log.Trace("<== Controller.ReadCodeObjectsConfig");
        }

        void ReadNodeValue(XmlNode node, string parentNodeId)
        {
            if (node.HasChildNodes)
            {
                XmlNodeList children = node.ChildNodes;
                foreach (XmlNode child in children)
                {
                    tempSb.Remove(0, tempSb.Length);
                    tempSb.AppendFormat("{0}{1}{2}",
                            parentNodeId == "/" ? "" : parentNodeId,
                            (parentNodeId == "/" || parentNodeId == "") ? "" : ".",
                            parentNodeId == "/" ? "" : node.Name);

                    if (node.Attributes != null && node.Attributes["name"] != null)
                        tempSb.AppendFormat("[{0}]", node.Attributes["name"].Value);

                    ReadNodeValue(child, tempSb.ToString());
                }
            }
            else
            {
                if (node.Value != null)
                {
                    this.tempDict[parentNodeId] = node.Value;
                }
            }
        }
    }
}
