using System;
using System.Windows.Forms;
using System.Drawing;

namespace NgNet.UI.Forms
{
    class ReplacePanel
    {
        #region public properties
        /// <summary>
        /// 获取一个值，该值指示窗体是否已经显示
        /// </summary>
        public bool IsShown
        {
            get
            {
                return UI.Forms.FormHelper.IsShown(_f);
            }
        }

        /// <summary>
        /// 窗体边框颜色
        /// </summary>
        public Color BorderColor
        {
            set
            {
                value = value == Color.Empty ? Color.LightSeaGreen : value;
                if (this.IsShown)
                {
                    this._f.BackColor = value;
                    this._tLabel.BackColor = value;
                }
            }
        }

        /// <summary>
        /// 背景色
        /// </summary>
        public Color BackColor
        {
            set
            {
                value = value == Color.Empty ? Color.Azure : value;
                if (this.IsShown)
                {
                    this._mPanel.BackColor = value;
                    this._tLabel.ForeColor = value;
                    this._replaceDataTextBox.BackColor = value;
                    this._searchWordsTextBox.BackColor = value;
                }
            }
        }

        /// <summary>
        /// 编辑框前景色
        /// </summary>
        public Color ForeColor
        {
            set
            {
                value = value == Color.Empty ? Color.DarkRed : value;
                if (this.IsShown)
                {
                    foreach (Control item in this._mPanel.Controls)
                    {
                        if (item != _tLabel)
                        {
                            item.ForeColor = value;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 记录当前的搜索关键字
        /// </summary>
        public string SearchWords
        {
            get
            {
                return _searchWordsTextBox.Text;
            }
        }

        /// <summary>
        /// 设置一个值，该值指示是否可以搜索
        /// </summary>
        public bool canReplace
        {
            set
            {
                _searchBtn.Enabled = value;
            }
        }
        #endregion

        #region private fields
        private Button _cancelBtn = new Button();
        private Button _replaceBtn = new Button();
        private Button _searchBtn = new Button();
        private Button _replaceAllBtn = new Button();
        private System.Windows.Forms.TextBox _replaceDataTextBox = new System.Windows.Forms.TextBox();
        private System.Windows.Forms.TextBox _searchWordsTextBox = new System.Windows.Forms.TextBox();
        private Label _tLabel = new Label();
        private Label _replaceLabel = new Label();
        private CheckBox _caseSensitiveChechBox = new CheckBox(); 
        private Panel _mPanel = new Panel();
        private Label _slabel = new Label();
        private Form _f;
        private UI.Forms.FormHelper _formHelper;
        private TextBoxF _owner;
        #endregion

        #region private properties
        private bool caseSensitive
        {
            get
            {
                return _caseSensitiveChechBox.Checked;
            }
        }
        #endregion

        #region constructor destructor 
        public ReplacePanel(TextBoxF owner)
        {
            this._owner = owner;
        }
        #endregion

        #region instance methods
        public void Show()
        {
            if (IsShown)
            {
                _f.Show();
                _f.Activate();
                return;
            }
            else
            {
                init();
                _f.Show();
            }
        }
        #endregion

        #region private methods
        private void init()
        {
            _cancelBtn = new Button();
            _replaceBtn = new Button();
            _searchBtn = new Button();
            _replaceAllBtn = new Button();
            _replaceDataTextBox = new System.Windows.Forms.TextBox();
            _searchWordsTextBox = new System.Windows.Forms.TextBox();
            _tLabel = new Label();
            _replaceLabel = new Label();
            _caseSensitiveChechBox = new CheckBox();
            _mPanel = new Panel();
            _slabel = new Label();
            _f = new Form();
            _formHelper = new UI.Forms.FormHelper(_f);
            _f.SuspendLayout();
            // 
            // cancelBtn
            // 
            _cancelBtn.BackColor = Color.Transparent;
            _cancelBtn.FlatStyle = FlatStyle.Flat;
            _cancelBtn.ForeColor = Color.DarkSlateGray;
            _cancelBtn.Location = new Point(317, 142);
            _cancelBtn.Size = new Size(75, 22);
            _cancelBtn.TabIndex = 0;
            _cancelBtn.Text = "取消";
            _cancelBtn.UseVisualStyleBackColor = false;
            _cancelBtn.Click += new System.EventHandler(btns_Click);
            // 
            // excBtn
            // 
            _replaceBtn.BackColor = Color.Transparent;
            _replaceBtn.FlatStyle = FlatStyle.Flat;
            _replaceBtn.ForeColor = Color.DarkSlateGray;
            _replaceBtn.Location = new Point(317, 68);
            _replaceBtn.Size = new Size(75, 22);
            _replaceBtn.TabIndex = 1;
            _replaceBtn.Text = "替换";
            _replaceBtn.UseVisualStyleBackColor = false;
            _replaceBtn.Enabled = false;
            _replaceBtn.Click += new System.EventHandler(btns_Click);
            // 
            // btn_s
            // 
            _searchBtn.BackColor = Color.Transparent;
            _searchBtn.FlatStyle = FlatStyle.Flat;
            _searchBtn.ForeColor = Color.DarkSlateGray;
            _searchBtn.Location = new Point(317, 31);
            _searchBtn.Size = new Size(75, 22);
            _searchBtn.TabIndex = 2;
            _searchBtn.Text = "查找下一个";
            _searchBtn.Enabled = false;
            _searchBtn.UseVisualStyleBackColor = false;
            _searchBtn.Click += new System.EventHandler(btns_Click);
            // 
            // btn_excall
            // 
            _replaceAllBtn.BackColor = Color.Transparent;
            _replaceAllBtn.FlatStyle = FlatStyle.Flat;
            _replaceAllBtn.ForeColor = Color.DarkSlateGray;
            _replaceAllBtn.Location = new Point(317, 105);
            _replaceAllBtn.Size = new Size(75, 22);
            _replaceAllBtn.TabIndex = 3;
            _replaceAllBtn.Text = "全部替换";
            _replaceAllBtn.UseVisualStyleBackColor = false;
            _replaceAllBtn.Enabled = false;
            _replaceAllBtn.Click += new System.EventHandler(btns_Click);
            // 
            // tb_exc
            // 
            _replaceDataTextBox.BorderStyle = BorderStyle.FixedSingle;
            _replaceDataTextBox.ForeColor = Color.DarkSlateGray;
            _replaceDataTextBox.Location = new Point(89, 69);
            _replaceDataTextBox.Size = new Size(203, 21);
            _replaceDataTextBox.TabIndex = 4;
            _replaceDataTextBox.BackColor = _owner.BackColor;
            // 
            // tb_s
            // 
            _searchWordsTextBox.BorderStyle = BorderStyle.FixedSingle;
            _searchWordsTextBox.ForeColor = Color.DarkSlateGray;
            _searchWordsTextBox.Location = new Point(89, 32);
            _searchWordsTextBox.Size = new Size(203, 21);
            _searchWordsTextBox.TabIndex = 5;
            _searchWordsTextBox.BackColor = _owner.BackColor;
            _searchWordsTextBox.TextChanged += new System.EventHandler(soWordsTextBox_TextChanged);
            // 
            // label_title
            // 
            _tLabel.BackColor = _owner.BorderColor;
            _tLabel.Cursor = Cursors.SizeAll;
            _tLabel.Dock = DockStyle.Top;
            _tLabel.ForeColor = _owner.BackColor;
            _tLabel.Location = new Point(0, 0);
            _tLabel.Size = new Size(420, 20);
            _tLabel.TabIndex = 6;
            _tLabel.Text = "替换";
            _tLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label_exc
            // 
            _replaceLabel.AutoSize = true;
            _replaceLabel.BackColor = Color.Transparent;
            _replaceLabel.ForeColor = Color.DarkSlateGray;
            _replaceLabel.Location = new Point(27, 75);
            _replaceLabel.Size = new Size(53, 12);
            _replaceLabel.TabIndex = 7;
            _replaceLabel.Text = "替换为：";
            // 
            // cb_lower
            // 
            _caseSensitiveChechBox.AutoSize = true;
            _caseSensitiveChechBox.BackColor = Color.Transparent;
            _caseSensitiveChechBox.ForeColor = Color.DarkSlateGray;
            _caseSensitiveChechBox.Location = new Point(29, 146);
            _caseSensitiveChechBox.Size = new Size(84, 16);
            _caseSensitiveChechBox.TabIndex = 8;
            _caseSensitiveChechBox.Text = "区分大小写";
            _caseSensitiveChechBox.UseVisualStyleBackColor = false;
            // 
            // mPanel
            // 
            _mPanel.BackColor = _owner.BackColor;
            _mPanel.Controls.Add(_slabel);
            _mPanel.Controls.Add(_replaceLabel);
            _mPanel.Controls.Add(_caseSensitiveChechBox);
            _mPanel.Controls.Add(_cancelBtn);
            _mPanel.Controls.Add(_replaceBtn);
            _mPanel.Controls.Add(_tLabel);
            _mPanel.Controls.Add(_searchBtn);
            _mPanel.Controls.Add(_searchWordsTextBox);
            _mPanel.Controls.Add(_replaceAllBtn);
            _mPanel.Controls.Add(_replaceDataTextBox);
            _mPanel.Dock = DockStyle.Fill;
            _mPanel.Location = new Point(3, 0);
            _mPanel.Size = new Size(420, 175);
            _mPanel.TabIndex = 9;
            // 
            // label_s
            // 
            _slabel.AutoSize = true;
            _slabel.BackColor = Color.Transparent;
            _slabel.ForeColor = Color.DarkSlateGray;
            _slabel.Location = new Point(27, 37);
            _slabel.Size = new Size(41, 12);
            _slabel.TabIndex = 9;
            _slabel.Text = "查找：";
            // 
            // f
            // 
            _f.AutoScaleDimensions = new SizeF(6F, 12F);
            _f.AutoScaleMode = AutoScaleMode.Font;
            _f.BackColor = _owner.BorderColor;
            _f.Owner = _owner;
            _f.ClientSize = new Size(426, 180);
            _f.Controls.Add(_mPanel);
            _f.FormBorderStyle = FormBorderStyle.None;
            _f.Opacity = 0.9D;
            _f.Padding = new Padding(3, 0, 3, 3);
            _f.ShowIcon = false;
            _f.ShowInTaskbar = false;
            _f.StartPosition = FormStartPosition.Manual;
            _f.Text = "替换";
            _f.Load += new System.EventHandler(f_Load);
            _f.Shown += new System.EventHandler(f_Shown);
            _mPanel.ResumeLayout(false);
            _mPanel.PerformLayout();
            _f.ResumeLayout(false);

            _formHelper.Move(_mPanel);
            _formHelper.Move(_tLabel);
        }

        private void f_Load(object sender, EventArgs e)
        {
            _formHelper.Move(_mPanel);
            _formHelper.Move(_tLabel);
            UI.Forms.FormHelper.CenterToOwner(_f);
            _formHelper.SetFormRoundRgn(3, 3);
            _f.Opacity = 0;
        }

        private void f_Shown(object sender, EventArgs e)
        {
            _formHelper.OpacityUp(0.03f, 0.8f, 10);
        }

        private void btns_Click(object sender, EventArgs e)
        {
            if (sender == _searchBtn)
            {

            }
            else if (sender == _replaceBtn)
            {

            }
            else if (sender == _replaceAllBtn)
            {

            }
            else if (sender == _cancelBtn)
            {
                _f.Hide();
            }
        }

        private void soWordsTextBox_TextChanged(object sender, EventArgs e)
        {
            bool tmp = string.IsNullOrEmpty(_owner.txtRtb.Text) || string.IsNullOrEmpty(_searchWordsTextBox.Text);
            _searchBtn.Enabled = !tmp;

            if (caseSensitive)
            {
                tmp = tmp || string.IsNullOrEmpty(_replaceDataTextBox.Text) || _owner.txtRtb.SelectedText != SearchWords;
            }
            else
            {
                tmp = tmp || string.IsNullOrEmpty(_replaceDataTextBox.Text) || _owner.txtRtb.SelectedText.ToLower() != SearchWords.ToLower();
            }
            _replaceBtn.Enabled = !tmp;
            _replaceAllBtn.Enabled = !tmp;
        }
        #endregion
    }
}
