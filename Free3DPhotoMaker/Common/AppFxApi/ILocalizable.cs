using System;
using System.Collections.Generic;
using System.Text;

namespace DVDVideoSoft.AppFxApi
{
    public interface ILocalizable
    {
        void SetLocFormList(ILocalizedFormList localizedFormList);
        void BeforeSetCulture();
        void SetCulture(string culture);
        void AfterSetCulture();
        void OnSystemThemeChanged(bool visualStylesEnabled);
    }
}
