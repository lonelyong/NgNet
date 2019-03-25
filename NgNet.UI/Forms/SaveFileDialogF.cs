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
    partial class SaveFileDialogF : FilterFileDialogF
    {
        #region  public properties
        /// <summary>
        /// 获取或设置要保存的文件名
        /// </summary>
        public string SaveName { get; set; }
        #endregion

        #region constructor destructor 
        public SaveFileDialogF() 
        {
            InitializeComponent();
            Text = SaveFileDialog.DEFAULT_TITLE;
        }
        #endregion

        #region private methods
        #region this
        protected override void this_Load(object sender, EventArgs e)
        {
            nameComboBox.Text = SaveName;
            base.this_Load(sender, e);
        }
        #endregion

        #region Buttons
        protected override void CancelButton_Click(object sender, EventArgs e)
        {
            InputPath = null;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        protected override void OkButton_Click(object sender, EventArgs e)
        {
            string sp = null;
            #region 未输入保存文件名
            if (string.IsNullOrWhiteSpace (SaveName)) 
            {
                MessageBox.Show(this, "请输入保存文件名");
                nameComboBox.Focus();
                return;
            }
            #endregion

            #region 输入的文件名存在非法字符
            else if (PathHelper.IsNameInvalidCharContained(SaveName))
            {
                MessageBox.Show(this, "不合法的文件名, 请勿输入以下非法字符 < > / \\ | ? * \" : 等");
                nameComboBox.Focus();
                return;
            }
            #endregion
            else
            {
                #region 获取文件完整路径
                // 获取文件拓展名
                string sType = _CurrentExts[0] == ".*" ? string.Empty : _CurrentExts[0];
                // 判断当前有没有选中一个文件夹   
                // 未选中文件或文件夹
                if (string.IsNullOrWhiteSpace(_CurrentName))
                {
                    sp = IOComm.PathCombin(_CurrentPath, SaveName + sType);
                }
                else
                {
                    sp = IOComm.PathCombin( _CurrentPath, Path.GetFileNameWithoutExtension(_CurrentName) + sType);
                }
                #endregion 
            } 
            #region 判断是否存在同名文件
            if (PathHelper.IsExisted(sp))
            {
                string inf = string.Format("存在同名文件或文件夹，是否继续？\r\npath = {0}",sp);
                DialogResult dr = MessageBox.Show(this, inf, "文件名冲突", MessageBoxButtons.YesNo);
                if (dr != DialogResult.Yes)
                {
                    nameComboBox.Focus();
                    return;
                }
            }
            #endregion
            this.InputPath = sp;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        #endregion

        #region  DataGridView
        protected override void dataGridView_SelectionChanged( object sender, EventArgs e )
        {
            if (_Thread.IsAlive)
                return;
            int filesCount = 0;
            foreach (DataGridViewRow item in dataGridView.SelectedRows)
                if ((PathType)item.Cells[typeColumn.Index].Tag == PathType.File)
                    filesCount++;
            DirectoryInfo di = new DirectoryInfo( _CurrentPath );
            setCurrentDirectoryInfo(
                di.GetDirectories().Length
              , dataGridView.Rows.Count - di.GetDirectories().Length
              , dataGridView.SelectedRows.Count - filesCount
              , filesCount );
            if(_CurrentFf == PathType.File)
                nameComboBox.Text = Path.GetFileNameWithoutExtension(_CurrentName);
        }
        #endregion

        #region Menu
        #region itemCms
        protected override void _Tsmi_item_Open_Click( object sender, EventArgs e )
        {
            string tmp = IOComm.PathCombin( _CurrentPath, _CurrentName );
            if (File.Exists( tmp ))
            {
                System.Diagnostics.Process.Start( tmp );
            }
            else if (Directory.Exists( tmp ))
            {
                loadDirectory( tmp );
            }
            else
            {
                MessageBox.Show( this, "系统找不到以下路径：\r\nPath = " + tmp, null, MessageBoxButtons.OK );
            }
        }

        protected override void _Tsmi_item_Select_Click(object sender, EventArgs e)
        {
            if (_CurrentFf == PathType.File)
                OkButton_Click( sender, e );
            else
                _Tsmi_item_Open_Click(sender, e);

        }

        protected override void cms_item_Opening(object sender, CancelEventArgs e)
        {
            if (_CurrentFf == PathType.File)
            {
                tsmi_item_open.Text = string.Format("查看 <{0}>", _CurrentName);
                tsmi_item_Select.Text = string.Format("选定所在目录 <{0}>", InputPath);
            }
            else if (_CurrentFf == PathType.Floder)
            {
                tsmi_item_open.Text = string.Format("打开目录 <{0}>", _CurrentName);
                tsmi_item_Select.Text = string.Format("选定目录 <{0}>", _CurrentName);
            }
            else
            {
                tsmi_item_open.Text = "error";
                tsmi_item_Select.Text = "error";
            }
            base.cms_item_Opening(sender, e);
        }
        #endregion
        #endregion

        #region Combobox
        private void nameComboBox_TextChanged(object sender, EventArgs e)
        {
            SaveName = nameComboBox.Text.Trim();
        }
        #endregion
        #endregion
    }
}
