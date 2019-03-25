using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    class ComboBoxEx : ComboBox
    {
        #region public properties
        public bool DisableMouseWheel { get; set; }

        #endregion

        #region constructor
        public ComboBoxEx()
        {
            DisableMouseWheel = true;
        }
        #endregion

        #region protected methods
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x020a && DisableMouseWheel)
            {

            }
            else
                base.WndProc(ref m);
        }
        #endregion
    }
}
