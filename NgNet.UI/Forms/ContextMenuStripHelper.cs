using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace NgNet.UI.Forms
{
    public class ContextMenuStripHelper
    {
        /// <summary>
        /// 居中窗口显示ContextMenuStrip
        /// </summary>
        /// <param name="cms"></param>
        /// <param name="toCenter"></param>
        public static void CenterToWindow(ContextMenuStrip cms, Form toCenter)
        {
            cms.Show(toCenter.PointToScreen(new Point((toCenter.Width - cms.Width) / 2, (toCenter.Height - cms.Height) / 2)));
        }

        /// <summary>
        ///  居中屏幕显示ContextMenuStrip
        /// </summary>
        /// <param name="cms"></param>
        public static void CenterToScreen(ContextMenuStrip cms)
        {
            cms.Show(new Point((Screen.PrimaryScreen.WorkingArea.Width - cms.Width) / 2, (Screen.PrimaryScreen.WorkingArea.Height - cms.Height) / 2));
        }

        /// <summary>
        /// 设置contextmenustrip的不透明度（包括子菜单）
        /// </summary>
        /// <param name="cms">contextmenustrip</param>
        /// <param name="opacity">不透明度</param>
        public static void SetOpacity(ContextMenuStrip cms, double opacity)
        {
            cms.Opacity = opacity;
            MenuHelper.SetOpacity(cms.Items, opacity);
        }
    }
}
