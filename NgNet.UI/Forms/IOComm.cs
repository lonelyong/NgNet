using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Collections;
using System.Threading.Tasks;
using NgNet.IO;

namespace NgNet.UI.Forms
{
    class IOComm
    {
        #region const
        public const string DEFAULT_FILTER = IO.FilterHelper.All;

        public static readonly string DEFAULT_PATH = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public const int ONCELOADCOUNT = 50;
        #endregion

        #region private fileds
        private static Icon _floderIcon = ConvertHelper.Bitmap2Icon(Properties.Resources.Folder_mini);

        private static Icon _defaultIcon = Windows.Ext.GetExtIcon("", true);

        private static Dictionary<string, Icon> _Icons = new Dictionary<string, Icon>();
        #endregion

        #region constructor destructor
        static IOComm()
        {
        }
        #endregion

        #region Icon
        public static Icon FloderIcon
        {
            get
            {
                return _floderIcon;
            }
        }

        public static Icon DefaultIcon
        {
            get
            {
                return _defaultIcon;
            }
        }

        public static Dictionary<string, Icon> Icons
        {
            get
            {
                return _Icons;
            }
        }
        #endregion

        #region public method
        /// <summary>
        /// 初始化Navi列表
        /// </summary>
        /// <param name="tv"></param>
        /// <param name="cms"></param>
        public static void ResetNavi(TreeView tv, ContextMenuStrip cms)
        {
            tv.Nodes.Clear();
            TreeNode tn0 = new TreeNode();
            tn0.Text = "这台电脑";
            tn0.Name = "ThisComputer";
            tv.Nodes.Add(tn0);
            TreeNode tn1 = new TreeNode();
            tn1.Text = "我的收藏";
            tn1.Name = "MyFavorites";
            tv.Nodes.Add(tn1);
            //获取驱动器列表，及收藏夹列表
            foreach (DriveInfo di in DriveInfo.GetDrives())
            {
                try
                {
                    string tnText = string.IsNullOrWhiteSpace(di.VolumeLabel) ? NgNet.IO.DriveHelper.GetDriveTypeDescription(di.DriveType) : di.VolumeLabel;
                    TreeNode tn = new TreeNode();
                    tn.Name = di.Name;
                    tn.Text = string.Format("{0}<{1}>", tnText, di.Name.Substring(0, 1));
                    tn.ContextMenuStrip = cms;
                    tn0.Nodes.Add(tn);
                }
                catch (Exception)
                { }
            }
            //初始化我的收藏列表
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "我的桌面");
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.Recent), "最近访问");
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.Templates), "我的模版");
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "我的文档");
            tn1.Nodes.Add( $"{Environment.GetFolderPath( Environment.SpecialFolder.UserProfile)}\\Downloads", "我的下载" );
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyMusic), "我的音乐");
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "我的图片");
            tn1.Nodes.Add(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos), "我的视频");
            foreach (TreeNode item in tn1.Nodes)
            {
                item.ContextMenuStrip = cms;
            }
            //展开导航栏列表
            tv.ExpandAll();
        }
        /// <summary>
        /// 加载指定目录的子目录到指定TreeNode下
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="cms"></param>
        public static void FillNode(TreeNode parentNode, ContextMenuStrip cms)
        {
            parentNode.Nodes.Clear();
            DirectoryInfo[] dir = null;
            try
            {
                dir = new DirectoryInfo(parentNode.Name).GetDirectories();
            }
            catch (Exception ex)
            {
                MessageBox.Show(parentNode.TreeView.FindForm(), ex.Message, "读取错误", MessageBoxButtons.OK);
                return;
            }
            foreach (DirectoryInfo item in dir)
            {
                TreeNode tn0 = new TreeNode();
                tn0.Name = item.FullName;
                tn0.Text = item.Name;
                tn0.ContextMenuStrip = cms;
                parentNode.Nodes.Add(tn0);
            }
            parentNode.Expand();
        }
        /// <summary>
        /// 在navi上定位到指定路径
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="path"></param>
        public static void LocateNode(TreeNode tn, string path, ContextMenuStrip cms)
        {
            string[] names = path.Split(new char[] { '\\', '/' }, StringSplitOptions.RemoveEmptyEntries);
            if (names.Length == 0)
                return;
            int index = -1;
            for (int i = 0; i < names.Length; i++)
            {
                index = tn.Nodes.IndexOfKey( PathHelper.GetParentByLevel(path, i));
                if (index == -1)
                    return;
                else
                {
                    tn = tn.Nodes[index];
                    FillNode(tn, cms);
                }
            }
            tn.TreeView.SelectedNode = null;
            tn.TreeView.SelectedNode = tn;
        }
        /// <summary>
        /// 在指定路径后添加一段新项
        /// </summary>
        /// <param name="basePath">主目录</param>
        /// <param name="pathAppend">添加的目录</param>
        /// <returns></returns>
        public static string PathCombin(string basePath, string pathAppend)
        {
            basePath = basePath.Trim();
            if (basePath.EndsWith(@"\"))
            {
                return basePath + pathAppend;
            }
            else
            {
                return basePath + @"\" + pathAppend;
            }
        }
        /// <summary>
        /// 返会形如 (.*;.mp3) 的拓展名数组
        /// </summary>
        /// <param name="filter">类型集合字符创</param>
        /// <returns></returns>
        public static List<string> GetExts(string filter)
        {
            //如果输入的strType为空则返回所有文件类型
            if (string.IsNullOrWhiteSpace(filter))
                return new List<string>() { ".*" };
            List<string> tmp = new List<string>(filter.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));
            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].StartsWith("*"))
                    tmp[i] = tmp[i].Remove(0, 1);
            }
            return tmp;
        }
        /// <summary>
        /// 首次进入对话框的路径检测
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string EnterPathTest(string path)
        {
            if (PathHelper.IsPath(path))
            {
               
                if (Directory.Exists(path))
                    return path;
                else if (File.Exists(path))
                    return Path.GetDirectoryName(path);
                else if (MessageBox.Show($"以下路径不存在，是否创建？\n    path = {path}", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        Directory.CreateDirectory(path);
                        return path;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"创建以下路径失败：\n    path = {path}\n可能原因：\n    1.{ex.Message}");
                    }
                }
            }
            return DEFAULT_PATH;
        }
        /// <summary>
        /// 指定路径创建指定类型的文件或文件夹
        /// </summary>
        /// <param name="isfile">是不是文件</param>
        /// <param name="type">文件类型说明</param>
        /// <param name="ext">文件拓展名</param>
        /// <param name="directory">指定在那个目录创建</param>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static bool PathCreate(IWin32Window owner, PathType ff, string directory, out string path)
        {
            path = "";
            string title  = $"新建文件{(ff == PathType.Floder ? "夹" : "")}";
            string notice = $"请输入文件{(ff == PathType.Floder ? "夹" : "")}名：";
            string defName = $"{title}1";
        Re: string name = InputBox.Show(owner, notice, defName, title, InputTypes.Text);
            if (string.IsNullOrWhiteSpace(name))
                return false;
            if (!PathHelper.IsName(name))
                if (MessageBox.Show("输入的文件名不合法，请重试！", "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    goto Re;
                else
                    return false;
            string _path = PathCombin( directory, name);
            if (File.Exists(_path) || Directory.Exists(_path))
                if (MessageBox.Show(owner, "已存在同名文件(夹)，请重试！", "", MessageBoxButtons.RetryCancel) == DialogResult.Retry)
                    goto Re;
                else
                    return false;
            try
            {
                if (ff == PathType.File)
                    File.Create(_path);
                else
                    Directory.CreateDirectory(_path);
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, ex.Message, "文件(夹)创建失败！\r\npath = " + _path, MessageBoxButtons.OK);
                return false;
            }
            path = _path;
            return true;
        }
        /// <summary>
        /// 从导航栏新建文件夹创建文件夹
        /// </summary>
        /// <param name="parentNode"></param>
        public static void NodeCreate(TreeNode parentNode)
        {
            Form _owner = parentNode.TreeView.FindForm();
            string _path;
            if(PathCreate(_owner, PathType.Floder, parentNode.Name, out _path ))
            {
                TreeNode tn = new TreeNode();
                tn.Name = _path;
                tn.ContextMenuStrip = parentNode.ContextMenuStrip;
                tn.Text = Path.GetFileName(_path);
                parentNode.Nodes.Add( tn );
                parentNode.TreeView.SelectedNode = tn;
            }
        }
        /// <summary>
        /// 从导航栏删除文件夹
        /// </summary>
        /// <param name="node"></param>
        public static void NodeDelete(TreeNode node)
        {
            Form owner = node.TreeView.FindForm();
            string inf = string.Format("即将永久删除以下路径文件（夹）\r\npath = {0}", node.Name);
            if (Forms.MessageBox.Show(owner, inf, "删除提示", MessageBoxButtons.YesNo) == DialogResult.No)
                return;
            try
            {
                Directory.Delete(node.Name);
                node.Remove();
            }
            catch (Exception ex)
            {
                Forms.MessageBox.Show(owner, ex.Message, "删除失败", MessageBoxButtons.OK);
            }
        }
        /// <summary>
        /// 重命名文件文件夹对话框
        /// </summary>
        /// <param name="path">原文件路径</param>
        /// <returns>新文件路径</returns>
        public static string ShowRenameDialog(Form owner, string path)
        {
            if (File.Exists(path) || Directory.Exists(path)) { }
            else
            {
                MessageBox.Show(owner, string.Format("由于以下路径文件（夹）不存在，不能重命名!\r\n<{0}>", path), "重命名");
                return null;
            }
            Re:
            string _newName = InputBox.Show(
                owner, 
                string.Format("即将重命名 <{0}>,请输入新名称：", path), 
                Path.GetFileName(path), 
                "重命名");
            if (string.IsNullOrWhiteSpace(_newName))
                return null;
            if (string.Compare(_newName, Path.GetFileName(path), true) == 0)
                return null;
            _newName = Path.Combine(Path.GetDirectoryName(path), _newName);
            if (File.Exists(_newName))
            {
                MessageBox.Show(owner, "已存在以下路径文件，请重试\n path = " + _newName);
                goto Re;
            }
            else if (Directory.Exists(_newName))
            {
                DialogResult dr = MessageBox.Show(owner, "已存在以下路径文件文件夹，是否进行合并\n path = " + _newName, MessageBoxButtons.YesNo);
                if(dr == DialogResult.No)
                goto Re;
            }
            try
            {
                if (File.Exists(path))
                    File.Move(path, _newName);
                else if (Directory.Exists(path))
                    Directory.Move(path, _newName);
                else
                    return null;
                return _newName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(owner, ex.Message, "重命名失败");
                return null;
            }

        }
        /// <summary>
        /// 不合法的字符检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void NameInputTest_Keypress( object sender, KeyPressEventArgs e)
        {
            e.Handled = IO.PathHelper.InvalidNameChars.Contains(e.KeyChar);
        }
        /// <summary>
        /// 获取指定类型文件的图标
        /// </summary>
        /// <param name="ext"></param>
        /// <returns></returns>
        public static Icon GetIcon(string ext)
        {
            if (string.IsNullOrWhiteSpace(ext))
                return DefaultIcon;
            var ico = from i in Icons where i.Key == ext select i.Value;
            if(ico.Count() == 0)
            {
                Icon tmp = Windows.Ext.GetExtIcon(ext, true);
                Icons.Add(ext, tmp);
                return tmp;
            }
            else
            {
                return ico.FirstOrDefault<Icon>();
            }
        }
        #endregion
    }
}
