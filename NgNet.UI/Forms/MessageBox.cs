using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class MessageBox : CommDialog<DialogResult>
    {
        #region private fields
        private MessageBoxF _mbf;
        internal const string DEFAULT_TITLE = "温馨提示";
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 设置一个值，该值指示对话框自动退出时间，0值表示不自动退出
        /// 单一的指示自动关闭时间而不指定默认返回值将不启动自动关闭
        /// </summary>
        public uint Exittime
        {
            set
            {
                this._mbf.Exittime = value;
            }
        }

        /// <summary>
        /// 设置一个值，该值指示默认的返回结果，若值和指定的MessageBoxButton不匹配将引发异常
        /// </summary>
        public DialogResult DefaultResult
        {
            set { _mbf.defaultResult = value; }
        }

        /// <summary>
        /// 设置一个值，该值指示对话框的按钮类型
        /// </summary>
        public MessageBoxButtons MessageBoxButton
        {
            set
            {
                this._mbf.messageBoxButton = value;
            }
        }

        /// <summary>
        /// 设置对话框的消息
        /// </summary>
        public string Message
        {
            set
            {
                this._mbf.msg = value;
            }
        }
        #endregion

        #region constructor destructor 
        public MessageBox()
        {
            _mbf = new MessageBoxF();
            _DialogWindow = _mbf;
        }
        #endregion

        #region public  methods
        /// <summary>
        /// 显示MessageBox
        /// </summary>
        /// <returns></returns>
        public override DialogResult Show(IWin32Window owner = null)
        {
            _mbf.ShowInTaskbar = owner == null;
            _mbf.ShowDialog(owner);
            return DialogResult;
        }
        #endregion

        #region static method
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <returns></returns>
        public static DialogResult Show(string msg)
        {
            return Show(null as IWin32Window, msg);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <returns></returns>
        public static DialogResult Show(string msg, string title)
        {
            return Show(null, msg, title);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <returns></returns>
        public static DialogResult Show(string msg, string title, MessageBoxButtons messageBoxButton)
        {
            return Show(null, msg, title, messageBoxButton);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="defaultResult">对话框默认返回值</param>
        /// <returns></returns>
        public static DialogResult Show(string msg, string title, MessageBoxButtons messageBoxButton, DialogResult defaultResult)
        {
            return Show(null, msg, title, messageBoxButton, defaultResult);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="defaultResult">对话框默认返回值</param>
        /// <param name="exittime">对话框自动退出时间</param>
        /// <returns></returns>
        public static DialogResult Show(string msg, string title, MessageBoxButtons messageBoxButton, DialogResult defaultResult, uint exitTime)
        {
            return Show(null, msg, title, messageBoxButton, defaultResult, exitTime);
        }
        /// <summary>
        /// 弹出对话框显示信息，用于用户对出选择
        /// </summary>
        /// <param name="msg">显示的信息</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="closetime">自动关闭的时间</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(string msg, MessageBoxButtons messageBoxButton)
        {
            return Show(null as IWin32Window, msg, messageBoxButton);
        }
        /// <summary>
        /// MessageBox对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="msg">msg</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string msg)
        {
            MessageBox mb = new MessageBox();
            mb.Message = msg;
            Comm.ApplyTheme(mb, owner);
            return mb.Show(owner);
        }
        /// <summary>
        /// MessageBox对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="msg">msg</param>
        /// <param name="title">标题</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string msg, string title)
        {
            MessageBox mb = new MessageBox();
            mb.Message = msg;
            mb.Title = title;
            Comm.ApplyTheme(mb, owner);
            return mb.Show(owner);
        }
        /// <summary>
        /// MessageBox对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="msg">msg</param>
        /// <param name="title">标题</param>
        /// <param name="messageBoxButton">按钮</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string msg, string title, MessageBoxButtons messageBoxButton)
        {
            MessageBox mb = new MessageBox();
            mb.Message = msg;
            mb.Title = title;
            mb.MessageBoxButton = messageBoxButton;
            Comm.ApplyTheme(mb, owner);
            return mb.Show(owner);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="owner">所有者</param>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="defaultResult">对话框默认返回值</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string msg, string title, MessageBoxButtons messageBoxButton, DialogResult defaultResult)
        {
            return Show(owner, msg, title, messageBoxButton, defaultResult, 0);
        }
        /// <summary>
        /// 通用选择对话框
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="msg">提示信息</param>
        /// <param name="title">对话框标题</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="defaultResult">对话框默认返回值</param>
        /// <param name="exittime">对话框自动退出时间</param>
        /// <returns></returns>
        public static DialogResult Show(IWin32Window owner, string msg, string title, MessageBoxButtons messageBoxButton, DialogResult defaultResult, uint exitTime)
        {
            MessageBox mb = new MessageBox();
            mb.Message = msg;
            mb.Title = title;
            mb.MessageBoxButton = messageBoxButton;
            mb.DefaultResult = defaultResult;
            mb.Exittime = exitTime;
            Comm.ApplyTheme(mb, owner);
            return mb.Show(owner);
        }
        /// <summary>
        /// 弹出对话框显示信息，用于用户对出选择
        /// </summary>
        /// <param name="owner">对话框拥有者</param>
        /// <param name="msg">显示的信息</param>
        /// <param name="messageBoxButton">对话框按钮</param>
        /// <param name="closetime">自动关闭的时间</param>
        /// <returns>对话框结果</returns>
        public static DialogResult Show(IWin32Window owner, string msg, MessageBoxButtons messageBoxButton)
        {
            MessageBox mb = new MessageBox();
            mb.Message = msg;
            mb.MessageBoxButton = messageBoxButton;
            Comm.ApplyTheme(mb, owner);
            return mb.Show(owner);
        }
        #endregion
    }
}
