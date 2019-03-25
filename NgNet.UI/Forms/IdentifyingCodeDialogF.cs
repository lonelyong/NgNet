using System;
using System.Drawing;

namespace NgNet.UI.Forms
{
    partial class IdentifyingCodeDialogF : TitleableForm, ICommDialogWindow
    {
        #region private fields
        private NgNet.Drawing.IdentifyingCodeGenerator _icg = new Drawing.IdentifyingCodeGenerator();

        private string _currentCode;
        #endregion

        #region public properties
        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                codeTextBox.BackColor = Drawing.ColorHelper.GetSimilarColor(value, false, Level.Level8);
                ntiLabel.ForeColor = Drawing.ColorHelper.GetOppositeColor(value);
                codeTextBox.ForeColor = Drawing.ColorHelper.GetOppositeColor(codeTextBox.BackColor);
            }
        }

        public override Color BorderColor
        {
            get
            {
                return base.BorderColor;
            }

            set
            {
                base.BorderColor = value;
                boxPanel.BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(value, false, Level.Level6);
            }
        }
        #endregion

        #region constructor
        public IdentifyingCodeDialogF()
        {
            InitializeComponent();
            TitleBar.Style = TitleBarStyles.EndOnly;
            TitleBar.IconVisible = false;
            FormHelper.AutoBorder(new BorderSize(), new BorderSize(1));
            TitleBackColorUseBackColor = true;
            Text = "输入验证码";
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region private methods
        private void IdentifyingCodeDialogF_Load(object sender, EventArgs e)
        {
            Comm.ApplyComm(this);
            _icg.Length = 6;
            _icg.LengthChanged += lengthChanged;
            codeTextBox.MaxLength = _icg.Length;
            changeCode();
        }

        private void codeTextBox_TextChanged(object sender, EventArgs e)
        {
            if(string.Compare(_currentCode, codeTextBox.Text, true) == 0)
            {
                ntiLabel.Text = null;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                Close();
            }
            else if (string.IsNullOrEmpty(codeTextBox.Text))
            {
                ntiLabel.Text = null;
            }
            else if(!_currentCode.ToLower().Contains(codeTextBox.Text.ToLower()))
            {
                ntiLabel.Text = "验证码错误";
            }
            else
            {
                ntiLabel.Text = null;
            }
        }

        private void changeLinkLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
        {
            changeCode();
        }

        private void lengthChanged(object sender, EventArgs e)
        {
            codeTextBox.MaxLength = _icg.Length;
        }

        private void changeCode()
        {
            _currentCode = _icg.CreateCode();
            Bitmap _bitmap = _icg.CreateCodePicture(_currentCode);
            pictureBox1.BackgroundImage = _bitmap;
            codeTextBox.Focus();
            codeTextBox.SelectAll();
        }
        #endregion
    }
}
