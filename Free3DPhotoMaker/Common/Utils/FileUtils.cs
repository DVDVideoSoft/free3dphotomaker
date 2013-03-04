using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml;

using DVDVideoSoft.Resources;

namespace DVDVideoSoft.Utils
{
    public static class FileUtils
    {
        public static string SharedDir;
        public static string InstallDir;
        // Deprecated
        public static string SharedPath;
        public static string InstallPath;

        [DllImport("kernel32")]
        public static extern int GetDiskFreeSpace(
            string lpRootPathName,
            out int lpSectorsPerCluster,
            out int lpBytesPerSector,
            out int lpNumberOfFreeClusters,
            out int lpTotalNumberOfClusters
            );

        [DllImport("kernel32")]
        public static extern int GetDiskFreeSpaceEx(
            string lpDirectoryName,
            ref long lpFreeBytesAvailable,
            ref long lpTotalNumberOfBytes,
            ref long lpTotalNumberOfFreeBytes
            );

        static FileUtils()
        {
            try
            {
                SharedDir = Regedit.ReadString(true, Programs.DVS_REG_KEY, DvsFolderType.SharedDir.ToString(), "");
                SharedDir = SharedDir.TrimEnd(new char[] { '\\', '/' });

                InstallDir = Regedit.ReadString(true, Programs.DVS_REG_KEY, "InstallDir", "");
                if (string.IsNullOrEmpty(InstallPath))
                    InstallDir = string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), Programs.CompanyName);
                InstallDir = InstallDir.TrimEnd(new char[] { '\\', '/' });

