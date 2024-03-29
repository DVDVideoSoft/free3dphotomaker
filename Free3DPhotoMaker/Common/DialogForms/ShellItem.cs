using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace DVDVideoSoft.DialogForms
{
    public class ShellItem
    {
        #region Constructor

        /// <summary>
        /// Constructor. Creates the ShellItem object for the Desktop.
        /// </summary>
        public ShellItem()
        {
            // Obtain the root IShellFolder interface.
            int hRes = ShellAPI.SHGetDesktopFolder(ref m_shRootShell);
            if (hRes != 0)
                Marshal.ThrowExceptionForHR(hRes);

            // Now get the PIDL for the Desktop shell item.
            hRes = ShellAPI.SHGetSpecialFolderLocation(IntPtr.Zero, ShellAPI.CSIDL.CSIDL_DESKTOP, ref m_pIDL);
            if (hRes != 0)
                Marshal.ThrowExceptionForHR(hRes);

            // Now retrieve some attributes for the root shell item.
            ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
            ShellAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), 
                ShellAPI.SHGFI.SHGFI_DISPLAYNAME | 
                ShellAPI.SHGFI.SHGFI_PIDL | 
                ShellAPI.SHGFI.SHGFI_SMALLICON | 
                ShellAPI.SHGFI.SHGFI_SYSICONINDEX
            );

            // Set the arributes to object properties.
            DisplayName  = shInfo.szDisplayName;
            IconIndex    = shInfo.iIcon;
            IsFolder     = true;
            HasSubFolder = true;
            HasFiles = GetFilesCount(GetPath()) > 0;
            Path         = GetPath();

            // Internal with no set{} mutator.
            m_shShellFolder  = RootShellFolder;
        }

        private int GetFilesCount(string folder)
        {
            try
            {
                System.IO.DirectoryInfo tmp = new System.IO.DirectoryInfo(folder);
                return tmp.GetFiles().Length;
            }
            catch
            {
                return 0;
            }
        }


        /// <summary>
        /// Constructor. Create a sub-item shell item object.
        /// </summary>
        /// <param name="shDesktop">IShellFolder interface of the Desktop</param>
        /// <param name="pIDL">The fully qualified PIDL for this shell item</param>
        /// <param name="shParent">The ShellItem object for this item's parent</param>
        public ShellItem(ShellAPI.IShellFolder shDesktop, IntPtr pIDL, ShellItem shParent)
        {
            // Create the FQ PIDL for this new item.
            m_pIDL = ShellAPI.ILCombine(shParent.PIDL, pIDL);

            // Get the properties of this item.
            ShellAPI.SFGAOF uFlags = ShellAPI.SFGAOF.SFGAO_FOLDER | ShellAPI.SFGAOF.SFGAO_HASSUBFOLDER;

            int hRes = ShellAPI.SHGetDesktopFolder(ref m_shRootShell);
            // Here we get some basic attributes.
            shDesktop.GetAttributesOf(1, out m_pIDL, out uFlags);
            IsFolder = Convert.ToBoolean(uFlags & ShellAPI.SFGAOF.SFGAO_FOLDER);
            HasSubFolder = Convert.ToBoolean(uFlags & ShellAPI.SFGAOF.SFGAO_HASSUBFOLDER);
           

            IsFolder = !System.IO.File.Exists(GetPath());
            if (!IsFolder)
                HasSubFolder = false;

            if (IsFolder)
            {
                if (System.IO.Directory.Exists(GetPath()))
                {
                    System.IO.DirectoryInfo tmp = new System.IO.DirectoryInfo(GetPath());
                    HasSubFolder = tmp.GetDirectories().Length > 0;
                    HasFiles = tmp.GetFiles().Length > 0;
                }
            }

            // Now we want to get extended attributes such as the icon index etc.
            ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
            ShellAPI.SHGFI vFlags =
                ShellAPI.SHGFI.SHGFI_SMALLICON |
                ShellAPI.SHGFI.SHGFI_SYSICONINDEX |
                ShellAPI.SHGFI.SHGFI_PIDL |
                ShellAPI.SHGFI.SHGFI_DISPLAYNAME;
            ShellAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);
            DisplayName = shInfo.szDisplayName;
            IconIndex = shInfo.iIcon;
            Path      = GetPath();

            // Create the IShellFolder interface for this item.
            if (IsFolder)
            {
                hRes = (int)shParent.m_shShellFolder.BindToObject(pIDL, IntPtr.Zero, ref ShellAPI.IID_IShellFolder, out m_shShellFolder);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR((int)hRes);
            }
        }

        #endregion

        #region Destructor

        ~ShellItem()
        {
            // Release the IShellFolder interface of this shell item.
            if (m_shShellFolder != null)
                Marshal.ReleaseComObject(m_shShellFolder);

            // Free the PIDL too.
            if (!m_pIDL.Equals(IntPtr.Zero))
                Marshal.FreeCoTaskMem(m_pIDL);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the system path for this shell item.
        /// </summary>
        /// <returns>A path string.</returns>
        public string GetPath()
        {
            StringBuilder strBuffer = new StringBuilder(256);
            ShellAPI.SHGetPathFromIDList(
                m_pIDL, 
                strBuffer
            );
            return strBuffer.ToString();
        }

        /// <summary>
        /// Retrieves an array of ShellItem objects for sub-folders of this shell item.
        /// </summary>
        /// <returns>ArrayList of ShellItem objects.</returns>
        public ArrayList GetSubFolders()
        {
            // Make sure we have a folder.
            if (IsFolder == false)
                throw new Exception("Unable to retrieve sub-folders for a non-folder.");

            ArrayList arrChildren = new ArrayList();
            try
            {
                // Get the IEnumIDList interface pointer.
                ShellAPI.IEnumIDList pEnum = null;
                uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, ShellAPI.SHCONTF.SHCONTF_FOLDERS, out pEnum);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR((int)hRes);
                
                IntPtr pIDL = IntPtr.Zero;
                Int32 iGot = 0;

                // Grab the first enumeration.
                pEnum.Next(1, out pIDL, out iGot);

                // Then continue with all the rest.
                while (!pIDL.Equals(IntPtr.Zero) && iGot == 1)
                {
                    try
                    {
                        // Create the new ShellItem object.
                        ShellItem tmp = new ShellItem(m_shRootShell, pIDL, this);
                        if (tmp.IsFolder)
                            arrChildren.Add(tmp);

                        // Free the PIDL and reset counters.
                        Marshal.FreeCoTaskMem(pIDL);
                        pIDL = IntPtr.Zero;
                        iGot = 0;

                        // Grab the next item.
                        pEnum.Next(1, out pIDL, out iGot);
                    }
                    catch
                    {
                    }
                }

                // Free the interface pointer.
                if (pEnum != null)
                    Marshal.ReleaseComObject(pEnum);
            }
            catch
            {
                /*System.Windows.Forms.MessageBox.Show(ex.Message, "Error:",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );*/
            }

            return arrChildren;
        }

        public ArrayList GetFiles()
        {
            // Make sure we have a folder.
            if (IsFolder == false)
                throw new Exception("Unable to retrieve sub-folders for a non-folder.");

            ArrayList arrChildren = new ArrayList();
            try
            {
                // Get the IEnumIDList interface pointer.
                ShellAPI.IEnumIDList pEnum = null;
                uint hRes = ShellFolder.EnumObjects(IntPtr.Zero, ShellAPI.SHCONTF.SHCONTF_NONFOLDERS, out pEnum);
                if (hRes != 0)
                    Marshal.ThrowExceptionForHR((int)hRes);

                IntPtr pIDL = IntPtr.Zero;
                Int32 iGot = 0;

                // Grab the first enumeration.
                pEnum.Next(1, out pIDL, out iGot);

                // Then continue with all the rest.
                while (!pIDL.Equals(IntPtr.Zero) && iGot == 1)
                {
                    try
                    {
                        // Create the new ShellItem object.
                        arrChildren.Add(new ShellItem(m_shRootShell, pIDL, this));
                    }
                    catch
                    {
                    }

                        // Free the PIDL and reset counters.
                        Marshal.FreeCoTaskMem(pIDL);
                        pIDL = IntPtr.Zero;
                        iGot = 0;

                        // Grab the next item.
                        pEnum.Next(1, out pIDL, out iGot);
                    
                }

                // Free the interface pointer.
                if (pEnum != null)
                    Marshal.ReleaseComObject(pEnum);
            }
            catch
            {
                /*System.Windows.Forms.MessageBox.Show(ex.Message, "Error:",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error
                );*/
            }

            return arrChildren;
        }

        public System.Drawing.Bitmap GetIcon()
        {
            ShellAPI.SHFILEINFO shInfo = new ShellAPI.SHFILEINFO();
            ShellAPI.SHGFI vFlags =
                ShellAPI.SHGFI.SHGFI_SMALLICON |
                ShellAPI.SHGFI.SHGFI_SYSICONINDEX |
                ShellAPI.SHGFI.SHGFI_PIDL |
                ShellAPI.SHGFI.SHGFI_DISPLAYNAME;
            ShellAPI.SHGetFileInfo(m_pIDL, 0, out shInfo, (uint)Marshal.SizeOf(shInfo), vFlags);
            System.Drawing.Bitmap ico = System.Drawing.Bitmap.FromHicon(shInfo.hIcon);
            DVDVideoSoft.Utils.WinApi.DestroyIcon(shInfo.hIcon);
            return ico;
        }


        #endregion

        #region Properties

        /// <summary>
        /// Gets or set the display name for this shell item.
        /// </summary>
        public string DisplayName
        {
            get { return m_strDisplayName; }
            set { m_strDisplayName = value; }
        }
        string m_strDisplayName = "";

        /// <summary>
        /// Gets or sets the system image list icon index for this shell item.
        /// </summary>
        public Int32 IconIndex
        {
            get { return m_iIconIndex; }
            set { m_iIconIndex = value; }
        }
        Int32 m_iIconIndex = -1;

        /// <summary>
        /// Gets the IShellFolder interface of the Desktop.
        /// </summary>
        public ShellAPI.IShellFolder RootShellFolder
        {
            get { return m_shRootShell; }
        }
        ShellAPI.IShellFolder m_shRootShell = null;

        /// <summary>
        /// Gets the IShellFolder interface of this shell item.
        /// </summary>
        public ShellAPI.IShellFolder ShellFolder
        {
            get { return m_shShellFolder; }
        }
        ShellAPI.IShellFolder m_shShellFolder = null;

        /// <summary>
        /// Gets the fully qualified PIDL for this shell item.
        /// </summary>
        public IntPtr PIDL
        {
            get { return m_pIDL; }
        }
        IntPtr m_pIDL = IntPtr.Zero;

        /// <summary>
        /// Gets or sets a boolean indicating whether this shell item is a folder.
        /// </summary>
        public bool IsFolder
        {
            get { return m_bIsFolder; }
            set { m_bIsFolder = value; }
        }
        bool m_bIsFolder = false;

        /// <summary>
        /// Gets or sets a boolean indicating whether this shell item has any sub-folders.
        /// </summary>
        public bool HasSubFolder
        {
            get { return m_bHasSubFolder; }
            set { m_bHasSubFolder = value; }
        }
        bool m_bHasSubFolder = false;

        bool m_bHasFiles = false;

        public bool HasFiles
        {
            get { return m_bHasFiles; }
            set { m_bHasFiles = value; }
        }

        /// <summary>
        /// Gets or sets the system path for this shell item.
        /// </summary>
        public string Path
        {
            get { return m_strPath;  }
            set { m_strPath = value; }
        }
        string m_strPath = "";

        #endregion
    }
}
