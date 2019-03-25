using NgNet.UI.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    partial class ImageBoxF : TitleableForm, ICommBoxWindow
    {
        #region private fields
        private double _pers = 1d;

        private string _path = string.Empty;

        private string _fixedTitle = string.Empty;

        private bool _autoClose = false;

        private bool _move;

        private Point _loc;

        private int _xp, _yp, _wc, _hc;
        #endregion

        #region public properties
        public string Path
        {
            get
            {
                return _path;
            }
            private set
            {
                _path = value;
            }
        }
        /// <summary>
        /// 获取或设置当前显示的图片
        /// </summary>
        public Image CurrentImage
        {
            get
            {
                return picBox.BackgroundImage;
            }
            set
            {
                SetImage(value);
            }
        }
        /// <summary>
        /// 获取或设置一个值该值指示用户是否可以编辑
        /// </summary>
        public bool Editable { get; set; } 
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
        /// 获取或设置一个值该值指示当窗口失去焦点时是否自动关闭窗口
        /// </summary>
        public bool AutoClose
        {
            get
            {
                return _autoClose;
            }
            set
            {
                this.Deactivate -= this_Deactivate;
                this.Deactivate += this_Deactivate;
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
        #endregion

        #region constructor
        public ImageBoxF()
        {
            InitializeComponent();
            Resizeable = true;
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            MaximumSize = MaximizedBounds.Size;
            FormHelper.SetFormRoundRgn(3, 3);
            FormHelper.AutoBorder(new UI.BorderSize(), new UI.BorderSize(1, 2, 1, 1));
        }
        #endregion

        #region private methods
        private void reLocation(bool _bool)
        {
            if (picBox.Width > ContentPanel.Width)
            {
                _xp = _bool ? picBox.Left - (_wc / 2) : picBox.Left;
                if (_xp > 0)
                    _xp = 0;
                else if (picBox.Right < ContentPanel.Width)
                    _xp = ContentPanel.Width - picBox.Width;
            }
            else
            {
                _xp = (ContentPanel.Width - picBox.Width) >> 1;
            }
            if (picBox.Height > ContentPanel.Height)
            {
                _yp = _bool ? picBox.Top - (_hc / 2) : picBox.Top;
                if (_yp > 0)
                    _yp = 0;
                else if (picBox.Bottom < ContentPanel.Height)
                    _yp = ContentPanel.Height - picBox.Height;
            }
            else
            {
                _yp = (ContentPanel.Height - picBox.Height) >> 1;
            }
            picBox.Location = new Point(_xp, _yp);
        }

        private void this_Load(object sender, EventArgs e)
        {
            FormHelper.SetWindowRoundRgn(this, 6, 6);
            FormHelper.CenterToOwner(this);
        }

        private void this_Deactivate(object sender, EventArgs e)
        {
            Close();
        }

        private void _Panel_SizeChanged(object sender, EventArgs e)
        {
            reLocation(false);
        }

        private void picBox_SizeChanged(object sender, EventArgs e)
        {

        }

        private void picBox_MouseWheel(object sender, MouseEventArgs e)
        {
            int _w = picBox.Width + e.Delta;
            int _h = (int)(_w * _pers);
            _w = _w < 20 ? 20 : (_w > 3840 ? 3840 : _w);
            _h = _h < 20 ? 20 : (_h > 3840 ? 3840 : _h);
            _wc = _w - picBox.Width;
            _hc = _h - picBox.Height;
            picBox.Size = new Size(_w, _h);
            reLocation(true);
        }

        private void picBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (picBox.Left >= 0 && picBox.Right <= ContentPanel.Width && picBox.Top >= 0 && picBox.Bottom <= picBox.Height)
            {
                return;
            }
            else if(e.Button == MouseButtons.Left)
            {
                _move = true;
                picBox.Cursor = Cursors.NoMove2D;
                _loc = e.Location;
            }
        }

        private void picBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_move)
                return;
            if(picBox.Height > ContentPanel.Height)
            {
                int _y_ = e.Location.Y - _loc.Y;
                if(_y_ > 0)
                {
                    if (picBox.Top < 0)
                        picBox.Top = picBox.Top + _y_;
                }else if(_y_ < 0)
                {
                    if (picBox.Bottom > ContentPanel.Height)
                        picBox.Top = picBox.Top + _y_;
                }
            }
            if(picBox.Width > ContentPanel.Width)
            {
                int _x_ = e.Location.X - _loc.X;
                if (_x_ > 0)
                {
                    if (picBox.Left < 0)
                        picBox.Left = picBox.Left + _x_;
                }
                else if (_x_ < 0)
                {
                    if (picBox.Right > ContentPanel.Width)
                        picBox.Left = picBox.Left + _x_;
                }
            }
        }

        private void picBox_MouseUp(object sender, MouseEventArgs e)
        {
            _move = false;
            picBox.Cursor = Cursors.Arrow;
        }
        #endregion

        #region public methods
        public void SetImage(Image img)
        {
            picBox.BackgroundImage = img;
            picBox.BackgroundImageLayout = ImageLayout.Zoom;
            if (img != null)
            {
                _pers = (double)img.Height / img.Width;
                picBox.MouseWheel -= picBox_MouseWheel;
                picBox.MouseWheel += picBox_MouseWheel;
                ContentPanel.MouseWheel -= picBox_MouseWheel;
                ContentPanel.MouseWheel += picBox_MouseWheel;
                Size = img.Size;
                picBox.Size = ContentPanel.Size;
                picBox.Location = new Point(0);
            }
        }

        public void OpenFile(string path)
        {
            SetImage(Image.FromFile(path));
        }
        #endregion
    }
}
