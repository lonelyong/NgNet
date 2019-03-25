using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public class InputBox : CommDialog<string>
    {
        #region private fields
        private InputBoxF _ipf;
        internal const string DEFAULT_TITLE = "请您输入";
        internal const string DEFAULT_NOTICE = "请在下输入框输入内容：";
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 设置对话框提示
        /// </summary>
        public string Notice
        {
            set
            {
                this._ipf.Notice = value;
            }
        }
        /// <summary>
        /// 获取或设置输入框的内容
        /// </summary>
        public string Text
        {
            set
            {
                this._ipf.InputedText = value;
            }
            get
            {
                return DialogResult == DialogResult.OK ? this._ipf.InputedText : null;
            }
        }
        /// <summary>
        /// 设置输入内容的类型
        /// </summary>
        public InputTypes InputType
        {
            set
            {
                _ipf.InputType = value;
            }
        }
        /// <summary>
        /// 设置窗口大小
        /// </summary>
        public Size Size
        {
            set { this._ipf.Size = value; }
        }
        #endregion

        #region constructor destructor
        public InputBox()
        {
            _ipf = new InputBoxF();
            _DialogWindow = _ipf;
        }
        #endregion

        #region instance method
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override string Show(IWin32Window owner = null)
        {
            _ipf.ShowInTaskbar = owner == null;
            _ipf.ShowDialog(owner);
            return Text;
        }
        #endregion

        #region static method
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="notice">对话框提醒</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(string notice)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            return ib.Show();
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(string notice, string defaultText)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            return ib.Show();
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <param name="title">对话框标题</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(string notice, string defaultText, string title)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            ib.Title = title;
            return ib.Show();
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <param name="title">对话框标题</param>
        /// <param name="inputType">文本值类型</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(string notice, string defaultText, string title, InputTypes inputType)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            ib.InputType = inputType;
            return ib.Show();
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="notice">对话框提醒</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(IWin32Window owner, string notice)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            Comm.ApplyTheme(ib, owner);
            return ib.Show(owner);
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(IWin32Window owner, string notice, string defaultText)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            Comm.ApplyTheme(ib, owner);
            return ib.Show(owner);
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <param name="title">对话框标题</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(IWin32Window owner, string notice, string defaultText, string title)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            ib.Title = title;
            Comm.ApplyTheme(ib, owner);
            return ib.Show(owner);
        }
        /// <summary>
        /// 显示文本输入对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="notice">对话框提醒</param>
        /// <param name="defaultText">对话框默认值</param>
        /// <param name="title">对话框标题</param>
        /// <param name="inputType">文本值类型</param>
        /// <returns>返回输入的文本</returns>
        public static string Show(IWin32Window owner, string notice, string defaultText, string title, InputTypes inputType)
        {
            InputBox ib = new InputBox();
            ib.Notice = notice;
            ib.Text = defaultText;
            ib.Title = title;
            ib.InputType = inputType;
            Comm.ApplyTheme(ib, owner);
            return ib.Show(owner);
        }
        #endregion
    }
}