                SharedPath = SharedDir + "\\";
                InstallPath = InstallDir + "\\";
            }
            catch { }
        }

        public enum DvsFolderType
        {
            AppInstallDir,
            SharedDir,
            CommonLib,
            CommonBin,
            Personal,
            AppData,
            PlaginsDir
        }

        public static long GetFileSize(string path)
        {
            try
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(path);
                if (fi.Exists)
                    return fi.Length;
            }
            catch (Exception)
            {
            }

            return 0;
        }

        public static ulong GetFreeSpaceOnDrive(string outputDirectory)
        {
            ulong freeSpace = 0;
            try
            {
                string driveLetter = Directory.GetDirectoryRoot(outputDirectory);
                DriveInfo di = new DriveInfo(driveLetter);
                freeSpace = (ulong)di.AvailableFreeSpace;
            }
            catch (Exception) { }
            return freeSpace;
        }

        public static ulong GetFreeSpaceOnDriveEx(string strOutDir)
        {
            ulong freeSpace = 0;
            try
            {
                if (strOutDir.StartsWith(@"\\"))
                {
                    int lpSectorsPerCluster;
                    int lpBytesPerSector;
                    int lpNumberOfFreeClusters;
                    int lpTotalNumberOfClusters;

                    int bRet = GetDiskFreeSpace(strOutDir, out lpSectorsPerCluster, out lpBytesPerSector, out lpNumberOfFreeClusters, out lpTotalNumberOfClusters);

                    freeSpace = (ulong)lpBytesPerSector * (ulong)lpNumberOfFreeClusters;
                }
                else
                {
                    DriveInfo di = new DriveInfo(Directory.GetDirectoryRoot(strOutDir));
                    freeSpace = (ulong)di.AvailableFreeSpace;
                }
            }
            catch (Exception) { }
            return freeSpace;
        }

        /// <summary>
        /// Get estimated size for all items being downloaded.
        /// </summary>
        /// <returns>Estimated size in Bytes</returns>
        public static double GetEstimatedSize(IList<double> fileDurations, int audioBitrate, int videoBitrate)
        {
            double totalSize = 0;
            double curSize = 0;

            foreach (double duration in fileDurations)
            {
                curSize = audioBitrate / 8.0 + 1;
                curSize += videoBitrate / 8.0 + 1;
                curSize *= duration;

                totalSize += curSize;
            }

            //totalSize += 0.05 * totalSize; // calculating error is 5%
            return totalSize;
        }

        /// <summary>
        /// Get estimated size for all items being downloaded.
        /// </summary>
        /// <returns>Estimated size in Bytes</returns>
        public static double GetEstimatedSize(double totalDuration, int audioBitrate, int videoBitrate)
        {
            double totalSize = 0;
            //double curSize = 0;

            double audioPart = audioBitrate / 8.0 + 1;
            double videoPart = videoBitrate / 8.0 + 1;
            totalSize = totalDuration * (audioPart + videoPart);
            //foreach (double duration in fileDurations)
            //{
            //    curSize = audioBitrate / 8.0 + 1;
            //    curSize += videoBitrate / 8.0 + 1;
            //    curSize *= duration;

            //    totalSize += curSize;
            //}

            //totalSize += 0.05 * totalSize; // calculating error is 5%
            return totalSize;
        }

        public static bool CheckFileExistsAndNotZeroLen( string fileName )
        {
            bool retVal = File.Exists(fileName);

            if (retVal)
            {
                FileInfo fi = new FileInfo(fileName);
                try
                {
                    retVal = fi.Length > 0;
                }
                catch (System.IO.FileNotFoundException)
                {
                }
            }

            return retVal;
        }

        public static void OpenDirectory(string path)
        {
            if (!path.StartsWith("\""))
                path = string.Format("\"{0}\"", path);
            try
            {
                System.Diagnostics.Process.Start(new ProcessStartInfo("explorer", path));
            }
            catch { }
        }

        public static void OpenContainingFolder(string path)
        {
            ProcessStartInfo startInfo = null;
            string dir = "";
            string arg = "";

            try
            {
                dir = Path.GetDirectoryName(path);
            }
            catch (Exception) { }

            try
            {
                if (File.Exists(path) || !Directory.Exists(dir))
                    arg = string.Format("/select, \"{0}", path);
                else
                    arg = dir;

                startInfo = new ProcessStartInfo("explorer.exe", arg);
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (Exception) { }
            finally
            {
                if (startInfo != null) startInfo = null;
            }
        }

        public static string ToShortPathName(string longName)
        {
            uint bufferSize = 256;

            StringBuilder shortNameBuffer = new StringBuilder((int)bufferSize);

            uint result = WinApi.GetShortPathName(longName, shortNameBuffer, bufferSize);

            return shortNameBuffer.ToString();
        }

        public static string MakeSafeName(string path)
        {
            char[] illegals = "/\\|*?><\":".ToCharArray();
            foreach (char ch in illegals)
                path = path.Replace(ch, '_');
            return path;
        }

        public static string GetOutputFolderPersonal(string folderName)
        {
            StringBuilder sb = new StringBuilder(GetDvsPath(DvsFolderType.Personal, folderName));
            try
            {
                Directory.CreateDirectory(sb.ToString());
                sb.Append(Path.DirectorySeparatorChar);
            }
            catch
            {
                sb = new StringBuilder(GetDvsPath(DvsFolderType.Personal));
                sb.Append(Path.DirectorySeparatorChar);
            }

            return sb.ToString();
        }

        /// <summary>
        /// This is the preferrable function among GetDvsXxx path routines. By the default convention a directory path string is NOT [back]slash terminated.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetDvsPath(DvsFolderType type)
        {
            switch (type)
            {
                case DvsFolderType.SharedDir:
                    return SharedDir;
                case DvsFolderType.CommonLib:
                    return string.Format("{0}\\lib", SharedDir);
                case DvsFolderType.CommonBin:
                    return string.Format("{0}\\bin", SharedDir);
                case DvsFolderType.AppInstallDir:
                    return string.Format("{0}\\{1}", InstallDir, Programs.ProductName);
                case DvsFolderType.Personal:
                    return string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), Programs.CompanyName);
                case DvsFolderType.AppData:
                    return string.Format("{0}\\{1}", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Programs.CompanyName);
                case DvsFolderType.PlaginsDir:
                    return string.Format("{0}\\plugins", SharedDir);
                    
            }
            return "";
        }

        public static string GetDvsPath(DvsFolderType type, string suffix)
        {
            if (string.IsNullOrEmpty(suffix))
                return GetDvsPath(type);

            return string.Format("{0}\\{1}", GetDvsPath(type), suffix);
        }

        public static DateTime GetFolderDate(string folder)
        {
            if (Directory.Exists(folder)) return Directory.GetCreationTime(folder);
            else return DateTime.Now;
        }

        public static bool EmptyFile(string fileName)
        {
            bool ret = false;
            try
            {
                File.Delete(fileName);
                ret = true;
            }
            catch (Exception)
            {
                FileInfo fi = new FileInfo(fileName);
                FileStream s = null;
                try
                {
                    s = fi.Open(FileMode.Truncate, FileAccess.Write);
                    ret = true;
                }
                catch (Exception) { }
                finally
                {
                    if (s != null)
                        s.Close();
                }
            }

            return ret;
        }

        public static string AppendIndexToFileName(string fileName)
        {
            if (fileName != "")
            {
                Regex pattern = new Regex(@"\([0-9+]\)");

                StringBuilder sb = new StringBuilder(Path.GetFileNameWithoutExtension(fileName));
                string ext = Path.GetExtension(fileName);

                int newSuffixIndex = 1;
                string sTrimmed = sb.ToString();

                if (pattern.IsMatch(sb.ToString()))
                {
                    sTrimmed = pattern.Replace(sTrimmed, string.Empty);
                    string counterString = sb.ToString().Substring(sTrimmed.Length, sb.Length - sTrimmed.Length);
                    counterString = Regex.Replace(counterString, @"\s|\(|\)", "");
                    try
                    {
                        newSuffixIndex = int.Parse(counterString) + 1;
                    }
                    catch (Exception) { }
                }

                string path = Path.GetDirectoryName(fileName);
                StringBuilder sbRet = new StringBuilder();

                do
                {
                    sbRet.Remove(0, sbRet.Length);
                    if (!string.IsNullOrEmpty(path))
                        sbRet.AppendFormat("{0}\\{1}({2}){3}", path, sTrimmed, newSuffixIndex++, ext);
                    else
                        sbRet.AppendFormat("{0}({1}){2}", sTrimmed, newSuffixIndex++, ext);
                } while (File.Exists(sbRet.ToString()));

                return sbRet.ToString();
            }
            return (fileName);
        }

        public static string GenerateArtworkFileName(string strTempPath)
        {
            return string.Format("{0}{1}{2}.jpg", strTempPath, "tmp_cover", new Random().Next(100, 999));
        }

        public static bool CheckOutputFolder(string path, out string errorMessage)
        {
            errorMessage = "";

            if (path == "")
            {
                errorMessage = CommonData.OutputPathEmpty;
            }
            else if (new DriveInfo(path).DriveType == DriveType.CDRom)
            {
                errorMessage = CommonData.UnableCreateOnCD;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    errorMessage = path + "\n" + CommonData.OutputFolderNotExist;
                }
            }

            if (errorMessage != "")
            {
                return false;
            }

            return true;
        }

        public static void EmptyDirectory(string dir, IList<string> patternsForExceptions)
        {
            if (!Directory.Exists(dir))
                return;

            string[] dirs = Directory.GetDirectories(dir, "*.*", SearchOption.AllDirectories);

            for (int j = dirs.Length - 1; j >= 0; j--)
            {
                try
                {
                    if (patternsForExceptions != null)
                    {
                        foreach (string exc in patternsForExceptions)
                        {
                            if (dirs[j].Contains(exc))
                                throw new Exception("Is in exception list, don't delete it");
                        }
                    }
                    Directory.Delete(dirs[j], true);
                }
                catch (IOException ex) { string s = ex.Message; }
                catch (Exception) { }
            }

            string[] files = Directory.GetFiles(dir, "*.*", SearchOption.TopDirectoryOnly);
            for (int j = 0; j < files.Length; j++)
            {
                try
                {
                    if (patternsForExceptions != null)
                    {
                        foreach (string exc in patternsForExceptions)
                        {
                            if (files[j].Contains(exc))
                                throw new Exception("Is in exception list, don't delete it");
                        }
                    }
                    File.Delete(files[j]);
                }
                catch (IOException) { }
                catch (Exception) { }
            }
        }

        public static void EmptyDirectory(string strDir, string strMask, bool bRecurse)
        {
            string[] files = null;
            try
            {
                files = Directory.GetFiles(strDir, strMask, SearchOption.TopDirectoryOnly);
            }
            catch { }

            if (files == null)
                return;

            for (int j = 0; j < files.Length; j++)
            {
                try
                {
                    File.Delete(files[j]);
                }
                catch { }
            }
        }

        public static void CopyDir(string src, string dest)
        {
            if (!Directory.Exists(src))
                throw new DirectoryNotFoundException();

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            string[] dirs = Directory.GetDirectories(src, "*.*", SearchOption.AllDirectories);
            try
            {
                Directory.CreateDirectory(dest);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            for (int j = 0; j < dirs.Length; j++)
            {
                try
                {
                    if (!dirs[j].Contains("\\.svn"))
                        Directory.CreateDirectory(dirs[j].Replace(src, dest));
                }
                catch (IOException) { }
                catch (Exception) { }
            }

            string[] files = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories);
            for (int j = 0; j < files.Length; j++)
            {
                try
                {
                    if (!files[j].Contains("\\.svn"))
                        File.Copy(files[j], files[j].Replace(src, dest));
                }
                catch (Exception)
                {
                }
            }
        }

        public static void CopyDirEx(string src, string dest)
        {
            if (!Directory.Exists(src))
                throw new DirectoryNotFoundException();

            if (!Directory.Exists(dest))
                Directory.CreateDirectory(dest);

            string[] dirs = Directory.GetDirectories(src, "*.*", SearchOption.AllDirectories);

            for (int j = 0; j < dirs.Length; j++)
            {
                try
                {
                    if (!dirs[j].Contains("\\.svn"))
                        Directory.CreateDirectory(dirs[j].Replace(src, dest));
                }
                catch (IOException) { }
                catch (Exception) { }
            }

            try
            {
                Directory.CreateDirectory(dest);
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            for (int j = 0; j < dirs.Length; j++)
            {
                try
                {
                    if (!dirs[j].Contains("\\.svn"))
                        Directory.CreateDirectory(dirs[j].Replace(src, dest));
                }
                catch (IOException) { }
                catch (Exception) { }
            }

            string[] files = Directory.GetFiles(src, "*.*", SearchOption.AllDirectories);
            for (int j = 0; j < files.Length; j++)
            {
                try
                {
                    if (!files[j].Contains("\\.svn"))
                        File.Copy(files[j], files[j].Replace(src, dest));
                }
                catch (Exception)
                {
                }
            }
        }

        public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting, IList<string> errorList)
        {
            bool ret = true;
            try
            {
                SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
                DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

                if (Directory.Exists(SourcePath))
                {
                    if (Directory.Exists(DestinationPath) == false)
                        Directory.CreateDirectory(DestinationPath);

                    foreach (string fls in Directory.GetFiles(SourcePath))
                    {
                        FileInfo flinfo = new FileInfo(fls);
                        try
                        {
                            flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
                        }
                        catch { ret = false; }
                    }
                    foreach (string dir in Directory.GetDirectories(SourcePath))
                    {
                        DirectoryInfo drinfo = new DirectoryInfo(dir);
                        if (!CopyDirectory(dir, DestinationPath + drinfo.Name, overwriteexisting, errorList))
                        {
                            ret = false;
                            if (errorList != null)
                                errorList.Add(dir);
                        }
                    }
                }
                ret = true;
            }
            catch (Exception)
            {
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// Create directory if it doesn't yet exist. Try to create 'alternatePath' on error.
        /// </summary>
        /// <param name="path">Full path of the dir to create</param>
        /// <param name="alternatePath">An alternate path, used if the main path creation fails.</param>
        /// <returns>True on success</returns>
        public static bool CreateDirectoryWithAlternate(string path, string alternatePath)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    if (path == alternatePath)
                        return false;

                    CreateDirectoryWithAlternate(alternatePath, null);
                    return false;
                }
            }
            return true;
        }

        public static string ReadEncryptedFile(string fileName)
        {
            StringBuilder sb = new StringBuilder();
              
            return sb.ToString();
        }

        public static string GetMyVideoFolderPath()
        {
            string result = NativeMethods.GetFolderPath(NativeMethods.CSIDL.MYVIDEO);
            if (string.IsNullOrEmpty(result))
            {
                result = Regedit.ReadString(false, @"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "My Video", "");
                if (string.IsNullOrEmpty(result))
                    result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), (Environment.OSVersion.Version.Major == 5 ? "My Videos" : "Videos"));
                if (string.IsNullOrEmpty(result))
                    result = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return result + "\\";
        }

        public static string GetMyMusicFolderPath()
        {
            string result = NativeMethods.GetFolderPath(NativeMethods.CSIDL.MYMUSIC);
            if (string.IsNullOrEmpty(result))
            {
                result = Regedit.ReadString(false, @"Software\Microsoft\Windows\CurrentVersion\Explorer\User Shell Folders", "My Music", "");
                if (string.IsNullOrEmpty(result))
                    result = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), (Environment.OSVersion.Version.Major == 5 ? "My Music" : "Music"));
                if (string.IsNullOrEmpty(result))
                    result = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            return result;
        }

        public static bool isDirectoryWritable(string dirFullPath)
        {
            string fileName = Path.Combine(dirFullPath, "dummy");
            try
            {
                File.WriteAllText(fileName, "dummy");
            }
            catch (Exception /*ex*/)
            {
                //Log.Error("Directory " + dirFullPath + " isn't writable // " + ex.Message);
                return false;
            }

            try
            {
                File.Delete(fileName);
            }
            catch { }

            return true;
        }

        public static string VerifyOutputFolder(string newOutputFolder, string defaultOutputFolder)
        {
            //string newOutputFolder = path;
            if (!Directory.Exists(newOutputFolder))
            {
                try
                {
                    Directory.CreateDirectory(newOutputFolder);
                }
                catch
                {
                    newOutputFolder = defaultOutputFolder;
                    //Log.Debug("Problem with the output folder. Will use the default one instead");
                }
            }
            if (!newOutputFolder.EndsWith("\\"))
                newOutputFolder += "\\";

            if (!isDirectoryWritable(newOutputFolder))
                newOutputFolder = defaultOutputFolder;

            //GetProvider().Set(Defs.PN.OutputFolder.ToString(), newOutputFolder);
            return newOutputFolder;
        }
    }
}
