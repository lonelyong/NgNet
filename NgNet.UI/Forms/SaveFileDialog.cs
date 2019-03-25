
using System.Collections.Generic;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class SaveFileDialog : FilterFileDialog, ITheme
    {
        #region private fields
        private SaveFileDialogF _sdf;
        internal const string DEFAULT_TITLE = "保存";
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 默认保存文件名
        /// </summary>
        public string DefaultName
        {
            set
            {
                _sdf.SaveName = value;
            }
        }
        #endregion

        #region constructor destructor
        public SaveFileDialog()
        {
            _sdf = new SaveFileDialogF();
            _DialogWindow = _sdf;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <returns></returns>
        public override string Show(IWin32Window owner = null)
        {
            _sdf.ShowInTaskbar = owner == null;
            _sdf.ShowDialog(owner);
            return InputPath;
        }

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override IEnumerable<string> Show2(IWin32Window owner = null)
        {
            _sdf.ShowInTaskbar = owner == null;
            _sdf.ShowDialog(owner);
            return new string[] { InputPath };
        }
        #endregion

        #region static method
        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="defaultName">默认文件名</param>
        /// <param name="path">默认的存储路径</param>
        /// <param name="filter">允许保存的文件格式</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string defaultName, string path, string filter)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultName = defaultName;
            sd.Enterpath = path;
            sd.Filter = filter;
            Comm.ApplyTheme(sd, owner);
            return sd.Show(owner);
        }
        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="defaultName">默认文件名</param>
        /// <param name="path">默认的存储路径</param>
        /// <param name="filter">允许保存的文件格式</param>
        /// <param name="filterIndex">默认保存的文件格式 默认未Filter0</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string defaultName, string path, string filter, uint filterIndex)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultName = defaultName;
            sd.Enterpath = path;
            sd.Filter = filter;
            sd.FilterIndex = filterIndex;
            Comm.ApplyTheme(sd, owner);
            return sd.Show(owner);
        }
        /// <summary>
        /// 保存文件对话框
        /// </summary>
        /// <param name="defaultName">默认文件名</param>
        /// <param name="path">默认的存储路径</param>
        /// <param name="filter">允许保存的文件格式</param>
        /// <param name="filterIndex">默认保存的文件格式 默认未Filter0</param>
        /// <param name="title">对话框的标题</param>
        /// <returns></returns>
        public static string Show(IWin32Window owner, string defaultName, string path, string filter, uint filterIndex, string title)
        {
            SaveFileDialog sd = new SaveFileDialog();
            sd.DefaultName = defaultName;
            sd.Enterpath = path;
            sd.Filter = filter;
            sd.FilterIndex = filterIndex;
            sd.Title = title;
            Comm.ApplyTheme(sd, owner);
            return sd.Show(owner);
        }
        #endregion
    }
}
