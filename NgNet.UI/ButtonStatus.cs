using System.ComponentModel;

namespace NgNet.UI
{
    /// <summary>
    /// 按钮状态
    /// </summary>
    public enum ButtonStatus
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal,
        /// <summary>
        /// 高亮
        /// </summary>
        [Description("高亮")]
        HighLight,
        /// <summary>
        /// 按下
        /// </summary>
        [Description("按下")]
        Pressed,
        /// <summary>
        /// 按下离开
        /// </summary>
        [Description("按下离开")]
        PressedLeave
    }
}
