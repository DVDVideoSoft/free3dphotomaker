using System;
using System.Collections.Generic;
using System.Text;

using DVDVideoSoft.Utils;

namespace DVDVideoSoft.AppFx
{
    public static class Helper
    {
        public static bool CheckEngineIsFullFeatured()
        {
            try {
                string ver = Regedit.ReadString( true, "SOFTWARE\\" + Programs.ToolID.CodecPack.ToString(), "Version", "" );
                return !string.IsNullOrEmpty( ver );
            } catch ( Exception ex ) {
                string s = ex.Message;
            } //(System.Runtime.InteropServices.COMException)

            return false;
        }
    }
}
