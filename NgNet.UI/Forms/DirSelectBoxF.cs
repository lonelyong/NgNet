using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class DirSelectBoxF : TitleableForm, IFileDialogWindow
    {
        #region private fields
        private TreeNode _currentNode = new TreeNode();//存储当前点击的节点

        private MenuRender _menuRender = new MenuRender();

        private string _enterPath = IOComm.DEFAULT_PATH;
        #endregion

        #region private properties
        private TreeNode rootNode
        {
            get
            {
                return naviTree.Nodes[naviTree.Nodes.IndexOfKey("ThisComputer")];
            }
        }
        #endregion

        #region  public properties
        /// <summary>
        /// 获取或设置一个值，该值为首次进入对话框时默认的路径
        /// </summary>
        public string  EnterPath
        {
            set 
            {
                _enterPath = IOComm.EnterPathTest(value);
            }
            get
            {
                return _enterPath;
            }
        }
        /// <summary>
        /// 获取或设置一个值该值为用户选定的路径
        /// </summary>
        public string InputPath { get; private set; }
        /// <summary>
        /// 获取或设置一个值该值指示当用户输入的路径不存在时是否提示
        /// </summary>
        public bool CheckPathExists { get; set; }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                pathTBox.BackColor = value;
                naviTree.BackColor = value;
                setMenuRender();
            }
        }

        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                pathTBox.ForeColor = value;
                naviTree.ForeColor = value;
                btn_Select.ForeColor = value;
                btn_cancel.ForeColor = value;
                setMenuRender();
            }
        }

        public override Color BorderColor
        {
            set
            {
                base.BorderColor = value;
                setMenuRender();
            }
            get
            {
                return base.BorderColor;
            }
        }
        #endregion

        #region constructor destructor
        public DirSelectBoxF()
        {
            InitializeComponent();
            //  初始化
            DialogResult = DialogResult.None;
            Resizeable = true;
            TitleBar.Style = TitleBarStyles.MaxEnd;

            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            Size = new Size(399, 528);
            Text = DirSelectBox.DEFAULT_TITLE;

            // 字段初始化
            CheckPathExists =true;
            Comm.Initialize(this, FormHelper);
        }

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                value = string.IsNullOrWhiteSpace( value ) ? "选择文件夹" : value;
                base.Text = value;
            }
        }
        #endregion

        #region private methods
        #region button<ok - cancel>
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.InputPath = null;
            this.Close();
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(InputPath))
            {
                MessageBox.Show(this, "您未有选定文件夹,请选择目录或手动输入目录", "",MessageBoxButtons.OK ,DialogResult.OK ,12);
                return;
            }
            if (!IO.PathHelper.IsPath( InputPath ))
            {
                MessageBox.Show(this, "您输入的路径不合法，请检查", "", MessageBoxButtons.OK, DialogResult.OK, 12);
                pathTBox.Focus();
                return;
            }
            InputPath = InputPath.Trim();
            if ( CheckPathExists && !Directory.Exists(InputPath))
            {
                if (MessageBox.Show(this, "您指定的文件夹不存在，是否继续？", "", MessageBoxButtons.YesNo, DialogResult.Yes, 12) == DialogResult.No)
                {
                    pathTBox.Focus();
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region this
        private void this_Load(object sender, EventArgs e)
        {
            setToolStrip();
            // 应用通用设置
            Comm.ApplyComm(this);
            // 初始化 navi
            IOComm.ResetNavi(naviTree, naviCms);
        }

        private void this_Shown(object sender, EventArgs e)
        {
            IOComm.LocateNode(rootNode, EnterPath, naviCms);
        }

        private void pnael_SizeChanged(object sender, EventArgs e)
        {
            this.naviTree.Location = new Point(Comm.DISTANCE_LEFT, Comm.DISTANCE_TOP);
            this.naviTree.Width = ContentPanel.Width - naviTree.Left * 2;

            this.btn_cancel.Top = ContentPanel.Height - btn_cancel.Height - Comm.DISTANCE_DOWN;
            this.btn_Select.Top = btn_cancel.Top;
            this.btn_cancel.Left = naviTree.Right - btn_cancel.Width;
            this.btn_Select.Left = btn_cancel.Left - btn_Select.Width + 1;

            this.pathTBox.Width = naviTree.Width;
            this.pathTBox.Left  = Comm.DISTANCE_LEFT;
            this.pathTBox.Top   = btn_cancel.Top - pathTBox.Height- Comm.DISTANCE_INTERVAL_V;

            this.naviTree.Height = pathTBox.Top - naviTree.Top - Comm.DISTANCE_INTERVAL_V;
        }
        #endregion

        #region treeview
        private void tv_navi_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0 || e.Button == MouseButtons.Right)
                return;
            if (e.Node.Nodes.Count == 0)
            {
                IOComm.FillNode(e.Node, naviCms);
            }
        }

        private void tv_navi_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            naviTree.SelectedNode = e.Node;
        }

        private void tv_navi_AfterSelect(object sender, TreeViewEventArgs e)
        {
            _currentNode.BackColor = BackColor;
            _currentNode.ForeColor = ForeColor;
            e.Node.BackColor = Drawing.ColorHelper.GetSimilarColor(BackColor, true, Level.Level6);
            e.Node.ForeColor = Drawing.ColorHelper.GetOppositeColor(e.Node.BackColor);
            _currentNode = e.Node;
            if (e.Node.Level > 0)
            {
                pathTBox.Text = e.Node.Name;
            }
        }
        #endregion

        #region cms
        private void cms_navi_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tsmi_navi_open.Text = _currentNode.IsExpanded ? "折叠" : "打开";
        }

        private void tsmi_navi_open_Click(object sender, EventArgs e)
        {
            if (naviTree.SelectedNode.IsExpanded)
            {
                naviTree.SelectedNode.Collapse();
            }
            else
            {
                IOComm.FillNode(_currentNode, naviCms);
            }
        }

        private void tsmi_navi_foldAll_Click(object sender, EventArgs e)
        {
            naviTree.SelectedNode = TreeViewHelper.GetParentByLevel(naviTree.SelectedNode, 1);
            naviTree.SelectedNode.Collapse();
        }

        private void tsmi_navi_attribute_Click(object sender, EventArgs e)
        {
            Windows.Explore.ShowPropertiesDialog(_currentNode.Name);
        }

        private void tsmi_navi_del_Click(object sender, EventArgs e)
        {
            IOComm.NodeDelete(_currentNode);
        }

        private void tsmi_navi_new_Click(object sender, EventArgs e)
        {
            IOComm.NodeCreate(_currentNode);
        }

        private void setToolStrip()
        {
            double opacity = 0.85d;
            ContextMenuStrip[] cmss = new ContextMenuStrip[] { this.naviCms };
            foreach (ContextMenuStrip cms in cmss)
            {
                ContextMenuStripHelper.SetOpacity(cms, opacity);
            }
        }

        private void setMenuRender()
        {
            _menuRender.Colors.DropDownItemBorderColor = BorderColor;
            _menuRender.Colors.DropDownItemEndColor = BackColor;
            _menuRender.Colors.DropDownItemStartColor = NgNet.Drawing.ColorHelper.GetSimilarColor(BorderColor, false, NgNet.Level.Level8); ;
            _menuRender.Colors.DropDownBackEndColor = BackColor;
            _menuRender.Colors.DropDownBackStartColor = NgNet.Drawing.ColorHelper.GetSimilarColor(BorderColor, false, NgNet.Level.Level12);
            _menuRender.Colors.MarginEndColor = _menuRender.Colors.DropDownBackEndColor;
            _menuRender.Colors.MarginStartColor = _menuRender.Colors.DropDownBackStartColor;
            _menuRender.Colors.DropDownBorderColor = BorderColor;
            _menuRender.Colors.FontColor = ForeColor;
            naviCms.Renderer = _menuRender;
        }
        #endregion

        #region path 
        private void pathTBox_TextChanged(object sender, EventArgs e)
        {
            InputPath = pathTBox.Text;
        }
        #endregion
        #endregion
    }
}
