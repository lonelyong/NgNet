using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NgNet.IO;
using NgNet.UI.Forms;

namespace NgNet.UI.Forms
{
    partial class FilterFileDialogF : TitleableForm, IFilterFileDialogWindow
    {
        #region private fields
        private string _enterPath = IOComm.DEFAULT_PATH;

        private string _filter = IOComm.DEFAULT_FILTER;

        private uint _filterIndex = 0;

        private MenuRender menuRender = new MenuRender();

        private ComboBoxHelper _hComboBox1;

        private ComboBoxHelper _hComboBox2;

        private ComboBoxHelper _hComBoBox3;
        #endregion

        #region protected fields
        protected string _CurrentName = string.Empty;//选中的文件名或文件夹名

        protected string _CurrentPath = string.Empty;

        protected string _DirLoading = string.Empty;//当前选中项的路径

        protected PathType _CurrentFf = PathType.Unknown;//当前选中项是文件还是文件夹

        protected DataGridViewRow _CurrentRow = new DataGridViewRow();//存储当前单击的行

        protected List<string> _CurrentExts = new List<string>();//显示的文件类型

        protected List<string> _FilterArray = new List<string>();//filter数组

        protected DirectoryInfo[] _CurrentDirs;//存储当前查看目录下的子目录

        protected FileInfo[] _CurrentFiles;//存储当前查看目录下的子文件 

        protected Thread _Thread;//获取列表线程

        protected TreeNode _CurrentNode = new TreeNode();
        #endregion

        #region private properties
        private TreeNode rootNode
        {
            get
            {
                return naviTree.Nodes[naviTree.Nodes.IndexOfKey( "ThisComputer" )];
            }
        }
        #endregion

