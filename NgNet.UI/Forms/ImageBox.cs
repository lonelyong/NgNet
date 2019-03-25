using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// 图片查看器
    /// </summary>
    public class ImageBox : CommBox
    {
        #region private fields
        private ImageBoxF _ibf;
        #endregion

        #region protected properties
        /// <summary>
        /// ImageBox窗体
        /// </summary>
        protected override ICommBoxWindow _CommBoxWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置一个值改值指示当窗体失去焦点时是否自动关闭
        /// </summary>
        public bool AutoClose {

            get
            { return _ibf.AutoClose; }
            set
            { _ibf.AutoClose = value; }
        }

        /// <summary>
        /// 获取或设置TextPad文本
        /// </summary>
        public Image CurrentImage
        {
            set
            {
                _ibf.CurrentImage = value;
            }
            get
            {
                return _ibf.CurrentImage;
            }
        }

        /// <summary>
        /// 设置窗体的永久标题，如果不使用永久性标题，请将其设置为空值即可
        /// </summary>
        public string FixedTitle
        {
            set { _ibf.FixedTitle = value; }
        }

        /// <summary>
        /// 获取或设置Box图标
        /// </summary>
        public Icon Icon
        {
            get { return _ibf.Icon; }
            set { _ibf.Icon = value; }
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable
        {
            set
            {
                _ibf.Editable = value;
            }
            get
            {
                return _ibf.Editable;
            }
        }
        #endregion

        #region constructor destructor
        /// <summary>
        /// 默认的参数初始化实例
        /// </summary>
        public ImageBox()
        {
            _ibf = new ImageBoxF();
            _CommBoxWindow = _ibf;
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
                _ibf.Show(owner);//tbf.Show(null)会显示在他前一个打开窗口的上面
            else
                _ibf.Show();
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
                _ibf.ShowDialog(owner);//tbf.Show(null)会显示在他前一个打开窗口的上面
            else
                _ibf.ShowDialog();
        }
        #endregion
    }
}
