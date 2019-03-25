namespace NgNet.UI.Forms
{
    partial class OpenFileDialogF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenFileDialogF));
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameComboBox
            // 
            this.nameComboBox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.nameComboBox.Size = new System.Drawing.Size(505, 27);
            // 
            // okButton
            // 
            this.okButton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.okButton.Location = new System.Drawing.Point(627, 589);
            this.okButton.Text = "打开(&O)";
            // 
            // cancelBotton
            // 
            this.cancelBotton.ForeColor = System.Drawing.SystemColors.WindowText;
            this.cancelBotton.Location = new System.Drawing.Point(814, 589);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Size = new System.Drawing.Size(953, 628);
            // 
            // OpenFileDialogF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpenFileDialogF";
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}