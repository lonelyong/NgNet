using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NgNet.UI.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Text;
using NgNet.IO;

namespace NgNet.UI.Forms
{
    partial class OpenFileDialogF : FilterFileDialogF
    {
        #region private fields
        private StringBuilder _selectedFileNames = new StringBuilder();//存储当前选中的文件
        #endregion

        #region  public properties
        /// <summary>
        /// 当前选中的路径集合
        /// </summary>
        public List<string> InputPaths { get; } = new List<string>();

        /// <summary>
        /// 是否允许选中多个项目
        /// </summary>
        public bool MultiSelect
        {
            set
            {
                dataGridView.MultiSelect = value;
            }
            get
            {
                return dataGridView.MultiSelect;
            }
        }
        #endregion

        #region constructor destructor  
        public OpenFileDialogF()
        {
            InitializeComponent();
            Text = OpenFileDialog.DEFAULT_TITLE;
        }
        #endregion

        #region private methods
        #region buttons
        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            this.InputPath = string.Empty;
            this.InputPaths.Clear();
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OkButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameComboBox.Text))
            {
                MessageBox.Show(this, "您还未选择任何文件，请选择要打开的文件");
                return;
            }
            string _path = string.Empty;
            List<string> _ps = new List<string>();
            string[] _names = nameComboBox.Text.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string item in _names)
            {
                if (string.IsNullOrWhiteSpace(item))
                    continue;
                _path = IOComm.PathCombin(_CurrentPath, item);
                if (File.Exists(_path))
                    _ps.Add(_path);
                else
                {
                    string inf = string.Format("系统找不到文件 < {0} >\r\npath = {1}", item, _path);
                    MessageBox.Show(inf);
                    return;
                }
            }
            if (_ps.Count == 0)
            {
                MessageBox.Show(this, "您选择的文件的个数为0，至少选择一个文件,请重新选择，退出请点击取消");
                return;
            }
            else
            {
                this.InputPath = string.Empty;
                foreach (string item in _ps)
                {
                    this.InputPath += item;
                    this.InputPath += "|";
                }
                this.InputPath = this.InputPath.Remove(this.InputPath.Length - 1, 1);
                this.InputPaths.Clear();
                this.InputPaths.AddRange(_ps);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
        #endregion

        #region  dataGridView    
        protected override void dataGridView_CellMouseDown( object sender, DataGridViewCellMouseEventArgs e )
        {
            base.dataGridView_CellMouseDown(sender, e);     
            //取消选中菜单是否显示
            tsmi_item_Select.Visible = _CurrentRow.Selected;
        }

        protected override void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (_Thread.IsAlive)
                return;
            int _filesCount = 0;
            _selectedFileNames.Clear();
            _selectedFileNames.Append("|");
            foreach (DataGridViewRow item in dataGridView.SelectedRows)
            {
                if ((PathType)item.Cells[typeColumn.Index].Tag == IO.PathType.File)
                {
                    _selectedFileNames.Append(item.Cells[nameColumn.Index].Value.ToString());
                    _selectedFileNames.Append("|");
                    _filesCount++;
                }
            }
            nameComboBox.Text = _selectedFileNames.ToString();
            DirectoryInfo _di = new DirectoryInfo(_CurrentPath);
            setCurrentDirectoryInfo(
                _di.GetDirectories().Length
              , dataGridView.Rows.Count - _di.GetDirectories().Length
              , dataGridView.SelectedRows.Count - _filesCount
              , _filesCount);          
        }
        #endregion

        #region menu
        #region itemCms
        protected override void _Tsmi_item_Open_Click( object sender, EventArgs e )
        {
            if (_CurrentFf == PathType.File)
                OkButton_Click( sender, e );
            else if (_CurrentFf == PathType.Floder)
                loadDirectory( IOComm.PathCombin( _CurrentPath, _CurrentName ) );
        }

        protected override void _Tsmi_item_Select_Click( object sender, EventArgs e )
        {
            _CurrentRow.Selected = false;
        }

        protected override void cms_item_Opening(object sender, CancelEventArgs e)
        {
            int tmp = 0;
            foreach (DataGridViewRow  item in dataGridView.SelectedRows)
            {
                if ((PathType)item.Cells[typeColumn.Index].Tag == PathType.File)
                    tmp++;
            }
            tsmi_item_Select.Text = string.Format("取消选中 <{0}>", _CurrentName);
            if (_CurrentFf == PathType.File)
                tsmi_item_open.Text = string.Format("选定选中 <{0}>", tmp <= 1 ? _CurrentName : tmp.ToString());
            else if (_CurrentFf == PathType.Floder)
                tsmi_item_open.Text = string.Format("打开<{0}>", _CurrentName);
            else
                tsmi_item_open.Text = "error";
            base.cms_item_Opening(sender, e);
        }
        #endregion
        #endregion
        #endregion
    }
}
