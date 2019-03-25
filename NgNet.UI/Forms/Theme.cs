using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class Theme : UI.Theme
    {
        #region public methods
        /// <summary>
        /// 为窗体绑定事件，并在窗体关闭时自动解绑
        /// </summary>
        /// <param name="f"></param>
        public void BindEvent(Form f, ThemeChangedEventHandler e)
        {
            ThemeChanged -= e;
            ThemeChanged += e;
            f.FormClosed += (object sender, FormClosedEventArgs e1) => { ThemeChanged -= e; };
        }

        public void BindEventNonForm(Control control, ThemeChangedEventHandler e)
        {
            if (control is Form)
                throw new Exception($"传入的控件不能是窗体");
            ThemeChanged -= e;
            ThemeChanged += e;
            control.Disposed += (object sender, EventArgs e1) => { ThemeChanged -= e; };
        }
        #endregion
    }
}
