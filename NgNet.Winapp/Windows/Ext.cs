using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace NgNet.Windows
{
    public class Ext
    {
        #region private fields

        #endregion

        #region proptected fields

        #endregion

        #region public properties

        #endregion

        #region constructor
        public Ext()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        /// <summary>
        /// 从文件扩展名得到文件关联图标
        /// </summary>
        /// <param name="fileExt">文件名或文件扩展名</param>
        /// <param name="smallIcon">是否是获取小图标，否则是大图标</param>
        /// <returns>图标</returns>
        public static Icon GetExtIcon(string fileExt, bool smallIcon)
        {
            Apis.Shell32.SHFILEINFO fi = new Apis.Shell32.SHFILEINFO();
            Icon ic = null;
            //SHGFI_ICON + SHGFI_USEFILEATTRIBUTES + SmallIcon   
            int iTotal = (int)Apis.Shell32.SHGetFileInfo(fileExt, 100, ref fi, 0, (uint)(smallIcon ? 273 : 272));
            if (iTotal > 0)
            {
                ic = Icon.FromHandle(fi.hIcon);
            }
            return ic;
        }

        /// <summary>  
        /// 通过扩展名得到图标和描述  
        /// </summary>  
        /// <param name="ext">扩展名(如“.txt”)</param>  
        /// <param name="largeIcon">得到大图标</param>  
        /// <param name="smallIcon">得到小图标</param>  
        /// <param name="description">得到类型描述或者空字符串</param>  
        public static string GetExtDescription( string ext)
        {
            string description;
            if (string.IsNullOrWhiteSpace( ext ))
                description = "未知文件类型";
            else
                description = ext;
                            //缺省类型描述  
            RegistryKey extsubkey = Registry.ClassesRoot.OpenSubKey( ext );   //从注册表中读取扩展名相应的子键  
            if (extsubkey == null) return description;
            string extdefaultvalue = extsubkey.GetValue( null ) as string;     //取出扩展名对应的文件类型名称  
            if (extdefaultvalue == null) return description;

            if (extdefaultvalue.Equals( "exefile", StringComparison.OrdinalIgnoreCase ))  //扩展名类型是可执行文件  
            {
                RegistryKey exefilesubkey = Registry.ClassesRoot.OpenSubKey( extdefaultvalue );  //从注册表中读取文件类型名称的相应子键  
                if (exefilesubkey != null)
                {
                    string exefiledescription = exefilesubkey.GetValue( null ) as string;   //得到exefile描述字符串  
                    if (exefiledescription != null) description = exefiledescription;
                }
            }
            RegistryKey typesubkey = Registry.ClassesRoot.OpenSubKey( extdefaultvalue );  //从注册表中读取文件类型名称的相应子键  
            if (typesubkey == null) return description;

            string typedescription = typesubkey.GetValue( null ) as string;   //得到类型描述字符串  
            if (typedescription != null) description = typedescription;
            return description;
        }
        #endregion
    }
}
