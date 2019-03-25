using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// 显示指定文本或文件的Box
    /// </summary>
    public class TextBox : CommBox
    {
        #region private fields
        private TextBoxF _tbf;
        #endregion

        #region protected properties
        /// <summary>
        /// TextBox窗体
        /// </summary>
        protected override ICommBoxWindow _CommBoxWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置TextPad文本
        /// </summary>
        public string CurrentText
        {
            set
            {
                this._tbf.CurrentText = value;
            }
            get
            {
                return this._tbf.CurrentText;
            }
        }

        /// <summary>
        /// 设置窗体的永久标题，如果不使用永久性标题，请将其设置为空值即可
        /// </summary>
        public string FixedTitle
        {
            set { _tbf.FixedTitle = value; }
        }

        /// <summary>
        /// 获取或设置Box图标
        /// </summary>
        public Icon Icon
        {
            get { return _tbf.Icon; }
            set { _tbf.Icon = value; }
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable
        {
            set
            {
                _tbf.Editable = value;
            }
            get
            {
                return _tbf.Editable;
            }
        }
        /// <summary>
        /// 自定义的右键菜单
        /// </summary>
        public ToolStripMenuItem[] UserMenus
        {
            set
            {
                _tbf.UserMenus = value;
            }
            get
            {
                return _tbf.UserMenus;
            }
        }
        #endregion

        #region constructor destructor
        /// <summary>
        /// 默认的参数初始化实例
        /// </summary>
        public TextBox()
        {
            _tbf = new TextBoxF();
            _CommBoxWindow = _tbf;
        }
        #endregion

        #region public instance methods
        /// <summary>
        /// 显示TextBox
        /// </summary>
        /// <param name="owner"></param>
        public override void Show(IWin32Window owner = null)
        {
            if (IsLoaded)
                Appear();
            else if (owner != null)
                _tbf.Show(owner);//tbf.Show(null)会显示在他前一个打开窗口的上面
            else
                _tbf.Show();
        }
        /// <summary>
        /// 以模式对话框显示窗体
        /// </summary>
        /// <param name="owner"></param>
        public override void ShowDialog(IWin32Window owner = null)
        {
            if (IsLoaded)
                Appear();
            else if (owner != null)
                _tbf.ShowDialog(owner);//tbf.Show(null)会显示在他前一个打开窗口的上面
            else
                _tbf.ShowDialog();
        }
        #endregion
    }
}