using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace NgNet.UI.Forms
{
    public class HotMessageBox : IDisposable, IComponent
    {
        #region private field
        private Form _f = null;
        private Label _label = null;
        // 自动退出计时器
        private Timer _timer = null;
        // 在非自动调整大小时消息框大小
        private Size _clientSize = new Size(168, 40);
        private bool _autoSize = false;
        private Color _backColor = Color.Black;
        private Color _foreColor = Color.LightSeaGreen;
        private Font _font = new Font("SIMYOU", 30F, FontStyle.Bold, GraphicsUnit.Pixel, ((byte)(134)));
        //要显示的消息
        private string _message = string.Empty;
        //消息已经显示的时长
        private uint _timeDisplayed = 0;
        //timer的interval
        private uint _interval = 30;

        private double _opacityStart = 0.7d;
        #endregion

        #region public properties
        /// <summary>
        /// 指示窗口是否显示
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(_f);
            }
        }

        /// <summary>
        /// 在非自动调整大小时消息框大小
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
                if (!AutoSize && IsLoaded)
                    _f.Size = value;
            }
        }

        /// <summary>
        /// 消息的显示时长
        /// </summary>
        public uint DisplayTime { get; set; }

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
                {
                    _label.Font = value;
                    if(AutoSize)
                        _f.Size = getMessageSize(_message);
                }
            }
        }
        #endregion

        #region constructor destructor 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="showTime">消息显示时长</param>
        public HotMessageBox(uint showTime)
        {
            DisplayTime = showTime;
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _f?.Dispose();
            _font?.Dispose();
            _timer?.Dispose();
            GC.Collect();
        }
        #endregion

        #region IComponent
        public ISite Site
        {
            get
            {
                return _f.Site;
            }
            set
            {
                _f.Site = value;
            }
        }

        public event EventHandler Disposed;
        #endregion

        #region private methods
        private void show()
        {
            if (IsLoaded)
                return;
            _f = new FormEx();
            _label = new Label();
            _timer = new Timer();

            //label
            _label.BackColor = Color.Transparent;
            _label.Dock = DockStyle.Fill;
            _label.Font = Font;
            _label.AutoSize = false;
            _label.ForeColor = ForeColor;
            _label.Text = _message;
            _label.TextAlign = ContentAlignment.MiddleCenter;
            _label.Update();

            //fm
            _f.AutoScaleDimensions = new SizeF(6F, 12F);
            _f.BackColor = BackColor;
            _f.ClientSize = AutoSize ? getMessageSize(_message) : ClientSize;
            _f.FormBorderStyle = FormBorderStyle.None;
            _f.Opacity = _opacityStart;
            _f.ShowIcon = false;
            _f.ShowInTaskbar = false;
            _f.StartPosition = FormStartPosition.CenterScreen;
            _f.TopMost = true;
            _f.ImeMode = ImeMode.Close;
            _f.SizeChanged += new EventHandler(_f_SizeChanged);
            _f.Load += new EventHandler(_f_Load);
            _f.Controls.Add(_label);
            _f.ResumeLayout(false);
            _f.Disposed += new EventHandler((object sender, EventArgs e) => { Disposed?.Invoke(this, new EventArgs()); });
            //timer
            _timer.Interval = (int)_interval;
            _timer.Tick += new EventHandler(_timer_Tick);

            _f.Show();
        }

        private void _f_Load(object sender, EventArgs e)
        {
            FormHelper hForm = new FormHelper(_f);
            hForm.SetFormRoundRgn(3, 3);
            _timer.Enabled = DisplayTime > 0;
        }

        private void _timer_Tick(object sender, EventArgs e)
        {
            _timeDisplayed += _interval;
            if (_timeDisplayed > DisplayTime)
            {
                _f.Opacity -= 0.03;
                if (_f.Opacity == 0)
                {
                    _timer.Stop();
                    _f.Close();
                }
            }
        }

        private void _f_SizeChanged(object sender, EventArgs e)
        {
            FormHelper.CenterToScreen(_f);
        }
        /// <summary>
        /// 获取指定信息在当前字体下显示的长宽
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private Size getMessageSize(string msg)
        {
            Graphics g = _label.CreateGraphics();
            Size size = g.MeasureString(msg, Font).ToSize();
            return new Size(size.Width + 12, size.Height + 6);
        }
        #endregion

        #region public methods
        /// <summary>
        /// 显示热消息
        /// </summary>
        /// <param name="msg"></param>
        public void Show(string msg)
        {
            if (string.IsNullOrWhiteSpace(msg))
                return;
            _message = msg;
            _timeDisplayed = 0;
            if (IsLoaded)
            {
                _f.Opacity = 0.7D;
                _label.Text = msg;
                if (AutoSize)
                {
                    _f.ClientSize = getMessageSize(msg);
                }
            }
            else
            {
                show();
            }
        }
        #endregion


    }
}
