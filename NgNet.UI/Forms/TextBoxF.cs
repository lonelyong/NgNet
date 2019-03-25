using System;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;
using NgNet.UI.Forms;
using System.Reflection;
using System.Diagnostics;

namespace NgNet.UI.Forms
{
    partial class TextBoxF : TitleableForm, ICommBoxWindow
    {
        #region private fields
        private string _path = string.Empty;

        private string _fixedTitle = string.Empty;

        private const string _OFILTER = "*.txt|纯文本文件|*.rtf|Rich text foramt|*.xml|xml文件|*.html|网页源文件|*.ini|程序配置文件|*.log|日志文件|*.cs;*.vb;*.h;*.cpp|程序源文件" + IO.FilterHelper.All;

        private const string _SFILTER = "*.txt|纯文本文档|*.rtf|rtf文档|*.*|任意文件";

        private string _lastSaveFolder = Windows.SpecialFolders.Desktop;

        private string _lastOpenFolder = Windows.SpecialFolders.Desktop;

        private int _x_pos, _y_pos;//记录输入焦点的位置

        private SearchPanel _searchBox;

        private ReplacePanel _replaceBox;

        private int _tmpTxtHash = 0;//记录上次文本保存使得哈希值

        private MenuRender _menuRender;
        #endregion

        #region private properties
        private bool _canSearch
        {
            get { return !(string.IsNullOrEmpty(txtRtb.Text) || string.IsNullOrEmpty(_searchBox.SearchWords)); }
        }

        private bool _canReplace
        {
            get { return !(string.IsNullOrEmpty(txtRtb.Text) || string.IsNullOrEmpty(_replaceBox.SearchWords)); }
        }
        #endregion

        #region public properties
        /// <summary>
        /// 当前文件的路径
        /// </summary>
        public string Path
        {
            private set
            {
                _path = value;
                Text = Text;
            }
            get
            {
                return _path;
            }
        }

        /// <summary>
        /// 获取或设置文本框文本
        /// </summary>
        public string CurrentText
        {
            set
            {
                txtRtb.Text = value;
                _tmpTxtHash = txtRtb.Text.GetHashCode();
            }
            get
            {
                return txtRtb.Text;
            }
        }

        /// <summary>
        /// 窗体永久性标题，如果不用永久性标题，将其设置为空即可
        /// </summary>
        public string FixedTitle
        {
            set
            {
                _fixedTitle = value;
                Text = Text;
            }
            get
            {
                return _fixedTitle;
            }
        }

