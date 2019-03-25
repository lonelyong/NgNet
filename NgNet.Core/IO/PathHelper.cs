using System;

namespace NgNet.IO
{
    #region <class - Path>
    public class PathHelper
    {
        #region private filed
        private const string pathPattern = @"[a-z]{1}:((((([\\]+)|([/]+))[^ /:*?<>\""|\\]{1}[^/:*?<>\""|\\]*)+\\?)|((\\)|(/))*)\s*$";
        private static System.Text.RegularExpressions.Regex pathRegex;
        #endregion

        #region constructor
        static PathHelper()
        {
            pathRegex = new System.Text.RegularExpressions.Regex(pathPattern, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        #endregion

        public const string InvalidNameChars = "<>/\\|:*?\"";
        /// <summary>
        /// 删除指定路径的文或文件夹（包括文件夹下的目录），异常:权限不足，指定路径为空
        /// </summary>
        /// <param name="path"></param>
        /// <returns>返回是否已成功删除（是true ，否false）</returns>
        public static void Delete(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException("path值不能为null");
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            else if (System.IO.Directory.Exists(path))
            {
                System.IO.Directory.Delete(path, true);
            }
        }
        /// <summary>
        /// 判断指定的路径是否合法
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;
            return pathRegex.IsMatch(path);
        }
        /// <summary>
        /// 检测指定路径是否是合法的Path，如果不是则返回默认值
        /// </summary>
        /// <param name="path"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string Test(string path, string defaultValue)
        {
            if (IsPath(path))
                return path;
            else
                return defaultValue;
        }
        /// <summary>
        /// 判断指定文件名或文件夹名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            name = name.Trim();

            if (IsNameInvalidCharContained(name) || name.StartsWith("."))
                return false;
            return true;
        }
        /// <summary>
        /// 判断指定文件名或文件夹名是否合法
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsNameInvalidCharContained(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            foreach (char item in System.IO.Path.GetInvalidFileNameChars())
            {
                if (name.Contains(item.ToString()))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 判断路径是否含有非法字符
        /// </summary>
        public static bool IsPathInvalidCharContained(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return false;

            foreach (char item in System.IO.Path.GetInvalidPathChars())
            {
                if (path.Contains(item.ToString()))
                    return true;
            }
            return false;
        }
        /// <summary>
        /// 获取父目录
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParent(string path)
        {
            if (IsPath(path) == false)
                throw new FormatException("不合法的路径");
            path = path.Replace("/", "\\").Replace("//", "\\").Replace("\\\\", "\\");
            if (path.IndexOf("\\") == -1)
                return path + "\\";
            if (path.IndexOf("\\") == path.LastIndexOf("\\"))
            {
                return path.Substring(0, path.IndexOf("\\") + 1);
            }
            else
            {
                return System.IO.Path.GetDirectoryName(path);
            }
        }
        /// <summary>
        /// 获取指定路径指定级别的父路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetParentByLevel(string path, int level)
        {
            if (string.IsNullOrWhiteSpace(path))
                return null;
            path = path.Replace('/', '\\');
            path = path.Trim();
            if (path.EndsWith("\\") == false)
                path += "\\";
            int sIndex = 0;
            int index = -1;
            for (int i = 0; i <= level; i++)
            {
                index = path.IndexOf('\\', sIndex);
                sIndex = index + 1;
                if (index == -1)
                    throw new Exception("level超出范围");
            }
            index = level == 0 ? index + 1 : index;
            return path.Substring(0, index);
        }
        /// <summary>
        /// 判断指定路径是不是根目录
        /// </summary>
        /// <param name="path">路径名</param>
        /// <returns></returns>
        public static bool IsRoot(string path)
        {
            if (IsPath(path) == false)
                return false;

            path = path.Trim();

            return System.IO.Path.GetPathRoot(path) == path;
        }
        /// <summary>
        /// 在Windows资源管理器上查看指定路径的文件或文件夹
        /// </summary>
        /// <param name="path">指定的路径</param>
        /// <param name="select">指示是否选中</param>
        public static void ShowInExplorer(string path, bool select)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            if (select)
                psi.Arguments = "/e,/select," + path;
            else
                psi.Arguments = path;
            System.Diagnostics.Process.Start(psi);
        }
        /// <summary>
        /// 在Windows资源管理器上查看指定路径的文件或文件夹
        /// </summary>
        /// <param name="paths">路径列表</param>
        public static void ShowInExplorer(string[] paths)
        {
            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo("Explorer.exe");
            psi.Arguments = "/e,/select," + NgNet.Collections.TCollection<string>.ToString(paths, ",");
            System.Diagnostics.Process.Start(psi);
        }
        /// <summary>
        /// 检测指定路径是否存在文件或文件夹
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool IsExisted(string path)
        {
            return System.IO.Directory.Exists(path) || System.IO.File.Exists(path);
        }
        /// <summary>
        /// 更改文件名，使其在指定的目录下不存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetInexistentPath(string path)
        {
            string _dir = System.IO.Path.GetDirectoryName(path);
            string _name = System.IO.Path.GetFileNameWithoutExtension(path);
            string _ext = System.IO.Path.GetExtension(path);
            string _path;
            while (IsExisted(_path = $"{_dir}\\{_name}_{Guid.NewGuid()}{_ext}"))
            {
            }
            return _path;
        }
    }
    #endregion
}
