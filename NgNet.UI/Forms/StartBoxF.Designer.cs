namespace NgNet.UI.Forms
{
    partial class StartBoxF
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
            this.msgLabel = new System.Windows.Forms.Label();
            this.infLabel = new System.Windows.Forms.Label();
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ContentPanel.Controls.Add(this.msgLabel);
            this.ContentPanel.Controls.Add(this.infLabel);
            this.ContentPanel.Location = new System.Drawing.Point(2, 2);
            this.ContentPanel.Size = new System.Drawing.Size(590, 360);
            this.ContentPanel.UseWaitCursor = true;
            // 
            // msgLabel
            // 
            this.msgLabel.AutoEllipsis = true;
            this.msgLabel.BackColor = System.Drawing.Color.Transparent;
            this.msgLabel.Location = new System.Drawing.Point(44, 290);
            this.msgLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.msgLabel.Name = "msgLabel";
            this.msgLabel.Size = new System.Drawing.Size(502, 33);
            this.msgLabel.TabIndex = 1;
            this.msgLabel.Text = "msg";
            this.msgLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.msgLabel.UseWaitCursor = true;
            // 
            // infLabel
            // 
            this.infLabel.BackColor = System.Drawing.Color.Transparent;
            this.infLabel.Location = new System.Drawing.Point(44, 102);
            this.infLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infLabel.Name = "infLabel";
            this.infLabel.Size = new System.Drawing.Size(502, 156);
            this.infLabel.TabIndex = 0;
            this.infLabel.Text = "info";
            this.infLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.infLabel.UseWaitCursor = true;
            // 
            // StartBoxF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.ClientSize = new System.Drawing.Size(594, 364);
            this.DoubleBuffered = true;
            this.Name = "StartBoxF";
            this.Padding = new System.Windows.Forms.Padding(2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartBox";
            this.UseWaitCursor = true;
            this.Load += new System.EventHandler(this.this_Load);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label infLabel;
        private System.Windows.Forms.Label msgLabel;
    }
}