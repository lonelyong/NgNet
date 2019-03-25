using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace NgNet.UI.Forms
{
    public class ExitBox : CommDialog<ExitStyles>, ITheme
    {
        #region private fields
        private ExitBoxF ebf;

        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 关闭显示样式
        /// </summary>
        public ExitStyles ExitStyle
        {
            set
            {
                this.ebf.ExitStyle = value;
            }
            get
            {
                return DialogResult == DialogResult.OK ? ebf.ExitStyle : ExitStyles.None;
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示对话框的工具栏背景色
        /// </summary>
        public Color BarColor
        {
            set
            {
                ebf.BarColor = value;
            }
            get
            {
                return ebf.BarColor;
            }
        }
        /// <summary>
        /// 设置对话框图标
        /// </summary>
        public Image Logo
        {
            set
            {
                this.ebf.Logo = value;
            }
        }
        #endregion

        #region constructor destructor
        public ExitBox()
        {
            ebf = new ExitBoxF();
            _DialogWindow = ebf;
        }
        #endregion

        #region public methods
        public override ExitStyles Show(IWin32Window owner = null)
        {
            ebf.ShowInTaskbar = owner == null;
            ebf.ShowDialog(owner);
            return ExitStyle;
        }
        #endregion
    }
}
