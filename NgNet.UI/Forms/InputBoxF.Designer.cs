namespace NgNet.UI.Forms
{
    partial class InputBoxF
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.notiLabel = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.inputTxtBox = new System.Windows.Forms.TextBox();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.splitContainer1);
            this.ContentPanel.Location = new System.Drawing.Point(3, 32);
            this.ContentPanel.Size = new System.Drawing.Size(765, 232);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.notiLabel);
            this.splitContainer1.Panel1.Controls.Add(this.btn_cancel);
            this.splitContainer1.Panel1.Controls.Add(this.btn_ok);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.splitContainer1.Panel1.SizeChanged += new System.EventHandler(this.splitContainer1_Panel1_SizeChanged);
            this.splitContainer1.Panel1MinSize = 80;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.inputTxtBox);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(10, 0, 10, 3);
            this.splitContainer1.Panel2MinSize = 68;
            this.splitContainer1.Size = new System.Drawing.Size(765, 232);
            this.splitContainer1.SplitterDistance = 80;
            this.splitContainer1.TabIndex = 7;
            // 
            // notiLabel
            // 
            this.notiLabel.BackColor = System.Drawing.Color.Transparent;
            this.notiLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.notiLabel.Location = new System.Drawing.Point(10, 12);
            this.notiLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.notiLabel.Name = "notiLabel";
            this.notiLabel.Size = new System.Drawing.Size(598, 68);
            this.notiLabel.TabIndex = 5;
            this.notiLabel.Text = "请输入";
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(616, 44);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(140, 33);
            this.btn_cancel.TabIndex = 3;
            this.btn_cancel.Text = "取消(C)";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Location = new System.Drawing.Point(616, 12);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(140, 33);
            this.btn_ok.TabIndex = 4;
            this.btn_ok.Text = "确定(&O)";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // inputTxtBox
            // 
            this.inputTxtBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputTxtBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.inputTxtBox.Location = new System.Drawing.Point(10, 0);
            this.inputTxtBox.Margin = new System.Windows.Forms.Padding(4);
            this.inputTxtBox.Multiline = true;
            this.inputTxtBox.Name = "inputTxtBox";
            this.inputTxtBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputTxtBox.Size = new System.Drawing.Size(745, 145);
            this.inputTxtBox.TabIndex = 1;
            this.inputTxtBox.Text = "defeattext";
            this.inputTxtBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tBox_input_KeyPress);
            // 
            // InputBoxF
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(771, 267);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(768, 228);
            this.Name = "InputBoxF";
            this.Opacity = 0.88D;
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "InputBox";
            this.Load += new System.EventHandler(this.InputBoxF_Load);
            this.Shown += new System.EventHandler(this.InputBoxF_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.Label notiLabel;
        public System.Windows.Forms.TextBox inputTxtBox;
        internal System.Windows.Forms.Button btn_ok;
        internal System.Windows.Forms.Button btn_cancel;

    }
}