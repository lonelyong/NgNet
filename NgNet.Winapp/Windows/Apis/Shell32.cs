using System;
using System.Runtime.InteropServices;

namespace NgNet.Windows.Apis
{
    public class Shell32
    {
        [Flags]
        public enum ShellExecuteMaskFlags : uint
        {
            SEE_MASK_DEFAULT = 0x00000000,
            SEE_MASK_CLASSNAME = 0x00000001,
            SEE_MASK_CLASSKEY = 0x00000003,
            SEE_MASK_IDLIST = 0x00000004,
            SEE_MASK_INVOKEIDLIST = 0x0000000c,   // Note SEE_MASK_INVOKEIDLIST(0xC) implies SEE_MASK_IDLIST(0x04) 
            SEE_MASK_HOTKEY = 0x00000020,
            SEE_MASK_NOCLOSEPROCESS = 0x00000040,
            SEE_MASK_CONNECTNETDRV = 0x00000080,
            SEE_MASK_NOASYNC = 0x00000100,
            SEE_MASK_FLAG_DDEWAIT = SEE_MASK_NOASYNC,
            SEE_MASK_DOENVSUBST = 0x00000200,
            SEE_MASK_FLAG_NO_UI = 0x00000400,
            SEE_MASK_UNICODE = 0x00004000,
            SEE_MASK_NO_CONSOLE = 0x00008000,
            SEE_MASK_ASYNCOK = 0x00100000,
            SEE_MASK_HMONITOR = 0x00200000,
            SEE_MASK_NOZONECHECKS = 0x00800000,
            SEE_MASK_NOQUERYCLASSSTORE = 0x01000000,
            SEE_MASK_WAITFORINPUTIDLE = 0x02000000,
            SEE_MASK_FLAG_LOG_USAGE = 0x04000000,
        }

        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SHELLEXECUTEINFO
        {
            public int cbSize;
            public uint fMask;
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpVerb;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpFile;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpParameters;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpDirectory;
            public int nShow;
            public IntPtr hInstApp;
            public IntPtr lpIDList;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpClass;
            public IntPtr hkeyClass;
            public uint dwHotKey;
            public IntPtr hIcon;
            public IntPtr hProcess;
        }

        //定义文件属性标识  
        public enum FileAttributeFlags : int
        {
            FILE_ATTRIBUTE_READONLY = 0x00000001,
            FILE_ATTRIBUTE_HIDDEN = 0x00000002,
            FILE_ATTRIBUTE_SYSTEM = 0x00000004,
            FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
            FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
            FILE_ATTRIBUTE_DEVICE = 0x00000040,
            FILE_ATTRIBUTE_NORMAL = 0x00000080,
            FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
            FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
            FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
            FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
            FILE_ATTRIBUTE_OFFLINE = 0x00001000,
            FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
            FILE_ATTRIBUTE_ENCRYPTED = 0x00004000
        }

        //定义获取资源标识  
        public enum FileInfoFlags : uint
        {
            SHGFI_ICON = 0x000000100, // get icon  
            SHGFI_DISPLAYNAME = 0x000000200, // get display name  
            SHGFI_TYPENAME = 0x000000400, // get type name  
            SHGFI_ATTRIBUTES = 0x000000800, // get attributes  
            SHGFI_ICONLOCATION = 0x000001000, // get icon location  
            SHGFI_EXETYPE = 0x000002000, // return exe type  
            SHGFI_SYSICONINDEX = 0x000004000, // get system icon index  
            SHGFI_LINKOVERLAY = 0x000008000, // put a link overlay on icon  
            SHGFI_SELECTED = 0x000010000, // show icon in selected state  
            SHGFI_ATTR_SPECIFIED = 0x000020000, // get only specified attributes  
            SHGFI_LARGEICON = 0x000000000, // get large icon  
            SHGFI_SMALLICON = 0x000000001, // get small icon  
            SHGFI_OPENICON = 0x000000002, // get open icon  
            SHGFI_SHELLICONSIZE = 0x000000004, // get shell size icon  
            SHGFI_PIDL = 0x000000008, // pszPath is a pidl  
            SHGFI_USEFILEATTRIBUTES = 0x000000010, // use passed dwFileAttribute  
            SHGFI_ADDOVERLAYS = 0x000000020, // apply the appropriate overlays  
            SHGFI_OVERLAYINDEX = 0x000000040 // Get the index of the overlay  
        }

        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            public char szDisplayName;
            public char szTypeName;
        }

        //定义SHFILEINFO结构  
        [StructLayout( LayoutKind.Sequential )]
        public struct FileInfo
        {
            public IntPtr hIcon;
            public int iIcon;
            public int dwAttributes;

            [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 260 )]
            public string szDisplayName;

            [MarshalAs( UnmanagedType.ByValTStr, SizeConst = 80 )]
            public string szTypeName;
        }

        [DllImport("shell32.dll")]
        public static extern bool ShellExecuteEx(ref SHELLEXECUTEINFO lpExecInfo);

        [DllImport("Shell32.dll")]
        public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport( "shell32.dll", EntryPoint = "SHGetFileInfo" )]
        public static extern int GetFileInfo( string pszPath, int dwFileAttributes, ref FileInfo psfi, int cbFileInfo, int uFlags );

        [DllImport( "shell32.dll" )]
        private static extern int ExtractIconEx( string lpszFile, int niconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons );
    }
}
