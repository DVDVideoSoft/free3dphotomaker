using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DVDVideoSoft.AppFxApi
{
    public interface IThemeLoader
    {
        bool Load(string fileName);
        Color GetColor(string elementName, Color defaultValue);
        Image GetImage(string elementName);
        bool IsBorderless { get; }
    }
}
