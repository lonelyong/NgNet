namespace NgNet.UI.Forms
{
    partial class ExitBoxF
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
            this.lb_notice = new System.Windows.Forms.Label();
            this.pictureBox_logo = new System.Windows.Forms.PictureBox();
            this.barPanel = new System.Windows.Forms.Panel();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.ckBox_showNext = new System.Windows.Forms.CheckBox();
            this.ckBox_min = new System.Windows.Forms.CheckBox();
            this.ckBox_exit = new System.Windows.Forms.CheckBox();
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).BeginInit();
            this.barPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.lb_notice);
            this.ContentPanel.Controls.Add(this.pictureBox_logo);
            this.ContentPanel.Controls.Add(this.barPanel);
            this.ContentPanel.Controls.Add(this.ckBox_min);
            this.ContentPanel.Controls.Add(this.ckBox_exit);
            this.ContentPanel.Location = new System.Drawing.Point(3, 26);
            this.ContentPanel.Size = new System.Drawing.Size(424, 182);
            // 
            // lb_notice
            // 
            this.lb_notice.AutoSize = true;
            this.lb_notice.BackColor = System.Drawing.Color.Transparent;
            this.lb_notice.Location = new System.Drawing.Point(194, 12);
            this.lb_notice.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lb_notice.Name = "lb_notice";
            this.lb_notice.Size = new System.Drawing.Size(89, 12);
            this.lb_notice.TabIndex = 5;
            this.lb_notice.Text = "您想怎样继续？";
            // 
            // pictureBox_logo
            // 
            this.pictureBox_logo.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_logo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_logo.ErrorImage = null;
            this.pictureBox_logo.InitialImage = null;
            this.pictureBox_logo.Location = new System.Drawing.Point(36, 12);
            this.pictureBox_logo.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_logo.Name = "pictureBox_logo";
            this.pictureBox_logo.Size = new System.Drawing.Size(100, 100);
            this.pictureBox_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_logo.TabIndex = 4;
            this.pictureBox_logo.TabStop = false;
            // 
            // barPanel
            // 
            this.barPanel.Controls.Add(this.button_cancel);
            this.barPanel.Controls.Add(this.button_ok);
            this.barPanel.Controls.Add(this.ckBox_showNext);
            this.barPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barPanel.Location = new System.Drawing.Point(0, 117);
            this.barPanel.Margin = new System.Windows.Forms.Padding(4);
            this.barPanel.Name = "barPanel";
            this.barPanel.Size = new System.Drawing.Size(424, 65);
            this.barPanel.TabIndex = 3;
            // 
            // button_cancel
            // 
            this.button_cancel.BackColor = System.Drawing.Color.Transparent;
            this.button_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Location = new System.Drawing.Point(321, 23);
            this.button_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "取消";
            this.button_cancel.UseVisualStyleBackColor = false;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.BackColor = System.Drawing.Color.Transparent;
            this.button_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_ok.Location = new System.Drawing.Point(238, 23);
            this.button_ok.Margin = new System.Windows.Forms.Padding(4);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "确定";
            this.button_ok.UseVisualStyleBackColor = false;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // ckBox_showNext
            // 
            this.ckBox_showNext.AutoSize = true;
            this.ckBox_showNext.BackColor = System.Drawing.Color.Transparent;
            this.ckBox_showNext.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.ckBox_showNext.Location = new System.Drawing.Point(36, 30);
            this.ckBox_showNext.Margin = new System.Windows.Forms.Padding(4);
            this.ckBox_showNext.Name = "ckBox_showNext";
            this.ckBox_showNext.Size = new System.Drawing.Size(96, 16);
            this.ckBox_showNext.TabIndex = 2;
            this.ckBox_showNext.Text = "下次不再显示";
            this.ckBox_showNext.UseVisualStyleBackColor = false;
            this.ckBox_showNext.CheckedChanged += new System.EventHandler(this.checkBoxs_CheckedChanged);
            // 
            // ckBox_min
            // 
            this.ckBox_min.AutoSize = true;
            this.ckBox_min.BackColor = System.Drawing.Color.Transparent;
            this.ckBox_min.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.ckBox_min.Location = new System.Drawing.Point(220, 67);
            this.ckBox_min.Margin = new System.Windows.Forms.Padding(4);
            this.ckBox_min.Name = "ckBox_min";
            this.ckBox_min.Size = new System.Drawing.Size(108, 16);
            this.ckBox_min.TabIndex = 0;
            this.ckBox_min.Text = "最小化到通知栏";
            this.ckBox_min.UseVisualStyleBackColor = false;
            this.ckBox_min.CheckedChanged += new System.EventHandler(this.checkBoxs_CheckedChanged);
            // 
            // ckBox_exit
            // 
            this.ckBox_exit.AutoSize = true;
            this.ckBox_exit.BackColor = System.Drawing.Color.Transparent;
            this.ckBox_exit.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.ckBox_exit.Location = new System.Drawing.Point(220, 43);
            this.ckBox_exit.Margin = new System.Windows.Forms.Padding(4);
            this.ckBox_exit.Name = "ckBox_exit";
            this.ckBox_exit.Size = new System.Drawing.Size(96, 16);
            this.ckBox_exit.TabIndex = 1;
            this.ckBox_exit.Text = "直接关闭程序";
            this.ckBox_exit.UseVisualStyleBackColor = false;
            this.ckBox_exit.CheckedChanged += new System.EventHandler(this.checkBoxs_CheckedChanged);
            // 
            // ExitBoxF
            // 
            this.AcceptButton = this.button_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.CancelButton = this.button_cancel;
            this.ClientSize = new System.Drawing.Size(430, 211);
            this.Name = "ExitBoxF";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ExitBox";
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_logo)).EndInit();
            this.barPanel.ResumeLayout(false);
            this.barPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_notice;
        private System.Windows.Forms.PictureBox pictureBox_logo;
        private System.Windows.Forms.Panel barPanel;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.CheckBox ckBox_showNext;
        private System.Windows.Forms.CheckBox ckBox_min;
        private System.Windows.Forms.CheckBox ckBox_exit;
    }
}