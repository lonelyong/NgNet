using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class IdentifyingCodeDialog : CommDialog<DialogResult>
    {
        #region privaate fields
        private IdentifyingCodeDialogF _icdf;
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region constructor
        public IdentifyingCodeDialog()
        {
            _icdf = new IdentifyingCodeDialogF();
            _DialogWindow = _icdf;
        }
        #endregion

        #region public methods
        public override DialogResult Show(IWin32Window owner)
        {
            _icdf.ShowInTaskbar = owner == null;
            _icdf.ShowDialog(owner);
            return DialogResult;
        }
        #endregion
    }
}
