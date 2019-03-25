

namespace NgNet.UI.Forms
{
    public class TextBoxHelper
    {

        #region private fileds
        private System.Windows.Forms.TextBox _textBox;

        // 提示信息
        private string _clue;
        // 提示文本颜色
        private System.Drawing.Color _clueForeColor;
        // 提示文本字体
        private System.Drawing.Font _clueFont;
        private System.Drawing.Color _normalForeColor;
        private System.Drawing.Font _normalFont;
        private char _pwdChar;
        #endregion

        #region constructor
        public TextBoxHelper(System.Windows.Forms.TextBox textBox)
        {
            if (textBox == null)
                throw new System.ArgumentNullException("textBox不能为null");
            _textBox = textBox;
            _normalForeColor = textBox.ForeColor;
            _normalFont = textBox.Font;
            _pwdChar = textBox.PasswordChar;
        }
        #endregion

        #region private methods
        private void setClueStyle_Enter(object sender, System.EventArgs e)
        {
             if(_textBox.Text == _clue)
            {
                _textBox.PasswordChar = _pwdChar;
                _textBox.Clear();
                _textBox.ForeColor = _normalForeColor;
                _textBox.Font = _normalFont;
            }   
        }

        private void setClueStyle_Leave(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(_textBox.Text))
            {
                _normalFont = _textBox.Font;
                _normalForeColor = _textBox.ForeColor;
                _pwdChar = _textBox.PasswordChar;
                _textBox.PasswordChar = (char)0;
                _textBox.Text = _clue;
                _textBox.ForeColor = _clueForeColor;
                _textBox.Font = _clueFont;
            }
        }
        #endregion

        #region public  methods
        /// <summary>
        /// 设置textbox的输入提示信息
        /// </summary>
        /// <param name="textBox"></param>
        /// <param name="clue"></param>
        public void SetClueStyle(string clue, System.Drawing.Color clueColor, System.Drawing.Font clueFont)
        {
            // 重新设置样式时，更新文本框内内容
            if (_textBox.Text == _clue)
                _textBox.Text = clue;
            _clue = clue;
            _clueForeColor = clueColor;
            _clueFont = clueFont;
          
            _textBox.Enter -= new System.EventHandler(setClueStyle_Enter);
            _textBox.Leave -= new System.EventHandler(setClueStyle_Leave);
            _textBox.Enter += new System.EventHandler(setClueStyle_Enter);
            _textBox.Leave += new System.EventHandler(setClueStyle_Leave);
                 
            if (_textBox.Focused)
            {
                setClueStyle_Leave(_textBox, null);
                setClueStyle_Enter(_textBox, null);
            }
            else
            {
                setClueStyle_Enter(_textBox, null);
                setClueStyle_Leave(_textBox, null);
            }
               

        }
        #endregion
    }
}
