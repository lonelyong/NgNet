using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public class NodesSelection : IEnumerable
    {
        List<TreeListNode> m_nodes = new List<TreeListNode>();
        Dictionary<TreeListNode, int> m_nodesMap = new Dictionary<TreeListNode, int>();
        public void Clear()
        {
            m_nodes.Clear();
            m_nodesMap.Clear();
        }
        public IEnumerator GetEnumerator()
        {
            return m_nodes.GetEnumerator();
        }
        public TreeListNode this[int index]
        {
            get { return m_nodes[index]; }
        }
        public int Count
        {
            get { return m_nodes.Count; }
        }
        public void Add(TreeListNode node)
        {
            m_nodes.Add(node);
            m_nodesMap.Add(node, 0);
        }
        public void Remove(TreeListNode node)
        {
            m_nodes.Remove(node);
            m_nodesMap.Remove(node);
        }
        public bool Contains(TreeListNode node)
        {
            return m_nodesMap.ContainsKey(node);
        }

        public IList<TreeListNode> GetSortedNodes()
        {
            SortedList<string, TreeListNode> list = new SortedList<string, TreeListNode>();
            foreach (TreeListNode node in m_nodes)
                list.Add(node.GetId(), node);
            return list.Values;
        }

    }
}
