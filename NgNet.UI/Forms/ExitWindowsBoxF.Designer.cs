namespace NgNet.UI.Forms
{
    partial class ExitWindowsBoxF
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
            this.components = new System.ComponentModel.Container();
            this.timeLabel = new System.Windows.Forms.Label();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_ok = new System.Windows.Forms.Button();
            this.itemPanel = new System.Windows.Forms.Panel();
            this.restartOptionComboBox = new System.Windows.Forms.ComboBox();
            this.exitTimer = new System.Windows.Forms.Timer(this.components);
            this.ContentPanel.SuspendLayout();
            this.itemPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.timeLabel);
            this.ContentPanel.Controls.Add(this.btn_cancel);
            this.ContentPanel.Controls.Add(this.btn_ok);
            this.ContentPanel.Controls.Add(this.itemPanel);
            this.ContentPanel.Location = new System.Drawing.Point(3, 32);
            this.ContentPanel.Size = new System.Drawing.Size(701, 429);
            this.ContentPanel.SizeChanged += new System.EventHandler(this.panel_SizeChanged);
            // 
            // timeLabel
            // 
            this.timeLabel.BackColor = System.Drawing.Color.Transparent;
            this.timeLabel.ForeColor = System.Drawing.Color.Maroon;
            this.timeLabel.Location = new System.Drawing.Point(87, 146);
            this.timeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(528, 27);
            this.timeLabel.TabIndex = 7;
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(357, 350);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(144, 33);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "取消";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_ok
            // 
            this.btn_ok.BackColor = System.Drawing.Color.Transparent;
            this.btn_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ok.Location = new System.Drawing.Point(211, 350);
            this.btn_ok.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(147, 33);
            this.btn_ok.TabIndex = 5;
            this.btn_ok.Text = "确定";
            this.btn_ok.UseVisualStyleBackColor = false;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // itemPanel
            // 
            this.itemPanel.AutoSize = true;
            this.itemPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itemPanel.Controls.Add(this.restartOptionComboBox);
            this.itemPanel.Location = new System.Drawing.Point(90, 90);
            this.itemPanel.Margin = new System.Windows.Forms.Padding(4);
            this.itemPanel.Name = "itemPanel";
            this.itemPanel.Size = new System.Drawing.Size(524, 35);
            this.itemPanel.TabIndex = 3;
            // 
            // restartOptionComboBox
            // 
            this.restartOptionComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.restartOptionComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.restartOptionComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.restartOptionComboBox.ForeColor = System.Drawing.Color.DarkSlateGray;
            this.restartOptionComboBox.FormattingEnabled = true;
            this.restartOptionComboBox.Location = new System.Drawing.Point(0, 0);
            this.restartOptionComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.restartOptionComboBox.Name = "restartOptionComboBox";
            this.restartOptionComboBox.Size = new System.Drawing.Size(522, 24);
            this.restartOptionComboBox.TabIndex = 2;
            this.restartOptionComboBox.SelectedIndexChanged += new System.EventHandler(this.restartOptionComboBox_SelectedIndexChanged);
            // 
            // exitTimer
            // 
            this.exitTimer.Interval = 1000;
            this.exitTimer.Tick += new System.EventHandler(this.exitTimer_Tick);
            // 
            // ExitWindowsBoxF
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.ClientSize = new System.Drawing.Size(707, 464);
            this.Name = "ExitWindowsBoxF";
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Exit Windows";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.itemPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox restartOptionComboBox;
        private System.Windows.Forms.Panel itemPanel;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Timer exitTimer;
    }
}