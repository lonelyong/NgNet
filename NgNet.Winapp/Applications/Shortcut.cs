using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;
using System.Management;

namespace NgNet.Applications
{
    public class ShellLink : IDisposable
    {
        #region interface struct class
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("000214F9-0000-0000-C000-000000000046")]
        private interface IShellLinkW
        {
            uint GetExecuteFile([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszFile, int cchMaxPath, ref WIN32_FIND_DATAW pfd, uint fFlags);

            uint GetIDList(out IntPtr ppidl);
            uint SetIDList(IntPtr pidl);

            uint GetDescription([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszName, int cchMaxName);
            uint SetDescription([MarshalAs(UnmanagedType.LPWStr)] string pszName);

            uint GetWorkingDirectory([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszDir, int cchMaxPath);
            uint SetWorkingDirectory([MarshalAs(UnmanagedType.LPWStr)] string pszDir);

            uint GetArguments([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszArgs, int cchMaxPath);
            uint SetArguments([MarshalAs(UnmanagedType.LPWStr)] string pszArgs);

            uint GetHotKey(out ushort pwHotkey);
            uint SetHotKey(ushort wHotKey);

            uint GetShowCmd(out int piShowCmd);
            uint SetShowCmd(int iShowCmd);

            uint GetIconLocation([Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pszIconPath, int cchIconPath, out int piIcon);
            uint SetIconLocation([MarshalAs(UnmanagedType.LPWStr)] string pszIconPath, int iIcon);

            uint SetRelativePath([MarshalAs(UnmanagedType.LPWStr)] string pszPathRel, uint dwReserved);
            uint Resolve(IntPtr hwnd, uint fFlags);

            uint SetExecuteFile([MarshalAs(UnmanagedType.LPWStr)] string pszFile);
        }
        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99")]
        private interface IPropertyStore
        {
            uint GetCount([Out] out uint cProps);
            uint GetAt([In] uint iProp, out PropertyKey pkey);
            uint GetValue([In] ref PropertyKey key, [Out] PropVariant pv);
            uint SetValue([In] ref PropertyKey key, [In] PropVariant pv);
            uint Commit();
        }
        [StructLayout(LayoutKind.Sequential, Pack = 4, CharSet = CharSet.Unicode)]
        private struct WIN32_FIND_DATAW
        {
            public uint dwFileAttributes;
            public CimType ftCreationTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastAccessTime;
            public System.Runtime.InteropServices.ComTypes.FILETIME ftLastWriteTime;
            public uint nFileSizeHigh;
            public uint nFileSizeLow;
            public uint dwReserved0;
            public uint dwReserved1;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_PATHLENGTH)]
            public string cFileName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 14)]
            public string cAlternateFileName;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct PropertyKey
        {
            #region private fields
            private Guid formatId;
            private int propertyId;
            #endregion

            #region public properties
            public Guid FormatId { get { return formatId; } }
            public int PropertyId { get { return propertyId; } }
            #endregion

            #region constructor
            public PropertyKey(Guid formatId, int propertyId)
            {
                this.formatId = formatId;
                this.propertyId = propertyId;
            }
            public PropertyKey(string formatId, int propertyId)
            {
                this.formatId = new Guid(formatId);
                this.propertyId = propertyId;
            }
            #endregion
        }
        [ComImport]
        [ClassInterface(ClassInterfaceType.None)]
        [Guid("00021401-0000-0000-C000-000000000046")]
        private class CShellLink { }
        [StructLayout(LayoutKind.Explicit)]
        private sealed class PropVariant : IDisposable
        {
            #region private fields
            [FieldOffset(0)]
            private ushort _valueType;

            [FieldOffset(8)]
            private IntPtr _intptr;
            #endregion

            #region public properties
            public VarEnum VarType
            {
                get { return (VarEnum)_valueType; }
                set { _valueType = (ushort)value; }
            }
            public bool IsNullOrEmpty { get { return (_valueType == (ushort)VarEnum.VT_EMPTY || _valueType == (ushort)VarEnum.VT_NULL); } }
            public string Value { get { return Marshal.PtrToStringUni(_intptr); } }
            #endregion

            #region constructor 
            public PropVariant() { }
            public PropVariant(string value)
            {
                if (value == null) throw new ArgumentException("Failed to set value.");
                _valueType = (ushort)VarEnum.VT_LPWSTR;
                _intptr = Marshal.StringToCoTaskMemUni(value);
            }

            ~PropVariant() { Dispose(); }
            #endregion

            #region public methods
            public void Dispose()
            {
                PropVariantClear(this);
                GC.SuppressFinalize(this);
            }
            #endregion
        }
        #endregion

        #region private fields
        private IShellLinkW shellLinkW = null;
        private readonly PropertyKey AppUserModelIDKey = new PropertyKey("{9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3}", 5);
        private const int MAX_PATHLENGTH = 260;
        private const int INFOTIPSIZE = 1024;
        private const int STGM_READ = 0x00000000;
        private const uint SLGP_UNCPRIORITY = 0x0002;
        #endregion

        #region constructor desconstructor
        public ShellLink() : this(null) { }

        /// <summary>
        /// 初始化后载入快捷方式档案
        /// </summary>
        /// <param name="FullLinkFileName">完整的快捷方式文件名称</param>
        public ShellLink(string FullLinkFileName)
        {
            try
            {
                shellLinkW = (IShellLinkW)new CShellLink();
            }
            catch
            {
                throw new COMException("Failed to create ShellLink object.");
            }

            if (FullLinkFileName != null) Load(FullLinkFileName);
        }

        ~ShellLink() { Dispose(false); }
        #endregion

        #region private methods
        [DllImport("Ole32.dll", PreserveSig = false)]
        private extern static void PropVariantClear([In, Out] PropVariant pvar);
        private System.Runtime.InteropServices.ComTypes.IPersistFile PersistFile
        {
            get
            {
                System.Runtime.InteropServices.ComTypes.IPersistFile PersistFile = shellLinkW as System.Runtime.InteropServices.ComTypes.IPersistFile;
                if (PersistFile == null) throw new COMException("Failed to create IPersistFile.");
                return PersistFile;
            }
        }
        private IPropertyStore PropertyStore
        {
            get
            {
                IPropertyStore PropertyStore = shellLinkW as IPropertyStore;
                if (PropertyStore == null) throw new COMException("Failed to create IPropertyStore.");
                return PropertyStore;
            }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 设定/读取 执行的档案
        /// </summary>
        public string ExecuteFile
        {
            get
            {
                StringBuilder FileName = new StringBuilder(MAX_PATHLENGTH);
                WIN32_FIND_DATAW data = new WIN32_FIND_DATAW();
                VerifySucceeded(shellLinkW.GetExecuteFile(FileName, FileName.Capacity, ref data, SLGP_UNCPRIORITY));
                return FileName.ToString();
            }
            set
            {
                VerifySucceeded(shellLinkW.SetExecuteFile(value));
            }
        }

        /// <summary>
        /// 设定/读取 执行档案的参数
        /// </summary>
        public string ExecuteArguments
        {
            get
            {
                StringBuilder ExecuteArgs = new StringBuilder(INFOTIPSIZE);
                VerifySucceeded(shellLinkW.GetArguments(ExecuteArgs, ExecuteArgs.Capacity));
                return ExecuteArgs.ToString();
            }
            set
            {
                VerifySucceeded(shellLinkW.SetArguments(value));
            }
        }

        /// <summary>
        /// 设定/读取 工作路径
        /// </summary>
        public string WorkDirectory
        {
            get
            {
                StringBuilder WorkDirectory = new StringBuilder(MAX_PATHLENGTH);
                VerifySucceeded(shellLinkW.GetWorkingDirectory(WorkDirectory, WorkDirectory.Capacity));
                return WorkDirectory.ToString();
            }
            set
            {
                VerifySucceeded(shellLinkW.SetWorkingDirectory(value));
            }
        }

        /// <summary>
        /// 设定/读取 档案批注
        /// </summary>
        public string Descriptions
        {
            get
            {
                StringBuilder FileDescription = new StringBuilder(MAX_PATHLENGTH);
                VerifySucceeded(shellLinkW.GetDescription(FileDescription, FileDescription.Capacity));
                return FileDescription.ToString();
            }
            set
            {
                VerifySucceeded(shellLinkW.SetDescription(value));
            }
        }

        /// <summary>
        /// 设定/读取 图示档案
        /// </summary>
        public string IconLocation
        {
            get
            {
                StringBuilder IconConfig = new StringBuilder(MAX_PATHLENGTH);
                int IconIndex;
                VerifySucceeded(shellLinkW.GetIconLocation(IconConfig, IconConfig.Capacity, out IconIndex));
                return IconConfig.ToString() + "," + IconIndex.ToString();
            }
            set
            {
                if (value.Split(',').Length == 2)
                {
                    VerifySucceeded(shellLinkW.SetIconLocation(value.Split(',')[0], Convert.ToInt32(value.Split(',')[1])));
                }
            }
        }

        /// <summary>
        /// 设定/读取 Application User Model IDs For Win7 以上操作系统
        /// </summary>
        public string AppUserModelID
        {
            get
            {
                using (PropVariant pv = new PropVariant())
                {
                    VerifySucceeded(PropertyStore.GetValue(AppUserModelIDKey, pv));

                    if (pv.Value == null)
                        return "Null";
                    else
                        return pv.Value;
                }
            }
            set
            {
                using (PropVariant pv = new PropVariant(value))
                {
                    VerifySucceeded(PropertyStore.SetValue(AppUserModelIDKey, pv));
                    VerifySucceeded(PropertyStore.Commit());
                }
            }
        }

        /// <summary>
        /// 释放 ShellLink 使用的资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放ShellLink 使用的资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (shellLinkW != null)
            {
                Marshal.FinalReleaseComObject(shellLinkW);
                shellLinkW = null;
            }
        }

        /// <summary>
        /// 储存快捷方式档案
        /// </summary>
        /// <param name="linkPath">完整的快捷方式文件名称</param>
        public void Save(string linkPath)
        {
            if (linkPath == null) throw new ArgumentNullException("link path is required.");
            PersistFile.Save(linkPath, true);
        }

        /// <summary>
        /// 读取快捷方式档案 
        /// </summary>
        /// <param name="linkPath">完整的快捷方式文件名称</param>
        public void Load(string linkPath)
        {
            if (!File.Exists(linkPath)) throw new FileNotFoundException("link file is not found.", linkPath);
            PersistFile.Load(linkPath, STGM_READ);
        }

        /// <summary>
        /// 确认程序执行
        /// </summary>
        /// <param name="hresult">回传值</param>
        public static void VerifySucceeded(uint hresult)
        {
            if (hresult > 1) throw new InvalidOperationException("Failed with HRESULT: " + hresult.ToString("X"));
        }
        #endregion
    }
}
