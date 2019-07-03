using System;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
namespace NgNet.UI.Forms
{
    #region <static>
    public partial class FormHelper
    {
        #region private fields
        private const int GCL_STYLE = -26;
        #endregion

        #region constructor
        static FormHelper()
        {

        }
        #endregion

        #region 移动窗体
        /// <summary>
        /// 移动窗体,在MouseDown事件中使用
        /// </summary>
        /// <param name="hwnd">当前窗体的句柄</param>
        public static void Move(IntPtr hwnd)
        {
            Windows.Apis.User32.ReleaseCapture();
            Windows.Apis.User32.SendMessage(hwnd, Windows.Apis.Const.WM_SYSCOMMAND, Windows.Apis.Const.SC_MOVE + Windows.Apis.Const.HTCAPTION, 0);
        }
        #endregion

        #region 窗体圆角
        /// <summary>
        /// 在SizeChanged事件中使用本方法
        /// </summary>
        /// <param name="form"></param>
        /// <param name="nWidthEllipse"></param>
        /// <param name="nHeightEllipse"></param>
        public static void SetWindowRoundRgn(Form form, int nWidthEllipse, int nHeightEllipse)
        {
            int hRgn = Windows.Apis.Gdi32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, nWidthEllipse, nHeightEllipse);
            Windows.Apis.User32.SetWindowRgn(form.Handle, hRgn, true);
            Windows.Apis.Gdi32.DeleteObject(hRgn);
        }
        #endregion

        #region 窗体阴影
        /// <summary>
        /// 设置窗体阴影， 在SizeChanged事件中使用本方法
        /// </summary>
        /// <param name="hwnd">输入窗体句柄</param>
        public static void SetShadow(IntPtr hwnd)
        {
            Windows.Apis.User32.SetClassLong(hwnd, GCL_STYLE, Windows.Apis.User32.GetClassLong(hwnd, GCL_STYLE) | (int)Windows.ClassStyles.CS_DROPSHADOW);
        }
        #endregion

        #region 设置窗体位置
        /// <summary>
        /// 将指定窗体居中到他的所有者
        /// </summary>
        /// <param name="f"></param>
        public static void CenterToOwner(Form f)
        {
            if (f.Owner == null || f.Owner.WindowState == FormWindowState.Minimized || f.Owner.Visible == false)
                FormHelper.CenterToScreen(f);
            else
                f.Location = f.Owner.PointToScreen(new Point((f.Owner.Width - f.Width) / 2, (f.Owner.Height - f.Height) / 2));
        }
        /// <summary>
        /// 将窗体居中到指定窗体
        /// </summary>
        /// <param name="f"></param>
        /// <param name="toCenter"></param>
        public static void CenterToWindow(Form f, Form toCenter)
        {
            f.Location = toCenter.PointToScreen(new Point((toCenter.Width - f.Width) / 2, (toCenter.Height - f.Height) / 2));
        }
        /// <summary>
        /// 将窗体居中到屏幕中央
        /// </summary>
        /// <param name="f"></param>
        public static void CenterToScreen(Form f)
        {
            f.Location = new Point((Screen.PrimaryScreen.WorkingArea.Right - f.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Bottom - f.Height) / 2);
        }
        #endregion

        #region 显示窗体
        /// <summary>
        /// 显示已经打开过的窗体
        /// </summary>
        /// <param name="f"></param>
        public static void Display(Form f)
        {
            if (f.Visible == false)
                f.Show();
            else if (f.WindowState == FormWindowState.Minimized)
                f.WindowState = FormWindowState.Normal;
            f.Activate();
        }
        #endregion

        public enum AnimateWindowFlags : int
        {
            /// <summary>
            /// 从左到右打开窗口
            /// </summary>
            HOR_POSITIVE = 0x00000001,
            /// <summary>
            /// 从右到左打开窗口
            /// </summary>
            HOR_NEGATIVE = 0x00000002,
            /// <summary>
            /// 从上到下打开窗口
            /// </summary>
            VER_POSITIVE = 0x00000004,
            /// <summary>
            /// 从下到上打开窗口
            /// </summary>
            VER_NEGATIVE = 0x00000008,
            /// <summary>
            /// 若使用了AW_HIDE标志，则使窗口向内重叠；若未使用AW_HIDE标志，则使窗口向外扩展。
            /// </summary>
            CENTER = 0x00000010,
            /// <summary>
            /// 在窗体卸载时若想使用本函数就得加上此常量
            /// </summary>
            HIDE = 0x00010000,
            /// <summary>
            /// 激活窗口。在使用了AW_HIDE标志后不要使用这个标志。
            /// </summary>
            ACTIVATE = 0x00020000,
            /// <summary>
            /// 使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略。
            /// </summary>
            SLIDE = 0x00040000,
            /// <summary>
            /// 使用淡出效果。只有当hWnd为顶层窗口的时候才可以使用此标志。
            /// </summary>
            BLEND = 0x00080000
        }

