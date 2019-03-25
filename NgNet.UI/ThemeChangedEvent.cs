using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI
{
    /// <summary>
    /// 主题改变事件的委托
    /// </summary>
    public delegate void ThemeChangedEventHandler(ThemeChangedEventArgs e);

    public class ThemeChangedEventArgs : EventArgs
    {
        #region public properties
        /// <summary>
        /// 获取当前的主题
        /// </summary>
        public Theme ThemeClass { get; }
        #endregion

        #region constructor
        public ThemeChangedEventArgs(Theme t)
        {
            ThemeClass = t;
        }
        #endregion
    }
}
