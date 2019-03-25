using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public partial class TitleableForm : ThemeableForm
    {
        #region private fields
        private bool _titleBackColorUseBackColor = false;
        #endregion

        #region protected properties
        public TitleBar TitleBar { get; }
        #endregion

        #region public properteis
        public override Color BorderColor
        {
            get
            {
                return base.BorderColor;
            }

            set
            {
                base.BorderColor = value;
                if (TitleBar != null && !TitleBackColorUseBackColor)
                    TitleBar.BackColor = value;
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }

            set
            {
                base.BackColor = value;
                if (TitleBar != null && TitleBackColorUseBackColor)
                    TitleBar.BackColor = value;
            }
        }

        public virtual new Icon Icon
        {
            get
            {
                return base.Icon;
            }
            set
            {
                base.Icon = value;
                if (TitleBar != null)
                    TitleBar.Icon = value;
            }
        }

        public bool TitleBackColorUseBackColor
        {
            get
            {
                return _titleBackColorUseBackColor;
            }
            set
            {
                if (value == _titleBackColorUseBackColor)
                    return;
                _titleBackColorUseBackColor = value;
                if (value)
                    TitleBar.BackColor = BackColor;
                else
                    TitleBar.BackColor = BorderColor;
            }
        }
        #endregion

        #region constructor
        public TitleableForm()
        {
            InitializeComponent();
            TitleBar = new TitleBar(this, TitleBarStyles.MinMaxEnd);
            SizeChanged += this_SizeChanged;
            Padding = Comm.DefaultBorderSize.ToPadding();
            PaddingChanged += this_PaddingChanged;
            this_SizeChanged(null, null);
        }
        #endregion

        #region private methods
        private void this_SizeChanged(object sender, EventArgs e)
        {
            ContentPanel.Height = Height - TitleBar.Height - Padding.Top - Padding.Bottom;
        }

        private void this_PaddingChanged(object sender, EventArgs e)
        {
            this_SizeChanged(sender, e);
        }
        #endregion

        #region protected methods
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
                if (TitleBar != null)
                    TitleBar.Title = value;
            }
        }
        #endregion


    }
}