        /// <summary>
        /// 设置当文件路径为空时的标题
        /// </summary>
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? "无标题" : value;
                //判断永久性标题是否存在
                if (!string.IsNullOrWhiteSpace(FixedTitle))
                    base.Text = FixedTitle;
                else
                {
                    if (string.IsNullOrWhiteSpace(Path))
                    {
                        base.Text = value;//显示的不是文件
                    }
                    else
                    {
                        base.Text = Path;
                    }
                }
            }
        }

        /// <summary>
        /// 获取或设置标题栏样式
        /// </summary>
        public TitleBarStyles TitleBarStyle
        {
            get
            {
                return TitleBar.Style;
            }
            set
            {
                TitleBar.Style = value;
            }
        }

        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool Editable
        {
            set
            {
                txtRtb.ReadOnly = !value;
                tsmi_open.Enabled = value;
                tsmi_new.Enabled = value;
                tsmi_replace.Enabled = value;
                tsmi_text_clean.Enabled = value;
                tsmi_save.Enabled = value;
                infLabel.Text = value ? "自由模式" : "只读模式";
                infLabel.Tag = value ? "自由模式" : "只读模式";
            }
            get
            {
                return txtRtb.ReadOnly == false;
            }
        }

        /// <summary>
        /// 拓展菜单
        /// </summary>
        public ToolStripMenuItem[] UserMenus { get; set; }

        public override Color ForeColor
        {
            set
            {
                base.ForeColor = value;
                locLabel.ForeColor = value;
                infLabel.ForeColor = value;

                if (_searchBox != null)
                    _searchBox.ForeColor = value;
                if (_replaceBox != null)
                    _replaceBox.ForeColor = value;
                if (_menuRender != null)
                    _menuRender.Colors.FontColor = ForeColor;
            }
            get
            {
                return base.ForeColor;
            }
        }

        public override Color BackColor
        {
            set
            {
                base.BackColor = value;
                mainSplitContainer.BackColor = value;
                txtRtb.BackColor = value;

                if (_replaceBox != null)
                    _replaceBox.BackColor = value;
                if (_searchBox != null)
                    _searchBox.BackColor = value;
                if (_menuRender != null)
                {
                    _menuRender.Colors.DropDownItemEndColor = BackColor;
                    _menuRender.Colors.DropDownBackStartColor = BackColor;
                    _menuRender.Colors.MarginStartColor = BackColor;
                    _menuRender.Colors.MenuItemStartColor = BackColor;
                    _menuRender.Colors.DropDownBackEndColor = NgNet.Drawing.ColorHelper.GetSimilarColor(BackColor, false, Level.Level9);
                    _menuRender.Colors.MarginEndColor = _menuRender.Colors.DropDownBackEndColor;
                    _menuRender.Colors.MenuItemEndColor = _menuRender.Colors.DropDownBackEndColor;
                }
            }
            get
            {
                return base.BackColor;
            }
        }

        public override Color BorderColor
        {
            set
            {
                base.BorderColor = value;
                if (_searchBox != null)
                    _searchBox.BorderColor = value;
                if (_searchBox != null)
                    _replaceBox.BorderColor = value;
                mainSplitContainer.Panel1.BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(BorderColor, false, Level.Level10);
                childSplitContainer.Panel2.BackColor = mainSplitContainer.Panel1.BackColor;

                if (_menuRender != null)
                {
                    _menuRender.Colors.ArrowColor = BorderColor;
                    _menuRender.Colors.DropDownBorderColor = BorderColor;
                    _menuRender.Colors.DropDownItemBorderColor = BorderColor;
                    _menuRender.Colors.DropDownItemStartColor = BorderColor;
                    _menuRender.Colors.MenuItemBorderColor = BorderColor;
                    _menuRender.Colors.SeparatorColor = BorderColor;
                }
            }
            get
            {
                return base.BorderColor;
            }
        }
        #endregion

        #region constructor destructor  
        public TextBoxF()
        {
            InitializeComponent();
            Resizeable = true;
            _searchBox = new SearchPanel(this);
            _replaceBox = new ReplacePanel(this);
            //移动窗体
            FormHelper.Move(panel_tools);
            FormHelper.SetFormRoundRgn(3, 3);
            FormHelper.AutoBorder(new UI.BorderSize(), new UI.BorderSize(3, 1, 3, 3));

            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            Icon = ConvertHelper.Bitmap2Icon(Windows.Logos.Application);
            _menuRender = new MenuRender();
            setTheme(2);
            CurrentText = string.Empty;
            TaskbarSetWindowState = true;
        }
        #endregion

        #region private methods
        #region  test
        /// <summary>
        /// 判断文本有没有被修改
        /// </summary>
        /// <returns></returns>
        private bool isTextChanged()
        {
            return txtRtb.Text.GetHashCode() != _tmpTxtHash;
        }

        /// <summary>
        /// 文件大小长度限制
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private bool fileTest(string path)
        {
            System.IO.FileInfo _fi = new System.IO.FileInfo(path);
            if (_fi.Exists)
                if (_fi.Length > 209715200)
                {
                    UI.Forms.MessageBox.Show(this, "您打开的文件过大，为保证程序性能，请选择小于2M的文件", "文件大小限制");
                    return false;
                }
                else
                    return true;
            else
                return false;
        }

        private bool sureClose()
        {
            if (!isTextChanged())
                return true;
            string _spath;
            DialogResult _dr;
            if (string.IsNullOrWhiteSpace(Path))
            {
                Re0:
                switch (UI.Forms.MessageBox.Show(this, "文件未保存，是否保存？", "", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        saveDialog("保存", out _spath);
                        if (string.IsNullOrWhiteSpace(_spath))
                            goto Re0;
                        else
                        {
                            if (saveText(_spath))
                                return true;
                            else
                            {
                                _dr = UI.Forms.MessageBox.Show(this, "无法保存文件，请将内容复制到别处，是否现在关闭或打开新文件？", "", MessageBoxButtons.YesNo);
                                return _dr == DialogResult.Yes;
                            }
                        }
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.No:
                        return true;
                    default:
                        throw new ArgumentException("DialogResult类型错误");
                }
            }
            else
            {
                switch (UI.Forms.MessageBox.Show(this, "文件已经修改，是否保存？", "", MessageBoxButtons.YesNoCancel))
                {
                    case DialogResult.Yes:
                        if (saveText(Path))
                            return true;
                        else
                        {
                            _dr = UI.Forms.MessageBox.Show(this, "无法保存文件，请将内容复制到别处，是否现在关闭或打开新文件？", "", MessageBoxButtons.YesNo);
                            return _dr == DialogResult.Yes;
                        }
                    case DialogResult.Cancel:
                        return false;
                    case DialogResult.No:
                        return true;
                    default:
                        throw new ArgumentException("DialogResult类型错误");
                }
            }
        }
        #endregion

        #region this
        private void setOpacity()
        {
            double opacity = 0.85d;
            Opacity = opacity;
            ContextMenuStripHelper.SetOpacity( cms_text, opacity );
            MenuHelper.SetOpacity( ms_left.Items, opacity );
            MenuHelper.SetOpacity( ms_right.Items, opacity );
        }

        private void setToolstrip()
        {

            _menuRender.Colors.MainMenuStartColor = Color.Transparent;
            _menuRender.Colors.MainMenuEndColor = Color.Transparent;

            cms_text.Renderer = _menuRender;
            ms_left.Renderer = _menuRender;
            ms_right.Renderer = _menuRender;
        }

        private void this_Load(object sender, EventArgs e)
        {
            childSplitContainer.SplitterDistance = childSplitContainer.Height - 13;
            setOpacity();
            setToolstrip();
        }

        private void this_Shown(object sender, EventArgs e)
        {
            txtRtb.Focus();
        }

        private void this_SizeChanged(object sender, EventArgs e)
        {
            ms_right.Left = mainSplitContainer.Panel1.Width - ms_right.Width - 2;
        }

        private void this_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.ApplicationExitCall
                || e.CloseReason == CloseReason.FormOwnerClosing
                || e.CloseReason == CloseReason.TaskManagerClosing
                || e.CloseReason == CloseReason.UserClosing
                || e.CloseReason == CloseReason.WindowsShutDown)
                e.Cancel = !sureClose();
        }
        #endregion

        #region richTextBox
        /// <summary>
        /// 显示光标位置信息
        /// </summary>
        private void displaySelectionStart(Point p)
        {
            locLabel.Text = string.Format("[X= {0} | Y = {1}]", p.X, p.Y);
        }

        private void txtRtb_SelectionChanged(object sender, EventArgs e)
        {
            infLabel.Text = string.Format("{0}       选中：[{1}]", infLabel.Tag, txtRtb.SelectionLength);
            _y_pos = txtRtb.GetLineFromCharIndex(txtRtb.SelectionStart);
            _x_pos = txtRtb.SelectionStart - txtRtb.GetFirstCharIndexOfCurrentLine();
            displaySelectionStart(new Point(_x_pos + 1, _y_pos + 1));
        }

        private void txtRtb_TextChanged(object sender, EventArgs e)
        {
            _searchBox.canSearch = _canSearch;
            _replaceBox.canReplace = _canReplace;
        }

        private void txtRtb_LinkClick(object sender, LinkClickedEventArgs e)
        {
            if (UI.Forms.MessageBox.Show(this, $"是否打开以下连接：\n  {e.LinkText}", MessageBoxButtons.YesNo) == DialogResult.Yes)
                IO.FileHelper.OpenAs(e.LinkText);
        }
        #endregion

        #region cms_text
        private void cms_text_Opening(object sender, CancelEventArgs e)
        {
            this.cms_text.Items.Clear();
            this.cms_text.Items.AddRange(new ToolStripMenuItem[] {
                 this.tsmi_text_copy,
                 this.tsmi_text_paste,
                 this.tsmi_text_redo,
                 this.tsmi_text_undo,
                 this.tsmi_text_toHead,
                 this.tsmi_text_toEnd,
                 this.tsmi_text_selectAll,
                 this.tsmi_text_cut,
                 this.tsmi_text_clean,
                 this.Tsmi_text_fColor,
                 this.Tsmi_text_bColor});

            if (UserMenus != null && UserMenus.Length > 0)
            {
                cms_text.Items.Add("-");
                cms_text.Items.AddRange(UserMenus);
            }
            this.tsmi_text_copy.Enabled = string.IsNullOrEmpty(this.txtRtb.SelectedText) ? false : true;
            this.tsmi_text_cut.Enabled = string.IsNullOrEmpty(this.txtRtb.SelectedText) || !Editable ? false : true;
            this.tsmi_text_paste.Enabled = string.IsNullOrEmpty(Clipboard.GetText()) || !Editable ? false : true;
            this.tsmi_text_redo.Enabled = txtRtb.CanRedo && Editable ? true : false;
            this.tsmi_text_undo.Enabled = txtRtb.CanUndo && Editable ? true : false;
        }

        private void cms_text_items_Click(object sender, EventArgs e)
        {
            if (sender == this.tsmi_text_clean)
            {
                if (UI.Forms.MessageBox.Show(this, "即将清空所有文本，是否继续？", "清空文本", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.txtRtb.Clear();
                }
            }
            else if (sender == tsmi_text_copy)
            {
                this.txtRtb.Copy();
            }
            else if (sender == tsmi_text_cut)
            {
                this.txtRtb.Cut();
            }
            else if (sender == tsmi_text_paste)
            {
                this.txtRtb.Paste();
            }
            else if (sender == this.tsmi_text_redo)
            {
                this.txtRtb.Redo();
            }
            else if (sender == tsmi_text_undo)
            {
                this.txtRtb.Undo();
            }
            else if (sender == this.tsmi_text_selectAll)
            {
                this.txtRtb.SelectAll();
            }
            else if (sender == tsmi_text_toEnd)
            {
                this.txtRtb.SelectionStart = this.txtRtb.Text.Length;
                this.txtRtb.SelectionLength = 0;
            }
            else if (sender == tsmi_text_toHead)
            {
                this.txtRtb.SelectionStart = 0;
                this.txtRtb.SelectionLength = 0;
            }
        }

        private void tsmi_text_bColor_items_Click(object sender, EventArgs e)
        {
            this.txtRtb.SelectionBackColor = Color.FromName(((ToolStripMenuItem)sender).Tag.ToString());
        }

        private void tsmi_text_fColor_items_Click(object sender, EventArgs e)
        {
            this.txtRtb.SelectionColor = Color.FromName(((ToolStripMenuItem)sender).Tag.ToString());
        }
        #endregion

        #region ms_left
        /// <summary>
        /// 获取存储路径
        /// </summary>
        /// <returns></returns>
        private void saveDialog(string title, out string path)
        {
            path = UI.Forms.SaveFileDialog.Show(
                this, 
                Path == null ? "" : System.IO.Path.GetFileNameWithoutExtension(Path), 
                _lastSaveFolder, 
                _SFILTER, 
                0, 
                title);
        }
        /// <summary>
        /// 保存当前文本到制定路径
        /// </summary>
        /// <param name="sPath"></param>
        /// <returns></returns>
        private bool saveText(string sPath)
        {
            try
            {
                switch (System.IO.Path.GetExtension(sPath).ToLower().Trim())
                {
                    case ".txt":
                        this.txtRtb.SaveFile(sPath, RichTextBoxStreamType.PlainText);
                        break;
                    case ".rtf":
                        this.txtRtb.SaveFile(sPath, RichTextBoxStreamType.RichText);
                        break;
                    default:
                        this.txtRtb.SaveFile(sPath, RichTextBoxStreamType.PlainText);
                        break;
                }
                new ProgressMessageBox(this) { DisplayTime = 2000 }.Show("保存成功");
            }
            catch (Exception ex)
            {
                UI.Forms.MessageBox.Show(this, ex.Message + "\r\npath = " + sPath, "保存失败");
                return false;
            }
            return true;
        }

        private void tsmi_open_Click(object sender, EventArgs e)
        {
            if (!sureClose())
                return;
            string _scrPath = UI.Forms.OpenFileDialog.Show(this, _lastOpenFolder, _OFILTER, 0, "打开文件对话框");
            if (string.IsNullOrWhiteSpace(_scrPath))
                return;
            OpenFile(_scrPath);
            _lastOpenFolder = System.IO.Path.GetDirectoryName(Path);
        } 

        private void tsmi_new_Click(object sender, EventArgs e)
        {
            try
            {
                Process _p = new Process();
                _p.StartInfo.FileName =  System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\NgNet.Host.exe";
                _p.StartInfo.Arguments = $"-r {typeof(TextBox).Assembly.GetName().Name} -c {typeof(TextBox).FullName} -m ShowDialog -p \"$null\"";
                _p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                _p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsmi_save_Click(object sender, EventArgs e)
        {
            string _sPath = null;
            if (string.IsNullOrWhiteSpace(Path))
            {
                saveDialog("保存", out _sPath);
                if (string.IsNullOrWhiteSpace(_sPath))
                    return;
            }
            else
                _sPath = Path;
            if (saveText(_sPath))
            {
                Path = _sPath;
                _lastSaveFolder = _sPath;
                _tmpTxtHash = txtRtb.Text.GetHashCode();
            }
        }

        private void tsmi_saveNew_Click(object sender, EventArgs e)
        {
            string _sPath;
            saveDialog("另存为", out _sPath);
            if (string.IsNullOrWhiteSpace(_sPath))
            {
                return;
            }
            saveText(_sPath);
        }

        private void tsmi_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //option
        private void tsmi_edit_DropDownOpening(object sender, EventArgs e)
        {
            this.tsmi_replace.Checked = this._replaceBox.IsShown;
            this.tsmi_search.Checked = this._searchBox.IsShown;
        }

        private void tsmi_search_Click(object sender, EventArgs e)
        {
            this._searchBox.Show();
        }

        private void tsmi_replace_Click(object sender, EventArgs e)
        {
            this._replaceBox.Show();
        }

        private void tsmi_gotoLine_Click(object sender, EventArgs e)
        {
            Re:
            UI.Forms.InputBox ib = new UI.Forms.InputBox();
            ib.BackColor = this.BackColor;
            ib.BorderColor = this.BorderColor;
            ib.ForeColor = this.ForeColor;
            ib.Blendable = this.Blendable;
            ib.Notice = string.Format("请输入行数(1 - {0})：", this.txtRtb.Lines.Length);
            ib.Title = "转到";
            ib.Text = "1";
            ib.InputType = UI.Forms.InputTypes.UInt;
            string input = ib.Show();
            if (string.IsNullOrWhiteSpace(input)) { return; }//无输入则返回

            int line;
            if (int.TryParse(input, out line))
            {
                line--;//第1行的索引是0
                if (line < this.txtRtb.Lines.Length && line >= 0)
                {
                    this.txtRtb.SelectionStart = 0;
                    this.txtRtb.SelectionStart = this.txtRtb.GetFirstCharIndexFromLine(line);
                }
                else if (line < 0)
                {
                    return;
                }
                else
                {
                    UI.Forms.MessageBox.Show(this, "输入的行数超出范围！", "提示您", MessageBoxButtons.OK, DialogResult.OK, 3);
                    goto Re;
                }
            }
        }

        private void tsmi_state_Click(object sender, EventArgs e)
        {
            this.childSplitContainer.Panel2Collapsed = !this.childSplitContainer.Panel2Collapsed;
            ((ToolStripMenuItem)sender).Checked = !this.childSplitContainer.Panel2Collapsed;
        }

        private void tsmi_font_Click(object sender, EventArgs e)
        {
            FontDialog fd = new FontDialog();
            fd.FontMustExist = true;
            fd.MaxSize = 36;
            fd.MinSize = 6;
            if (fd.ShowDialog() == DialogResult.OK)
                txtRtb.Font = fd.Font;

        }

        private void tsmi_encoding_items_Click(object sender, EventArgs e)
        {
            NgNet.UI.Forms.MessageBox.Show(this, "暂未实现该方法");
            ToolStripMenuItem _tsmi = sender as ToolStripMenuItem;
            Encoding _coding = Encoding.GetEncoding(_tsmi.Name);
            txtRtb.Text = _coding.GetString( Encoding.Default.GetBytes( txtRtb.Text ) );
        }

        private void tsmi_encoding_DropDownOpening( object sender, EventArgs e )
        {
            if (tsmi_encoding.DropDownItems.Count > 0)
                return;
            string[] _encodings = new string[]
            {
                Encoding.ASCII.WebName,
                Encoding.Unicode.WebName,
                Encoding.UTF7.WebName,
                Encoding.UTF8.WebName,
                Encoding.UTF32.WebName,
                Encoding.GetEncoding("GB2312").WebName,
            };
            foreach (var item in _encodings)
            {
                ToolStripMenuItem _tsmi = new ToolStripMenuItem();
                _tsmi.Text = Encoding.GetEncoding(item).EncodingName;
                _tsmi.Name = item;
                _tsmi.Click += tsmi_encoding_items_Click;
                tsmi_encoding.DropDownItems.Add(_tsmi);
            }
        }

        private void tsmi_about_Click(object sender, EventArgs e)
        {
            string about = "简易文本编辑器";
            UI.Forms.MessageBox.Show(this, about, string.Format("关于{0}", "TextEditor"), MessageBoxButtons.OK, DialogResult.OK);
        }


        #endregion

        #region ms_right
        private void setTheme(int theme)
        {
            switch (theme)
            {
                case 0:
                    BackColor = Color.Azure;
                    BorderColor = Color.LightSeaGreen;
                    ForeColor = Color.DarkSlateGray;
                    break;
                case 1:
                    BackColor = Color.Gray;
                    BorderColor = Color.FromArgb(52, 52, 52);
                    ForeColor = Color.White;
                    break;
                case 2:
                    BackColor = Color.Azure;
                    BorderColor = Color.DarkGreen;
                    ForeColor = Color.FromArgb(00, 32, 00);
                    break;
                case 3:
                    BackColor = Color.Thistle;
                    BorderColor = Color.Purple;
                    ForeColor = Color.FromArgb(62, 00, 62);
                    break;
                case 4:
                    BackColor = Color.FromArgb(244, 217, 240);
                    BorderColor = Color.FromArgb(182, 35, 172);
                    ForeColor = Color.FromArgb(52, 2, 52);
                    break;
                default:
                    setTheme(3);
                    return;
            }
        }

        private void tsmi_fontSizeud_Click(object sender, EventArgs e)
        {
            int add = sender == this.tsmi_fontSizeup ? 1 : -1;
            txtRtb.Font = new Font(txtRtb.Font.Name, txtRtb.Font.Size + add, txtRtb.Font.Style, txtRtb.Font.Unit);
        }

        private void tsmi_fonts_items_Click(object sender, EventArgs e)
        {
            if (UI.Forms.MessageBox.Show(this, "此操作会将所有字体设置为选择字体，并且此操作不可撤销，是否继续？", "更改字体", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                txtRtb.Font = new Font(tsmi.Tag.ToString(), txtRtb.Font.Size, txtRtb.Font.Style, txtRtb.Font.Unit);
            }
        }

        private void tsmi_fcolor_items_Click(object sender, EventArgs e)
        {
            if (UI.Forms.MessageBox.Show(this, "此操作会将所有字体颜色设置为选择颜色，并且此操作不可恢复，是否继续？", "前景色设置", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
                txtRtb.ForeColor = Color.FromName(tsmi.Tag.ToString());
            }
        }

        private void tsmi_theme_items_Click(object sender, EventArgs e)
        {
            setTheme(((ToolStripMenuItem)sender).Tag.ToString().ToInt(2));
        }
        #endregion
        #endregion

        #region public methods
        public void OpenFile(string filePath)
        {
            try
            {
                //判断文件是否存在
                //文件存在
                if (System.IO.File.Exists(filePath))
                {
                    if (fileTest(filePath))
                    {
                        switch (System.IO.Path.GetExtension(filePath).ToLower().Trim())
                        {
                            case ".txt":
                                txtRtb.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                                break;
                            case ".rtf":
                                txtRtb.LoadFile(filePath, RichTextBoxStreamType.RichText);
                                break;
                            default:
                                txtRtb.LoadFile(filePath, RichTextBoxStreamType.PlainText);
                                break;
                        }
                        Path = filePath;
                        _tmpTxtHash = txtRtb.Text.GetHashCode();
                    }
                    else//文件大小超过预定大小
                    {
                      throw new Exception( "文件大小超过限制!");
                    }
                }
                else//文件不存在
                {
                    throw new Exception("文件不存在!");
                }
            }
            catch (Exception ex)
            {
                UI.Forms.MessageBox.Show(this, "文件读取错误!\r\nMessage:" + ex.Message);
            }
        }
        #endregion
    }
}
