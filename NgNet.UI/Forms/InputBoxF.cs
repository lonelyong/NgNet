using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class InputBoxF : TitleableForm, ICommDialogWindow
    {
        #region private field

        #endregion

        #region  public properties
        /// <summary>
        /// 获取或设置用户输入的文本
        /// </summary>
        public string InputedText
        {
            set
            {
                inputTxtBox.Text = value;
            }
            get
            {
                return DialogResult == DialogResult.OK ? inputTxtBox.Text : null;
            }
        }
        /// <summary>
        /// 设置对话框提示信息
        /// </summary>
        public string Notice
        {
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? InputBox.DEFAULT_NOTICE : value;
                notiLabel.Text = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示要求用户输入的文本类型
        /// </summary>
        public InputTypes InputType { get; set; }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                splitContainer1.BackColor = value;
                inputTxtBox.BackColor = value;
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

                btn_ok.ForeColor = value;
                btn_cancel.ForeColor = value;
                inputTxtBox.ForeColor = value;
                notiLabel.ForeColor = value;
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
                value = string.IsNullOrWhiteSpace(value) ? InputBox.DEFAULT_TITLE : value;
                base.Text = value;
            }
        }
        #endregion

        #region constructor destructor
        public InputBoxF()
        {
            InitializeComponent();
            DialogResult = DialogResult.None;
            Resizeable = true;
            TitleBar.Style = TitleBarStyles.None;
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            InputedText = string.Empty;
            Notice = InputBox.DEFAULT_NOTICE;
            Text = InputBox.DEFAULT_TITLE;
            //字段初始化
            InputType = InputTypes.Text;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region size change   
        //控件大小调整
        private void splitContainer1_Panel1_SizeChanged(object sender, EventArgs e)
        {
            this.btn_ok.Left = splitContainer1.Panel1.Width - splitContainer1.Panel2.Padding.Right - btn_ok.Width;
            this.btn_ok.Top = notiLabel.Top;
            this.btn_cancel.Left = btn_ok.Left;
            this.btn_cancel.Top = btn_ok.Bottom - 1;
            this.notiLabel.Size = new Size(btn_ok.Left - notiLabel.Left, splitContainer1.Panel1.Height - 10);
        }
        #endregion

        #region this
        private void InputBoxF_Load(object sender, EventArgs e)
        {
            //移动窗体
            FormHelper.Move(splitContainer1);
            FormHelper.Move(notiLabel);

            Comm.ApplyComm(this);
        }

        private void InputBoxF_Shown(object sender, EventArgs e)
        {
            this.Activate();
            //输入框获得焦点
            this.inputTxtBox.SelectionStart = 0;
            this.inputTxtBox.SelectionLength = this.inputTxtBox.TextLength;
            this.inputTxtBox.Focus();
        }
        #endregion

        #region input content type test
        /// <summary>
        /// 检测字符串是否是整数
        /// </summary>
        /// <param name="numstr"></param>
        /// <returns></returns>
        private bool IsInt(string numstr)
        {
            if (string.IsNullOrWhiteSpace(numstr))
            {
                return false;
            }
            int tmp;
            return Int32.TryParse(numstr, out tmp);
        }

        /// <summary>
        /// 检测字符串数字是否是正数
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private bool IsU(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return false;
            }
            double tmp;
            if (double.TryParse(number.Replace(" ", ""), out tmp))
            {
                return tmp >= 0;
            }
            else
            {
                return false;
            }
        }

        private void tBox_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) { return; }
            switch (this.InputType)
            {
                case InputTypes.Text:
                    break;
                case InputTypes.Int:
                    if (e.KeyChar.Equals('-'))
                    {
                        if (this.inputTxtBox.Text.Trim().IndexOf('-') == -1
                            && string.IsNullOrWhiteSpace(this.inputTxtBox.Text.Substring(0, this.inputTxtBox.SelectionStart)))
                        {
                            return;
                        }
                    }

                    if (!Char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case InputTypes.UInt:
                    if (!Char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case InputTypes.Number:
                    if (e.KeyChar.Equals('-'))
                    {
                        if (this.inputTxtBox.Text.Trim().IndexOf('-') == -1
                            && string.IsNullOrWhiteSpace(this.inputTxtBox.Text.Substring(0, this.inputTxtBox.SelectionStart)))
                        {
                            return;
                        }
                    }
                    if (e.KeyChar.Equals('.'))
                    {
                        if (this.inputTxtBox.Text.IndexOf('.') != -1
                            || string.IsNullOrWhiteSpace(this.inputTxtBox.Text.Substring(0, this.inputTxtBox.SelectionStart)))
                        {
                            return;
                        }
                    }
                    if (!Char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case InputTypes.UNumber:
                    if (!Char.IsNumber(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                    break;
                case InputTypes.English:
                    break;
                case InputTypes.NumberEnglish:
                    break;
                case InputTypes.Chinese:
                    break;
                case InputTypes.DateTime:
                    break;
                case InputTypes.NoSpace:
                    break;
                case InputTypes.Password:
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region button <ok & cancel>
        private void btn_ok_Click(object sender, EventArgs e)
        {
            string intext = this.inputTxtBox.Text;

            bool ok = true;

            #region 判断内容是否符合要求
            switch (this.InputType)
            {
                case InputTypes.Text:
                    break;
                case InputTypes.Int:
                    intext = intext.Replace(" ", "");
                    ok = this.IsInt(intext);
                    break;
                case InputTypes.UInt:
                    intext = intext.Replace(" ", "");
                    ok = this.IsInt(intext) && this.IsU(intext);
                    break;
                case InputTypes.Number:
                    intext = intext.Replace(" ", "");
                    ok = Math.Test.IsDouble(intext);
                    break;
                case InputTypes.English:

                    break;
                case InputTypes.Chinese:

                    break;
                case InputTypes.DateTime:

                    break;
                default:
                    break;
            }
            #endregion

            if (ok)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }

            #region 不合法提示信息
            string inf = "输入的内容为不合法的{0}，是否继续？";
            string typ = string.Empty;
            switch (this.InputType)
            {
                case InputTypes.Text:

                    break;
                case InputTypes.Int:
                    typ = "整数";
                    break;
                case InputTypes.UInt:
                    typ = "正整数";
                    break;
                case InputTypes.Number:
                    typ = "数字";
                    break;
                case InputTypes.UNumber:
                    typ = "正数";
                    break;
                case InputTypes.English:
                    break;
                case InputTypes.NumberEnglish:
                    break;
                case InputTypes.Chinese:
                    break;
                case InputTypes.DateTime:
                    break;
                case InputTypes.NoSpace:
                    break;
                case InputTypes.Password:
                    break;
                default:
                    break;
            }
            inf = string.Format(inf, typ);
            if (MessageBox.Show(this, inf, "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
                return;
            }
            else
            {
                this.inputTxtBox.Focus();
            }
            #endregion
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.InputedText = string.Empty;
            this.DialogResult = DialogResult.Cancel;

            this.Close();
        }
        #endregion
    }
}
