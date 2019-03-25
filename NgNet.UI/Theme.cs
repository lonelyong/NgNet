using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI
{
    /// <summary>
    /// 默认主题
    /// </summary>
    public class Theme : ITheme
    {
        #region private fields

        private Color _backColor = Color.Azure;

        private Color _foreColor = Color.DarkGreen;

        private Color _borderColor = Color.Green;

        private bool _blendable = false;

        private double _opacity = 0.85d;

        private ThemeChangedEventHandler _themeChanged;
        #endregion

        #region protected fields
        protected bool HasChange { get; set; } = false;

        protected bool UpdateState { get; set; } = false;
        #endregion

        #region  public properties
        /// <summary>
        /// 获取或设置背景色
        /// </summary>
        public virtual Color BackColor
        {
            get
            {
                return _backColor;
            }
            set
            {
                if (_backColor == value)
                    return;
                _backColor = value;
                HasChange = true;
                if (!UpdateState)
                    OnThemeChanged(new ThemeChangedEventArgs(this));

            }
        }
        /// <summary>
        /// 获取或设置边框颜色
        /// </summary>
        public virtual Color BorderColor
        {
            get
            {
                return _borderColor;
            }
            set
            {
                if (_borderColor == value)
                    return;
                _borderColor = value;
                HasChange = true;
                if (!UpdateState)
                    OnThemeChanged(new ThemeChangedEventArgs(this));
            }
        }
        /// <summary>
        /// 获取或设置前景色
        /// </summary>
        public virtual Color ForeColor
        {
            get
            {
                return _foreColor;
            }
            set
            {
                if (_foreColor == value)
                    return;
                _foreColor = value;
                HasChange = true;
                if (!UpdateState)
                    OnThemeChanged(new ThemeChangedEventArgs(this));
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示是否启用淡入淡出效果
        /// </summary>
        public virtual bool Blendable
        {
            get
            {
                return _blendable;
            }
            set
            {
                if (_blendable == value)
                    return;
                _blendable = value;
                HasChange = true;
                if (!UpdateState)
                    OnThemeChanged(new ThemeChangedEventArgs(this));
            }
        }
        /// <summary>
        /// 获取或设置当前主题的透明度
        /// </summary>
        public virtual double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                if (_opacity == value)
                    return;
                _opacity = value;

                HasChange = true;
                if (!UpdateState)
                    OnThemeChanged(new ThemeChangedEventArgs(this));
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示，当绑定方法到ThemeChanged事件时是否执行该方法，默认为True
        /// </summary>
        public bool RunAtOnce { get; set; }
        #endregion

        #region events
        /// <summary>
        ///主题改变事件
        /// </summary>
        public event ThemeChangedEventHandler ThemeChanged
        {
            add
            {
                _themeChanged += value;
                if (RunAtOnce)
                    value?.Invoke(new ThemeChangedEventArgs(this));
            }
            remove
            {
                _themeChanged -= value;
            }
        }
        #endregion

        #region constructor
        /// <summary>
        /// 无参实例化对象
        /// </summary>
        public Theme()
        {
            RunAtOnce = true;
        }
        #endregion

        #region protected methods
        /// <summary>
        /// 主题开始改变事件
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnThemeChanged(ThemeChangedEventArgs e)
        {
            _themeChanged?.Invoke(e);
            HasChange = false;
        }
        #endregion

        #region public methods
        /// <summary>
        /// 主题开始改变
        /// </summary>
        public void BeginUpdate()
        {
            UpdateState = true;
        }
        /// <summary>
        /// 主题改变结束
        /// </summary>
        public void EndUpdate()
        {
            if (UpdateState)
            {
                UpdateState = false;
            }
            OnThemeChanged(new ThemeChangedEventArgs(this));
        }

        public virtual void SetTheme(IThemeBase t)
        {
            BackColor = t.BackColor;
            ForeColor = t.ForeColor;
            BorderColor = t.BorderColor;
        }
        #endregion

        #region static
        /// <summary>
        /// 返回默认的主题
        /// </summary>
        public static Theme Default
        {
            get
            {
                return new Theme();
            }
        }
        /// <summary>
        /// 用指定的颜色创建新的主题类
        /// </summary>
        /// <param name="back"></param>
        /// <param name="fore"></param>
        /// <param name="border"></param>
        /// <returns></returns>
        public static Theme CreateTheme(Color back, Color fore, Color border)
        {
            return new Theme { BackColor = back, ForeColor = fore, BorderColor = border };
        }
        #endregion
    }
}
