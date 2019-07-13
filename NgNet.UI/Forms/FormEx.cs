using System.Windows.Forms;
namespace NgNet.UI.Forms
{
    class FormEx : Form
    {
        #region private fields
        private const int WS_EX_APPWINDOW = 0x40000;

        private const int WS_EX_TOOLWINDOW = 0x80;
        #endregion

        #region public properties
        public bool ShowInAltTab { get; set; } = false;
        #endregion

        #region constructor
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormEx
            // 
            this.ClientSize = new System.Drawing.Size(290, 277);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEx";
            this.ResumeLayout(false);

        }
        #endregion

        #region protected methods
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!ShowInTaskbar)
                    cp.ExStyle &= (~WS_EX_APPWINDOW);    // 不显示在TaskBar
                if (!ShowInAltTab)
                    cp.ExStyle |= WS_EX_TOOLWINDOW;      // 不显示在Alt-Tab
                return cp;
            }
        }
        #endregion
    }
}
