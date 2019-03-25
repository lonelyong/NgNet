using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class ExitBoxF : TitleableForm, ICommDialogWindow 
    {
        #region private fields
        private ExitStyles _exitStyle = ExitStyles.MinsizeChoose;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置对话框样式
        /// </summary>
        public ExitStyles ExitStyle
        {
            get { return _exitStyle; }
            set 
            { 
                _exitStyle = value;
                switch (value)
                {
                    case ExitStyles.ExitDirectly:
                    case ExitStyles.ExitChoose:
                        ckBox_exit.Checked = true;
                        break;
                    case ExitStyles.MinsizeDirectly:
                    case ExitStyles.MinsizeChoose:
                    case ExitStyles.None :
                        ckBox_min.Checked = true;
                        break;
                    default:
                        throw new ArgumentException("ExitStyle类型错误");
                }
                ckBox_showNext.Checked = ExitStyle == ExitStyles.ExitDirectly || ExitStyle == ExitStyles.MinsizeDirectly;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示对话框的工具栏背景色
        /// </summary>
        public Color BarColor
        {
            set 
            {
                value = value == Color.Empty || value == Color.Transparent ? Color.LightCyan : value;
                barPanel.BackColor = value;
            }
            get 
            {
                return barPanel.BackColor;
            }
        }
        /// <summary>
        /// 设置对话框图标
        /// </summary>
        public Image Logo
        {
            set
            {
                if (value == null)
                {
                    pictureBox_logo.Image = SystemIcons.Application.ToBitmap();
                }
                else
                {
                    pictureBox_logo.Image = value;
                }
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
                value = string.IsNullOrWhiteSpace(value) ? string.Format("退出 {0}", Applications.Current.AssemblyTitle) : value;
                base.Text = value;
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
                ckBox_exit.ForeColor = value;
                ckBox_min.ForeColor = value;
                ckBox_showNext.ForeColor = value;
                button_cancel.ForeColor = value;
                button_ok.ForeColor = value;
            }
        }
        #endregion

        #region constructor destructor
        public ExitBoxF()
        {
            InitializeComponent();
            DialogResult = DialogResult.None;
            TitleBar.Style = TitleBarStyles.EndOnly;
           
            Text = string.Format("退出 {0}", Application.ProductName);
            ExitStyle = ExitStyles.MinsizeChoose;
            Logo = SystemIcons.Application.ToBitmap();
            BarColor = BarColor;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region  checkbox< set exit style>
        private void checkBoxs_CheckedChanged(object sender, EventArgs e)
        {
            if(sender == this.ckBox_exit)
            {
                ckBox_min.Checked = !ckBox_exit.Checked;
            }
            else if(sender == this.ckBox_min)
            {
                ckBox_exit.Checked = !ckBox_min.Checked;                
            }
            else if (sender == this.ckBox_showNext)
            {

            }
            this.ExitStyle = this.ckBox_exit.Checked
                ? this.ckBox_showNext.Checked ? ExitStyles.ExitDirectly : ExitStyles.ExitChoose
                : this.ckBox_showNext.Checked ? ExitStyles.MinsizeDirectly : ExitStyles.MinsizeChoose;
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            FormHelper.Move(barPanel);
            FormHelper.Move(pictureBox_logo);
            Comm.ApplyComm(this);      
        }

        private void this_Shown(object sender, EventArgs e)
        {
            this.Activate();
        }
        #endregion

        #region button <ok - cancel>
        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion
    }
}
