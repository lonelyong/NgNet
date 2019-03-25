using NgNet.UI;
using System.Drawing;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// Box窗体应实现的接口
    /// </summary>
    public interface ICommBoxWindow : ITheme, System.ComponentModel.ISynchronizeInvoke
    {
        /// <summary>
        /// 窗体是否已经加载
        /// </summary>
        bool IsLoaded { get; }
        /// <summary>
        /// 获取或设置一个值，该值指示窗体的可见性
        /// </summary>
        bool Visible { get; set; }
        /// <summary>
        /// 获取或设置标题
        /// </summary>
        string Text { get; set; }
        /// <summary>
        /// 获取或设置Box要打开的文件
        /// </summary>
        string Path { get; }
        /// <summary>
        /// 获取或设置Box的大小
        /// </summary>
        Size Size { get; set; }
        /// <summary>
        /// 获取或设置一个值该值指示是否显示窗体图标
        /// </summary>
        bool ShowIcon { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示窗口是否显示在任务栏
        /// </summary>
        bool ShowInTaskbar { get; set; }
        /// <summary>
        /// 获取或设置一个值该值指示标题栏的颜色是否和背景色一样
        /// </summary>
        bool TitleBackColorUseBackColor { get; set; }
        /// <summary>
        /// 获取或设置窗口的启动位置
        /// </summary>
        FormStartPosition StartPosition { get; set; }
        /// <summary>
        /// 获取或设置边框大小
        /// </summary>
        BorderSize BorderSize { get; set; }
        /// <summary>
        /// 获取或设置标题栏样式
        /// </summary>
        UI.Forms.TitleBarStyles TitleBarStyle { get; set; }
        /// <summary>
        /// 打开制定的文件
        /// </summary>
        void OpenFile(string path);
        /// <summary>
        /// 关闭窗体
        /// </summary>
        void Close();
        /// <summary>
        /// 隐藏Box
        /// </summary>
        void Hide();
    }
}
