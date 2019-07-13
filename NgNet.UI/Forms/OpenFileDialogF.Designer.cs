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
            this.nameComboBox.Size = new System.Drawing.Size(572, 26);
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(857, 613);
            this.okButton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            // 
            // cancelBotton
            // 
            this.cancelBotton.Location = new System.Drawing.Point(956, 613);
            this.cancelBotton.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            // 
            // ContentPanel
            // 
            this.ContentPanel.Location = new System.Drawing.Point(5, 26);
            this.ContentPanel.Size = new System.Drawing.Size(1063, 648);
            // 
            // OpenFileDialogF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.ClientSize = new System.Drawing.Size(1073, 679);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1067, 586);
            this.Name = "OpenFileDialogF";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}