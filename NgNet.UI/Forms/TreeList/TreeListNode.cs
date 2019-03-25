using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace NgNet.UI.Forms
{
	public class TreeListNode
	{
		NodeCollection		m_owner = null;
		TreeListNode				m_prevSibling = null;
		TreeListNode				m_nextSibling = null;
		NodeCollection		m_children = null;
		bool				m_hasChildren = false;
		bool				m_expanded = false;
		int					m_imageId = -1;
		int					m_id = -1;
		object				m_tag = null;

		public TreeListNode Parent
		{
			get 
			{ 
				if (m_owner != null)
					return m_owner.Owner; 
				return null;
			}
		}
		public TreeListNode PrevSibling
		{
			get { return m_prevSibling; }
		}
		public TreeListNode NextSibling
		{
			get { return m_nextSibling; }
		}
		public bool HasChildren
		{
			get 
			{
				if (m_children != null && m_children.IsEmpty() == false)
					return true;
				return m_hasChildren;
			}
			set
			{
				m_hasChildren = value;
			}
		}
		public int ImageId
		{
			get { return m_imageId; }
			set { m_imageId = value; }
		}
		public virtual NodeCollection Owner
		{
			get { return m_owner; }
		}
		public virtual NodeCollection Nodes
		{
			get
			{
				if (m_children == null)
					m_children = new NodeCollection(this);
				return m_children;
			}
		}
		public bool Expanded
		{
			get { return m_expanded && HasChildren; }
			set
			{
				if (m_expanded == value)
					return;
				NodeCollection root = GetRootCollection();
				if (root != null)
					root.NodetifyBeforeExpand(this, value);

				int oldcount = VisibleNodeCount;
				m_expanded = value;
				if (m_expanded)
					UpdateOwnerTotalCount(1, VisibleNodeCount);
				else
					UpdateOwnerTotalCount(oldcount, 1);

				if (root != null)
					root.NodetifyAfterExpand(this, value);
			}
		}
		public void Collapse()
		{
			Expanded = false;
		}
		public void Expand()
		{
			Expanded = true;
		}
		public void ExpandAll()
		{
			Expanded = true;
			if (HasChildren)
			{
				foreach (TreeListNode node in Nodes)
					node.ExpandAll();
			}
		}
		public object Tag
		{
			get { return m_tag; }
			set { m_tag = value; }
		}
		public TreeListNode()
		{
			m_data = new object[1];
		}
		public TreeListNode(string text)
		{
			m_data = new object[1] {text};
		}
		public TreeListNode(object[] fields)
		{
			SetData(fields);
		}
		object[] m_data = null;
		public object this [string fieldname]
		{
			get
			{
				return this[Owner.FieldIndex(fieldname)];
			}
			set
			{
				this[Owner.FieldIndex(fieldname)] = value;
			}
		}
		public object this [int index]
		{
			get
			{
				if (index < 0 || index >= m_data.Length)
					return null;
				return m_data[index];
			}
			set
			{
				if (index >= 0 && index < 100) // within this range just increase
				{
					if (m_data == null || index >= m_data.Length)
					{
						object[] newdata = new object[index+1];
						if (m_data != null)
							m_data.CopyTo(newdata, 0);
						m_data = newdata;
					}
				}
				AssertData(index);
				m_data[index] = value;
			}
		}
		public void SetData(object[] fields)
		{
			m_data = new object[fields.Length];
			fields.CopyTo(m_data, 0);
		}
		public int VisibleNodeCount
		{
			get 
			{ 
				// can not use Expanded property here as it returns false node has no children
				if (m_expanded)
					return m_childVisibleCount + 1; 
				return 1;
			}
		}
		/// <summary>
		/// MakeVisible will expand all the parents up the tree.
		/// </summary>
		public void MakeVisible()
		{
			TreeListNode parent = Parent;
			while (parent != null)
			{
				parent.Expanded = true;
				parent = parent.Parent;
			}
		}
		/// <summary>
		/// IsVisible returns true if all parents are expanded, else false
		/// </summary>
		public bool IsVisible()
		{
			TreeListNode parent = Parent;
			while (parent != null)
			{
				// parent not expanded, so this node is not visible
				if (parent.Expanded == false)
					return false;
				// parent not hooked up to a collection, so this node is not visible
				if (parent.Owner == null)
					return false;
				parent = parent.Parent;
			}
			return true;
		}
		public TreeListNode GetRoot()
		{
			TreeListNode parent = this;
			while (parent.Parent != null)
				parent = parent.Parent;
			return parent;
		}
		public NodeCollection GetRootCollection()
		{
			return GetRoot().Owner;
		}
		public string GetId()
		{
			StringBuilder sb = new StringBuilder(32);
			TreeListNode node = this;
			while (node != null)
			{
				node.Owner.UpdateChildIds(false);
				if (node.Parent != null)
					sb.Insert(0, "." + node.Id.ToString());
				else
					sb.Insert(0, node.Id.ToString());
				node = node.Parent;
			}
			return sb.ToString();
		}
		internal void InsertBefore(TreeListNode insertBefore, NodeCollection owner)
		{
			this.m_owner = owner;
			TreeListNode next = insertBefore;
			TreeListNode prev = null;
			if (next != null)
			{
				prev = insertBefore.PrevSibling;
				next.m_prevSibling = this;
			}
			if (prev != null)
				prev.m_nextSibling = this;

			this.m_nextSibling = next;
			this.m_prevSibling = prev;
			UpdateOwnerTotalCount(0, VisibleNodeCount);
		}
		internal void InsertAfter(TreeListNode insertAfter, NodeCollection owner)
		{
			this.m_owner = owner;
			TreeListNode prev = insertAfter;
			TreeListNode next = null;
			if (prev != null)
			{
				next = prev.NextSibling;
				prev.m_nextSibling = this;
				this.m_prevSibling = prev;
			}
			if (next != null)
				next.m_prevSibling = this;
			this.m_nextSibling = next;
			UpdateOwnerTotalCount(0, VisibleNodeCount);
		}
		internal void Remove()
		{
			TreeListNode prev = this.PrevSibling;
			TreeListNode next = this.NextSibling;
			if (prev != null)
				prev.m_nextSibling = next;
			if (next != null)
				next.m_prevSibling = prev;

			this.m_nextSibling = null;
			this.m_prevSibling = null;
			UpdateOwnerTotalCount(VisibleNodeCount, 0);
			this.m_owner = null;
			this.m_id = -1;
		}
		internal static void SetHasChildren(TreeListNode node, bool hasChildren)
		{
			if (node != null)
				node.m_hasChildren = hasChildren;
		}
		public int NodeIndex
		{
			get { return Id;}
		}
		internal int Id
		{
			get 
			{ 
				if (m_owner == null)
					return -1;
				m_owner.UpdateChildIds(false);
				return m_id; 
			}
			set { m_id = value; }
		}
		int m_childVisibleCount = 0;
		void UpdateTotalCount(int oldValue, int newValue)
		{
			int old = VisibleNodeCount;
			m_childVisibleCount += (newValue - oldValue);
			UpdateOwnerTotalCount(old, VisibleNodeCount);
		}
		void UpdateOwnerTotalCount(int oldValue, int newValue)
		{
			if (Owner != null)
				Owner.internalUpdateNodeCount(oldValue, newValue);
			if (Parent != null)
				Parent.UpdateTotalCount(oldValue, newValue);
		}

		void AssertData(int index)
		{
			Debug.Assert(index >= 0, "index >= 0");
			Debug.Assert(index < m_data.Length, "index < m_data.Length");
		}
	}
}
