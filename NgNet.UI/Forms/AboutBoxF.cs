using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    partial class AboutBoxF : TitleableForm, ICommDialogWindow
    {
        #region private proiperties
        private string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        private string AssemblyVersion
        {
            get
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
        }

        private string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        private string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        private string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        private string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        #region public properties
        /// <summary>
        /// 应用的logo
        /// </summary>
        public Image Logo
        {
            set
            {
                if (value != null)
                {
                    logoPicBox.Image = value;
                }
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
                tBoxDescription.BackColor = value;
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
                this.labelCompanyName.ForeColor = value;
                this.labelCopyright.ForeColor = value;
                this.labelProductName.ForeColor = value;
                this.labelVersion.ForeColor = value;
                this.tBoxDescription.ForeColor = value;
                this.okButton.ForeColor = value;
            }
        }
        #endregion

        #region constructor destructor
        public AboutBoxF()
        {
            InitializeComponent();
            DialogResult = DialogResult.None;
            TitleBar.Style = TitleBarStyles.EndOnly;
            Comm.Initialize(this, FormHelper);

        }
        #endregion

        #region private methods
        #region button ok
        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region protected methods

        #endregion

        #region this
        private void AboutBox_Load(object sender, EventArgs e)
        {
            //移动窗体最大化最小化
            FormHelper.Move(tableLayoutPanel);
            FormHelper.Move(labelCompanyName);
            FormHelper.Move(labelCopyright);
            FormHelper.Move(labelProductName);
            FormHelper.Move(labelVersion);
            FormHelper.Move(logoPicBox);
            //如果么有父窗体（或未显示）则显示在屏幕中央
            Comm.ApplyComm(this);

            this.Text = string.Format("关于 {0}", AssemblyTitle);

            this.labelProductName.Text = String.Format("程序名称: {0} ", AssemblyProduct);

            this.labelVersion.Text = String.Format("版本: {0}", AssemblyVersion);

            this.labelCopyright.Text = String.Format("版权: {0}", AssemblyCopyright);

            this.labelCompanyName.Text = String.Format("公司名称: {0}", AssemblyCompany);

            string _description = AssemblyDescription;

            this.tBoxDescription.Text = String.Format("程序描述:\r\n\r\n    {0}", _description);
        }
        #endregion
        #endregion

        #region public methods

        #endregion
    }
}
