using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class StartBoxF : ThemeableForm, ICommDialogWindow
    {
        #region private fields

        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置一个值，该值为APP LOGO
        /// </summary>
        public Image Logo
        {
            set
            {
                ContentPanel.BackgroundImage = value;
            }
        }
        /// <summary>
        /// 获取或设置对话框信息
        /// </summary>
        public string Info
        {
            set
            {
                this.infLabel.Text = value;
            }
        }
        /// <summary>
        /// 获取或设置及时消息
        /// </summary>
        public string msg
        {
            set
            {
                this.msgLabel.Text = value;
            }
        }
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                infLabel.ForeColor = value;
                msgLabel.ForeColor = value;
            }
        }
        #endregion

        #region constructor destructor
        public StartBoxF()
        {
            InitializeComponent();
            //初始化
            Blendable = true;

            MaximizedBounds = this.ClientRectangle;
            Text = string.Format("启动中.. - {0}", Applications.Current.AssemblyTitle);
            Info = string.Format("{0}\r\n启动中。。", Applications.Current.AssemblyTitle);
            msg = "请稍候...";
            base.Icon = SystemIcons.Application;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region public methods
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? string.Format("启动中.. - {0}", Applications.Current.AssemblyTitle) : value;
                base.Text = value;
            }
        }

        new public void Close()
        {
            if (this.Blendable)
            {
                FormHelper.OpacityDown(0.05f, true, 0f, 10);
            }
            else
            {
                Forms.FormHelper.CloseWindow(this.Handle);
            }
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            this.Activate();
        }
        #endregion
    }


}
