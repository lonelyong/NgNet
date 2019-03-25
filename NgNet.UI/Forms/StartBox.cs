using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NgNet.UI.Forms
{
    public class StartBox : CommDialog<DialogResult>
    {
        #region private fields
        private StartBoxF _sbf;
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 设置或获取对话框ICON
        /// </summary>
        public Icon Icon
        {
            set
            {
                _sbf.Icon = value;
            }
            get
            {
                return _sbf.Icon;
            }
        }

        /// <summary>
        /// 设置AppLogo
        /// </summary>
        public Image Logo
        {
            set
            {
                _sbf.Logo = value;
            }
        }

        /// <summary>
        /// 设置对话框信息
        /// </summary>
        public string Text
        {
            set
            {
                this._sbf.Info = value;
            }
        }

        /// <summary>
        /// 通过StartBox.Close()关闭对话框为DialogResult.OK,否则为DialogResult.None
        /// </summary>
        public override DialogResult DialogResult
        {
            get
            {
                return base.DialogResult;
            }
        }
        #endregion

        #region constructor destructor
        public StartBox()
        {
            _sbf = new StartBoxF();
            _DialogWindow = _sbf;
        }
        #endregion

        #region instance method
        /// <summary>
        /// 是否显示为对话框，一般用于在新线程上显示窗体
        /// </summary>
        /// <param name="dialog"></param>
        public override DialogResult Show(IWin32Window owner = null)
        {
            return _sbf.ShowDialog(owner);
        }
        /// <summary>
        /// 关闭启动窗口
        /// </summary>
        /// <param name="blend">是否启用淡入淡出效果</param>
        public void Close()
        {
            _sbf.DialogResult = DialogResult.OK;
            if (_sbf.InvokeRequired)
                _sbf.BeginInvoke(new Action(()=> { _sbf.Close(); }));
            else
                _sbf.Close();
        }
        /// <summary>
        /// 向启动窗口发送状态消息
        /// </summary>
        /// <param name="msg">当前加载状态</param>
        public void SendMessage(string msg)
        {
            if (_sbf.InvokeRequired)
                _sbf.Invoke(new Action(() => { _sbf.msg = msg; }));
            else
                _sbf.msg = msg;

        }
        #endregion
    }
}
