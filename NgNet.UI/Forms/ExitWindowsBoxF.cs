using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using NgNet.UI.Forms;
using System.Data;

namespace NgNet.UI.Forms
{
    partial class ExitWindowsBoxF : TitleableForm, ICommDialogWindow
    {
        #region private fields
        private Windows.RestartOptions _RestartOption = Windows.RestartOptions.LogOff;

        private ComboBoxHelper _hComboBox1;
        #endregion

        #region public properties
        /// <summary>
        /// 设置或获取自动确认时间
        /// </summary>
        public uint WaitTime { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是否强制关机
        /// </summary>
        public bool Force { get; set; }
        /// <summary>
        /// 获取或设置一个值，该值指示是注销还是关机还是重启
        /// </summary>
        public Windows.RestartOptions RestartOption
        {
            get
            {
                return _RestartOption;
            }
            set
            {
                _RestartOption = value;
                restartOptionComboBox.SelectedItem = value;
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
                restartOptionComboBox.BackColor = value;
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                timeLabel.ForeColor = value;
                btn_cancel.ForeColor = value;
                btn_ok.ForeColor = value;
                restartOptionComboBox.ForeColor = value;
            }
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                value = string.IsNullOrWhiteSpace(value) ? "退出Windows" : value;
                base.Text = value;
            }
        }
        #endregion

        #region constructor destructor
        public ExitWindowsBoxF()
        {
            InitializeComponent();
            _hComboBox1 = new ComboBoxHelper( restartOptionComboBox ) { DropdownAutoWidth = true, DelegateDrawItem = true };
            TitleBar.Style = TitleBarStyles.EndOnly;

            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            Text = "退出 Windows";
            Icon = NgNet.ConvertHelper.Bitmap2Icon(Windows.Logos.Windows);

            //字段初始化
            WaitTime = 60;
            Force = false;

            _hComboBox1.SetEnum( typeof( Windows.RestartOptions ) );
            restartOptionComboBox.SelectedItem = RestartOption;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            if (WaitTime < 60 && WaitTime >0)
            {
                WaitTime = 60;
            }
        }

        private void panel_SizeChanged(object sender, EventArgs e)
        {
            itemPanel.Location = new Point(60, 60);
            itemPanel.Width = ContentPanel.Width - itemPanel.Left * 2;
            timeLabel.Left = itemPanel.Left;
            timeLabel.Width = itemPanel.Width;
            timeLabel.Top = itemPanel.Bottom + 20;
            btn_ok.Left = ContentPanel.Width / 2 - btn_ok.Width;
            btn_cancel.Left = btn_ok.Right;
            btn_ok.Top = ContentPanel.Height - btn_ok.Height - Comm.DISTANCE_DOWN;
            btn_cancel.Top = btn_ok.Top;
        }

        private void this_Shown(object sender, EventArgs e)
        {
            exitTimer.Enabled = WaitTime > 0;
        }
        #endregion

        #region ok cancel
        private void btn_ok_Click(object sender, EventArgs e)
        {
            Windows.Current.ExitWindows(this.RestartOption, this.Force);
            Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void exitTimer_Tick(object sender, EventArgs e)
        {
            if (this.WaitTime == 0)
            {
                timeLabel.Text = null;
                Windows.Current.ExitWindows(RestartOption, Force);
                exitTimer.Enabled = false;
            }
            else
            {
                WaitTime--;
                timeLabel.Text = NgNet.ConvertHelper.ToTimeString((int)WaitTime);
            }
        }
        #endregion

        #region combobox restrat options
        private void restartOptionComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(restartOptionComboBox.SelectedItem is DataRowView)
            {
                _RestartOption = (Windows.RestartOptions)((DataRowView)restartOptionComboBox.SelectedItem).Row[EnumHelper.VALUE];
            }
            else
            {
                _RestartOption = (Windows.RestartOptions)restartOptionComboBox.SelectedValue;
            }
            Text = string.Format( "退出({0}) Windows", EnumHelper.GetEnumDescription( _RestartOption ) );
        }
        #endregion

        #region public methods
        public override void SetTheme( IThemeBase t )
        {
            base.SetTheme( t );
            _hComboBox1.Theme = t;
        }
        #endregion
    }
}
