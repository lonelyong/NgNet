namespace NgNet.UI.Forms
{
    partial class SaveFileDialogF
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveFileDialogF));
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameComboBox
            // 
            this.nameComboBox.TextChanged += new System.EventHandler(this.nameComboBox_TextChanged);
            // 
            // SaveFileDialogF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SaveFileDialogF";
            this.Text = "SaveDialog";
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

    }
}