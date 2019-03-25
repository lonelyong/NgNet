namespace NgNet.UI.Forms
{
    partial class MessageBoxF
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
            this.msgRichTextBox = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.exitTimer = new System.Windows.Forms.Timer(this.components);
            this.ContentPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.msgRichTextBox);
            this.ContentPanel.Controls.Add(this.button1);
            this.ContentPanel.Controls.Add(this.button2);
            this.ContentPanel.Controls.Add(this.button3);
            this.ContentPanel.Location = new System.Drawing.Point(3, 32);
            this.ContentPanel.Size = new System.Drawing.Size(688, 313);
            this.ContentPanel.SizeChanged += new System.EventHandler(this.panel_SizeChanged);
            // 
            // msgRichTextBox
            // 
            this.msgRichTextBox.AutoWordSelection = true;
            this.msgRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msgRichTextBox.Location = new System.Drawing.Point(10, 12);
            this.msgRichTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.msgRichTextBox.Name = "msgRichTextBox";
            this.msgRichTextBox.ReadOnly = true;
            this.msgRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.msgRichTextBox.ShowSelectionMargin = true;
            this.msgRichTextBox.Size = new System.Drawing.Size(668, 231);
            this.msgRichTextBox.TabIndex = 5;
            this.msgRichTextBox.Text = "message";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(504, 251);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(147, 33);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Transparent;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(358, 251);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(147, 33);
            this.button2.TabIndex = 3;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Transparent;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Location = new System.Drawing.Point(212, 251);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(147, 33);
            this.button3.TabIndex = 2;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // exitTimer
            // 
            this.exitTimer.Interval = 1000;
            this.exitTimer.Tick += new System.EventHandler(this.exitTimer_Tick);
            // 
            // MessageBoxF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.ClientSize = new System.Drawing.Size(694, 348);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(694, 339);
            this.Name = "MessageBoxF";
            this.Opacity = 0.88D;
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "提示您";
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer exitTimer;
        private System.Windows.Forms.RichTextBox msgRichTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}