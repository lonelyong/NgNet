using System;
using System.Drawing;
using System.Windows.Forms;
using NgNet.UI;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// Box基类
    /// </summary>
    public abstract class CommBox : ITheme, System.ComponentModel.ISynchronizeInvoke
    {
        #region protected properties
        /// <summary>
        /// Box窗体
        /// </summary>
        protected abstract ICommBoxWindow _CommBoxWindow { get; }
        #endregion

        #region ISynchronizeInvoke
        /// <summary>
        /// 判断需不需要委托执行
        /// </summary>
        public virtual bool InvokeRequired
        {
            get { return _CommBoxWindow.InvokeRequired; }
        }
        /// <summary>
        /// 异步执行委托
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual IAsyncResult BeginInvoke(Delegate method, object[] args)
        {
            return _CommBoxWindow.BeginInvoke(method, args);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public virtual object EndInvoke(IAsyncResult result)
        {
            return _CommBoxWindow.EndInvoke(result);
        }
        /// <summary>
        /// 同步执行委托
        /// </summary>
        /// <param name="method"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public virtual object Invoke(Delegate method, object[] args)
        {
            return _CommBoxWindow.Invoke(method, args);
        }
        #endregion

        #region ICommBox
        /// <summary>
        /// 获取或设置一个值该值指示是否显示窗口图标
        /// </summary>
        public bool ShowIcon
        {
            get
            {
                return _CommBoxWindow.ShowInTaskbar;
            }
            set
            {
                _CommBoxWindow.ShowInTaskbar = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示窗口是否显示在任务栏
        /// </summary>
        public bool ShowInTaskbar
        {
            get
            {
                return _CommBoxWindow.ShowInTaskbar;
            }
            set
            {
                _CommBoxWindow.ShowInTaskbar = value;
            }
        }
        /// <summary>
        /// 获取或设置窗体大小
        /// </summary>
        public Size Size
        {
            get
            {
                return _CommBoxWindow.Size;
            }
            set
            {
                _CommBoxWindow.Size = value;
            }
        }
        /// <summary>
        /// 获取或设置窗口的启动位置
        /// </summary>
        public FormStartPosition StartPosition
        {
            get
            {
                return _CommBoxWindow.StartPosition;
            }
            set
            {
                _CommBoxWindow.StartPosition = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值该值指示标题栏颜色是否使用背景色
        /// </summary>
        public bool TitleBackColorUseBackColor { get
            {
                return _CommBoxWindow.TitleBackColorUseBackColor;
            }
            set
            {
                _CommBoxWindow.TitleBackColorUseBackColor = value;
            }
        }
        /// <summary>
        /// 获取或设置窗体边框大小
        /// </summary>
        public BorderSize BorderSize { get
            {
                return _CommBoxWindow.BorderSize;
            }
        set {
                _CommBoxWindow.BorderSize = value;
            }
        }
        /// <summary>
        /// 获取或设置窗口标题栏样式
        /// </summary>
        public UI.Forms.TitleBarStyles TitleBarStyle
        {
            get { return _CommBoxWindow.TitleBarStyle; }
            set
            {
                _CommBoxWindow.TitleBarStyle = value;
            }
        }
        /// <summary>
        /// 获取或设置标题
        /// </summary>
        public virtual string Title
        {
            get
            {
                return _CommBoxWindow.Text;
            }
            set
            {
                _CommBoxWindow.Text = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示窗体是否隐藏
        /// </summary>
        public virtual bool IsHidden
        {
            get
            {
                return !_CommBoxWindow.Visible;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示窗体是否已经加载
        /// </summary>
        public virtual bool IsLoaded
        {
            get
            {
                return _CommBoxWindow.IsLoaded;
            }
        }
        /// <summary>
        /// 获取或设置Box要打开的文件
        /// </summary>
        public virtual string Path
        {
            get
            {
                return _CommBoxWindow.Path;
            }
        }
        /// <summary>
        /// 打开制定的文件
        /// </summary>
        public void OpenFile(string path)
        {
            _CommBoxWindow.OpenFile(path);
        }
        /// <summary>
        /// 隐藏Box
        /// </summary>
        public void Hide()
        {
            _CommBoxWindow.Hide();
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void Appear()
        {
            UI.Forms.FormHelper.Display(_CommBoxWindow as Form);
        }
        /// <summary>
        /// 关闭窗体
        /// </summary>
        public virtual void Close()
        {
            _CommBoxWindow.Close();
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="owner"></param>
        public abstract void Show(IWin32Window owner);
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="owner"></param>
        public abstract void ShowDialog(IWin32Window owner);
        #endregion

        #region ITheme
        /// <summary>
        /// 获取或设置一个值，该值指示是否启用淡入淡出效果
        /// </summary>
        public virtual bool Blendable
        {
            get
            {
                return _CommBoxWindow.Blendable;
            }
            set
            {
                _CommBoxWindow.Blendable = value;
            }
        }
        /// <summary>
        /// 获取或设置Box的背景色
        /// </summary>
        public virtual Color BackColor
        {
            get
            {
                return _CommBoxWindow.BackColor;
            }
            set
            {
                _CommBoxWindow.BackColor = value;
            }
        }
        /// <summary>
        /// 获取或设置Box的前景色
        /// </summary>
        public virtual Color ForeColor
        {
            get
            {
                return _CommBoxWindow.ForeColor;
            }
            set
            {
                _CommBoxWindow.ForeColor = value;
            }
        }
        /// <summary>
        /// 获取或设置Box的边框颜色
        /// </summary>
        public virtual Color BorderColor
        {
            get
            {
                return _CommBoxWindow.BorderColor;
            }
            set
            {
                _CommBoxWindow.BorderColor = value;
            }
        }
        /// <summary>
        /// 获取或设置Box的透明度
        /// </summary>
        public virtual double Opacity
        {
            get
            {
                return _CommBoxWindow.Opacity;
            }
            set
            {
                _CommBoxWindow.Opacity = value;
            }
        }
        /// <summary>
        /// 设置主题
        /// </summary>
        /// <param name="t"></param>
        public virtual void SetTheme(IThemeBase t)
        {
            BackColor = t.BackColor;
            BorderColor = t.BorderColor;
            ForeColor = t.ForeColor;
            if (t is ITheme)
            {
                Opacity = (t as ITheme).Opacity;
                Blendable = (t as ITheme).Blendable;
            }
        }
        #endregion
    }
}
