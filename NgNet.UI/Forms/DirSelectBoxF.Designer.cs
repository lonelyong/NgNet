namespace NgNet.UI.Forms
{
    partial class DirSelectBoxF
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
            this.pathTBox = new System.Windows.Forms.TextBox();
            this.btn_Select = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.naviTree = new System.Windows.Forms.TreeView();
            this.naviCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_navi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_navi_attribute = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_navi_foldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_navi_del = new System.Windows.Forms.ToolStripMenuItem();
            this.Tsmi_navi_new = new System.Windows.Forms.ToolStripMenuItem();
            this.ContentPanel.SuspendLayout();
            this.naviCms.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.pathTBox);
            this.ContentPanel.Controls.Add(this.btn_Select);
            this.ContentPanel.Controls.Add(this.btn_cancel);
            this.ContentPanel.Controls.Add(this.naviTree);
            this.ContentPanel.Location = new System.Drawing.Point(3, 26);
            this.ContentPanel.Size = new System.Drawing.Size(351, 450);
            this.ContentPanel.SizeChanged += new System.EventHandler(this.pnael_SizeChanged);
            // 
            // pathTBox
            // 
            this.pathTBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pathTBox.ForeColor = System.Drawing.Color.Brown;
            this.pathTBox.Location = new System.Drawing.Point(10, 371);
            this.pathTBox.Margin = new System.Windows.Forms.Padding(4);
            this.pathTBox.Name = "pathTBox";
            this.pathTBox.Size = new System.Drawing.Size(333, 21);
            this.pathTBox.TabIndex = 6;
            this.pathTBox.TextChanged += new System.EventHandler(this.pathTBox_TextChanged);
            // 
            // btn_Select
            // 
            this.btn_Select.BackColor = System.Drawing.Color.Transparent;
            this.btn_Select.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Select.Location = new System.Drawing.Point(185, 410);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Size = new System.Drawing.Size(75, 23);
            this.btn_Select.TabIndex = 4;
            this.btn_Select.Text = "选择(&O)";
            this.btn_Select.UseVisualStyleBackColor = false;
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.Transparent;
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_cancel.Location = new System.Drawing.Point(268, 410);
            this.btn_cancel.Margin = new System.Windows.Forms.Padding(4);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 5;
            this.btn_cancel.Text = "取消(&C)";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // naviTree
            // 
            this.naviTree.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.naviTree.ForeColor = System.Drawing.Color.DarkGreen;
            this.naviTree.Location = new System.Drawing.Point(10, 12);
            this.naviTree.Margin = new System.Windows.Forms.Padding(0);
            this.naviTree.Name = "naviTree";
            this.naviTree.Size = new System.Drawing.Size(333, 344);
            this.naviTree.TabIndex = 1;
            this.naviTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_navi_AfterSelect);
            this.naviTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_navi_NodeMouseClick);
            this.naviTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_navi_NodeMouseDoubleClick);
            // 
            // naviCms
            // 
            this.naviCms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.naviCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_navi_open,
            this.Tsmi_navi_attribute,
            this.Tsmi_navi_foldAll,
            this.Tsmi_navi_del,
            this.Tsmi_navi_new});
            this.naviCms.Name = "Cms_navi";
            this.naviCms.Size = new System.Drawing.Size(125, 114);
            this.naviCms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_navi_Opening);
            // 
            // tsmi_navi_open
            // 
            this.tsmi_navi_open.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_navi_open.Name = "tsmi_navi_open";
            this.tsmi_navi_open.Size = new System.Drawing.Size(124, 22);
            this.tsmi_navi_open.Text = "打开";
            this.tsmi_navi_open.Click += new System.EventHandler(this.tsmi_navi_open_Click);
            // 
            // Tsmi_navi_attribute
            // 
            this.Tsmi_navi_attribute.ForeColor = System.Drawing.Color.Teal;
            this.Tsmi_navi_attribute.Name = "Tsmi_navi_attribute";
            this.Tsmi_navi_attribute.Size = new System.Drawing.Size(124, 22);
            this.Tsmi_navi_attribute.Text = "属性";
            this.Tsmi_navi_attribute.Click += new System.EventHandler(this.tsmi_navi_attribute_Click);
            // 
            // Tsmi_navi_foldAll
            // 
            this.Tsmi_navi_foldAll.ForeColor = System.Drawing.Color.Teal;
            this.Tsmi_navi_foldAll.Name = "Tsmi_navi_foldAll";
            this.Tsmi_navi_foldAll.Size = new System.Drawing.Size(124, 22);
            this.Tsmi_navi_foldAll.Text = "折叠所有";
            this.Tsmi_navi_foldAll.Click += new System.EventHandler(this.tsmi_navi_foldAll_Click);
            // 
            // Tsmi_navi_del
            // 
            this.Tsmi_navi_del.ForeColor = System.Drawing.Color.Teal;
            this.Tsmi_navi_del.Name = "Tsmi_navi_del";
            this.Tsmi_navi_del.Size = new System.Drawing.Size(124, 22);
            this.Tsmi_navi_del.Text = "删除";
            this.Tsmi_navi_del.Click += new System.EventHandler(this.tsmi_navi_del_Click);
            // 
            // Tsmi_navi_new
            // 
            this.Tsmi_navi_new.ForeColor = System.Drawing.Color.Teal;
            this.Tsmi_navi_new.Name = "Tsmi_navi_new";
            this.Tsmi_navi_new.Size = new System.Drawing.Size(124, 22);
            this.Tsmi_navi_new.Text = "新建";
            this.Tsmi_navi_new.Click += new System.EventHandler(this.tsmi_navi_new_Click);
            // 
            // DirSelectBoxF
            // 
            this.AcceptButton = this.btn_Select;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(357, 479);
            this.MinimumSize = new System.Drawing.Size(200, 100);
            this.Name = "DirSelectBoxF";
            this.Opacity = 0.9D;
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "DirSelect";
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.naviCms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView naviTree;
        private System.Windows.Forms.Button btn_Select;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox pathTBox;
        internal System.Windows.Forms.ContextMenuStrip naviCms;
        internal System.Windows.Forms.ToolStripMenuItem tsmi_navi_open;
        internal System.Windows.Forms.ToolStripMenuItem Tsmi_navi_attribute;
        internal System.Windows.Forms.ToolStripMenuItem Tsmi_navi_foldAll;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_navi_del;
        private System.Windows.Forms.ToolStripMenuItem Tsmi_navi_new;
    }
}