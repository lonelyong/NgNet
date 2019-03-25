namespace NgNet.UI.Forms
{
    partial class FilterFileDialogF
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.infLabel = new System.Windows.Forms.Label();
            this.aheadLabel = new System.Windows.Forms.Label();
            this.backLabel = new System.Windows.Forms.Label();
            this.upLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelBotton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.nameComboBox = new System.Windows.Forms.ComboBox();
            this.filterComboBox = new NgNet.UI.Forms.ComboBoxEx();
            this.swordTBox = new System.Windows.Forms.TextBox();
            this.nameLabel = new System.Windows.Forms.Label();
            this.pathComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.naviTree = new System.Windows.Forms.TreeView();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.icoColumn = new System.Windows.Forms.DataGridViewImageColumn();
            this.nameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.typeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lengthColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.creattimeColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_dgv_parent = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dgv_refresh = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dgv_new = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dgv_new_dir = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dgv_new_txt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_dgv_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.itemCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_item_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_Select = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_cut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_rename = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_item_attr = new System.Windows.Forms.ToolStripMenuItem();
            this.naviCms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmi_navi_open = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_navi_attr = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi_navi_foldAll = new System.Windows.Forms.ToolStripMenuItem();
            this.ttip = new System.Windows.Forms.ToolTip(this.components);
            this.ContentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.dgvCms.SuspendLayout();
            this.itemCms.SuspendLayout();
            this.naviCms.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContentPanel
            // 
            this.ContentPanel.Controls.Add(this.infLabel);
            this.ContentPanel.Controls.Add(this.aheadLabel);
            this.ContentPanel.Controls.Add(this.backLabel);
            this.ContentPanel.Controls.Add(this.upLabel);
            this.ContentPanel.Controls.Add(this.okButton);
            this.ContentPanel.Controls.Add(this.cancelBotton);
            this.ContentPanel.Controls.Add(this.splitContainer2);
            this.ContentPanel.Controls.Add(this.swordTBox);
            this.ContentPanel.Controls.Add(this.nameLabel);
            this.ContentPanel.Controls.Add(this.pathComboBox);
            this.ContentPanel.Controls.Add(this.splitContainer1);
            this.ContentPanel.Location = new System.Drawing.Point(3, 32);
            this.ContentPanel.Size = new System.Drawing.Size(934, 804);
            this.ContentPanel.SizeChanged += new System.EventHandler(this.this_SizeChanged);
            // 
            // infLabel
            // 
            this.infLabel.AutoSize = true;
            this.infLabel.BackColor = System.Drawing.Color.Transparent;
            this.infLabel.Location = new System.Drawing.Point(23, 692);
            this.infLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.infLabel.Name = "infLabel";
            this.infLabel.Size = new System.Drawing.Size(152, 16);
            this.infLabel.TabIndex = 20;
            this.infLabel.Text = "文件夹：0  文件：0";
            // 
            // aheadLabel
            // 
            this.aheadLabel.BackColor = System.Drawing.Color.Transparent;
            this.aheadLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.aheadLabel.ForeColor = System.Drawing.Color.Black;
            this.aheadLabel.Location = new System.Drawing.Point(64, 12);
            this.aheadLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.aheadLabel.Name = "aheadLabel";
            this.aheadLabel.Size = new System.Drawing.Size(57, 30);
            this.aheadLabel.TabIndex = 19;
            this.aheadLabel.Text = "→";
            this.aheadLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.aheadLabel.MouseEnter += new System.EventHandler(this.naviLabel_MouseEnter);
            this.aheadLabel.MouseLeave += new System.EventHandler(this.naviLabel_MouseLeave);
            // 
            // backLabel
            // 
            this.backLabel.BackColor = System.Drawing.Color.Transparent;
            this.backLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.backLabel.ForeColor = System.Drawing.Color.Black;
            this.backLabel.Location = new System.Drawing.Point(9, 12);
            this.backLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.backLabel.Name = "backLabel";
            this.backLabel.Size = new System.Drawing.Size(57, 30);
            this.backLabel.TabIndex = 18;
            this.backLabel.Text = "←";
            this.backLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.backLabel.MouseEnter += new System.EventHandler(this.naviLabel_MouseEnter);
            this.backLabel.MouseLeave += new System.EventHandler(this.naviLabel_MouseLeave);
            // 
            // upLabel
            // 
            this.upLabel.BackColor = System.Drawing.Color.Transparent;
            this.upLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upLabel.ForeColor = System.Drawing.Color.Black;
            this.upLabel.Location = new System.Drawing.Point(120, 12);
            this.upLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.upLabel.Name = "upLabel";
            this.upLabel.Size = new System.Drawing.Size(84, 30);
            this.upLabel.TabIndex = 17;
            this.upLabel.Text = "↑(&U)";
            this.upLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.upLabel.Click += new System.EventHandler(this.naviLabel_Click);
            this.upLabel.MouseEnter += new System.EventHandler(this.naviLabel_MouseEnter);
            this.upLabel.MouseLeave += new System.EventHandler(this.naviLabel_MouseLeave);
            // 
            // okButton
            // 
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.okButton.ForeColor = System.Drawing.Color.Black;
            this.okButton.Location = new System.Drawing.Point(580, 675);
            this.okButton.Margin = new System.Windows.Forms.Padding(4);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(188, 33);
            this.okButton.TabIndex = 15;
            this.okButton.Text = "保存(&S)";
            this.okButton.UseVisualStyleBackColor = false;
            this.okButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // cancelBotton
            // 
            this.cancelBotton.BackColor = System.Drawing.Color.Transparent;
            this.cancelBotton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBotton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBotton.ForeColor = System.Drawing.Color.Black;
            this.cancelBotton.Location = new System.Drawing.Point(766, 675);
            this.cancelBotton.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBotton.Name = "cancelBotton";
            this.cancelBotton.Size = new System.Drawing.Size(132, 33);
            this.cancelBotton.TabIndex = 16;
            this.cancelBotton.Text = "取消(&C)";
            this.cancelBotton.UseVisualStyleBackColor = false;
            this.cancelBotton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer2.Location = new System.Drawing.Point(131, 635);
            this.splitContainer2.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.nameComboBox);
            this.splitContainer2.Panel1MinSize = 200;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.filterComboBox);
            this.splitContainer2.Panel2MinSize = 200;
            this.splitContainer2.Size = new System.Drawing.Size(766, 33);
            this.splitContainer2.SplitterDistance = 460;
            this.splitContainer2.SplitterWidth = 6;
            this.splitContainer2.TabIndex = 14;
            // 
            // nameComboBox
            // 
            this.nameComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.nameComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nameComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.nameComboBox.ForeColor = System.Drawing.Color.Crimson;
            this.nameComboBox.FormattingEnabled = true;
            this.nameComboBox.Location = new System.Drawing.Point(0, 0);
            this.nameComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.nameComboBox.Name = "nameComboBox";
            this.nameComboBox.Size = new System.Drawing.Size(458, 24);
            this.nameComboBox.TabIndex = 4;
            this.nameComboBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.nameComboBox_KeyPress);
            // 
            // filterComboBox
            // 
            this.filterComboBox.DisableMouseWheel = true;
            this.filterComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.filterComboBox.ForeColor = System.Drawing.Color.Purple;
            this.filterComboBox.FormattingEnabled = true;
            this.filterComboBox.Location = new System.Drawing.Point(0, 0);
            this.filterComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.filterComboBox.Name = "filterComboBox";
            this.filterComboBox.Size = new System.Drawing.Size(298, 24);
            this.filterComboBox.TabIndex = 5;
            this.filterComboBox.SelectedIndexChanged += new System.EventHandler(this.filterComboBox_SelectedIndexChanged);
            // 
            // swordTBox
            // 
            this.swordTBox.Location = new System.Drawing.Point(778, 12);
            this.swordTBox.Margin = new System.Windows.Forms.Padding(4);
            this.swordTBox.Multiline = true;
            this.swordTBox.Name = "swordTBox";
            this.swordTBox.Size = new System.Drawing.Size(130, 28);
            this.swordTBox.TabIndex = 11;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Location = new System.Drawing.Point(23, 643);
            this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(88, 16);
            this.nameLabel.TabIndex = 10;
            this.nameLabel.Text = "文件名称：";
            // 
            // pathComboBox
            // 
            this.pathComboBox.FormattingEnabled = true;
            this.pathComboBox.Location = new System.Drawing.Point(210, 12);
            this.pathComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.pathComboBox.Name = "pathComboBox";
            this.pathComboBox.Size = new System.Drawing.Size(554, 24);
            this.pathComboBox.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Location = new System.Drawing.Point(10, 52);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.naviTree);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(897, 563);
            this.splitContainer1.SplitterDistance = 130;
            this.splitContainer1.SplitterWidth = 6;
            this.splitContainer1.TabIndex = 1;
            // 
            // naviTree
            // 
            this.naviTree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.naviTree.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.naviTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.naviTree.ForeColor = System.Drawing.Color.DarkGreen;
            this.naviTree.FullRowSelect = true;
            this.naviTree.Indent = 12;
            this.naviTree.Location = new System.Drawing.Point(0, 0);
            this.naviTree.Margin = new System.Windows.Forms.Padding(4);
            this.naviTree.Name = "naviTree";
            this.naviTree.Size = new System.Drawing.Size(128, 561);
            this.naviTree.TabIndex = 0;
            this.naviTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.naviTree_AfterSelect);
            this.naviTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.naviTree_NodeMouseClick);
            this.naviTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.naviTree_NodeMouseDoubleClick);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.NullValue = " ";
            this.dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.dataGridView.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView.ColumnHeadersHeight = 17;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.icoColumn,
            this.nameColumn,
            this.typeColumn,
            this.lengthColumn,
            this.creattimeColumn});
            this.dataGridView.ContextMenuStrip = this.dgvCms;
            this.dataGridView.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.NullValue = " ";
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.EnableHeadersVisualStyles = false;
            this.dataGridView.GridColor = System.Drawing.Color.Silver;
            this.dataGridView.Location = new System.Drawing.Point(0, 0);
            this.dataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView.RowHeadersVisible = false;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.dataGridView.RowsDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridView.RowTemplate.Height = 19;
            this.dataGridView.RowTemplate.ReadOnly = true;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.ShowCellErrors = false;
            this.dataGridView.ShowRowErrors = false;
            this.dataGridView.Size = new System.Drawing.Size(759, 561);
            this.dataGridView.TabIndex = 2;
            this.dataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDoubleClick);
            this.dataGridView.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView_CellMouseDown);
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.SelectionChanged += new System.EventHandler(this.dataGridView_SelectionChanged);
            // 
            // icoColumn
            // 
            this.icoColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = "System.Drawing.String";
            this.icoColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.icoColumn.FillWeight = 51.16751F;
            this.icoColumn.Frozen = true;
            this.icoColumn.HeaderText = "";
            this.icoColumn.Name = "icoColumn";
            this.icoColumn.ReadOnly = true;
            this.icoColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.icoColumn.Width = 21;
            // 
            // nameColumn
            // 
            this.nameColumn.FillWeight = 149.1591F;
            this.nameColumn.HeaderText = "名称";
            this.nameColumn.Name = "nameColumn";
            this.nameColumn.ReadOnly = true;
            // 
            // typeColumn
            // 
            this.typeColumn.FillWeight = 93.22446F;
            this.typeColumn.HeaderText = "类型";
            this.typeColumn.Name = "typeColumn";
            this.typeColumn.ReadOnly = true;
            // 
            // lengthColumn
            // 
            this.lengthColumn.FillWeight = 93.22446F;
            this.lengthColumn.HeaderText = "大小";
            this.lengthColumn.Name = "lengthColumn";
            this.lengthColumn.ReadOnly = true;
            // 
            // creattimeColumn
            // 
            this.creattimeColumn.FillWeight = 93.22446F;
            this.creattimeColumn.HeaderText = "创建日期";
            this.creattimeColumn.Name = "creattimeColumn";
            this.creattimeColumn.ReadOnly = true;
            // 
            // dgvCms
            // 
            this.dgvCms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.dgvCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_dgv_parent,
            this.tsmi_dgv_refresh,
            this.tsmi_dgv_new,
            this.tsmi_dgv_paste});
            this.dgvCms.Name = "dgvCms";
            this.dgvCms.Size = new System.Drawing.Size(135, 116);
            this.dgvCms.Opening += new System.ComponentModel.CancelEventHandler(this.dgvCms_Opening);
            // 
            // tsmi_dgv_parent
            // 
            this.tsmi_dgv_parent.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_parent.Name = "tsmi_dgv_parent";
            this.tsmi_dgv_parent.Size = new System.Drawing.Size(134, 28);
            this.tsmi_dgv_parent.Text = "父目录";
            this.tsmi_dgv_parent.Click += new System.EventHandler(this.tsmi_dgv_parent_Click);
            // 
            // tsmi_dgv_refresh
            // 
            this.tsmi_dgv_refresh.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_refresh.Name = "tsmi_dgv_refresh";
            this.tsmi_dgv_refresh.Size = new System.Drawing.Size(134, 28);
            this.tsmi_dgv_refresh.Text = "刷新";
            this.tsmi_dgv_refresh.Click += new System.EventHandler(this.tsmi_dgv_refresh_Click);
            // 
            // tsmi_dgv_new
            // 
            this.tsmi_dgv_new.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_dgv_new_dir,
            this.tsmi_dgv_new_txt});
            this.tsmi_dgv_new.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_new.Name = "tsmi_dgv_new";
            this.tsmi_dgv_new.Size = new System.Drawing.Size(134, 28);
            this.tsmi_dgv_new.Text = "新建";
            // 
            // tsmi_dgv_new_dir
            // 
            this.tsmi_dgv_new_dir.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_new_dir.Name = "tsmi_dgv_new_dir";
            this.tsmi_dgv_new_dir.Size = new System.Drawing.Size(146, 30);
            this.tsmi_dgv_new_dir.Tag = ".txt";
            this.tsmi_dgv_new_dir.Text = "文件夹";
            this.tsmi_dgv_new_dir.Click += new System.EventHandler(this.tsmi_dgv_new_items_Click);
            // 
            // tsmi_dgv_new_txt
            // 
            this.tsmi_dgv_new_txt.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_new_txt.Name = "tsmi_dgv_new_txt";
            this.tsmi_dgv_new_txt.Size = new System.Drawing.Size(146, 30);
            this.tsmi_dgv_new_txt.Tag = "文件夹";
            this.tsmi_dgv_new_txt.Text = "文件";
            this.tsmi_dgv_new_txt.Click += new System.EventHandler(this.tsmi_dgv_new_items_Click);
            // 
            // tsmi_dgv_paste
            // 
            this.tsmi_dgv_paste.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_dgv_paste.Name = "tsmi_dgv_paste";
            this.tsmi_dgv_paste.Size = new System.Drawing.Size(134, 28);
            this.tsmi_dgv_paste.Text = "粘贴";
            // 
            // itemCms
            // 
            this.itemCms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.itemCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_item_open,
            this.tsmi_item_Select,
            this.tsmi_item_paste,
            this.tsmi_item_cut,
            this.tsmi_item_delete,
            this.tsmi_item_rename,
            this.tsmi_item_attr});
            this.itemCms.Name = "itemCms";
            this.itemCms.Size = new System.Drawing.Size(153, 200);
            this.itemCms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_item_Opening);
            // 
            // tsmi_item_open
            // 
            this.tsmi_item_open.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_open.Name = "tsmi_item_open";
            this.tsmi_item_open.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_open.Text = "打开";
            this.tsmi_item_open.Click += new System.EventHandler(this._Tsmi_item_Open_Click);
            // 
            // tsmi_item_Select
            // 
            this.tsmi_item_Select.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_Select.Name = "tsmi_item_Select";
            this.tsmi_item_Select.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_Select.Text = "选定目录";
            this.tsmi_item_Select.Click += new System.EventHandler(this._Tsmi_item_Select_Click);
            // 
            // tsmi_item_paste
            // 
            this.tsmi_item_paste.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_paste.Name = "tsmi_item_paste";
            this.tsmi_item_paste.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_paste.Text = "粘贴";
            // 
            // tsmi_item_cut
            // 
            this.tsmi_item_cut.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_cut.Name = "tsmi_item_cut";
            this.tsmi_item_cut.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_cut.Text = "剪切";
            // 
            // tsmi_item_delete
            // 
            this.tsmi_item_delete.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_delete.Name = "tsmi_item_delete";
            this.tsmi_item_delete.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_delete.Text = "删除";
            this.tsmi_item_delete.Click += new System.EventHandler(this.tsmi_item_delete_Click);
            // 
            // tsmi_item_rename
            // 
            this.tsmi_item_rename.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_rename.Name = "tsmi_item_rename";
            this.tsmi_item_rename.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_rename.Text = "重命名";
            this.tsmi_item_rename.Click += new System.EventHandler(this.tsmi_item_rename_Click);
            // 
            // tsmi_item_attr
            // 
            this.tsmi_item_attr.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_item_attr.Name = "tsmi_item_attr";
            this.tsmi_item_attr.Size = new System.Drawing.Size(152, 28);
            this.tsmi_item_attr.Text = "属性";
            this.tsmi_item_attr.Click += new System.EventHandler(this.tsmi_item_attribute_Click);
            // 
            // naviCms
            // 
            this.naviCms.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.naviCms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmi_navi_open,
            this.tsmi_navi_attr,
            this.tsmi_navi_foldAll});
            this.naviCms.Name = "naviCms";
            this.naviCms.Size = new System.Drawing.Size(153, 88);
            this.naviCms.Opening += new System.ComponentModel.CancelEventHandler(this.cms_navi_Opening);
            // 
            // tsmi_navi_open
            // 
            this.tsmi_navi_open.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_navi_open.Name = "tsmi_navi_open";
            this.tsmi_navi_open.Size = new System.Drawing.Size(152, 28);
            this.tsmi_navi_open.Text = "打开";
            this.tsmi_navi_open.Click += new System.EventHandler(this.tsmi_navi_open_Click);
            // 
            // tsmi_navi_attr
            // 
            this.tsmi_navi_attr.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_navi_attr.Name = "tsmi_navi_attr";
            this.tsmi_navi_attr.Size = new System.Drawing.Size(152, 28);
            this.tsmi_navi_attr.Text = "属性";
            this.tsmi_navi_attr.Click += new System.EventHandler(this.tsmi_navi_attribute_Click);
            // 
            // tsmi_navi_foldAll
            // 
            this.tsmi_navi_foldAll.ForeColor = System.Drawing.Color.Teal;
            this.tsmi_navi_foldAll.Name = "tsmi_navi_foldAll";
            this.tsmi_navi_foldAll.Size = new System.Drawing.Size(152, 28);
            this.tsmi_navi_foldAll.Text = "折叠所有";
            this.tsmi_navi_foldAll.Click += new System.EventHandler(this.tsmi_navi_foldAll_Click);
            // 
            // ttip
            // 
            this.ttip.AutomaticDelay = 300;
            this.ttip.AutoPopDelay = 12000;
            this.ttip.InitialDelay = 1000;
            this.ttip.ReshowDelay = 300;
            this.ttip.ToolTipTitle = "< _ >";
            // 
            // FilterFileDialogF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
            this.CancelButton = this.cancelBotton;
            this.ClientSize = new System.Drawing.Size(940, 839);
            this.MinimumSize = new System.Drawing.Size(924, 543);
            this.Name = "FilterFileDialogF";
            this.Opacity = 0.88D;
            this.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FilterFileDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.this_FormClosing);
            this.Load += new System.EventHandler(this.this_Load);
            this.Shown += new System.EventHandler(this.this_Shown);
            this.Controls.SetChildIndex(this.ContentPanel, 0);
            this.ContentPanel.ResumeLayout(false);
            this.ContentPanel.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.dgvCms.ResumeLayout(false);
            this.itemCms.ResumeLayout(false);
            this.naviCms.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label infLabel;
        private System.Windows.Forms.Label aheadLabel;
        private System.Windows.Forms.Label backLabel;
        private System.Windows.Forms.Label upLabel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TextBox swordTBox;
        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.ComboBox pathComboBox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView naviTree;
        private System.Windows.Forms.ToolTip ttip;
        private System.Windows.Forms.ContextMenuStrip naviCms;
        private System.Windows.Forms.ToolStripMenuItem tsmi_navi_open;
        private System.Windows.Forms.ToolStripMenuItem tsmi_navi_attr;
        private System.Windows.Forms.ToolStripMenuItem tsmi_navi_foldAll;
        private System.Windows.Forms.ContextMenuStrip itemCms;
        private System.Windows.Forms.ToolStripMenuItem tsmi_item_paste;
        private System.Windows.Forms.ToolStripMenuItem tsmi_item_cut;
        private System.Windows.Forms.ToolStripMenuItem tsmi_item_delete;
        private System.Windows.Forms.ToolStripMenuItem tsmi_item_rename;
        private System.Windows.Forms.ToolStripMenuItem tsmi_item_attr;
        private System.Windows.Forms.ContextMenuStrip dgvCms;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_parent;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_refresh;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_new;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_new_dir;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_new_txt;
        private System.Windows.Forms.ToolStripMenuItem tsmi_dgv_paste;
        protected System.Windows.Forms.ComboBox nameComboBox;
        protected System.Windows.Forms.DataGridView dataGridView;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_item_open;
        protected System.Windows.Forms.ToolStripMenuItem tsmi_item_Select;
        protected System.Windows.Forms.DataGridViewImageColumn icoColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn nameColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn typeColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn lengthColumn;
        protected System.Windows.Forms.DataGridViewTextBoxColumn creattimeColumn;
        private NgNet.UI.Forms.ComboBoxEx filterComboBox;
        protected System.Windows.Forms.Button okButton;
        protected System.Windows.Forms.Button cancelBotton;
    }
}