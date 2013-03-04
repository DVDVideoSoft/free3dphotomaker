using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace DVDVideoSoft.Utils
{
    public class Functions
    {
        public static bool Shutdown(bool reboot, bool force)
        {
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c shutdown -s";
            if (reboot)
            {
                p.StartInfo.Arguments += " -r";
            }
            if (force)
            {
                p.StartInfo.Arguments += " -f";
            }
            return p.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tool"></param>
        /// <param name="optionalArgs"></param>
        /// <param name="waitForExit"></param>
        /// <returns>Process exit code if waitForExit=true, '0' otherwise.</returns>
        public static int LaunchTool(Programs.ToolID tool, string optionalArgs = null, bool waitForExit = false)
        {
            int result = 0;
            System.Diagnostics.Process p = new System.Diagnostics.Process();

            try
            {
                string toolExePath = string.Format("{0}\\{1}.exe", FileUtils.GetDvsPath(FileUtils.DvsFolderType.CommonBin), tool.ToString());
                if (File.Exists(toolExePath))
                {
                    p.StartInfo.FileName = toolExePath;
                    p.StartInfo.Arguments = string.Format("-{0}={1}", "lang", System.Threading.Thread.CurrentThread.CurrentCulture.Name);
                    if (!string.IsNullOrEmpty(optionalArgs))
                        p.StartInfo.Arguments = p.StartInfo.Arguments + " " + optionalArgs;
                    p.Start();

                    if (waitForExit)
                    {
                        p.WaitForExit();
                        result = p.ExitCode;
                    }
                }
            }
            //catch (System.ComponentModel.Win32Exception)
            //{
            //    return 0;
            //}
            catch
            {
                return 0xFFFFFF;
            }

            return result;
        }

        public enum FinishSound
        {
            Success,
            Stopped
        };

        public static void PlayFinishSound(FinishSound sound)
        {
            Stream wavStream;

            switch (sound)
            {
                case FinishSound.Success:
                    wavStream = Properties.Resources.chimes;
                    break;
                case FinishSound.Stopped:
                    wavStream = Properties.Resources.Information;
                    break;
                default:
                    wavStream = Properties.Resources.Information;
                    break;
            }

            System.Media.SoundPlayer soundPlayer = new System.Media.SoundPlayer(wavStream);
            soundPlayer.PlaySync();
        }

        public static ArrayList  ParseList( string fileName, long listType /* 0 - files; 1 - URLs */ )
        {
            ArrayList list = new ArrayList();

            StreamReader reader = new StreamReader(fileName);
            
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (File.Exists(line)) list.Add(line);
            } 

            return (list != null && list.Count != 0) ? list : null;
        }

        public static string ConvertBytesToStringWithSpace(double value)
        {
            Int64 bytes = (Int64) value;

            string strBytes = Convert.ToString(bytes);

            int length = strBytes.Length;

            int pos = length - 3;

            while (length > 3)
            {
                strBytes = strBytes.Insert(pos, " ");
                pos -= 3;
                length -= 3;
            }

            return strBytes;
        }

        public static int MulDiv(int a, int b, int c)
        {
            var whole = a / c * b;
            var fraction1 = (a % c) * (b / c);
            var fraction2 = (a % c) * (b % c) / c;
            return whole + fraction1 + fraction2;
        }

        public static string[] IntBitArrToStrBitArr<T>(T[] arr, string unit)
        {
            string[] newArr = new string[arr.Length];

            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = System.Convert.ToString(arr[i]) + " " + unit;
            }
            return newArr;
        }

        public static string[] IntBitArrToStrBitArr<T>(T[] arr, string[] unit)
        {
            string[] newArr = new string[arr.Length];

            for (int i = 0; i < newArr.Length; i++)
            {
                newArr[i] = System.Convert.ToString(arr[i]) + " " + unit[i];
            }
            return newArr;
        }
    }
}
