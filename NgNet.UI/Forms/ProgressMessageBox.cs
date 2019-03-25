using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace NgNet.UI.Forms
{
    public class ProgressMessageBox : IDisposable, IComponent, IThemeBase
    {
        #region private fields
        private Form _f;
        private Label _label;
        private Form  _owner;
        private Timer _timer;
        private string _message;
        private uint _displayTime = 0;
        private uint _tempDisplayTime = 0;
        private float _dispalyedTime = 0;
        private int _interval = 100;

        private Size  _clientSize = new Size(200, 108);
        private bool  _autoSize = false;
        private Color _backColor = Color.Black;
        private Color _foreColor = Color.LightSeaGreen;
        private Color _borderColor = Color.Green;
        private Font  _font = new Font("SIMSUN", 22, FontStyle.Regular, GraphicsUnit.Pixel);
        #endregion

        #region public properties
        /// <summary>
        /// 指示搜索栏是否显示
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(_f);
            }
        }

        /// <summary>
        /// 获取或设置消息框是否可见
        /// </summary>
        public bool Visible
        {
            set
            {
                if (IsLoaded)
                    _f.Visible = value;
            }
        }

        /// <summary>
        /// 默认的消息框大小
        /// </summary>
        public Size ClientSize
        {
            get
            {
                return _clientSize;
            }
            set
            {
                _clientSize = value;
                if (AutoSize == false && IsLoaded)
                    _f.Size = value;
            }
        }

        /// <summary>
        /// 消息的显示时长
        /// </summary>
        public uint DisplayTime
        {
            get { return _displayTime; }
            set
            {
                _dispalyedTime = 0;
                if (value < _interval)
                    value = (uint)_interval;
                _displayTime = value;
                if(_timer != null)
                    _timer.Enabled = value != 0;
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否根据消息自动设置客户端大小
        /// </summary>
        public bool AutoSize
        {
            get
            {
                return _autoSize;
            }
            set
            {
                _autoSize = value;
                if (IsLoaded)
                    _f.Size = value ? getMessageSize(_message) : ClientSize;

            }
        }

        public Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                _backColor = value;
                if (IsLoaded)
                    _f.BackColor = value;
            }
        }

        public Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                _foreColor = value;
                if (IsLoaded)
                    _label.ForeColor = value;
            }
        }

        public Color BorderColor { get
            {
                return _borderColor;
            }
            set
            {
                _borderColor = value;
                if (IsLoaded)
                    _f.BackColor = value;
            }
        }

        public Font Font
        {
            get
            {
                return _font;
            }
            set
            {
                _font = value;
                if (IsLoaded)
                    _label.Font = value;
            }
        }

        public IWin32Window Owner
        {
            get { return _owner; }
        }
        #endregion

        #region constructor destructor 
        public ProgressMessageBox(Form owner)
        {
            if (owner == null)
                throw new NullReferenceException();
            _owner = owner;
            if (owner is IThemeBase)
                SetTheme(owner as IThemeBase);
        }
        #endregion

        #region public method
        /// <summary>
        /// 显示处理消息框
        /// </summary>
        /// <param name="message"></param>
        public void Show(string message)
        {
            _tempDisplayTime = 0;
            showCore(message);
        }

        /// <summary>
        /// 显示处理消息框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="displayTime"></param>
        public void Show(string message, uint displayTime)
        {
            _tempDisplayTime = displayTime;
            showCore(message);

        }
        /// <summary>
        /// 关闭消息框
        /// </summary>
        public void Close()
        {
            if (IsLoaded)
                _f.Close();
        }
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="t"></param>
        public void SetTheme(IThemeBase t)
        {
            BackColor = t.BackColor;
            ForeColor = t.ForeColor;
            BorderColor = t.BorderColor;
        }
        #endregion

        #region private method
        private void showCore(string message) 
        {
            if (string.IsNullOrWhiteSpace(message))
                return;
            _message = message;
            if (!IsLoaded)
            {
                _label = new Label();
                _timer = new Timer();
                _f = new Form();
                // 绑定事件
                if (_owner != null)
                {
                    bindEvents();
                }
                _label.AutoSize = false;
                _label.BackColor = BackColor;
                _label.Dock = DockStyle.Fill;
                _label.Font = Font;
                _label.TextAlign = ContentAlignment.MiddleCenter;
                _label.ForeColor = ForeColor;

                _timer.Interval = _interval;

                _f.AutoScaleDimensions = new SizeF(6F, 12F);
                _f.ClientSize = AutoSize ? getMessageSize(_message) : ClientSize;
                _f.FormBorderStyle = FormBorderStyle.None;
                _f.Opacity = 0.7d;
                _f.ShowInTaskbar = false;
                _f.ShowIcon = false;
                _f.BackColor = BorderColor;
                _f.Owner = _owner;
                _f.StartPosition = FormStartPosition.Manual;
                _f.Controls.Add(_label);
                _f.Padding = new Padding(1, 3, 1, 3);
                _f.ImeMode = ImeMode.Close;
                _f.ResumeLayout(true);
                _f.Visible = false;
                _f.Show(_owner);
                _f.Disposed += new EventHandler((object sender, EventArgs e) => { Disposed?.Invoke(this, new EventArgs()); });
            }
            // 每次设置消息都将已经显示的时间设置为0
            _dispalyedTime = 0;
            _timer.Enabled = DisplayTime != 0 || _tempDisplayTime != 0;
            _label.Text = message;
            _label.Refresh();
        }

        private void setLocation(object sender, EventArgs e)
        {
            if (this.IsLoaded)
            {
                FormHelper.CenterToOwner(this._f);
            }
        }

        private void setVisible(object sender, EventArgs e)
        {
            if (_owner == null)
                return;
            _f.Visible = _owner.Visible;
            if (_owner.Visible)
                setLocation(_f, null);
        }

        private void setLoad(object sender, EventArgs e)
        {
            setLocation(null, null);
        }

        private void setShown(object sender, EventArgs e)
        {
            _f.Visible = FormHelper.IsShown(_owner);
        }

        private void setTick(object sender, EventArgs e)
        {
            _dispalyedTime += _interval;
            if(_tempDisplayTime > 0)
            {
                if (_dispalyedTime >= _tempDisplayTime)
                {
                    _tempDisplayTime = 0;
                    _timer.Stop();
                    Close();
                }
            }
            else{
                if (_dispalyedTime >= DisplayTime)
                {
                    _timer.Stop();
                    Close();
                }
            }
        }

        private void bindEvents()
        {
            _owner.SizeChanged += new EventHandler(setLocation);
            _owner.LocationChanged += new EventHandler(setLocation);
            _owner.VisibleChanged += new EventHandler(setVisible);
            _f.Load += new EventHandler(setLoad);
            _f.Shown += setShown;
            _timer.Tick += new EventHandler(setTick);
            _f.FormClosed += new FormClosedEventHandler((object sender, FormClosedEventArgs e) =>
            {
                _owner.SizeChanged -= new EventHandler(setLocation);
                _owner.LocationChanged -= new EventHandler(setLocation);
                _owner.VisibleChanged -= new EventHandler(setVisible);
            });
        }

        private Size getMessageSize(string msg)
        {
            Graphics g = _label.CreateGraphics();
            Size size = g.MeasureString(msg, Font).ToSize();
            return new Size(size.Width + 16, size.Height + 12);
        }
        #endregion

        #region IComponent
        public ISite Site
        {
            get
            {
                return _f?.Site;
            }
            set
            {
                if(IsLoaded)
                    _f.Site = value;
            }
        }

        public event EventHandler Disposed;
        #endregion

        #region IDisposible
        public void Dispose()
        {
            _f?.Dispose();
            _timer?.Dispose();
            _font?.Dispose();
            GC.Collect();
        }
        #endregion
    }
}
