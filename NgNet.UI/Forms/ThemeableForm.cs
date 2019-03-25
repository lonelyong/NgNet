using System;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public partial class ThemeableForm : Form, ITheme
    {
        #region private fields
        private Color _borderColorCache;

        private Color _borderColorCache1;

        private bool _isModalSetBorderColor;

        private int _index=0;

        private ThemeableForm _modal;

        private Color _modalSetBorderColor
        {
            set
            {
                _isModalSetBorderColor = true;
                BorderColor = value; 
                _isModalSetBorderColor = false;
            }
        }
        #endregion

        #region protected properties
        protected FormHelper FormHelper { get; }
        #endregion

        #region public properties
        public new virtual Color BackColor
        {
            get
            {
                return ContentPanel.BackColor;
            }
            set
            {
                ContentPanel.BackColor = value;
            }
        }

        public virtual Color BorderColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                if (!_isModalSetBorderColor)
                {
                    _borderColorCache = value;
                    _borderColorCache1 = Drawing.ColorHelper.GetOppositeColor(value);     
                } 
            }
        }

        /// <summary>
        /// 获取一个值，该值指示窗体有没有已经显示
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return FormHelper.IsLoaded(this);
            }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示是否启用淡入淡出效果
        /// </summary>
        public bool Blendable { get; set; }

        /// <summary>
        /// 获取或设置一个值该值指示是否允许用户修改窗体大小
        /// </summary>
        public bool Resizeable { get; set; }
        /// <summary>
        /// 获取或设置边框大小
        /// </summary>
        public BorderSize BorderSize
        {
            get
            {
                return new BorderSize(Padding.Left, Padding.Top, Padding.Right, Padding.Bottom);
            }
            set
            {
                Padding = new Padding(value.Left, value.Top, value.Right, value.Bottom);
            }
        }

        /// <summary>
        /// 点击任务栏最小化或还原
        /// </summary>
        public bool TaskbarSetWindowState { get; set; }

        /// <summary>
        /// 首次加载时是否自动缩放， 默认为true
        /// </summary>
        public bool AutoScaleEnable { get; set; } 
        #endregion

        #region constructor
        public ThemeableForm()
        {
            InitializeComponent();
            FormHelper = new FormHelper(this);
            AutoScaleEnable = true;
            Init();
        }
        #endregion

        #region private methods
        private void Init()
        {
            _borderColorCache = BorderColor;
        }

        private void ModalTimer_Tick(object sender, EventArgs e)
        {
            _index++;
            _modal._modalSetBorderColor = _index % 2 == 0 ? _modal._borderColorCache1 : _modal._borderColorCache;
            if (_index >= 10)
            {
                modalTimer.Enabled = false;
                _modal.BorderColor = _modal._borderColorCache;
            }
        }

        private Form FindModal(Form f)
        {
            if(f.OwnedForms.Length == 0)
            {
                return null;
            }
            else
            {
                foreach (Form item in f.OwnedForms)
                {
                    if (item.Modal)
                    {
                        if (item.OwnedForms.Length > 0)
                        {
                            var _f = FindModal(item);
                            if (_f == null)
                                return item;
                            else
                                return _f;
                        }
                        else return item;
                    }
                }
            }
            return null;
        }

       
        #endregion

        #region  protected properties
        protected override CreateParams CreateParams //点击任务栏实现最小化与还原
        {
            get
            {
                if (TaskbarSetWindowState)
                {
                    var cp = base.CreateParams;
                    cp.Style = cp.Style | Windows.Apis.Const.WS_MINIMIZEBOX;   // 允许最小化操作  
                    return cp;
                }
                else
                {
                    return base.CreateParams;
                }
            }
        }
        #endregion

        #region protected methods
        protected override void WndProc( ref Message m )
        {
            switch (m.Msg)
            {
                case 0x0084:
                    base.WndProc( ref m );
                    if(Resizeable)
                        FormHelper.Resize( ref m, this );
                    break;
                case 0x20:
                    base.WndProc(ref m);
                    if (m.LParam.ToInt32() == 0x202fffe || m.LParam.ToInt32() == 0x201fffe)
                    {
                        var _f = FindModal(this);
                        if(_f is ThemeableForm)
                        {
                            _modal = _f as ThemeableForm;
                            modalTimer.Enabled = true;
                            _index = _index % 2 == 0 ? 1 : 0;
                            break;
                        }
                    }
                    break;
                default:
                    base.WndProc( ref m );
                    break;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            FitDpi();
            base.OnLoad(e);
        }
        #endregion

        #region public methods

        public void SuspendLayout(Control c)
        {
            if (c is ContainerControl)
            {
                c.SuspendLayout();
            }
            foreach (Control item in c.Controls)
            {
                SuspendLayout(item);
            }
        }

        public void ResumeLayout(Control c)
        {
            if (c is ContainerControl)
            {
                c.PerformLayout();
                c.ResumeLayout(false);
            }
            foreach (Control item in c.Controls)
            {
                ResumeLayout(item);
            }
        }

        public void BeginAutoScale()
        {
            SuspendLayout();
            AutoScaleDimensions = new SizeF(ScreenHelper.PrimaryScreen.DpiX * ScreenHelper.PrimaryScreen.ScaleX, ScreenHelper.PrimaryScreen.DpiY * ScreenHelper.PrimaryScreen.ScaleY);
        }

        public void EndAutoScale()
        {
            PerformAutoScale();
            //继续布局
            ResumeLayout();
        }

        public virtual void SetTheme(IThemeBase t)
        {
            BackColor = t.BackColor;
            ForeColor = t.ForeColor;
            BorderColor = t.BorderColor;
            if(t is Theme)
            {
                Opacity = (t as ITheme).Opacity;
                Blendable = (t as Theme).Blendable;
            }
        }
        /// <summary>
        /// 当最大化时将border（form.padding）设为0，正常时还原border（Comm.DefaultBorderSize）
        /// </summary>
        /// <param name="enable"></param>
        public void AutoBorder()
        {
            if (Padding == new Padding())
            {
                FormHelper.AutoBorder(false);
            }
            else
            {
                FormHelper.AutoBorder(new BorderSize(), Comm.DefaultBorderSize);
            }
        }

        public void DisableAutoBorder()
        {
            FormHelper.AutoBorder(false);
        }

        public void FitDpi()
        {
            var inch = ScreenHelper.PrimaryScreen.Inch;
            var dpix = ScreenHelper.PrimaryScreen.DpiX;
            var dpiy = ScreenHelper.PrimaryScreen.DpiY;
            var pxX = ScreenHelper.PrimaryScreen.PxX;
            var pxY = ScreenHelper.PrimaryScreen.PxY;
            var ppxX = Screen.PrimaryScreen.Bounds.Width;
            var ppxY = Screen.PrimaryScreen.Bounds.Height;
            var pdpi = System.Math.Sqrt(System.Math.Pow(2160, 2) + System.Math.Pow(3840, 2)) / inch;
            SuspendLayout();
            AutoScaleDimensions = new SizeF(dpix, dpiy);
            PerformAutoScale();
            //继续布局
            ResumeLayout();
        }
        #endregion
    }
}
