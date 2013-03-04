using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.AppFxApi
{
    public interface IAppExtender
    {
        void Init(ILogWriter log, Form mainForm, ILocalizedFormList locMgr, IPropsCache propsCacheAndProvider, IPropMan propman, string regKey);
        /// <summary>
        /// Do any action, specific for the given app extender
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="form">Optional </param>
        /// <param name="obj1">1st arg, e.g. the preset object</param>
        /// <param name="obj2">2nd arg, e.g. the string to replace</param>
        /// <returns></returns>
        object DoAction(Defs.ACT action, Form form, object obj1, object obj2);
        object DoAction(string action, Form form, object obj1, object obj2);

        object Get(string name);
        object Get(Defs.PN name);
    }


    public class DummyExtender : IAppExtender
    {
        public DummyExtender() { }

        public void Init(ILogWriter log, Form mainForm, ILocalizedFormList locMgr, DVDVideoSoft.Utils.IPropsCache propsCacheAndProvider, IPropMan propman, string regKey) {}
        public object DoAction(Defs.ACT action, Form form, object obj1, object obj2) { return null; }
        public object DoAction(string action, Form form, object obj1, object obj2) { return null; }
        public object Get(string name) { return null; }
        public object Get(Defs.PN name) { return null; }
    }
}
