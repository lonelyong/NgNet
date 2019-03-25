using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI.Forms
{
    public class TextBoxMenu : ContextMenuStrip
    {
        #region private fields
        private ToolStripMenuItem _tsmi_ctrl_z = new ToolStripMenuItem()
        {
            Text = "撤销"
        };
        private ToolStripMenuItem _tsmi_ctrl_x = new ToolStripMenuItem()
        {
            Text = "剪切"
        };
        private ToolStripMenuItem _tsmi_ctrl_c = new ToolStripMenuItem()
        {
            Text = "复制"
        };
        private ToolStripMenuItem _tsmi_ctrl_v = new ToolStripMenuItem()
        {
            Text = "粘贴"
        };
        private ToolStripMenuItem _tsmi_ctrl_d = new ToolStripMenuItem()
        {
            Text = "删除"
        };
        private ToolStripMenuItem _tsmi_ctrl_a = new ToolStripMenuItem()
        {
            Text = "全选"
        };
        #endregion

        #region public properties

        #endregion

        #region constructor
        public TextBoxMenu()
        {
            initControls();
        }
        public TextBoxMenu(System.Windows.Forms.TextBox textBoxTOBind) : base()
        {
            BindTextBox(textBoxTOBind);
        }
        #endregion

        #region private methods
        private void initControls()
        {
            this.Items.AddRange(new ToolStripItem[] {
                _tsmi_ctrl_z,
                _tsmi_ctrl_x,
                _tsmi_ctrl_c,
                _tsmi_ctrl_v,
                _tsmi_ctrl_d,
                _tsmi_ctrl_a
            });
            _tsmi_ctrl_a.Click += tsmi_ctrl_a_Click;
            _tsmi_ctrl_c.Click += tsmi_ctrl_c_Click;
            _tsmi_ctrl_d.Click += tsmi_ctrl_d_Click;
            _tsmi_ctrl_v.Click += tsmi_ctrl_v_Click;
            _tsmi_ctrl_x.Click += tsmi_ctrl_x_Click;
            _tsmi_ctrl_z.Click += tsmi_ctrl_z_Click;
        }

        private void tsmi_ctrl_z_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).Undo();
        }
        private void tsmi_ctrl_x_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).Cut();
        }
        private void tsmi_ctrl_c_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).Copy();
        }
        private void tsmi_ctrl_v_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).Paste();
        }
        private void tsmi_ctrl_d_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).Cut();
        }
        private void tsmi_ctrl_a_Click(object sender, EventArgs e)
        {
            ((System.Windows.Forms.TextBox)SourceControl).SelectAll();
        }

        protected override void OnOpening(CancelEventArgs e)
        {
            if(!(SourceControl is System.Windows.Forms.TextBox))
            {
                e.Cancel = true;
                base.OnOpening(e);
                return;
            }
            System.Windows.Forms.TextBox _txtBox = SourceControl as System.Windows.Forms.TextBox;
            _tsmi_ctrl_a.Enabled = _txtBox.TextLength > 0 && _txtBox.TextLength != _txtBox.SelectionLength;
            _tsmi_ctrl_c.Enabled = _txtBox.SelectionLength > 0;
            _tsmi_ctrl_v.Enabled = !string.IsNullOrEmpty(Clipboard.GetText()) && !_txtBox.ReadOnly;
            _tsmi_ctrl_x.Enabled = _txtBox.SelectionLength > 0 && !_txtBox.ReadOnly;
            _tsmi_ctrl_d.Enabled = _txtBox.SelectionLength > 0 && !_txtBox.ReadOnly;
            _tsmi_ctrl_z.Enabled = _txtBox.CanUndo;
            base.OnOpening(e);
        }
        #endregion

        #region public methods
        public void BindTextBox(System.Windows.Forms.TextBox txtBox)
        {
            txtBox.ContextMenuStrip = this;
        }

        public void UnbindTextBox(System.Windows.Forms.TextBox txtBox)
        {
            if (txtBox.ContextMenuStrip == this)
                txtBox.ContextMenuStrip = null;
        }

        public void AddLine()
        {
            this.Items.Add("_");
        }

        public void AddMenu(ToolStripMenuItem menu)
        {
            this.Items.Add(menu);
        }
        #endregion
    }
}
