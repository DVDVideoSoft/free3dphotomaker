using System;
using System.Collections.Generic;

namespace DVDVideoSoft.AppFxApi
{
    public interface ILocalizedFormList
    {
        void Add(ILocalizable form);
        void Remove(ILocalizable form);
    }
}
