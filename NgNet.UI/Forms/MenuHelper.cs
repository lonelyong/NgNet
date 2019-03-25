using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    #region <class - hMenu>
    public class MenuHelper
    {
        /// <summary>
        /// 设置toolstripitem的不透明度，包裹子菜单
        /// </summary>
        /// <param name="tsis">toolstripitem</param>
        /// <param name="opacity">不透明度</param>
        public static void SetOpacity(IEnumerable<ToolStripItem> toolStripItems, double opacity)
        {
            SetOpacity(toolStripItems.GetEnumerator(), opacity);
        }

        public static void SetOpacity(IEnumerator<ToolStripItem> toolStripItems, double opacity)
        {
            while (toolStripItems.MoveNext())
            {
                SetOpacity(toolStripItems.Current, opacity);
            }
        }

        public static void SetOpacity(ToolStripItemCollection tsic, double opacity)
        {
            foreach (ToolStripItem item in tsic)
            {
                SetOpacity(item, opacity);
            }
        }

        public static void SetOpacity(ToolStripItem tsi, double opacity)
        {
            // ToolStripDropDownItem是ToolStripMenuItem的父类
            if(tsi is ToolStripDropDownItem)
            {
                (tsi as ToolStripDropDownItem).DropDown.Opacity = opacity;
                foreach (ToolStripItem item in (tsi as ToolStripDropDownItem).DropDownItems)
                {
                    SetOpacity(item, opacity);
                }
            }
        }
    }
    #endregion
}
