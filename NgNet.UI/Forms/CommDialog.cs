using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// 对话框的基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CommDialog<T> : ITheme, IDisposable, IComponent
    {
        #region protected properties
        /// <summary>
        /// 对话框窗体
        /// </summary>
        protected abstract ICommDialogWindow _DialogWindow { get; }
        #endregion

        #region CommDialog
        /// <summary>
        /// 获取一个值，该值指示对话框是否成功
        /// </summary>
        public virtual DialogResult DialogResult
        {
            get
            {
                return _DialogWindow.DialogResult;
            }
        }
         /// <summary>
         /// 获取或设置对话框标题
         /// </summary>
        public virtual string Title
        {
            get { return _DialogWindow.Text; }
            set { _DialogWindow.Text = value; }
        }
        /// <summary>
        /// 获取一个值，该值指示对话框是否已经加载
        /// </summary>
        public bool IsLoaded
        {
            get
            {
                return _DialogWindow.IsLoaded;
            }
        }
        /// <summary>
        /// 使对话框获取焦点
        /// </summary>
        public void Activate()
        {
            _DialogWindow.Activate();
        }
        /// <summary>
        /// 获取或设置对话框宽度
        /// </summary>
        public int Width { get { return _DialogWindow.Width; } set { _DialogWindow.Width = value; } }
        /// <summary>
        /// 获取或设置对话框高度
        /// </summary>
        public int Height { get { return _DialogWindow.Height; } set { _DialogWindow.Height = value; } }
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public abstract T Show(IWin32Window window);
        /// <summary>
        /// 获取或设置一个值，该值指示是否启用淡入淡出效果
        /// </summary>
        public bool Blendable
        {
            get
            {
                return _DialogWindow.Blendable;
            }

            set
            {
                _DialogWindow.Blendable = value;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值为对话框的透明度
        /// </summary>
        public double Opacity
        {
            get
            {
                return _DialogWindow.Opacity;
            }

            set
            {
                _DialogWindow.Opacity = value;
            }
        }
        /// <summary>
        /// 获取或设置对话框的背景色
        /// </summary>
        public Color BackColor
        {
            get
            {
                return _DialogWindow.BackColor;
            }
            set
            {
                _DialogWindow.BackColor = value;
            }
        }
        /// <summary>
        /// 获取或设置对话框前景色
        /// </summary>
        public Color ForeColor
        {
            get
            {
                return _DialogWindow.ForeColor;
            }

            set
            {
                _DialogWindow.ForeColor = value;
            }
        }
        /// <summary>
        /// 获取或设置对话框的边框颜色
        /// </summary>
        public Color BorderColor
        {
            get
            {
                return _DialogWindow.BorderColor;
            }

            set
            {
                _DialogWindow.BorderColor = value;
            }
        }
        /// <summary>
        /// 将指定的ThemeBase应用到对话框
        /// </summary>
        /// <param name="themeBase"></param>
        public virtual void SetTheme(IThemeBase themeBase)
        {
            _DialogWindow.SetTheme(themeBase);
        }
        /// <summary>
        /// 使对话框获得焦点
        /// </summary>
        public bool Focus()
        {
            return _DialogWindow.Focus();
        }
        /// <summary>
        /// 显示已经加载的窗口
        /// </summary>
        public void Appear()
        {
            FormHelper.Display(_DialogWindow as Form);
        }
        #endregion

        #region IDisposable
        public void Dispose()
        {
            _DialogWindow.Dispose();
            Disposed?.Invoke(this, new EventArgs());
        }
        #endregion

        #region IComponent
        public ISite Site
        {
            get
            {
                return _DialogWindow.Site;
            }
            set
            {
                _DialogWindow.Site = value;
            }
        }

        public event EventHandler Disposed;
        #endregion
    }
}
