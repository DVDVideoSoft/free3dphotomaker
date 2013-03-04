using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;

namespace DVDVideoSoft.Utils
{
    public class FormattingFunctions
    {
        public static string FormatFileSize( long bytes )
        {
            if (bytes > 1099511627776)
                return ( (float)bytes / (float)1099511627776 ).ToString( "0.00 TB" );
            else if (bytes > 1073741824)
                return ( (float)bytes / (float)1073741824 ).ToString( "0.00 GB" );
            else if (bytes > 1048576)
                return ( ( (float)bytes / (float)1048576 ) ).ToString( "0.00 MB" );
            else if (bytes > 1024)
                return ( (float)bytes / (float)1024 ).ToString( "0.00 KB" );
            else return bytes + " B";
        }

        public static bool IsTrueValue( string value )
        {
            if (string.IsNullOrEmpty( value ))
                return false;
            string lowerCaseValue = value.ToLower();
            return lowerCaseValue == "true" || lowerCaseValue == "yes";
        }

        public static bool IsFalseValue( string value )
        {
            if (string.IsNullOrEmpty( value ))
                return false;
            string lowerCaseValue = value.ToLower();
            return lowerCaseValue == "false" || lowerCaseValue == "no";
        }

        public static string TransformCapitalize( string str )
        {
            char[] tmp = str.ToCharArray();
            str = str.ToUpper();
            tmp[0] = str[0];
            return new string( tmp );
        }

        public static string TransformCapitalize( string value, bool capitalize )
        {
            if (string.IsNullOrEmpty( value ))
                return value;
            StringBuilder sb = new StringBuilder( capitalize ? value.Substring( 0, 1 ).ToUpper() : value.Substring( 0, 1 ).ToLower() );
            if (value.Length > 1)
                sb.Append( value.Substring( 1, value.Length - 1 ) );
            return sb.ToString();
        }

        public static int GetInternetUrlMediaType( string url )
        {
            if (Path.GetExtension( url ).ToLower() == ".m3u")
                return 3;
            if (Path.GetExtension( url ).ToLower() == ".asx")
                return 7;
            return -1;
        }

        public static Color ParseColor( string value )
        {
            Color ret = Color.FromName( value );

            if (!ret.IsNamedColor || !ret.IsKnownColor) {
                ret = System.Drawing.ColorTranslator.FromHtml( value );
            }

            return ret;
        }

        public static string MakePresetName( string videoFormat, string videoBitrate, string videoHeight, string videoWidth,
                                        string audioFormat, string audioSampleRate, string audioChannels, string audioBitrate )
        {
            StringBuilder sb = new StringBuilder();
            string secondVideoFormatPart;

            if (!string.IsNullOrEmpty( videoFormat )) {
                if (!videoFormat.Contains( "," )) {
                    secondVideoFormatPart = videoFormat;
                } else {
                    secondVideoFormatPart = videoFormat.Split( ',' )[1];
                    secondVideoFormatPart = secondVideoFormatPart.Remove( 0, 1 );
                }
                sb.AppendFormat( "{0}, {1}x{2}, {3}; ", secondVideoFormatPart, videoWidth, videoHeight, videoBitrate );
            }

            if (audioFormat != null)
                sb.AppendFormat( "{0}, {1}, {2}, {3}", audioFormat, audioSampleRate, audioChannels, audioBitrate );

            if (videoWidth == "0" && videoHeight == "0")
                return sb.ToString().Replace( "0x0", "Same size" );
            else
                return sb.ToString();
        }

        public static string MakeFileExtension( string formatInUpperCase, bool prependWithDot )
        {
            return ( prependWithDot ? "." : "" ) + formatInUpperCase.ToLower();
        }

        public static string ListToString( IList<string> list, string separator )
        {
            StringBuilder sb = new StringBuilder();

            foreach (string item in list) {
                sb.AppendFormat( "{0}{1}", item, separator );
            }

            sb.Remove( sb.Length - separator.Length, separator.Length );

            return sb.ToString();
        }

        public static IList<string> CloneList( IList<string> source )
        {
            List<string> ret = new List<string>();
            foreach (string s in source)
                ret.Add( s );
            return ret;
        }

        public static IList<string> MultilineStringToList( string source )
        {
            string s = source.Replace( "\r\n", "|" );
            while (s[s.Length - 1] == '|')
                s.Remove( s.Length - 1, 1 );
            string[] lines = s.Split( new char[] { '|' } );
            return lines;
        }

        public static bool IsProxyNameValid( string proxy )
        {
            proxy = proxy.Trim();

            if (proxy.Length == 0) {
                return false;
            }
            return true;
        }

        public static string GetThreePartVersion( string versionString )
        {
            return versionString.Substring( 0, versionString.LastIndexOf( '.' ) );
        }

        public static string GetVerboseVersion( string versionString )
        {
            StringBuilder sb = new StringBuilder();
            int pos = versionString.LastIndexOf( '.' );
            if (pos == -1)
                return "";
            sb.AppendFormat( " v. {0} build {1}", versionString.Substring( 0, pos ), versionString.Substring( pos + 1, versionString.Length - pos - 1 ) );
            return sb.ToString();
        }

        public static string GetThreePartVersion( System.Diagnostics.FileVersionInfo verInfo )
        {
            return string.Format( "{0}.{1}.{2}", verInfo.FileMajorPart, verInfo.FileMinorPart, verInfo.FileBuildPart );
        }

        /// <summary>
        /// Parses binumeric size string into x,y
        /// </summary>
        /// <param name="sizeString">Input value in "800,600" (x,y)</param>
        /// <returns></returns>
        public static void ParseSizeString( string sizeString, ref int width, ref int height )
        {
            int pos = sizeString.IndexOf( ',' );
            width = int.Parse( sizeString.Substring( 0, pos ) );
            height = int.Parse( sizeString.Substring( pos + 1, sizeString.Length - pos - 1 ) );
        }

        public static int StrToIntDef( string strValue, int nDefaultValue )
        {
            int nRet;
            if ( int.TryParse(strValue, out nRet ) )
                return nRet;
            else
                return nDefaultValue;
        }

        public static string GetTokenAfter( string source, string separator = "=" )
        {
            int pos = source.IndexOf( separator );
            if (pos == -1)
                return null;
            return source.Substring( pos + 1 ).Trim();
        }
    }
}