        #region  public properties
        public string EnterPath
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
        /// 表示当前显示的路径
        /// </summary>
        public string InputPath { get; protected set; }
        /// <summary>
        /// 获取或设置文件过滤器
        /// </summary>
        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                //初始化Filter变量
                value = string.IsNullOrWhiteSpace( value ) ? IOComm.DEFAULT_FILTER : value;
                value = FilterHelper.Test( value, IOComm.DEFAULT_FILTER );
                _filter = value;
                _FilterArray = new List<string>( value.Split( new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries ) );
                //将filter添加到filter栏
                filterComboBox.Items.Clear();
                for (int i = 0; i < _FilterArray.Count; i += 2)
                {
                    filterComboBox.Items.Add( string.Format( "{0} {1} {2} {3}", _FilterArray[i + 1], "{", _FilterArray[i], "}" ) );
                }
                //重置filterIndex
                FilterIndex = 0;
            }
        }
        /// <summary>
        /// 初始的显示的指定拓展名在filter中的索引
        /// </summary>
        public uint FilterIndex
        {
            set
            {
                //判断指定的初始索引是否超出范围
                if (value > (_FilterArray.Count - 1) / 2)
                {
                    value = 0;
                }
                _filterIndex = value;
            }
            get
            {
                return _filterIndex;
            }
        }

        public override Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
                this.naviTree.BackColor = value;
                this.swordTBox.BackColor = value;
                this.filterComboBox.BackColor = value;
                this.nameComboBox.BackColor = value;
                this.pathComboBox.BackColor = value;

                dataGridView.BackgroundColor = value;
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = value;
                dataGridView.RowsDefaultCellStyle.BackColor = value;
                dataGridView.RowsDefaultCellStyle.SelectionBackColor = NgNet.Drawing.ColorHelper.GetSimilarColor( value, true, Level.Level3 );
                dataGridView.RowsDefaultCellStyle.SelectionForeColor = NgNet.Drawing.ColorHelper.GetOppositeColor( dataGridView.RowsDefaultCellStyle.SelectionBackColor );
                dataGridView.AlternatingRowsDefaultCellStyle.BackColor = NgNet.Drawing.ColorHelper.GetSimilarColor( value, true, Level.Level1 );
                dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = dataGridView.RowsDefaultCellStyle.SelectionBackColor;
                dataGridView.AlternatingRowsDefaultCellStyle.SelectionForeColor = dataGridView.RowsDefaultCellStyle.SelectionForeColor;
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
                dataGridView.RowsDefaultCellStyle.ForeColor = value;
                dataGridView.AlternatingRowsDefaultCellStyle.ForeColor = value;
                infLabel.ForeColor = value;
                nameLabel.ForeColor = value;
                swordTBox.ForeColor = value;
                backLabel.ForeColor = value;
                aheadLabel.ForeColor = value;
                upLabel.ForeColor = value;
                naviTree.ForeColor = value;
                pathComboBox.ForeColor = value;
                nameComboBox.ForeColor = value;
                filterComboBox.ForeColor = value;
                okButton.ForeColor = value;
                cancelBotton.ForeColor = value;
            }
        }

        public override Color BorderColor
        {
            set
            {
                base.BorderColor = value;
            }
            get
            {
                return base.BorderColor;
            }
        }
        #endregion

        #region constructor destructor 
        public FilterFileDialogF()
        {
            InitializeComponent();
            _hComboBox1 = new ComboBoxHelper( pathComboBox ) { DropdownAutoWidth = true, DelegateDrawItem = true };
            _hComboBox2 = new ComboBoxHelper( filterComboBox ) { DropdownAutoWidth = true, DelegateDrawItem = true };
            _hComBoBox3 = new ComboBoxHelper( nameComboBox ) { DropdownAutoWidth = true, DelegateDrawItem = true };
            DialogResult = DialogResult.None;
            Resizeable = true;
            TitleBar.Style = TitleBarStyles.MaxEnd;
            // 初始化
            Filter = Filter;   
            icoColumn.ValuesAreIcons = true;
            icoColumn.Icon = ConvertHelper.Bitmap2Icon( new Bitmap( 1, 1 ) );
            //窗口的最大大小
            //字段初始化
            MaximizedBounds = Screen.PrimaryScreen.WorkingArea;
            Text = SaveFileDialog.DEFAULT_TITLE;
            Comm.Initialize(this, FormHelper);
        }
        #endregion

        #region private methods
        #region get list
        //刷新数目信息
        protected void setCurrentDirectoryInfo( int foldersCount, int filesCount, int selectedFoldersCount, int selectedFilesCount )
        {
            Invoke( new Action( () => {
                infLabel.Text = string.Format( "文件夹:{0}/{1}  文件:{2}/{3}", selectedFoldersCount, foldersCount, selectedFilesCount, filesCount );
            } ) );
        }
        //获取主列表
        private void _loadDirectory()
        {
            Action _action;
            DirectoryInfo _di = null;
            DataGridViewRow _row;
            try
            {
                _di = new DirectoryInfo( _DirLoading );
                _CurrentDirs = _di.GetDirectories();
                _CurrentFiles = _di.GetFiles();
            }
            catch (Exception ex)
            {
                _action = delegate { MessageBox.Show( this, ex.Message, "读取错误", MessageBoxButtons.OK ); };
                Invoke( _action );
                return;
            }
            _CurrentPath = _DirLoading;//更新当前显示的路径

            #region 导航栏更新
            _action = delegate
            {
                dataGridView.Rows.Clear();
                pathComboBox.Items.Clear();
                pathComboBox.Items.Add( _CurrentPath );
                pathComboBox.Text = _CurrentPath;
            };
            Invoke( _action );
            #endregion

            #region 加载文件夹
            foreach (DirectoryInfo item in _CurrentDirs)
            {
                _action = delegate
                {
                    _row = dataGridView.Rows[dataGridView.Rows.Add()];
                    _row.Cells[nameColumn.Index].Value = item.Name;
                    _row.Cells[typeColumn.Index].Value = "文件夹";
                    _row.Cells[typeColumn.Index].Tag = PathType.Floder;
                    _row.Cells[lengthColumn.Index].Value = string.Empty;
                    _row.Cells[creattimeColumn.Index].Value = item.CreationTime;
                };
                Invoke( _action );
            }
            #endregion

            #region 加载文件列表
            IEnumerable<FileInfo> _query;
            if (_CurrentExts.Contains( ".*" ))
                _query = _CurrentFiles;
            else
                _query = from FileInfo fi in _CurrentFiles where _CurrentExts.FindIndex( t => string.Compare( fi.Extension, t, true ) == 0 ) != -1 select fi;
            foreach (FileInfo item in _query)
            {
                _action = delegate
                {
                    _row = dataGridView.Rows[dataGridView.Rows.Add()];
                    _row.Cells[nameColumn.Index].Value = item.Name;
                    _row.Cells[typeColumn.Index].Value = item.Extension;
                    _row.Cells[typeColumn.Index].Tag = PathType.File;
                    _row.Cells[lengthColumn.Index].Value = FileHelper.LengthFormat( item.Length );
                    _row.Cells[creattimeColumn.Index].Value = item.CreationTime;
                };
                Invoke( _action );
            }
            //当前查看所有文件类型
            #endregion

            #region 分组委托加载文件图标
            PathType ff = PathType.Unknown;
            foreach (DataGridViewRow item in dataGridView.Rows)
            {
                _action = delegate
                {
                    ff = (PathType)item.Cells[typeColumn.Index].Tag;
                    if (ff == PathType.File)
                        item.Cells[icoColumn.Index].Value = IOComm.GetIcon( item.Cells[typeColumn.Index].Value?.ToString() );
                    else if (ff == PathType.Floder)
                        item.Cells[icoColumn.Index].Value = IOComm.FloderIcon;
                    else
                        item.Cells[icoColumn.Index].Value = IOComm.DefaultIcon;
                };
                Invoke( _action );
            }
            #endregion
            //刷新列表后清空选择的数据
            _CurrentName = null;
            dataGridView.ClearSelection();
            _CurrentFf = PathType.Floder;
        }

        protected void loadDirectory( string tmp )
        {
            if ((_Thread == null) == false)
                while (_Thread.ThreadState != ThreadState.Stopped)
                {
                    _Thread.Abort();
                    Thread.Sleep( 10 );
                }
            _DirLoading = tmp;
            _Thread = new Thread( new ThreadStart( _loadDirectory ) );
            _Thread.Priority = ThreadPriority.Lowest;
            _Thread.IsBackground = true;
            _Thread.Start();
        }
        #endregion

        #region this
        protected virtual void this_Load( object sender, EventArgs e )
        {
            setToolStrip();
            //如果么有父窗体（或未显示）则显示在屏幕中央
            Comm.ApplyComm( this );
            // 初始化 navi
            IOComm.ResetNavi( naviTree, naviCms );
        }

        private void this_Shown( object sender, EventArgs e )
        {
            _CurrentPath = EnterPath;
            //显示filterindex并刷新列表
            setupFilterIndex( FilterIndex );
        }

        private void this_SizeChanged( object sender, EventArgs e )
        {
            backLabel.Top = Comm.DISTANCE_TOP;
            aheadLabel.Top = Comm.DISTANCE_TOP;
            upLabel.Top = Comm.DISTANCE_TOP;

            backLabel.Left = Comm.DISTANCE_LEFT;
            aheadLabel.Left = backLabel.Right;
            upLabel.Left = aheadLabel.Right;

            pathComboBox.Top = Comm.DISTANCE_TOP;
            swordTBox.Top = Comm.DISTANCE_TOP;

            pathComboBox.Left = upLabel.Right;
            swordTBox.Left = ContentPanel.Width - swordTBox.Width - Comm.DISTANCE_LEFT;

            pathComboBox.Width = swordTBox.Left - pathComboBox.Left - Comm.DISTANCE_INTERVAL_H;

            cancelBotton.Top = ContentPanel.Height - cancelBotton.Height - Comm.DISTANCE_DOWN;
            okButton.Top = cancelBotton.Top;
            cancelBotton.Left = ContentPanel.Width - cancelBotton.Width - Comm.DISTANCE_LEFT;
            okButton.Left = cancelBotton.Left - okButton.Width + 1;

            infLabel.Left = Comm.DISTANCE_LEFT;
            infLabel.Top = okButton.Bottom - infLabel.Height - (cancelBotton.Height - infLabel.Height) / 2;

            nameLabel.Left = Comm.DISTANCE_LEFT;
            splitContainer2.Left = nameLabel.Right + Comm.DISTANCE_INTERVAL_H;
            splitContainer2.Top = okButton.Top - splitContainer2.Height - Comm.DISTANCE_INTERVAL_V;
            nameLabel.Top = splitContainer2.Top + (splitContainer2.Height - nameLabel.Height) / 2;

            splitContainer1.Location = new Point( Comm.DISTANCE_LEFT, pathComboBox.Bottom + Comm.DISTANCE_INTERVAL_V );
            splitContainer1.Height = splitContainer2.Top - splitContainer1.Top - Comm.DISTANCE_INTERVAL_V;

            splitContainer1.Width = ContentPanel.Width - Comm.DISTANCE_LEFT * 2;
            splitContainer2.Width = splitContainer1.Right - splitContainer2.Left;
        }

        private void this_FormClosing( object sender, FormClosingEventArgs e )
        {
            if (_Thread != null)
                while (_Thread.ThreadState != ThreadState.Stopped)
                {
                    _Thread.Abort();
                    Thread.Sleep( 10 );
                }
        }
        #endregion

        #region  dataGridView
        protected virtual void dataGridView_CellMouseDown( object sender, DataGridViewCellMouseEventArgs e )
        {
            if (e.RowIndex < 0)
                return;
            //记录当前行
            _CurrentRow = dataGridView.Rows[e.RowIndex];
            //根据鼠标操作判断是否清除列表选择项
            if (dataGridView.MultiSelect)
                //不管左右键都清除选择
                if (!_CurrentRow.Selected)
                    dataGridView.ClearSelection();
                else
                    if (e.Button == MouseButtons.Left)
                    dataGridView.ClearSelection();
            //记录当前行所对应的文件名
            _CurrentName = _CurrentRow.Cells[nameColumn.Index].Value.ToString();
            //记录当前是文件还是文件夹
            _CurrentFf = (PathType)_CurrentRow.Cells[typeColumn.Index].Tag;
            _CurrentRow.ContextMenuStrip = itemCms;
            //当前行选中
            _CurrentRow.Selected = true;//会触发SelectionChanged
        }

        private void dataGridView_CellMouseDoubleClick( object sender, DataGridViewCellMouseEventArgs e )
        {
            if (e.RowIndex < 0 || e.Button != MouseButtons.Left)
                return;
            _Tsmi_item_Open_Click( sender, e );
        }

        private void dataGridView_DataError( object sender, DataGridViewDataErrorEventArgs e )
        {

        }

        protected virtual void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
           
        }
        #endregion

        #region treeview
        private void naviTree_NodeMouseDoubleClick( object sender, TreeNodeMouseClickEventArgs e )
        {
            if (e.Button == MouseButtons.Right)
                return;
            if (e.Node.Level == 0)
                return;
            if (e.Node.Nodes.Count == 0)
                IOComm.FillNode( e.Node, naviCms );
        }

        private void naviTree_NodeMouseClick( object sender, TreeNodeMouseClickEventArgs e )
        {
            naviTree.SelectedNode = e.Node;
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node.Level > 0 && string.Compare( _CurrentPath, e.Node.Name, true ) != 0)
                {
                    loadDirectory( e.Node.Name );
                }
            }
        }

        private void naviTree_AfterSelect( object sender, TreeViewEventArgs e )
        {
            _CurrentNode.BackColor = BackColor;
            _CurrentNode.ForeColor = ForeColor;
            e.Node.BackColor = Drawing.ColorHelper.GetSimilarColor( BackColor, true, Level.Level6 );
            e.Node.ForeColor = Drawing.ColorHelper.GetOppositeColor( e.Node.BackColor );
            _CurrentNode = e.Node;//词句要在获取导航列表之前，获取列表时要用到
        }
        #endregion

        #region navigation labels  & search
        private void naviLabel_Click( object sender, EventArgs e )
        {
            if (PathHelper.IsRoot( _CurrentPath )) //判断是不是顶级目录
            {
                return;
            }
            this.loadDirectory( Path.GetDirectoryName( _CurrentPath ) );
        }

        private void naviLabel_MouseEnter( object sender, EventArgs e )
        {
            ((Control)sender).ForeColor = BorderColor;
        }

        private void naviLabel_MouseLeave( object sender, EventArgs e )
        {
            ((Control)sender).ForeColor = Color.Black;
        }
        #endregion

        #region buttons
        protected virtual void OkButton_Click( object sender, EventArgs e )
        {
    
        }

        protected virtual void CancelButton_Click( object sender, EventArgs e )
        {

        }
        #endregion

        #region menu
        private void setToolStrip()
        {
            double opacity = 0.85d;
            ContextMenuStrip[] cmss = new ContextMenuStrip[] { this.itemCms, this.dgvCms, this.naviCms };
            foreach (ContextMenuStrip cms in cmss)
            {
                ContextMenuStripHelper.SetOpacity( cms, opacity );
            }
        }

        private void setMenuRender()
        {
            menuRender.Colors.DropDownItemBorderColor = BorderColor;
            menuRender.Colors.DropDownItemEndColor = BackColor;
            menuRender.Colors.DropDownItemStartColor = NgNet.Drawing.ColorHelper.GetSimilarColor( BorderColor, false, NgNet.Level.Level8 ); ;
            menuRender.Colors.DropDownBackEndColor = BackColor;
            menuRender.Colors.DropDownBackStartColor = NgNet.Drawing.ColorHelper.GetSimilarColor( BorderColor, false, NgNet.Level.Level12 );
            menuRender.Colors.MarginEndColor = menuRender.Colors.DropDownBackEndColor;
            menuRender.Colors.MarginStartColor = menuRender.Colors.DropDownBackStartColor;
            menuRender.Colors.DropDownBorderColor = BorderColor;
            menuRender.Colors.FontColor = ForeColor;
            itemCms.Renderer = menuRender;
            dgvCms.Renderer = menuRender;
            naviCms.Renderer = menuRender;
        }

        #region naviCms
        private void tsmi_navi_open_Click( object sender, EventArgs e )
        {
            if (naviTree.SelectedNode.IsExpanded)
            {
                _CurrentNode.Collapse();
            }
            else
            {
                IOComm.FillNode( _CurrentNode, naviCms );
            }

        }

        private void tsmi_navi_attribute_Click( object sender, EventArgs e )
        {
            Windows.Explore.ShowPropertiesDialog( _CurrentNode.Name );
        }

        private void cms_navi_Opening( object sender, CancelEventArgs e )
        {
            tsmi_navi_open.Text = _CurrentNode.IsExpanded ? "折叠" : "展开";

        }

        private void tsmi_navi_foldAll_Click( object sender, EventArgs e )
        {
            naviTree.SelectedNode = TreeViewHelper.GetParentByLevel( naviTree.SelectedNode, 1 );
            _CurrentNode.Collapse();
        }
        #endregion

        #region dgvCms
        private void dgvCms_Opening( object sender, CancelEventArgs e )
        {
            tsmi_dgv_paste.Enabled = Clipboard.ContainsFileDropList();
            tsmi_dgv_new_dir.Tag = PathType.Floder;
            tsmi_dgv_new_txt.Tag = PathType.File;
        }

        private void tsmi_dgv_parent_Click( object sender, EventArgs e )
        {
            naviLabel_Click( sender, e );
        }

        private void tsmi_dgv_refresh_Click( object sender, EventArgs e )
        {
            loadDirectory( _CurrentPath );
        }

        private void tsmi_dgv_new_items_Click( object sender, EventArgs e )
        {
            string _path;
            ToolStripMenuItem tsmi = (ToolStripMenuItem)sender;
            if(IOComm.PathCreate( this, (PathType)tsmi.Tag, _CurrentPath, out _path ))
            {
                FileInfo _fi = new FileInfo(_path);
                if (_fi.Exists)
                {
                    DataGridViewRow _row = dataGridView.Rows[dataGridView.Rows.Add()];
                    _row.Cells[nameColumn.Index].Value = _fi.Name;
                    _row.Cells[typeColumn.Index].Value = _fi.Extension;
                    _row.Cells[typeColumn.Index].Tag = PathType.File;
                    _row.Cells[lengthColumn.Index].Value = string.Empty;
                    _row.Cells[creattimeColumn.Index].Value = FileHelper.LengthFormat( _fi.Length );
                    _row.Cells[icoColumn.Index].Value = IOComm.GetIcon(_fi.Extension);
                }
                else
                {
                    DirectoryInfo _di = new DirectoryInfo(_path);
                    DataGridViewRow _row = dataGridView.Rows[dataGridView.Rows.Add()];
                    _row.Cells[nameColumn.Index].Value = _di.Name;
                    _row.Cells[typeColumn.Index].Value = "文件夹";
                    _row.Cells[typeColumn.Index].Tag = PathType.Floder;
                    _row.Cells[lengthColumn.Index].Value = string.Empty;
                    _row.Cells[creattimeColumn.Index].Value = _di.CreationTime;
                    _row.Cells[icoColumn.Index].Value = IOComm.FloderIcon;
                }
            }
        }
        #endregion

        #region itemCms
        protected virtual void _Tsmi_item_Open_Click( object sender, EventArgs e )
        {
           
        }

        protected virtual void _Tsmi_item_Select_Click( object sender, EventArgs e )
        {

        }

        private void tsmi_item_attribute_Click( object sender, EventArgs e )
        {
            Windows.Explore.ShowPropertiesDialog( IOComm.PathCombin( _CurrentPath, _CurrentName ) );
        }

        private void tsmi_item_rename_Click( object sender, EventArgs e )
        {
            string newName = IOComm.ShowRenameDialog( this, IOComm.PathCombin( _CurrentPath, _CurrentName ) );
            if (string.IsNullOrWhiteSpace( newName )) return;
            _CurrentName = Path.GetFileName( newName );
            _CurrentRow.Cells[nameColumn.Index].Value = _CurrentName;
        }

        private void tsmi_item_delete_Click( object sender, EventArgs e )
        {
            if (Forms.MessageBox.Show( this, string.Format( "即将永久删除 <{0}> 个文件（夹),是否继续？",  dataGridView.SelectedRows.Count ), "删除提示", MessageBoxButtons.YesNo ) == DialogResult.No)
            {
                return;
            }
            foreach (DataGridViewRow item in dataGridView.SelectedRows)
            {
                string delpath = IOComm.PathCombin( _CurrentPath, item.Cells[nameColumn.Index].Value.ToString() );
                try
                {
                    PathHelper.Delete( delpath );
                    dataGridView.Rows.Remove( item );
                }
                catch (Exception ex)
                {
                    MessageBox.Show( this, string.Format( "未删除 <{0}>,失败原因\r\n{1}", delpath, ex.Message ), "删除失败", MessageBoxButtons.OK );
                }
            }

        }

        protected virtual void cms_item_Opening( object sender, CancelEventArgs e )
        {
            tsmi_item_paste.Enabled = Clipboard.ContainsFileDropList();
        }
        #endregion

        #endregion

        #region combobox
        private void setupFilterIndex( uint filterIndex )
        {
            FilterIndex = filterIndex;
            filterComboBox.SelectedIndex = (int)filterIndex;
            _CurrentExts = IOComm.GetExts( _FilterArray[(int)filterIndex * 2] );
            loadDirectory( _CurrentPath );//刷新列表

        }

        private void filterComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            //如果未改变Filterindex则不刷新
            if ((int)FilterIndex == filterComboBox.SelectedIndex || filterComboBox.SelectedIndex == -1)
                return;
            setupFilterIndex( (uint)filterComboBox.SelectedIndex );
        }

        private void nameComboBox_KeyPress( object sender, KeyPressEventArgs e )
        {
            IOComm.NameInputTest_Keypress( sender, e );
        }
        #endregion
        #endregion

        #region public methods
        public override void SetTheme( IThemeBase t )
        {
            base.SetTheme( t );
            _hComboBox1.Theme = t;
            _hComboBox2.Theme = t;
            _hComBoBox3.Theme = t;
            setMenuRender();
        }
        #endregion
    }
}
