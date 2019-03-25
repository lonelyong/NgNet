using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class MessageBoxF : TitleableForm, ICommDialogWindow
    {
        #region private fields
        private DialogResult _defaultResult = DialogResult.None;
        private MessageBoxButtons _messageBoxButton = MessageBoxButtons.OK;
        // 表示对话框的acceptbutton;
        private Button _acceptButton = new Button();
        #endregion

        #region public properties
        /// <summary>
        /// 自动关闭时间,值为0表示不自动关闭
        /// 单一的指示自动关闭时间而不指定默认返回值将不启动自动关闭
        /// </summary>
        public uint Exittime { get; set; }
        /// <summary>
        /// 默认的返回结果，若值和指定的MessageBoxButton不匹配将引发运行异常
        /// </summary>
        public DialogResult defaultResult
        {
            set
            {
                this._defaultResult = value;
                this.setAcceptButton(value, this.messageBoxButton);
            }
            get
            {
                return this._defaultResult;
            }
        }
        /// <summary>
        /// 对话框按钮
        /// </summary>
        public MessageBoxButtons messageBoxButton
        {
            set
            {
                this.button1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
                switch (value)
                {
                    case MessageBoxButtons.AbortRetryIgnore:
                        this.button3.Text = "终止(&S)";
                        this.button2.Text = "重试(&R)";
                        this.button1.Text = "忽视(&I)";
                        break;
                    case MessageBoxButtons.OK:
                        this.button3.Visible = false;
                        this.button2.Visible = false;
                        this.button1.Text = "确定(&O)";
                        break;
                    case MessageBoxButtons.OKCancel:
                        this.button3.Visible = false;
                        this.button2.Text = "确定(&O)";
                        this.button1.Text = "取消(&C)";
                        break;
                    case MessageBoxButtons.RetryCancel:
                        this.button3.Visible = false;
                        this.button2.Text = "重试(&R)";
                        this.button1.Text = "取消(&C)";
                        break;
                    case MessageBoxButtons.YesNo:
                        this.button3.Visible = false;
                        this.button2.Text = "是(&Y)";
                        this.button1.Text = "否(&N)";
                        break;
                    case MessageBoxButtons.YesNoCancel:
                        this.button3.Text = "是(&Y)";
                        this.button2.Text = "否(&N)";
                        this.button1.Text = "取消(&C)";
                        break;
                    default:
                        this.button3.Text = "是(&Y)";
                        this.button2.Text = "否(&N)";
                        this.button1.Text = "取消(&C)";
                        break;
                }
                this.button1.Tag = this.button1.Text;
                this.button2.Tag = this.button2.Text;
                this.button3.Tag = this.button3.Text;
                //
                this._messageBoxButton = value;
                this.setAcceptButton(this.defaultResult, value);
            }
            get
            {
                return this._messageBoxButton;
            }
        }
        /// <summary>
        /// 设置要显示的message
        /// </summary>
        public string msg
        {
            set
            {
                value = value == null ? null : value.TrimEnd();
                this.msgRichTextBox.Text = value;
                SizeF sizef = this.msgRichTextBox.CreateGraphics().MeasureString("\r\n " + value + "\r\n ", this.msgRichTextBox.Font);
                float h, w;
                if (sizef.Height < msgRichTextBox.Height)
                    h = msgRichTextBox.Height;
                else
                    if (sizef.Height > Screen.PrimaryScreen.WorkingArea.Height)
                    h = Screen.PrimaryScreen.WorkingArea.Height - (this.Height - msgRichTextBox.Height);
                else
                    h = sizef.Height;
                if (sizef.Width < this.msgRichTextBox.Width)
                    w = msgRichTextBox.Width;
                else
                    if (sizef.Width > Screen.PrimaryScreen.WorkingArea.Width)
                    w = Screen.PrimaryScreen.WorkingArea.Width - (this.Width - this.msgRichTextBox.Width);
                else
                    w = sizef.Width;
                this.Size = new Size((int)(w + (this.Width - this.msgRichTextBox.Width)), (int)(h + (this.Height - this.msgRichTextBox.Height)));
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? MessageBox.DEFAULT_TITLE : value;
                base.Text = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                msgRichTextBox.BackColor = value;
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
                msgRichTextBox.ForeColor = value;
                button1.ForeColor = value;
                button2.ForeColor = value;
                button3.ForeColor = value;
            }
        }
        #endregion

        #region constructor destructor 
        public MessageBoxF()
        {
            InitializeComponent();
            //初始化
            DialogResult = DialogResult.None;
            Resizeable = true;
            TitleBar.Style = TitleBarStyles.None;
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            msg = null;
            messageBoxButton = MessageBoxButtons.OK;
            defaultResult = DialogResult.OK;
            Text = MessageBox.DEFAULT_TITLE;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region acceptButon & defaultResult
        private bool defaultResultTest()
        {
            bool tmp = true;
            switch (this.messageBoxButton)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    tmp = this.defaultResult == DialogResult.Abort || this.defaultResult == DialogResult.Retry || this.defaultResult == DialogResult.Ignore || DialogResult == DialogResult.None;
                    break;
                case MessageBoxButtons.OK:
                    tmp = this.defaultResult == DialogResult.OK || DialogResult == DialogResult.None;
                    break;
                case MessageBoxButtons.OKCancel:
                    tmp = this.defaultResult == DialogResult.OK || this.defaultResult == DialogResult.Cancel || DialogResult == DialogResult.None;
                    break;
                case MessageBoxButtons.RetryCancel:
                    tmp = this.defaultResult == DialogResult.Retry || this.defaultResult == DialogResult.Cancel || DialogResult == DialogResult.None;
                    break;
                case MessageBoxButtons.YesNo:
                    tmp = this.defaultResult == DialogResult.Yes || this.defaultResult == DialogResult.No || DialogResult == DialogResult.None;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    tmp = this.defaultResult == DialogResult.Yes || this.defaultResult == DialogResult.No || this.defaultResult == DialogResult.Cancel || DialogResult == DialogResult.None;
                    break;
                default:
                    tmp = false;
                    break;
            }
            if (tmp == false) { throw new Exception("运行错误 <deaultResult与指定的messageBoxButton不匹配>"); }

            return tmp;
        }

        private void setAcceptButton(DialogResult dr, MessageBoxButtons mbb)
        {
            this._acceptButton = new Button();
            switch (mbb)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    if (dr == DialogResult.Ignore || dr == DialogResult.None)
                    {
                        _acceptButton = button1;
                    }
                    else if (dr == DialogResult.Retry)
                    {
                        _acceptButton = button2;
                    }
                    else if (dr == DialogResult.Abort)
                    {
                        _acceptButton = button3;
                    }
                    break;
                case MessageBoxButtons.OK:
                    _acceptButton = button1;
                    break;
                case MessageBoxButtons.OKCancel:
                    if (dr == DialogResult.Cancel || dr == DialogResult.None)
                    {
                        _acceptButton = button1;
                    }
                    else if (dr == DialogResult.OK)
                    {
                        _acceptButton = button2;
                    }
                    break;
                case MessageBoxButtons.RetryCancel:
                    if (dr == DialogResult.Cancel || dr == DialogResult.None)
                    {
                        _acceptButton = button1;
                    }
                    else if (dr == DialogResult.Retry)
                    {
                        _acceptButton = button2;
                    }
                    break;
                case MessageBoxButtons.YesNo:
                    if (dr == DialogResult.No || dr == DialogResult.None)
                    {
                        _acceptButton = button1;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        _acceptButton = button2;
                    }
                    break;
                case MessageBoxButtons.YesNoCancel:
                    if (dr == DialogResult.Cancel || dr == DialogResult.None)
                    {
                        _acceptButton = button1;
                    }
                    else if (dr == DialogResult.No || dr == DialogResult.None)
                    {
                        _acceptButton = button2;
                    }
                    else if (dr == DialogResult.Yes)
                    {
                        _acceptButton = button3;
                    }
                    break;
                default:
                    _acceptButton = button1;
                    break;
            }
            this.AcceptButton = _acceptButton;
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            defaultResultTest();
            //无所有者时居中
            Comm.ApplyComm(this);

            msgRichTextBox.LinkClicked += new LinkClickedEventHandler(msgRichTextBox_LinkClick);
        }

        private void this_Shown(object sender, EventArgs e)
        {
            //当自动退出时间不为0 时打开计时器
            this.exitTimer.Enabled = this.Exittime != 0 && this.defaultResult != DialogResult.None;
            //消息栏获取交点
            this.msgRichTextBox.SelectionStart = this.msgRichTextBox.TextLength;
            this.Activate();
            this.msgRichTextBox.Focus();
        }

        private void panel_SizeChanged(object sender, EventArgs e)
        {
            this.msgRichTextBox.Location = new Point(Comm.DISTANCE_LEFT, Comm.DISTANCE_TOP);
            this.msgRichTextBox.Width = ContentPanel.Width - Comm.DISTANCE_LEFT * 2;

            this.button1.Top = ContentPanel.Height - button1.Height - Comm.DISTANCE_DOWN;
            this.button2.Top = button1.Top;
            this.button3.Top = button1.Top;
            this.button1.Left = msgRichTextBox.Right - button1.Width;
            this.button2.Left = button1.Left - button2.Width + 1;
            this.button3.Left = button2.Left - button3.Width + 1;

            this.msgRichTextBox.Height = button1.Top - msgRichTextBox.Top - Comm.DISTANCE_INTERVAL_V;
        }
        #endregion

        #region msgRichTextBox
        private void msgRichTextBox_LinkClick(object sender, LinkClickedEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(e.LinkText);
            }
            catch
            {

            }           
        }
        #endregion

        #region button<dialogResult> & autoclose

        private void button3_Click(object sender, EventArgs e)
        {
            switch (this.messageBoxButton)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    this.DialogResult = DialogResult.Abort;
                    break;
                case MessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;
                case MessageBoxButtons.OKCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.RetryCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.YesNo:
                    this.DialogResult = DialogResult.No;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.DialogResult = DialogResult.Yes;
                    break;
                default:
                    this.DialogResult = DialogResult.Yes;
                    break;
            }
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            switch (this.messageBoxButton)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    this.DialogResult = DialogResult.Retry;
                    break;
                case MessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;
                case MessageBoxButtons.OKCancel:
                    this.DialogResult = DialogResult.OK;
                    break;
                case MessageBoxButtons.RetryCancel:
                    this.DialogResult = DialogResult.Retry;
                    break;
                case MessageBoxButtons.YesNo:
                    this.DialogResult = DialogResult.Yes;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.DialogResult = DialogResult.No;
                    break;
                default:
                    this.DialogResult = DialogResult.No;
                    break;
            }
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (this.messageBoxButton)
            {
                case MessageBoxButtons.AbortRetryIgnore:
                    this.DialogResult = DialogResult.Ignore;
                    break;
                case MessageBoxButtons.OK:
                    this.DialogResult = DialogResult.OK;
                    break;
                case MessageBoxButtons.OKCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.RetryCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                case MessageBoxButtons.YesNo:
                    this.DialogResult = DialogResult.No;
                    break;
                case MessageBoxButtons.YesNoCancel:
                    this.DialogResult = DialogResult.Cancel;
                    break;
                default:
                    this.DialogResult = DialogResult.Cancel;
                    break;
            }
            this.Close();
        }

        private void exitTimer_Tick(object sender, EventArgs e)
        {
            this._acceptButton.Text = string.Format("{0}<{1}>", _acceptButton.Tag, this.Exittime);
            if (this.Exittime <= 0)
            {
                this.DialogResult = this.defaultResult;
                this.Close();
            }
            else
            {
                Exittime--;
            }
        }
        #endregion
    }
}
