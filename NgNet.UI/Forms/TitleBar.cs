using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class TitleBar : IDisposable, IComponent
    {
        #region private fields
        private Form _f;

        private FormHelper _formHelper;

        private PictureBox _iconPictureBox = new PictureBox();

        private Panel _tPanel = new Panel();

        private Label _tLabel = new Label();

        private Label _minLabel = new Label();

        private Label _maxLabel = new Label();

        private Label _endLabel = new Label();

        private ContextMenuStrip _defCms = new ContextMenuStrip();

        private TitleBarStyles _style = TitleBarStyles.MinMaxEnd;

        private int _defMenuCount = 0;

        private const int _BUTTON_PADDING_LEFT = 6;

        private const int _ICON_PADDING_LEFT = 6;

        private const int _PADDING_RIGHT = 6;
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置标题栏的样式
        /// </summary>
        public TitleBarStyles Style
        {
            get
            {
                return _style;
            }
            set
            {
                resetStyle(value);
            }
        }
        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public string Title
        {
            get
            {
                return _tLabel.Text;
            }
            set
            {
                _tLabel.Text = value;
            }
        }
        /// <summary>
        /// 获取或设置标题的对齐方式
        /// </summary>
        public ContentAlignment TitleAlignment
        {
            get
            {
                return _tLabel.TextAlign;
            }
            set
            {
                _tLabel.TextAlign = value;
            }
        }
        /// <summary>
        /// 获取或设置标题栏背景色
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _tPanel.BackColor;
            }
            set
            {
                _tPanel.BackColor = value;
                _tLabel.ForeColor = NgNet.Drawing.ColorHelper.GetOppositeColor(value);
                _minLabel.ForeColor = _tLabel.ForeColor;
                _maxLabel.ForeColor = _tLabel.ForeColor;
                _endLabel.ForeColor = _tLabel.ForeColor;
                this.setMenuRender();
            }
        }
        /// <summary>
        /// 获取或设置标题栏的图标
        /// </summary>
        public Icon Icon
        {
            set
            {
                _iconPictureBox.BackgroundImage = value.ToBitmap();
                _iconPictureBox.ErrorImage = _iconPictureBox.BackgroundImage;
                _iconPictureBox.InitialImage = _iconPictureBox.BackgroundImage;
            }
            get
            {
                return NgNet.Drawing.PictureHelper.Image2Icon(_iconPictureBox.BackgroundImage);
            }
        }
        /// <summary>
        /// 获取或设置标题栏的高度
        /// </summary>
        public int Height
        {
            set
            {
                _tPanel.Height = value;
                _tLabel.Height = value;
                _iconPictureBox.Height = value - 2;
                _iconPictureBox.Width = value - 2;
                _minLabel.Height = value - 2;
                _maxLabel.Height = value - 2;
                _endLabel.Height = value - 2;
            }
            get
            {
                return _tPanel.Height;
            }
        }
        /// <summary>
        /// 设置标题栏字体
        /// </summary>
        public Font Font
        {
            set
            {
                _tLabel.Font = value;
                _tPanel.Font = value;
                _maxLabel.Font = value;
                _minLabel.Font = value;
                _endLabel.Font = value;
                _maxLabel.Width = value.Height * 2;
                _endLabel.Width = _maxLabel.Width;
                _minLabel.Width = _maxLabel.Width;
                resetControlLocation();
            }
            get
            {
                return _tLabel.Font;
            }
        }
        /// <summary>
        /// 获取或设置是否显示Icon
        /// </summary>
        public bool IconVisible
        {
            get { return _iconPictureBox.Parent != null; }
            set
            {
                if (value)
                    _tPanel.Controls.Add(_iconPictureBox);
                else
                    _tPanel.Controls.Remove(_iconPictureBox);
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示在关闭窗体时是否将关闭窗体转换为隐藏窗体
        /// </summary>
        public bool CloseToHide { get; set; }
        /// <summary>
        /// 设置标题栏的快捷菜单
        /// </summary>
        public ContextMenuStrip Cms
        {
            set
            {
                _tPanel.ContextMenuStrip = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示是否可通过标题栏移动窗体
        /// </summary>
        public bool Moveable { get; set; }
        #endregion

        #region events
        public event EventHandler DiyCloseWindowEvent;
        #endregion

        #region constructor destructor
        public TitleBar(Form owner, TitleBarStyles titleBarStyle)
        {
            this._f = owner;
            this._formHelper = new FormHelper(owner);
            this.init();
            Moveable = true;
            Style = titleBarStyle;
            Show();
        }

        public TitleBar(Form owner) : this(owner, TitleBarStyles.MinMaxEnd)
        {
           
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _tPanel.Dispose();
            _tLabel.Dispose();
            _maxLabel.Dispose();
            _minLabel.Dispose();
            _endLabel.Dispose();
            _defCms.Dispose();
        }
        #endregion

        #region IComponent
        public ISite Site { get
            {
                return _tPanel.Site;
            }
            set
            {
                _tPanel.Site = value;
            }
        }

        public event EventHandler Disposed;
        #endregion

        #region private methods
        private void init()
        {
            //
            //cms
            //           
            ToolStripMenuItem tsmi_max = new ToolStripMenuItem();
            tsmi_max.Text = "最大化";
            tsmi_max.Click += new EventHandler((object sender, EventArgs e) => {
                if (_f.WindowState == FormWindowState.Normal)
                    _f.WindowState = FormWindowState.Maximized;
                else
                    _f.WindowState = FormWindowState.Normal;
            });
            ToolStripMenuItem tsmi_min = new ToolStripMenuItem();
            tsmi_min.Text = "最小化";
            tsmi_min.Click += new EventHandler((object sender, EventArgs e) => { _f.WindowState = FormWindowState.Minimized; });
            ToolStripMenuItem tsmi_end = new ToolStripMenuItem();
            tsmi_end.Text = "关闭";
            tsmi_end.Click += new EventHandler((object sender, EventArgs e) => 
            {
                if (DiyCloseWindowEvent == null)
                    closeWindow();
                else
                    DiyCloseWindowEvent(null, null);
            });
            this._defCms.Items.AddRange(new ToolStripItem[] { tsmi_max, tsmi_min, tsmi_end });
            this._defCms.Opening += new System.ComponentModel.CancelEventHandler((object sender, System.ComponentModel.CancelEventArgs e) => {

                tsmi_min.Enabled = _f.WindowState != FormWindowState.Minimized
                && (Style == TitleBarStyles.MinOnly
                || Style == TitleBarStyles.MinEnd
                || Style == TitleBarStyles.MinMax
                || Style == TitleBarStyles.MinMaxEnd);
                tsmi_max.Enabled = Style == TitleBarStyles.MaxEnd
                || Style == TitleBarStyles.MaxOnly
                || Style == TitleBarStyles.MinMax
                || Style == TitleBarStyles.MinMaxEnd;
                tsmi_end.Enabled = Style == TitleBarStyles.EndOnly
                || Style == TitleBarStyles.MaxEnd
                || Style == TitleBarStyles.MinEnd
                || Style == TitleBarStyles.MinMaxEnd;
                tsmi_max.Text = _f.WindowState == FormWindowState.Maximized ? "还原" : "最大化";
                ContextMenuStripHelper.SetOpacity(this._defCms, _f.Opacity);
                this._defCms.Font = _f.Font;
            });
            this._tPanel.SuspendLayout();
            // 
            // maxLabel
            // 
            this._maxLabel.AutoSize = false;
            this._maxLabel.BackColor = System.Drawing.Color.Transparent;
            this._maxLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._maxLabel.Cursor = Cursors.Default;
            this._maxLabel.Top = 1;
            this._maxLabel.Name = "maxLabel";
            this._maxLabel.Text = "=";
            this._maxLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // minLabel
            // 
            this._minLabel.AutoSize = false;
            this._minLabel.BackColor = System.Drawing.Color.Transparent;
            this._minLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._minLabel.Top = 1;
            this._minLabel.Cursor = Cursors.Default;
            this._minLabel.Name = "minLabel";
            this._minLabel.Text = "—";
            this._minLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // 
            // endLabel
            // 
            this._endLabel.AutoSize = false;
            this._endLabel.BackColor = System.Drawing.Color.Transparent;
            this._endLabel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._endLabel.Top = 1;
            this._endLabel.Name = "endLabel";
            this._endLabel.Cursor = Cursors.Default;
            this._endLabel.Text = "X";
            this._endLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            //iconPictureBox
            //
            this._iconPictureBox.BackColor = System.Drawing.Color.Transparent;
            this._iconPictureBox.Cursor = Cursors.Arrow;
            this._iconPictureBox.Top = 1;
            this._iconPictureBox.Name = "iconPictureBox";
            this._iconPictureBox.BackgroundImageLayout = ImageLayout.Zoom;
            this._iconPictureBox.ContextMenuStrip = null;
            this._iconPictureBox.ParentChanged += new EventHandler((object sender, EventArgs e) => { resetControlLocation(); });
            // 
            // tPanel
            // 
            this._tPanel.Controls.Add(this._minLabel);
            this._tPanel.Controls.Add(this._maxLabel);
            this._tPanel.Controls.Add(this._endLabel);
            this._tPanel.Controls.Add(this._tLabel);
            this._tPanel.Controls.Add(this._iconPictureBox);
            this._tPanel.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this._tPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this._tPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this._tPanel.Name = "tPanel";
            this._tPanel.ContextMenuStrip = _defCms;
            this._tPanel.TabIndex = 0;
            this._tPanel.Disposed += new EventHandler((object sender, EventArgs e)=> { Disposed?.Invoke(this, e); });
            this._tPanel.MouseDown += (object sender, MouseEventArgs e) =>
            {
                if (e.Button == MouseButtons.Left)
                    if (Moveable)
                        FormHelper.Move(_f.Handle);
            };
            this._tPanel.SizeChanged += new EventHandler((object sender, EventArgs e) =>
            {
                this.resetControlLocation();
            });
            //
            //tLabel
            //
            this._tLabel.Left = _iconPictureBox.Right;
            this._tLabel.Top = 0;
            this._tLabel.AutoEllipsis = true;
            this._tLabel.AutoSize = false;
            this._tLabel.BackColor = Color.Transparent;
            this._tLabel.Height = _tPanel.Height;
            this._tLabel.Name = "tLabel";
            this._tLabel.MouseDown += (object sender, MouseEventArgs e) => 
            {
                if (e.Button == MouseButtons.Left)
                    if(Moveable)
                        FormHelper.Move(_f.Handle);
            };

            _f.SizeChanged += new EventHandler((object sender, EventArgs e) =>
            {
                if (_f.WindowState == FormWindowState.Maximized)
                {
                    //_maxLabel.Font = new Font(_maxLabel.Font.Name, 10.5f, _maxLabel.Font.Style);
                    _maxLabel.Text = "＝";
                    _maxLabel.Size = _minLabel.Size;
                }
                else
                {
                    //_maxLabel.Font = new Font(_maxLabel.Font.Name, 9.5f, _maxLabel.Font.Style);
                    _maxLabel.Text = "□";
                    _maxLabel.Size = _minLabel.Size;
                }
            });
            Icon = _f.Icon;
            BackColor = _f.BackColor;
            Title = _f.Text;
            TitleAlignment = ContentAlignment.MiddleLeft;
            Cms = _defCms;
            IconVisible = true;
            Style = Style;
            Font = new Font("SIMSUN", 18f, FontStyle.Regular, GraphicsUnit.Pixel);
            Height = 32;
            if (_f is IThemeBase)
                BackColor = (_f as IThemeBase).BorderColor;
            else
                BackColor = Drawing.ColorHelper.GetSimilarColor(_f.BackColor, true, Level.Level10);


            this.setControlButtons();
            this._tPanel.ResumeLayout(false);
        }

        private void closeWindow()
        {
            if (CloseToHide)
                _f.Hide();
            else
                _f.Close();
        }

        private void resetStyle(TitleBarStyles titleBarStyle)
        {
            this._tPanel.Controls.Add(this._minLabel);
            this._tPanel.Controls.Add(this._maxLabel);
            this._tPanel.Controls.Add(this._endLabel);
            switch (titleBarStyle)
            {
                case TitleBarStyles.None:
                    this._tPanel.Controls.Remove(_minLabel);
                    this._tPanel.Controls.Remove(_maxLabel);
                    this._tPanel.Controls.Remove(_endLabel);
                    break;
                case TitleBarStyles.MinOnly:
                    this._tPanel.Controls.Remove(_maxLabel);
                    this._tPanel.Controls.Remove(_endLabel);
                    break;
                case TitleBarStyles.MaxOnly:
                    this._tPanel.Controls.Remove(_minLabel);
                    this._tPanel.Controls.Remove(_endLabel);
                    break;
                case TitleBarStyles.EndOnly:
                    this._tPanel.Controls.Remove(_minLabel);
                    this._tPanel.Controls.Remove(_maxLabel);
                    break;
                case TitleBarStyles.MinMax:
                    this._tPanel.Controls.Remove(_endLabel);
                    break;
                case TitleBarStyles.MinEnd:
                    this._tPanel.Controls.Remove(_maxLabel);
                    break;
                case TitleBarStyles.MaxEnd:
                    this._tPanel.Controls.Remove(_minLabel);
                    break;
                case TitleBarStyles.MinMaxEnd:
                    break;
                default:
                    throw new Exception("titleBarStyle类型错误");
            }
            this._style = titleBarStyle;
            this.resetControlLocation();
        }

        private void setControlButtons()
        {
            EventHandler eh_enter = new EventHandler((object sender, EventArgs e) =>
            {
                ((Control)sender).BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(_tPanel.BackColor, false, NgNet.Level.Level5);
            });
            MouseEventHandler meh_down = new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                if (e.Button == MouseButtons.Left)
                    ((Control)sender).BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(_tPanel.BackColor, true, NgNet.Level.Level4);
            });
            MouseEventHandler meh_up = new MouseEventHandler((object sender, MouseEventArgs e) =>
            {
                ((Control)sender).BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor(_tPanel.BackColor, false, NgNet.Level.Level5);
            });
            EventHandler eh_leave = new EventHandler((object sender, EventArgs e) =>
            {
                ((Control)sender).BackColor = Color.Transparent;
            });
            if (_minLabel != null)
            {
                _minLabel.MouseClick += new MouseEventHandler((object sender, MouseEventArgs e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        _f.WindowState = FormWindowState.Minimized;
                });
                _minLabel.MouseEnter += new EventHandler(eh_enter);
                _minLabel.MouseDown += new MouseEventHandler(meh_down);
                _minLabel.MouseUp += new MouseEventHandler(meh_up);
                _minLabel.MouseLeave += new EventHandler(eh_leave); 
            }
            if (_maxLabel != null)
            {
                _maxLabel.MouseClick += new MouseEventHandler((object sender, MouseEventArgs e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        _f.WindowState = _f.WindowState == FormWindowState.Maximized ? FormWindowState.Normal : FormWindowState.Maximized;
                });
                _maxLabel.MouseEnter += new EventHandler(eh_enter);
                _maxLabel.MouseDown += new MouseEventHandler(meh_down);
                _maxLabel.MouseUp += new MouseEventHandler(meh_up);
                _maxLabel.MouseLeave += new EventHandler(eh_leave);
            }
            if (_endLabel != null)
            {
                _endLabel.MouseClick += new MouseEventHandler((object sender, MouseEventArgs e) =>
                {
                    if (e.Button == MouseButtons.Left)
                        closeWindow();
                });
                _endLabel.MouseEnter += new EventHandler(eh_enter);
                _endLabel.MouseDown += new MouseEventHandler(meh_down);
                _endLabel.MouseUp += new MouseEventHandler(meh_up);
                _endLabel.MouseLeave += new EventHandler(eh_leave);
            }
        }

        private void resetControlLocation()
        {
            int _left = _tPanel.Width;
            switch (Style)
            {
                case TitleBarStyles.None:
                    break;
                case TitleBarStyles.MinOnly:
                    _minLabel.Left = _tPanel.Width - _minLabel.Width - _BUTTON_PADDING_LEFT;
                    _left = _minLabel.Left;
                    break;
                case TitleBarStyles.MaxOnly:
                    _maxLabel.Left = _tPanel.Width - _maxLabel.Width - _BUTTON_PADDING_LEFT;
                    _left = _maxLabel.Left;
                    break;
                case TitleBarStyles.EndOnly:
                    _endLabel.Left = _tPanel.Width - _endLabel.Width - _BUTTON_PADDING_LEFT;
                    _left = _endLabel.Left;
                    break;
                case TitleBarStyles.MinMax:
                    _maxLabel.Left = _tPanel.Width - _maxLabel.Width - _BUTTON_PADDING_LEFT;
                    _minLabel.Left = _maxLabel.Left - _minLabel.Width;
                    _left = _minLabel.Left;
                    break;
                case TitleBarStyles.MinEnd:
                    _endLabel.Left = _tPanel.Width - _endLabel.Width - _BUTTON_PADDING_LEFT;
                    _minLabel.Left = _endLabel.Left - _minLabel.Width;
                    _left = _minLabel.Left;
                    break;
                case TitleBarStyles.MaxEnd:
                    _endLabel.Left = _tPanel.Width - _endLabel.Width - _BUTTON_PADDING_LEFT;
                    _maxLabel.Left = _endLabel.Left - _maxLabel.Width;
                    _left = _maxLabel.Left;
                    break;
                case TitleBarStyles.MinMaxEnd:
                    _endLabel.Left = _tPanel.Width - _endLabel.Width - _BUTTON_PADDING_LEFT;
                    _maxLabel.Left = _endLabel.Left - _maxLabel.Width;
                    _minLabel.Left = _maxLabel.Left - _minLabel.Width;
                    _left = _minLabel.Left;
                    break;
            }
            _tLabel.Left = IconVisible ? _iconPictureBox.Right + _ICON_PADDING_LEFT : _PADDING_RIGHT;
            _tLabel.Width = _left - _tLabel.Left;
        }

        private void setMenuRender()
        {
            MenuRenderColors mrc = new MenuRenderColors();
            mrc.DropDownItemBorderColor = BackColor;
            mrc.DropDownItemEndColor = Drawing.ColorHelper.GetSimilarColor(BackColor, false, Level.Level7);
            mrc.DropDownItemStartColor = Drawing.ColorHelper.GetSimilarColor(BackColor, false, Level.Level3);
            mrc.DropDownBackEndColor = Color.White;
            mrc.DropDownBackStartColor = Drawing.ColorHelper.GetSimilarColor(BackColor, false, Level.Level7);
            mrc.MarginEndColor = mrc.DropDownBackEndColor;
            mrc.MarginStartColor = mrc.DropDownBackStartColor;
            mrc.DropDownBorderColor = BackColor;
            mrc.FontColor = Drawing.ColorHelper.GetSimilarColor(BackColor, true, Level.Level16);
            mrc.SeparatorColor = mrc.FontColor;
            this._defCms.Renderer = new MenuRender(mrc);
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示标题栏
        /// </summary>
        public void Show()
        {
            _f.Controls.Add(_tPanel);
        }
        /// <summary>
        /// 隐藏标题栏
        /// </summary>
        public void Hide()
        {
            _f.Controls.Remove(_tPanel);
        }
        /// <summary>
        /// 添加自定义菜单到默认菜单
        /// </summary>
        /// <param name="text"></param>
        /// <param name="evt"></param>
        /// <returns></returns>
        public ToolStripMenuItem AddItemTODefaultMenu(string text, EventHandler evt)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            if (_defMenuCount++ == 0)
                _defCms.Items.Add("-");
            ToolStripMenuItem _tsmi = new ToolStripMenuItem();
            _tsmi.Text = text;
            _tsmi.Click += evt;
            _defCms.Items.Add(_tsmi);
            return _tsmi;
        }
        /// <summary>
        /// 绑定事件到默认菜单弹出事件
        /// </summary>
        /// <param name="evt"></param>
        public void WhenDefaultMenuOpening(CancelEventHandler evt)
        {
            _defCms.Opening += evt;
        }
        #endregion
    }
}
