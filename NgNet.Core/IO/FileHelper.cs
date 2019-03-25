using System;
using System.Runtime.InteropServices;
namespace NgNet.IO
{
    #region <class - File>
    public class FileHelper
    {
        #region file copy move
        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool SHFileOperation([In, Out]  SHFILEOPSTRUCT str);
        private const int FO_MOVE = 0x1;
        private const int FO_COPY = 0x2;
        private const int FO_DELETE = 0x3;
        private const ushort FOF_NOCONFIRMATION = 0x10;
        private const ushort FOF_ALLOWUNDO = 0x40;
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]

        private class SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            /// <summary> 
            /// 设置操作方式，移动：FO_MOVE，复制：FO_COPY，删除：FO_DELETE 
            /// </summary> 
            public UInt32 wFunc;
            /// <summary> 
            /// 源文件路径 
            /// </summary> 
            public string pFrom;
            /// <summary> 
            /// 目标文件路径 
            /// </summary> 
            public string pTo;
            /// <summary> 
            /// 允许恢复 
            /// </summary> 
            public UInt16 fFlags;
            /// <summary> 
            /// 监测有无中止 
            /// </summary> 
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            /// <summary> 
            /// 设置标题 
            /// </summary> 
            public string lpszProgressTitle;
        }

        public static bool Copy(string source, string dest, string title)
        {
            SHFILEOPSTRUCT pm = new SHFILEOPSTRUCT();
            pm.wFunc = FO_COPY;
            pm.pFrom = source;
            pm.pTo = dest;
            pm.fFlags = FOF_ALLOWUNDO;//允许恢复 
            pm.lpszProgressTitle = title;
            return !SHFileOperation(pm);
        }

        #endregion
        /// <summary>
        /// 判断指定路径是不是指定类型的文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static bool IsFile(string path, string ext)
        {
            if (System.IO.File.Exists(path))
                if (System.IO.Path.GetExtension(path).ToLower() == ext)
                    return true;
            return false;
        }

        /// <summary>
        /// 显示以某种方式打开文件框
        /// </summary>
        /// <param name="filePath"></param>
        public static void OpenAs(string filePath)
        {
            System.Diagnostics.Process proc = new System.Diagnostics.Process();
            proc.EnableRaisingEvents = false;
            proc.StartInfo.FileName = "rundll32.exe";
            proc.StartInfo.Arguments = "shell32,OpenAs_RunDLL " + filePath;
            proc.Start();
        }

        /// <summary>
        /// 格式化文件长度
        /// </summary>
        /// <param name="length">输入文件的长度，单位为：B</param>
        /// <param name="unit">转换后的单位</param>
        /// <param name="formatFlag">小数位数</param>
        /// <returns></returns>
        public static string LengthFormat(Int64 length, LengthUnit lengthUnit, string formatFlag = "#0.000")
        {
            double rtn = length;
            Int64 div = 0;
            switch (lengthUnit)
            {
                case LengthUnit.B:
                    div = 1;
                    break;
                case LengthUnit.KB:
                    div = 1024;
                    break;
                case LengthUnit.MB:
                    div = 1048576;
                    break;
                case LengthUnit.GB:
                    div = 1073741824;
                    break;
                case LengthUnit.TB:
                    div = 1099511627776;
                    break;
                default:
                    div = 1045876;
                    break;
            }
            return (rtn / div).ToString(formatFlag) + Enum.GetName(typeof(LengthUnit), lengthUnit);
        }

        /// <summary>
        /// 将B为单位的长度转换为自动单位的长度
        /// </summary>
        /// <param name="length">输入文件的长度，单位为：B</param>
        /// <returns></returns>
        public static string LengthFormat(Int64 length)
        {
            if (length < 1024)
                return LengthFormat(length, LengthUnit.B);
            else
                if (length < 1048576)
                return LengthFormat(length, LengthUnit.KB);
            else
                    if (length < 1073741824)
                return LengthFormat(length, LengthUnit.MB);
            else
                return LengthFormat(length, LengthUnit.GB);
        }
    }
    #endregion
}
