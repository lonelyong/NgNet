using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class ExitWindowsBox : CommDialog<DialogResult>
    {
        #region private fields
        private ExitWindowsBoxF _ewb;
        #endregion

        #region protected properties
        protected override ICommDialogWindow _DialogWindow
        {
            get;
        }
        #endregion

        #region public properties
        public Windows.RestartOptions RestartOption
        {
            get { return _ewb.RestartOption; }
            set
            {
                _ewb.RestartOption = value;
            }
        }

        public uint WaitTime
        {
            get
            {
                return _ewb.WaitTime;
            }
            set
            {
                _ewb.WaitTime = value;
            }
        }

        public bool Force
        {
            get
            {
                return _ewb.Force;
            }
            set
            {
                _ewb.Force = value;
            }
        }
        #endregion

        #region constructor
        public ExitWindowsBox()
        {
            _ewb = new ExitWindowsBoxF();
            _DialogWindow = _ewb;
        }
        #endregion

        #region public methods
        public override DialogResult Show(IWin32Window owner = null)
        {
           _DialogWindow.Show(owner);
            return _DialogWindow.DialogResult;
        }
        #endregion
    }
}
