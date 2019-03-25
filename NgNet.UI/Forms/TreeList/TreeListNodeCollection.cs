using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    public class NodeCollection : IEnumerable
    {
        internal int m_version = 0;
        int m_nextId = 0;
        int m_IdDirty = 0;

        TreeListNode[] m_nodesInternal = null;
        TreeListNode m_owner = null;
        TreeListNode m_firstNode = null;
        TreeListNode m_lastNode = null;
        int m_count = 0;
        public TreeListNode Owner
        {
            get { return m_owner; }
        }
        public TreeListNode FirstNode
        {
            get { return m_firstNode; }
        }
        public TreeListNode LastNode
        {
            get { return m_lastNode; }
        }
        public bool IsEmpty()
        {
            return m_firstNode == null;
        }
        public int Count
        {
            get { return m_count; }
        }
        public NodeCollection(TreeListNode owner)
        {
            m_owner = owner;
        }
        public virtual void Clear()
        {
            m_version++;
            while (m_firstNode != null)
            {
                TreeListNode node = m_firstNode;
                m_firstNode = node.NextSibling;
                node.Remove();
            }
            m_firstNode = null;
            m_lastNode = null;
            m_count = 0;
            m_totalNodeCount = 0;
            m_IdDirty = 0;
            m_nextId = 0;
            ClearInternalArray();
            TreeListNode.SetHasChildren(m_owner, m_count != 0);
        }
        public TreeListNode Add(string text)
        {
            return Add(new TreeListNode(text));
        }
        public TreeListNode Add(TreeListNode newnode)
        {
            m_version++;
            ClearInternalArray();
            Debug.Assert(newnode != null && newnode.Owner == null, "Add(Node newnode)");
            newnode.InsertAfter(m_lastNode, this);
            m_lastNode = newnode;
            if (m_firstNode == null)
                m_firstNode = newnode;
            newnode.Id = m_nextId++;
            m_count++;
            return newnode;
        }
        public void Remove(TreeListNode node)
        {
            if (m_lastNode == null)
                return;
            m_version++;
            ClearInternalArray();
            Debug.Assert(node != null && object.ReferenceEquals(node.Owner, this), "Remove(Node node)");

            TreeListNode prev = node.PrevSibling;
            TreeListNode next = node.NextSibling;
            node.Remove();

            if (prev == null) // first node
                m_firstNode = next;
            if (next == null) // last node
                m_lastNode = prev;
            m_IdDirty++;
            m_count--;
            TreeListNode.SetHasChildren(m_owner, m_count != 0);
        }
        public void InsertAfter(TreeListNode node, TreeListNode insertAfter)
        {
            m_version++;
            ClearInternalArray();
            Debug.Assert(node.Owner == null, "node.Owner == null");
            if (insertAfter == null)
            {
                node.InsertBefore(m_firstNode, this);
                m_firstNode = node;
            }
            else
            {
                node.InsertAfter(insertAfter, this);
            }
            if (m_lastNode == insertAfter)
            {
                m_lastNode = node;
                node.Id = m_nextId++;
            }
            else
                m_IdDirty++;
            m_count++;
        }
        public TreeListNode this[int index]
        {
            get
            {
                Debug.Assert(index >= 0 && index < Count, "Index out of range");
                if (index >= Count)
                    throw new IndexOutOfRangeException(string.Format("Node this [{0}], Collection Count {1}", index, Count));
                EnsureInternalArray();
                return m_nodesInternal[index];
            }
        }

        public TreeListNode NodeAtIndex(int index)
        {
            TreeListNode node = FirstNode;
            while (index-- > 0 && node != null)
                node = node.NextSibling;
            return node;
        }
        public int GetNodeIndex(TreeListNode node)
        {
            int index = 0;
            TreeListNode tmp = FirstNode;
            while (tmp != null && tmp != node)
            {
                tmp = tmp.NextSibling;
                index++;
            }
            if (tmp == null)
                return -1;
            return index;
        }
        public virtual int FieldIndex(string fieldname)
        {
            NodeCollection rootCollection = this;
            while (rootCollection.Owner != null && rootCollection.Owner.Owner != null)
                rootCollection = rootCollection.Owner.Owner;
            return rootCollection.GetFieldIndex(fieldname);
        }
        public TreeListNode FirstVisibleNode()
        {
            return FirstNode;
        }
        public TreeListNode LastVisibleNode(bool recursive)
        {
            if (recursive)
                return FindNodesBottomLeaf(LastNode, true);
            return LastNode;
        }
        public virtual void NodetifyBeforeExpand(TreeListNode nodeToExpand, bool expanding)
        {
        }
        public virtual void NodetifyAfterExpand(TreeListNode nodeToExpand, bool expanding)
        {
        }
        internal void UpdateChildIds(bool recursive)
        {
            if (recursive == false && m_IdDirty == 0)
                return;
            m_IdDirty = 0;
            m_nextId = 0;
            foreach (TreeListNode node in this)
            {
                node.Id = m_nextId++;
                if (node.HasChildren && recursive)
                    node.Nodes.UpdateChildIds(true);
            }
        }
        protected virtual int GetFieldIndex(string fieldname)
        {
            return -1;
        }
        public TreeListNode slowGetNodeFromVisibleIndex(int index)
        {
            int startindex = index;
            Tracing.StartTrack(0);
            RecursiveNodesEnumerator iterator = new RecursiveNodesEnumerator(m_firstNode, true);
            while (iterator.MoveNext())
            {
                index--;
                if (index < 0)
                {
                    Tracing.EndTrack(0, "slowGetNodeFromVisibleIndex({0})", startindex);
                    return iterator.Current as TreeListNode;
                }
            }
            Tracing.EndTrack(0, "slowGetNodeFromVisibleIndex (null)");
            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return new NodesEnumerator(m_firstNode);
        }
        /// <summary>
        /// TotalRowCount returns the total number of 'visible' nodes. Visible meaning visible for the
        /// tree view, This is used to determine the size of the scroll bar
        /// If 10 nodes each has 10 children and 9 of them are expanded, then 100 will be returned.
        /// </summary>
        /// <returns></returns>
        public virtual int slowTotalRowCount(bool mustBeVisible)
        {
            int cnt = 0;
            RecursiveNodesEnumerator iterator = new RecursiveNodesEnumerator(this, mustBeVisible);
            while (iterator.MoveNext())
                cnt++;
            //Debug.Assert(cnt == m_totalNodeCount);
            return cnt;
        }
        public virtual int VisibleNodeCount
        {
            get { return m_totalNodeCount; }
        }
        /// <summary>
        /// Returns the number of pixels required to show all visible nodes
        /// </summary>

        void EnsureInternalArray()
        {
            if (m_nodesInternal != null)
            {
                Debug.Assert(m_nodesInternal.Length == Count, "m_nodesInternal.Length == Count");
                return;
            }
            m_nodesInternal = new TreeListNode[Count];
            int index = 0;
            foreach (TreeListNode xnode in this)
                m_nodesInternal[index++] = xnode;
        }
        void ClearInternalArray()
        {
            m_nodesInternal = null;
        }

        int m_totalNodeCount = 0;
        protected virtual void UpdateNodeCount(int oldvalue, int newvalue)
        {
            m_totalNodeCount += (newvalue - oldvalue);
        }
        internal void internalUpdateNodeCount(int oldvalue, int newvalue)
        {
            UpdateNodeCount(oldvalue, newvalue);
        }
        internal class NodesEnumerator : IEnumerator
        {
            TreeListNode m_firstNode;
            TreeListNode m_current = null;
            public NodesEnumerator(TreeListNode firstNode)
            {
                m_firstNode = firstNode;
            }
            public object Current
            {
                get { return m_current; }
            }
            public bool MoveNext()
            {
                if (m_firstNode == null)
                    return false;
                if (m_current == null)
                {
                    m_current = m_firstNode;
                    return true;
                }
                m_current = m_current.NextSibling;
                return m_current != null;
            }
            public void Reset()
            {
                m_current = null;
            }
        }
        internal class RecursiveNodesEnumerator : IEnumerator<TreeListNode>
        {
            class NodeCollIterator : IEnumerator<TreeListNode>
            {
                TreeListNode m_firstNode;
                TreeListNode m_current;
                bool m_visible;
                public NodeCollIterator(NodeCollection collection, bool mustBeVisible)
                {
                    m_firstNode = collection.FirstNode;
                    m_visible = mustBeVisible;
                }
                public TreeListNode Current
                {
                    get { return m_current; }
                }
                object IEnumerator.Current
                {
                    get { return m_current; }
                }
                public bool MoveNext()
                {
                    if (m_firstNode == null)
                        return false;
                    if (m_current == null)
                    {
                        m_current = m_firstNode;
                        return true;
                    }
                    if (m_current.HasChildren && m_current.Nodes.FirstNode != null)
                    {
                        if (m_visible == false || m_current.Expanded)
                        {
                            m_current = m_current.Nodes.FirstNode;
                            return true;
                        }
                    }
                    if (m_current == m_firstNode)
                    {
                        m_firstNode = m_firstNode.NextSibling;
                        m_current = m_firstNode;
                        return m_current != null;
                    }
                    if (m_current.NextSibling != null)
                    {
                        m_current = m_current.NextSibling;
                        return true;
                    }

                    // search up the parent tree
                    while (m_current.Parent != null)
                    {
                        m_current = m_current.Parent;
                        // back at collection level, now go to next sibling
                        if (m_current == m_firstNode)
                        {
                            m_firstNode = m_firstNode.NextSibling;
                            m_current = m_firstNode;
                            return m_current != null;
                        }
                        if (m_current.NextSibling != null)
                        {
                            m_current = m_current.NextSibling;
                            return true;
                        }
                    }
                    m_current = null;
                    return false;
                }
                public void Reset()
                {
                    m_current = null;
                }
                public void Dispose()
                {
                    throw new Exception("The method or operation is not implemented.");
                }
            }
            IEnumerator<TreeListNode> m_enumerator = null;
            public RecursiveNodesEnumerator(TreeListNode firstNode, bool mustBeVisible)
            {
                m_enumerator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
            }
            public RecursiveNodesEnumerator(NodeCollection collection, bool mustBeVisible)
            {
                m_enumerator = new NodeCollIterator(collection, mustBeVisible);
            }
            public TreeListNode Current
            {
                get { return m_enumerator.Current; }
            }
            object IEnumerator.Current
            {
                get { return m_enumerator.Current; }
            }
            public bool MoveNext()
            {
                return m_enumerator.MoveNext();
            }
            public void Reset()
            {
                m_enumerator.Reset();
            }
            public void Dispose()
            {
                m_enumerator.Dispose();
            }
        }
        internal class ForwardNodeEnumerator : IEnumerator<TreeListNode>
        {
            TreeListNode m_firstNode;
            TreeListNode m_current;
            bool m_visible;
            public ForwardNodeEnumerator(TreeListNode firstNode, bool mustBeVisible)
            {
                m_firstNode = firstNode;
                m_visible = mustBeVisible;
            }
            public TreeListNode Current
            {
                get { return m_current; }
            }
            public void Dispose()
            {
            }
            object IEnumerator.Current
            {
                get { return m_current; }
            }
            public bool MoveNext()
            {
                if (m_firstNode == null)
                    return false;
                if (m_current == null)
                {
                    m_current = m_firstNode;
                    return true;
                }
                if (m_current.HasChildren && m_current.Nodes.FirstNode != null)
                {
                    if (m_visible == false || m_current.Expanded)
                    {
                        m_current = m_current.Nodes.FirstNode;
                        return true;
                    }
                }
                if (m_current.NextSibling != null)
                {
                    m_current = m_current.NextSibling;
                    return true;
                }
                // search up the paret tree until we find a parent with a sibling
                while (m_current.Parent != null && m_current.Parent.NextSibling == null)
                {
                    m_current = m_current.Parent;
                }

                if (m_current.Parent != null && m_current.Parent.NextSibling != null)
                {
                    m_current = m_current.Parent.NextSibling;
                    return true;
                }
                m_current = null;
                return false;
            }
            public void Reset()
            {
                m_current = m_firstNode;
            }
        }
        internal class ReverseNodeEnumerator : IEnumerator<TreeListNode>
        {
            TreeListNode m_firstNode;
            TreeListNode m_current;
            bool m_visible;
            public ReverseNodeEnumerator(TreeListNode firstNode, bool mustBeVisible)
            {
                m_firstNode = firstNode;
                m_visible = mustBeVisible;
            }
            public TreeListNode Current
            {
                get { return m_current; }
            }
            public void Dispose()
            {
            }
            object IEnumerator.Current
            {
                get { return m_current; }
            }
            public bool MoveNext()
            {
                if (m_firstNode == null)
                    return false;
                if (m_current == null)
                {
                    m_current = m_firstNode;
                    return true;
                }
                if (m_current.PrevSibling != null)
                {
                    m_current = FindNodesBottomLeaf(m_current.PrevSibling, m_visible);
                    return true;
                }
                if (m_current.Parent != null)
                {
                    m_current = m_current.Parent;
                    return true;
                }
                m_current = null;
                return false;
            }
            public void Reset()
            {
                m_current = m_firstNode;
            }
        }

        public static TreeListNode GetNextNode(TreeListNode startingNode, int searchOffset)
        {
            if (searchOffset == 0)
                return startingNode;
            if (searchOffset > 0)
            {
                ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(startingNode, true);
                while (searchOffset-- >= 0 && iterator.MoveNext()) ;
                return iterator.Current;
            }
            if (searchOffset < 0)
            {
                ReverseNodeEnumerator iterator = new ReverseNodeEnumerator(startingNode, true);
                while (searchOffset++ <= 0 && iterator.MoveNext()) ;
                return iterator.Current;
            }
            return null;
        }

        public static IEnumerable ReverseNodeIterator(TreeListNode firstNode, TreeListNode lastNode, bool mustBeVisible)
        {
            bool m_done = false;
            ReverseNodeEnumerator iterator = new ReverseNodeEnumerator(firstNode, mustBeVisible);
            while (iterator.MoveNext())
            {
                if (m_done)
                    break;
                if (iterator.Current == lastNode)
                    m_done = true;
                yield return iterator.Current;
            }
        }
        public static IEnumerable ForwardNodeIterator(TreeListNode firstNode, TreeListNode lastNode, bool mustBeVisible)
        {
            bool m_done = false;
            ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
            while (iterator.MoveNext())
            {
                if (m_done)
                    break;
                if (iterator.Current == lastNode)
                    m_done = true;
                yield return iterator.Current;
            }
        }
        public static IEnumerable ForwardNodeIterator(TreeListNode firstNode, bool mustBeVisible)
        {
            ForwardNodeEnumerator iterator = new ForwardNodeEnumerator(firstNode, mustBeVisible);
            while (iterator.MoveNext())
                yield return iterator.Current;
        }
        public static int GetVisibleNodeIndex(TreeListNode node)
        {
            if (node == null || node.IsVisible() == false || node.GetRootCollection() == null)
                return -1;

            // Finding the node index is done by searching up the tree and use the visible node count from each node.
            // First all previous siblings are searched, then when first sibling in the node collection is reached
            // the node is switch to the parent node and the again a search is done up the sibling list.
            // This way only higher up the tree are being iterated while nodes at the same level are skipped.
            // Worst case scenario is if all nodes are at the same level. In that case the search is a linear search.

            // adjust count for the visible count of the current node.
            int count = -node.VisibleNodeCount;
            while (node != null)
            {
                count += node.VisibleNodeCount;
                if (node.PrevSibling != null)
                    node = node.PrevSibling;
                else
                {
                    node = node.Parent;
                    if (node != null)
                        count -= node.VisibleNodeCount - 1; // -1 is for the node itself
                }
            }
            return count;
        }
        public static TreeListNode FindNodesBottomLeaf(TreeListNode node, bool mustBeVisible)
        {
            if (mustBeVisible && node.Expanded == false)
                return node;
            if (node.HasChildren == false || node.Nodes.LastNode == null)
                return node;
            node = node.Nodes.LastNode;
            return FindNodesBottomLeaf(node, mustBeVisible);
        }
    }
}
