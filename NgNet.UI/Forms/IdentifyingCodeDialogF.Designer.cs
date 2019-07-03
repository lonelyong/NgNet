namespace NgNet.UI.Forms
{
    partial class IdentifyingCodeDialogF
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.codeTextBox = new System.Windows.Forms.TextBox();
            this.boxPanel = new System.Windows.Forms.Panel();
            this.changeLinkLabel = new System.Windows.Forms.LinkLabel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.ntiLabel = new System.Windows.Forms.Label();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.boxPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.ntiLabel);
            this.ContentPanel.Controls.Add(this.bottomPanel);
            this.ContentPanel.Controls.Add(this.pictureBox1);
            this.ContentPanel.Padding = new System.Windows.Forms.Padding(12, 12, 12, 24);
            this.ContentPanel.Size = new System.Drawing.Size(344, 163);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(320, 54);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // codeTextBox
            // 
            this.codeTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.codeTextBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.codeTextBox.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.codeTextBox.Location = new System.Drawing.Point(1, 1);
            this.codeTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.codeTextBox.Name = "codeTextBox";
            this.codeTextBox.Size = new System.Drawing.Size(256, 24);
            this.codeTextBox.TabIndex = 2;
            this.codeTextBox.TextChanged += new System.EventHandler(this.codeTextBox_TextChanged);
            // 
            // boxPanel
            // 
            this.boxPanel.BackColor = System.Drawing.Color.Transparent;
            this.boxPanel.Controls.Add(this.codeTextBox);
            this.boxPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.boxPanel.Location = new System.Drawing.Point(0, 0);
            this.boxPanel.Margin = new System.Windows.Forms.Padding(4);
            this.boxPanel.Name = "boxPanel";
            this.boxPanel.Padding = new System.Windows.Forms.Padding(1);
            this.boxPanel.Size = new System.Drawing.Size(258, 26);
            this.boxPanel.TabIndex = 3;
            // 
            // changeLinkLabel
            // 
            this.changeLinkLabel.Dock = System.Windows.Forms.DockStyle.Right;
            this.changeLinkLabel.Location = new System.Drawing.Point(276, 0);
            this.changeLinkLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.changeLinkLabel.Name = "changeLinkLabel";
            this.changeLinkLabel.Size = new System.Drawing.Size(44, 26);
            this.changeLinkLabel.TabIndex = 3;
            this.changeLinkLabel.TabStop = true;
            this.changeLinkLabel.Text = "更换";
            this.changeLinkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.changeLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.changeLinkLabel_LinkClicked);
            // 
            // bottomPanel
            // 
            this.bottomPanel.BackColor = System.Drawing.Color.Transparent;
            this.bottomPanel.Controls.Add(this.boxPanel);
            this.bottomPanel.Controls.Add(this.changeLinkLabel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(12, 113);
            this.bottomPanel.Margin = new System.Windows.Forms.Padding(4);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(320, 26);
            this.bottomPanel.TabIndex = 4;
            // 
            // ntiLabel
            // 
            this.ntiLabel.AutoSize = true;
            this.ntiLabel.BackColor = System.Drawing.Color.Transparent;
            this.ntiLabel.Location = new System.Drawing.Point(9, 80);
            this.ntiLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.ntiLabel.Name = "ntiLabel";
            this.ntiLabel.Size = new System.Drawing.Size(0, 12);
            this.ntiLabel.TabIndex = 5;
            // 
            // IdentifyingCodeDialogF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(352, 195);
            this.Name = "IdentifyingCodeDialogF";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "IdentifyingCodeDialogF";
            this.Load += new System.EventHandler(this.IdentifyingCodeDialogF_Load);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.boxPanel.ResumeLayout(false);
            this.boxPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel boxPanel;
        private System.Windows.Forms.TextBox codeTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel changeLinkLabel;
        private System.Windows.Forms.Panel bottomPanel;
        private System.Windows.Forms.Label ntiLabel;
    }
}