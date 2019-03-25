using System.ComponentModel;

namespace NgNet.UI
{
    /// <summary>
    /// 鼠标操作枚举
    /// </summary>
    public enum MouseOperate
    {
        /// <summary>
        /// 进入控件
        /// </summary>
        [Description("进入控件")]
        Enter,
        /// <summary>
        /// 在控件上移动
        /// </summary>
        [Description("在控件上移动")]
        Move,
        /// <summary>
        /// 按下
        /// </summary>
        [Description("按下")]
        Down,
        /// <summary>
        /// 松开
        /// </summary>
        [Description("松开")]
        Up,
        /// <summary>
        /// 离开控件
        /// </summary>
        [Description("离开控件")]
        Leave
    }
}
