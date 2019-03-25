using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public class OpenFileDialog : FilterFileDialog
    {
        #region private fields
        private OpenFileDialogF _odf;

        internal const string DEFAULT_TITLE = "打开";
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 返回选择的路径
        /// </summary>
        public List<string> paths
        {
            get
            {
                return DialogResult == DialogResult.OK ? this._odf.InputPaths : null;
            }
        }
        #endregion

        #region constructor
        public OpenFileDialog()
        {
            _odf = new OpenFileDialogF();
            _DialogWindow = _odf;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示不可多选对话框
        /// </summary>
        /// <returns></returns>
        public override string Show(IWin32Window owner = null)
        {
            _odf.ShowInTaskbar = owner == null;
            _odf.MultiSelect = false;
            _odf.ShowDialog(owner);
            return InputPath;
        }

        /// <summary>
        /// 显示可多选对话框
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> Show2(IWin32Window owner = null)
        {
            _odf.ShowInTaskbar = owner == null;
            _odf.MultiSelect = true;
            _odf.ShowDialog(owner);
            return paths;
        }
        #endregion

        #region static method
        /// <summary>
        ///打开选择文件对话框，返回选则文件的路径
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">初始化选择文件的目录</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string path)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            Comm.ApplyTheme(od, owner);
            return od.Show(owner);
        }
        /// <summary>
        ///打开选择文件对话框，返回选则文件的路径
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">初始化选择文件的目录</param>
        /// <param name="filter">文件筛选器</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string path, string filter)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            Comm.ApplyTheme(od, owner);
            return od.Show(owner);
        }
        /// <summary>
        ///打开选择文件对话框，返回选则文件的路径
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">初始化选择文件的目录</param>
        /// <param name="filter">文件筛选器</param>
        /// <param name="filterIndex">初始文件筛选器选中的索引</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string path, string filter, uint filterIndex)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            od.FilterIndex = filterIndex;
            Comm.ApplyTheme(od, owner);
            return od.Show(owner);
        }
        /// <summary>
        ///打开选择文件对话框，返回选则文件的路径
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">初始化选择文件的目录</param>
        /// <param name="filter">文件筛选器</param>
        /// <param name="filterIndex">初始文件筛选器选中的索引</param>
        /// <param name="title">对话框标题</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string path, string filter, uint filterIndex, string title)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            od.FilterIndex = filterIndex;
            od.Title = title;
            Comm.ApplyTheme(od, owner);
            return od.Show(owner);
        }


        /// <summary>
        /// 打开选择文件对话框，返回选则文件的路径,可多选
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">默认显示路径</param>
        /// <returns></returns>
        public static IEnumerable<string> Show2(IWin32Window owner, string path)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            Comm.ApplyTheme(od, owner);
            return od.Show2(owner);
        }
        /// <summary>
        /// 打开选择文件对话框，返回选则文件的路径,可多选
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">默认显示路径</param>
        /// <param name="filter">文件筛选器</param>
        /// <param name="filterIndex">初始文件筛选器选中的索引</param>
        /// <param name="title">对话框标题</param>
        /// <returns></returns>
        public static IEnumerable<string> Show2(IWin32Window owner, string path, string filter)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            Comm.ApplyTheme(od, owner);
            return od.Show2(owner);
        }
        /// <summary>
        /// 打开选择文件对话框，返回选则文件的路径,可多选
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">默认显示路径</param>
        /// <param name="filter">文件筛选器</param>
        /// <param name="filterIndex">初始文件筛选器选中的索引</param>
        /// <returns></returns>
        public static IEnumerable<string> Show2(IWin32Window owner, string path, string filter, uint filterIndex)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            od.FilterIndex = filterIndex;
            Comm.ApplyTheme(od, owner);
            return od.Show2(owner);
        }
        /// <summary>
        /// 打开选择文件对话框，返回选则文件的路径,可多选
        /// </summary>
        /// <param name="owner">指定对话框的拥有者</param>
        /// <param name="path">默认显示路径</param>
        /// <param name="filter">文件筛选器</param>
        /// <param name="filterIndex">初始文件筛选器选中的索引</param>
        /// <param name="title">对话框标题</param>
        /// <returns></returns>
        public static IEnumerable<string> Show2(IWin32Window owner, string path, string filter, uint filterIndex, string title)
        {
            OpenFileDialog od = new OpenFileDialog();
            od.Enterpath = path;
            od.Filter = filter;
            od.FilterIndex = filterIndex;
            od.Title = title;
            Comm.ApplyTheme(od, owner);
            return od.Show2(owner);
        }
        #endregion
    }
}
