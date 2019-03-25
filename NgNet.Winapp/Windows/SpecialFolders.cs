using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Windows
{
    public class SpecialFolders
    {
        #region public properties
        /// <summary>
        /// 获取桌面路径
        /// </summary>
        public static string Desktop
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.Desktop); }
        }

        /// <summary>
        /// 获取音乐路径
        /// </summary>
        public static string MyMusic
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyMusic); }
        }

        /// <summary>
        /// 获取我的文档路径
        /// </summary>
        public static string MyDocuments
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments); }
        }

        /// <summary>
        /// 应用程序数据公共存储目录
        /// </summary>
        public static string ApplicationData
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); }
        }

        /// <summary>
        /// 我的下载
        /// </summary>
        public static string MyDownloads { get
            {
                return $"{Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)}\\Downloads";
            } }
        #endregion

        #region constructor
        public SpecialFolders()
        {

        }
        #endregion

    }
}