        public static void AnimateWindow(IntPtr handle, int ms, AnimateWindowFlags flags)
        {
            Windows.Apis.User32.AnimateWindow(handle, ms, (int)flags);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="hWnd"></param>
        public static void CloseWindow(IntPtr hWnd)
        {
            Windows.Apis.User32.SendMessage(hWnd, 16, 0, 0);
            // Win32.PostMessage(hWnd, 0x0112, (IntPtr)0xF060, IntPtr.Zero);  
        }
        /// <summary>
        /// 调整窗体大小
        /// </summary>
        /// <param name="m"></param>
        /// <param name="f"></param>
        /// <returns></returns>
        public static void Resize(ref Message m, Form f)
        {
            if (m.Msg != 0x0084)
                return;
            if (f.WindowState == FormWindowState.Maximized)
                return;
            Point vPoint = new Point((int)m.LParam & 0xFFFF, (int)m.LParam >> 16 & 0xFFFF);
            vPoint = f.PointToClient(vPoint);
            if (vPoint.X <= 5)
                if (vPoint.Y <= 5)
                    m.Result = (IntPtr)Windows.Apis.Const.HTTOPLEFT;
                else if (vPoint.Y >= f.ClientSize.Height - 5)
                    m.Result = (IntPtr)Windows.Apis.Const.HTBOTTOMLEFT;
                else m.Result = (IntPtr)Windows.Apis.Const.HTLEFT;
            else if (vPoint.X >= f.ClientSize.Width - 5)
                if (vPoint.Y <= 5)
                    m.Result = (IntPtr)Windows.Apis.Const.HTTOPRIGHT;
                else if (vPoint.Y >= f.ClientSize.Height - 5)
                    m.Result = (IntPtr)Windows.Apis.Const.HTBOTTOMRIGHT;
                else m.Result = (IntPtr)Windows.Apis.Const.HTRIGHT;
            else if (vPoint.Y <= 5)
                m.Result = (IntPtr)Windows.Apis.Const.HTTOP;
            else if (vPoint.Y >= f.ClientSize.Height - 5)
                m.Result = (IntPtr)Windows.Apis.Const.HTBOTTOM;
        }
        /// <summary>
        /// 指示指定的窗体是否已经加载
        /// </summary>
        /// <param name="f">窗体</param>
        /// <returns></returns>
        public static bool IsLoaded(Form f)
        {
            if (f == null)
                return false;
            return f.IsHandleCreated && f.IsDisposed == false;
        }
        /// <summary>
        /// 指示指定的窗体是否已经加载并且可见
        /// </summary>
        /// <param name="f">窗体</param>
        /// <returns></returns>
        public static bool IsShown(Form f)
        {
            return IsLoaded(f) && f.Visible;
        }
    }
    #endregion

    #region <instance>
    public partial class FormHelper
    {
        private Form form;

        #region construct function
        public FormHelper(Form form)
        {
            this.form = form;
            this.initOpacity();
        }
        #endregion

        #region  窗体缓慢显示或缓慢隐藏（Opacity）
        private bool  _opacity_boolClose;

        private bool  _opacity_boolUp;

        private float _opacity_setp;

        private float _opactiy_endOpactiy;

        private Timer _opactiy_timer;

