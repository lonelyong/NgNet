
using System.Drawing;
using NgNet;

namespace NgNet.UI.Forms
{
    class Comm
    {

        #region private fileds
        #endregion

        #region const
        /// <summary>
        /// 控件水平间距
        /// </summary>
        public const int DISTANCE_INTERVAL_H = 6;
        /// <summary>
        /// 控件垂直间距
        /// </summary>
        public const int DISTANCE_INTERVAL_V = 6;
        /// <summary>
        /// 控件据容器上边缘距离
        /// </summary>
        public const int DISTANCE_TOP = 8;
        /// <summary>
        /// 控件距容器下边缘距离
        /// </summary>
        public const int DISTANCE_DOWN = 6;
        /// <summary>
        /// 控件距容器左边缘距离
        /// </summary>
        public const int DISTANCE_LEFT = 7;
        /// <summary>
        /// 标题栏高度
        /// </summary>
        public const int HEIGHT_TITLEBAR = 20;
        /// <summary>
        /// 边框宽度
        /// </summary>
        public static System.Windows.Forms.Padding BORDER_SIZE { get; } = new System.Windows.Forms.Padding(3, 1, 3, 3);

        /// <summary>
        /// 窗体左圆角度
        /// </summary>
        public const int LROUND_RGN = 3;
        /// <summary>
        /// 窗体右圆角度
        /// </summary>
        public const int RROUND_RGN = 3;
        #endregion

        #region Defaut Values
        public static bool DefaultBlendable { get;  }
        /// <summary>
        /// 获取一个值，该值为默认对话框背景色
        /// </summary>
        public static Color DefaultBackColor { get;  }
        /// <summary>
        /// 获取一个值，该值为默认对话框前景色
        /// </summary>
        public static Color DefaultForeColor { get; }
        /// <summary>
        /// 获取一个值，该值为默认对话框边框颜色
        /// </summary>
        public static Color DefaultBorderColor { get;} 
        /// <summary>
        /// 获取一个值，该值为默认的窗体透明度
        /// </summary>
        public static double DefaultOpacity { get; }

        public static BorderSize DefaultBorderSize { get; }
        #endregion

        #region constructor destructor
        static Comm()
        {
            DefaultBlendable = true;
            DefaultBackColor = Color.Azure;
            DefaultForeColor = Color.Purple;
            DefaultBorderColor = Color.DarkGreen;
            DefaultOpacity = 0.85f;
            DefaultBorderSize = new BorderSize(4, 2, 4, 4);
        }
        #endregion

        #region internal method
        /// <summary>
        /// 应用对话框公共属性，应在Load事件中调用此方法
        /// </summary>
        /// <param name="f"></param>
        internal static void ApplyComm(System.Windows.Forms.Form f)
        {
            if (f.Owner == null || f.Owner.Visible == false || f.Owner.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                FormHelper.CenterToScreen(f);
            }
            f.ShowInTaskbar = f.Owner == null;
            Icon _ico;
            if (f.Owner == null)
                _ico = ConvertHelper.Bitmap2Icon(Windows.Logos.Application);
            else
                _ico = f.Owner.Icon;

            if (f is ICommDialogWindow)
                ((ICommDialogWindow)f).Icon = _ico;
            else
                f.Icon = _ico;
        }
        /// <summary>
        /// 初始化对话框，应在构造函数时调用此方法
        /// </summary>
        /// <param name="window"></param>
        /// <param name="formhelper"></param>
        internal static void Initialize(ITheme f, FormHelper formhelper)
        {
            f.SetTheme(new Theme() { BackColor = DefaultBackColor, ForeColor = DefaultForeColor, BorderColor = DefaultBorderColor, Blendable = DefaultBlendable, Opacity = DefaultOpacity } );
            formhelper.SetFormRoundRgn(LROUND_RGN, RROUND_RGN);
        }
        /// <summary>
        /// 设置对话框主题，使之与其所有者主题一致
        /// </summary>
        /// <param name="themeCore"></param>
        /// <param name="owner"></param>
        internal static void ApplyTheme(ITheme f, System.Windows.Forms.IWin32Window owner)
        {
            if(owner is IThemeBase)
                f.SetTheme(owner as IThemeBase);
        }
        #endregion
    }
}
