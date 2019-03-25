using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    class CraashDialog : CommDialog<string>
    {
        #region private fields
        private CrashDialogF _cdf = new CrashDialogF();
        #endregion

        #region proptected fields
        protected override ICommDialogWindow _DialogWindow
        {
            get
            {
                return _cdf; ;
            }
        }
        #endregion

        #region public properties
        /// <summary>
        /// 获取或设置致歉信息
        /// </summary>
        public string ApologiesInformation { get; set; }
        /// <summary>
        /// 获取或设置程序的Logo
        /// </summary>
        public Image Logo { get; set; }
        #endregion

        #region constructor
        public CraashDialog()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public methods
        public override string Show(IWin32Window window)
        {
            _cdf.ShowDialog(window);
            return null;
        }    
        #endregion
    }
}