        private void  _opactiy_timer_tick(object sender, EventArgs e)
        {
            //判断是增加还是减少
            if (_opacity_boolUp)
                if (form.Opacity >= _opactiy_endOpactiy)
                    _opactiy_timer.Enabled = false;
                else
                    form.Opacity += _opacity_setp;
            else if (form.Opacity <= _opactiy_endOpactiy)
            {
                if (_opacity_boolClose)
                    CloseWindow(form.Handle);
                _opactiy_timer.Enabled = false;
            }
            else
                form.Opacity -= _opacity_setp;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void initOpacity()
        {
            _opactiy_timer = new Timer();
            _opactiy_timer.Tick += new EventHandler(_opactiy_timer_tick);

        }
        /// <summary>
        /// 增大窗体的透明度
        /// </summary>
        /// <param name="step">每次降低多少</param>
        /// <param name="bClose">完成后是否关闭窗体</param>
        /// <param name="time">频率</param>
        public void OpacityDown(float step, bool bClose, float endOpacity = 0, uint time = 100)
        {
            this._opacity_setp = step;
            this._opacity_boolUp = false;
            this._opactiy_endOpactiy = endOpacity;
            this._opacity_boolClose = bClose;

            _opactiy_timer.Interval = (int)time;
            _opactiy_timer.Enabled = true;
        }
        /// <summary>
        /// 降低窗体的透明度
        /// </summary>
        /// <param name="step">每次降低多少</param>
        /// <param name="EndOpacity">降低到的最低透明度</param>
        /// <param name="time">频率</param>
        public void OpacityUp(float step, float EndOpacity, uint time = 100)
        {
            this._opacity_setp = step;
            this._opacity_boolUp = true;
            this._opactiy_endOpactiy = EndOpacity;

            _opactiy_timer.Interval = (int)time;
            _opactiy_timer.Enabled = true;
        }
        #endregion

        #region 最大化最小化时自动设置Border（Padding原理）
        private BorderSize _borderMaxSize = new BorderSize();

        private BorderSize _borderNorSize = new  BorderSize(3, 1, 3, 3);
        /// <summary>
        /// 初始化
        /// </summary>
        private void _AutoBorder(object sender, EventArgs e)
        {
            if (form.WindowState == FormWindowState.Maximized)
                form.Padding = new Padding(_borderMaxSize.Left, _borderMaxSize.Top, _borderMaxSize.Right, _borderMaxSize.Bottom);
            else if (form.WindowState == FormWindowState.Normal)
                form.Padding = new Padding(_borderNorSize.Left, _borderNorSize.Top, _borderNorSize.Right, _borderNorSize.Bottom);
        }
        /// <summary>
        /// WinForm最大化最小化时自动设置Padding，此设置不会立即出发窗体的SizeChanged事件，但可能导致窗体的子控件大小改变
        /// </summary>
        /// <param name="maxPadding">最大化时的Padding</param>
        /// <param name="norPadding">正常时的Padding</param>
        public void AutoBorder(BorderSize max, BorderSize nor)
        {
            _borderMaxSize = max;
            _borderNorSize = nor;
            form.SizeChanged -= new EventHandler(_AutoBorder);
            form.SizeChanged += new EventHandler(_AutoBorder);
            _AutoBorder(this, null);
        }
        /// <summary>
        /// 是否启用自动Padding，如果启用则为上次设置的值
        /// </summary>
        /// <param name="enable"></param>
        public void AutoBorder(bool enable)
        {
            form.SizeChanged -= new EventHandler(_AutoBorder);
            if (enable)
                form.SizeChanged += new EventHandler(_AutoBorder);
        }
        #endregion

        #region 窗体圆角
        private int _formRoundRgn_wEllipse = 3;

        private int _formRoundRgn_hEllipse = 3;

        private void _formRoundRgn(object sender, EventArgs e)
        {
            int hRgn = Windows.Apis.Gdi32.CreateRoundRectRgn(0, 0, form.Width + 1, form.Height + 1, _formRoundRgn_wEllipse, _formRoundRgn_hEllipse);
            Windows.Apis.User32.SetWindowRgn(form.Handle, hRgn, true);
            Windows.Apis.Gdi32.DeleteObject(hRgn);
        }

        /// <summary>
        /// 设置窗体圆角
        /// </summary>
        /// <param name="wEllipse">圆角宽度逻辑单位（像素）</param>
        /// <param name="hEllipse">圆角高度逻辑单位（像素）</param>
        public void SetFormRoundRgn(int wEllipse, int hEllipse)
        {
            _formRoundRgn_wEllipse = wEllipse;
            _formRoundRgn_hEllipse = hEllipse;
            form.SizeChanged -= new EventHandler(_formRoundRgn);
            form.SizeChanged += new EventHandler(_formRoundRgn);
        }
        /// <summary>
        /// 是否启用窗体圆角，如果启用则圆角为上次设置的值
        /// </summary>
        /// <param name="enable"></param>
        public void SetFormRoundRgn(bool enable)
        {
            form.SizeChanged -= new EventHandler(_formRoundRgn);
            if (enable)
                form.SizeChanged += new EventHandler(_formRoundRgn);
        }
        #endregion

        #region 窗体阴影

        #endregion

        #region 移动窗体
        private int _move_xpos, _move_ypos;

        private bool _moveBool = false;

        private void _move(object sender, MouseEventArgs e)
        {
            Move(form.Handle);
        }

        private void _move_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _moveBool = true;
                _move_xpos = e.X;
                _move_ypos = e.Y;
            }
        }

        private void _move_MouseMove(object sender, MouseEventArgs e)
        {
            if (_moveBool)
            {
                form.Location = new Point(form.Left + e.X - _move_xpos, form.Top + e.Y - _move_ypos);
            }
        }

        private void _move_MouseUp(object sender, MouseEventArgs e)
        {
            _moveBool = false;
        }

        public void Move(Control ctr, bool showContent = false)
        {
            ctr.MouseDown -= new MouseEventHandler(_move);
            ctr.MouseDown -= new MouseEventHandler(_move_MouseDown);
            ctr.MouseMove -= new MouseEventHandler(_move_MouseMove);
            ctr.MouseUp -= new MouseEventHandler(_move_MouseUp);

            if (showContent)
            {
                ctr.MouseDown += new MouseEventHandler(_move_MouseDown);
                ctr.MouseMove += new MouseEventHandler(_move_MouseMove);
                ctr.MouseUp += new MouseEventHandler(_move_MouseUp);
            }
            else
            {
                ctr.MouseDown += new MouseEventHandler(_move);
            }
        }
        #endregion
    }
    #endregion
}
