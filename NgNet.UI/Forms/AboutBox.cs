using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class AboutBox : CommDialog<DialogResult>
    {
        #region private fields
        private AboutBoxF _abf;
        #endregion

        #region  constructor destructor
        public AboutBox()
        {
            _abf = new AboutBoxF();
            _DialogWindow = _abf;
        }
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        /// <summary>
        /// 设置程序logo
        /// </summary>
        public Image Logo
        {
            set { _abf.Logo = value; }
        }
        #endregion

        #region public methods
        /// <summary>
        /// 
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        public override DialogResult Show(IWin32Window owner = null)
        {
            _abf.ShowInTaskbar = owner == null;
            _abf.ShowDialog(owner);
            return DialogResult;
        }
        #endregion

        #region static method
        /// <summary>
        /// 显示程序关于框
        /// </summary>
        /// <param name="owner">指定对话框的所有者</param>
        /// <param name="logo">程序的logo</param>
        public static void Show(IWin32Window owner, Image logo)
        {
            AboutBox ab = new AboutBox();
            Comm.ApplyTheme(ab, owner);
            ab.Logo = logo;
            ab.Show(owner);
        }
        #endregion
    }
}
