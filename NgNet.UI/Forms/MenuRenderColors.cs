using System.Drawing;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// menuRender的样式
    /// </summary>
    public sealed class MenuRenderColors
    {
        #region constructor destructor  
        public MenuRenderColors()
        {
            #region margin
            MarginStartColor = Color.LightGreen;

            MarginEndColor = Color.Azure;
            #endregion

            #region dropdown
            DropDownBackStartColor = Color.LightGreen;

            DropDownBackEndColor = Color.Azure;

            DropDownBorderColor = Color.GreenYellow;

            DropDownItemStartColor = Color.LightGreen;

            DropDownItemEndColor = Color.Azure;

            DropDownItemBorderColor = Color.Green;

            #endregion

            #region menustrip(不包括dropdown部分)
            MenuItemStartColor = Color.Transparent;

            MenuItemEndColor = Color.Transparent;

            MenuItemBorderColor = Color.Transparent;

            MainMenuStartColor = Color.Transparent;

            MainMenuEndColor = Color.Transparent;

            MainMenuBorderColor = Color.Transparent;

            #endregion

            SeparatorColor = Color.LightGreen;

            ArrowColor = Color.DarkGreen;

            FontColor = Color.DarkGreen;
        }
        #endregion

        #region margin
        /// <summary>
        /// 下拉菜单坐标图标区域开始颜色
        /// </summary>
        public Color MarginStartColor { get; set; }
        /// <summary>
        /// 下拉菜单坐标图标区域结束颜色
        /// </summary>
        public Color MarginEndColor { get; set; }
        #endregion

        #region dropdown
        /// <summary>
        /// 下拉开始背景颜色
        /// </summary>
        public Color DropDownBackStartColor { get; set; }
        /// <summary>
        /// 下拉结束背景颜色
        /// </summary>
        public Color DropDownBackEndColor { get; set; }
        /// <summary>
        /// 下拉区域边框颜色
        /// </summary>
        public Color DropDownBorderColor { get; set; }
        /// <summary>
        /// 下拉项选中时开始颜色
        /// </summary>
        public Color DropDownItemStartColor { get; set; }
        /// <summary>
        /// 下拉项选中时结束颜色
        /// </summary>
        public Color DropDownItemEndColor { get; set; }
        /// <summary>
        /// 下拉项选中边框颜色
        /// </summary>
        public Color DropDownItemBorderColor { get; set; }
        #endregion

        #region menustrip(不包括dropdown部分)
        /// <summary>
        /// 获取或设置一个值，该值指示Menustrip项选中时的开始颜色,非下拉项
        /// </summary>
        public Color MenuItemStartColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示MenuStrip项选中时的结束颜色,非下拉项
        /// </summary>
        public Color MenuItemEndColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示Menustrip边框颜色
        /// </summary>
        public Color MenuItemBorderColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示MenuStrip背景开始颜色,非下拉项
        /// </summary>
        public Color MainMenuStartColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示MenuStrip背景结束颜色,非下拉项
        /// </summary>
        public Color MainMenuEndColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示Menustrip边框颜色
        /// </summary>
        public Color MainMenuBorderColor { get; set; }
        #endregion

        /// <summary>
        /// 获取或设置一个值，该值指示分割线颜色
        /// </summary>
        public Color SeparatorColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示箭头颜色
        /// </summary>
        public Color ArrowColor { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示菜单字体颜色
        /// </summary>
        public Color FontColor { get; set; }
    }
}
