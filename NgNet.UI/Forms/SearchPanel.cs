using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    class SearchPanel
    {
        #region public properties
        /// <summary>
        /// 获取一个值，该值指示当前窗体是否已经显示
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
                    this._schTextBox.BackColor = value;
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
            get { return _schTextBox.Text; }
        }

        /// <summary>
        /// 设置一个值，该值指示是否可以搜索
        /// </summary>
        public bool canSearch
        {
            set
            {
                _schBtn.Enabled = value;
            }
        }
        #endregion

        #region private fileds
        private Panel _mPanel = new Panel();
        private Label _schTextLabel = new Label();
        private Button _cancelBtn = new Button();
        private Button _schBtn = new Button();
        System.Windows.Forms.TextBox _schTextBox = new System.Windows.Forms.TextBox();
        private CheckBox _sequentialSearchCheckBox = new CheckBox();
        private CheckBox _caseSensstiveCheckBox = new CheckBox();
        private Label _tLabel = new Label();
        private Form _f;

        private int _sloc = 0;
        private TextBoxF _owner = null;
        private UI.Forms.FormHelper _formHelper;
        #endregion

        #region private properties
        private bool _sequentialSearch
        {
            get
            {
                return _sequentialSearchCheckBox.Checked;
            }
        }
        private bool _caseSensitive
        {
            get
            {
                return _caseSensstiveCheckBox.Checked;
            }
        }
        #endregion

        #region constructor destructor  
        public SearchPanel(TextBoxF owner)
        {
            this._owner = owner;
        }
        #endregion

        #region private methods
        private void init()
        {
            _mPanel = new Panel();
            _tLabel = new Label();
            _caseSensstiveCheckBox = new CheckBox();
            _sequentialSearchCheckBox = new CheckBox();
            _schTextBox = new System.Windows.Forms.TextBox();
            _schBtn = new Button();
            _cancelBtn = new Button();
            _schTextLabel = new Label();
            _f = new Form();
            _formHelper = new UI.Forms.FormHelper(_f);
            // 
            // mPanel
            // 
            _mPanel.BackColor = _owner.BackColor;
            _mPanel.Controls.Add(_schTextLabel);
            _mPanel.Controls.Add(_cancelBtn);
            _mPanel.Controls.Add(_schBtn);
            _mPanel.Controls.Add(_schTextBox);
            _mPanel.Controls.Add(_sequentialSearchCheckBox);
            _mPanel.Controls.Add(_caseSensstiveCheckBox);
            _mPanel.Controls.Add(_tLabel);
            _mPanel.Dock = DockStyle.Fill;
            _mPanel.ForeColor = Color.DarkSlateGray;
            _mPanel.Location = new Point(3, 0);
            _mPanel.Size = new Size(420, 175);
            _mPanel.TabIndex = 0;
            // 
            // tLabel
            // 
            _tLabel.BackColor = _owner.BorderColor;
            _tLabel.Dock = DockStyle.Top;
            _tLabel.ForeColor = _owner.BackColor;
            _tLabel.Location = new Point(0, 0);
            _tLabel.Size = new Size(420, 20);
            _tLabel.TabIndex = 0;
            _tLabel.Text = "查找";
            _tLabel.TextAlign = ContentAlignment.MiddleCenter;
            _tLabel.Cursor = Cursors.SizeAll;
            //
            //
            //
            _caseSensstiveCheckBox.AutoSize = true;
            _caseSensstiveCheckBox.Checked = true;
            _caseSensstiveCheckBox.Location = new Point(136, 131);
            _caseSensstiveCheckBox.Size = new Size(72, 16);
            _caseSensstiveCheckBox.TabIndex = 3;
            _caseSensstiveCheckBox.Text = "区分大小写";
            _caseSensstiveCheckBox.UseVisualStyleBackColor = true;
            _caseSensstiveCheckBox.BackColor = Color.Transparent;
            _caseSensstiveCheckBox.CheckStateChanged += new EventHandler(caseSensstiveCheckedChanged);
            // 
            // sequentialSearchCheckBox
            // 
            _sequentialSearchCheckBox.AutoSize = true;
            _sequentialSearchCheckBox.Checked = true;
            _sequentialSearchCheckBox.Location = new Point(230, 131);
            _sequentialSearchCheckBox.Size = new Size(72, 16);
            _sequentialSearchCheckBox.TabIndex = 3;
            _sequentialSearchCheckBox.Text = "顺序查找";
            _sequentialSearchCheckBox.UseVisualStyleBackColor = true;
            _sequentialSearchCheckBox.BackColor = Color.Transparent;
            _sequentialSearchCheckBox.CheckStateChanged += new EventHandler(caseSensstiveCheckedChanged);
            // 
            // schTextBox
            // 
            _schTextBox.BorderStyle = BorderStyle.FixedSingle;
            _schTextBox.Location = new Point(99, 40);
            _schTextBox.Size = new Size(227, 21);
            _schTextBox.TabIndex = 4;
            _schTextBox.BackColor = _owner.BackColor;
            _schTextBox.TextChanged += new EventHandler(searchWordsTextBox_TextChanged);
            // 
            // schBtn
            // 
            _schBtn.FlatStyle = FlatStyle.Flat;
            _schBtn.Location = new Point(332, 39);
            _schBtn.Enabled = false;
            _schBtn.Size = new Size(75, 23);
            _schBtn.TabIndex = 5;
            _schBtn.Text = "查找下一个";
            _schBtn.UseVisualStyleBackColor = true;
            _schBtn.BackColor = Color.Transparent;
            _schBtn.Click += new EventHandler(btn_s_Click);
            // 
            // cancelBtn
            // 
            _cancelBtn.FlatStyle = FlatStyle.Flat;
            _cancelBtn.Location = new Point(332, 127);
            _cancelBtn.Size = new Size(75, 23);
            _cancelBtn.TabIndex = 6;
            _cancelBtn.Text = "取消";
            _cancelBtn.UseVisualStyleBackColor = true;
            _cancelBtn.BackColor = Color.Transparent;
            _cancelBtn.Click += new EventHandler((object sender, EventArgs e) => { _f.Hide(); });
            // 
            // schTextLabel
            // 
            _schTextLabel.AutoSize = true;
            _schTextLabel.Location = new Point(16, 44);
            _schTextLabel.Size = new Size(77, 12);
            _schTextLabel.TabIndex = 7;
            _schTextLabel.Text = "查找关键词：";
            _schTextLabel.BackColor = Color.Transparent;
            // 
            // f
            // 
            _f.BackColor = _owner.BorderColor;
            _f.Owner = _owner;
            _f.ClientSize = new Size(426, 180);
            _f.Controls.Add(_mPanel);
            _f.FormBorderStyle = FormBorderStyle.None;
            _f.ShowIcon = false;
            _f.ShowInTaskbar = false;
            _f.Location = new Point(_owner.Left + 66, _owner.Top + _owner.Height / 3);
            _f.StartPosition = FormStartPosition.Manual;
            _f.Padding = new Padding(3, 0, 3, 3);
            _f.Text = "查找";
            _mPanel.ResumeLayout(false);
            _mPanel.PerformLayout();
            _f.ResumeLayout(false);
            _f.Load += new EventHandler(f_Load);
            _f.Shown += new EventHandler(f_Shown);
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
            init();
            _f.Show();
        }
        #endregion

        #region search and search set

        private void caseSensstiveCheckedChanged(object sender, EventArgs e)
        {
            if (_sequentialSearch)
            {
                _sloc = _sloc == 0 ? _owner.txtRtb.Text.Length - 1 : _sloc;
            }
            else
            {
                _sloc = _sloc == _owner.txtRtb.Text.Length - 1 ? 0 : _sloc;
            }
        }

        private void btn_s_Click(object sender, EventArgs e)
        {
            int indextmp = -1;
            string swordtmp = _caseSensitive ? SearchWords.ToLower() : SearchWords;
            if (_sequentialSearch)
            {
                if (_sloc == _owner.txtRtb.Text.Length - 1)
                    if (UI.Forms.MessageBox.Show(_f, "已经搜索至最后,是否重新从头开始搜索？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        _sloc = 0;
                indextmp = _owner.txtRtb.Text.IndexOf(swordtmp, _sloc + 1, _owner.txtRtb.Text.Length - _sloc - 1);

            }
            else
            {
                if (_sloc == 0)
                    if (UI.Forms.MessageBox.Show(_f, "已经搜索至最前,是否重新从最后开始搜索？", "", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        _sloc = _owner.txtRtb.Text.Length - 1;
                indextmp = _owner.txtRtb.Text.LastIndexOf(swordtmp, _sloc - 1, _sloc - 1);
            }

            if (indextmp == -1)
            {
                string inf = "未搜索到任何内容";
                UI.Forms.MessageBox.Show(_f, inf, "", MessageBoxButtons.OK);
            }
            else
            {
                _sloc = indextmp;
                _owner.txtRtb.SelectionStart = _sloc;
                _owner.txtRtb.SelectionLength = SearchWords.Length;
            }
        }

        private void searchWordsTextBox_TextChanged(object sender, EventArgs e)
        {
            canSearch = !string.IsNullOrEmpty(_schTextBox.Text) && !string.IsNullOrEmpty(_owner.txtRtb.Text);
        }
        #endregion

        #region f
        private void f_Load(object sender, EventArgs e)
        {
            UI.Forms.FormHelper.CenterToOwner(_f);
            _formHelper.Move(_mPanel);
            _formHelper.Move(_tLabel);
            _formHelper.SetFormRoundRgn(3, 3);
            _schTextBox.Text = this.SearchWords;
            _f.Opacity = 0;
        }

        private void f_Shown(object sender, EventArgs e)
        {
            this._formHelper.OpacityUp(0.03f, 0.8f, 10);
        }
        #endregion
    }
}
