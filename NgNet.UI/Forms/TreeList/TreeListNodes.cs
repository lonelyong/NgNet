using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public class TreeListNodes : NodeCollection
    {
        TreeList m_tree;
        bool m_isUpdating = false;
        public void BeginUpdate()
        {
            m_isUpdating = true;
        }
        public void EndUpdate()
        {
            m_isUpdating = false;
        }
        public TreeListNodes(TreeList owner) : base(null)
        {
            m_tree = owner;
        }
        protected override void UpdateNodeCount(int oldvalue, int newvalue)
        {
            base.UpdateNodeCount(oldvalue, newvalue);
            if (!m_isUpdating)
                m_tree.RecalcLayout();
        }
        public override void Clear()
        {
            base.Clear();
            m_tree.RecalcLayout();
        }
        public override void NodetifyBeforeExpand(TreeListNode nodeToExpand, bool expanding)
        {
            if (!m_tree.DesignMode)
                m_tree.OnNotifyBeforeExpand(nodeToExpand, expanding);
        }
        public override void NodetifyAfterExpand(TreeListNode nodeToExpand, bool expanded)
        {
            m_tree.OnNotifyAfterExpand(nodeToExpand, expanded);
        }
        protected override int GetFieldIndex(string fieldname)
        {
            TreeListColumn col = m_tree.Columns[fieldname];
            if (col != null)
                return col.Index;
            return -1;
        }
    }
}
