using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class DirSelectBox : FileDialog
    {
        #region private fields
        private DirSelectBoxF _dsf;

        internal const string DEFAULT_TITLE = "选择目录";
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties

        #endregion

        #region constructor destructor
        public DirSelectBox()
        {
            _dsf = new DirSelectBoxF();
            _DialogWindow = _dsf;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <returns></returns>
        public override string Show(IWin32Window owner = null)
        {
            _dsf.ShowInTaskbar = owner == null;
            _dsf.ShowDialog(owner);
            return InputPath;
        }
        #endregion

        #region static method
        /// <summary>
        /// 选择文件夹对话框
        /// </summary>
        /// <param name="defaultPath">默认选择的文件夹</param>
        /// <returns>返回选定的文件夹完整路径</returns>
        public static string Show(IWin32Window owner, string defaultPath)
        {
            DirSelectBox ds = new DirSelectBox();
            Comm.ApplyTheme(ds, owner);
            ds.Enterpath = defaultPath;
            return ds.Show(owner);
        }

        /// <summary>
        /// 选择文件夹对话框
        /// </summary>
        /// <param name="defaultPath">默认选择的文件夹</param>
        /// <param name="title">对话框标题</param>
        /// <returns>返回选定的文件夹完整路径</returns>
        public static string Show(IWin32Window owner, string defaultPath, string title)
        {
            DirSelectBox ds = new DirSelectBox();
            Comm.ApplyTheme(ds, owner);
            ds.Enterpath = defaultPath;
            ds.Title = title;
            return ds.Show(owner);
        }
        #endregion
    }
}
