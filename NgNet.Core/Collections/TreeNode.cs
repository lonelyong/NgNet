using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Collections
{
    /// <summary>
    /// 树状集合的节点
    /// </summary>
    /// <typeparam name="V1">值1的类型</typeparam>
    /// <typeparam name="V2">值2的类型</typeparam>
    public class TreeNode<V1, V2> : IEnumerable
    {
        #region private fields
        private List<TreeNode<V1, V2>> _innerList = new List<TreeNode<V1, V2>>();
        #endregion

        #region public properties
        /// <summary>
        /// 获取根节点
        /// </summary>
        public TreeNode<V1, V2> Root
        {
            get
            {
                if (Parent == null)
                    return this;
                else
                    return Parent.Root;
            }
        }
        /// <summary>
        /// 获取此节点的父节点
        /// </summary>
        public TreeNode<V1, V2> Parent { get; private set; }
        /// <summary>
        /// 获取此节点的所有子节点
        /// </summary>
        public IReadOnlyCollection<TreeNode<V1, V2>> Items
        {
            get
            {
                return _innerList;
            }
        }

        /// <summary>
        /// 获取或设置此节点的值1
        /// </summary>
        public V1 Value1 { get; set; }
        /// <summary>
        /// 获取或设置此节点的值2
        /// </summary>
        public V2 Value2 { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// 无参构造函数
        /// </summary>
        public TreeNode()
        {

        }
        #endregion

        #region private methods

        #endregion

        #region public mehtods
        /// <summary>
        /// 添加子节点
        /// </summary>
        /// <param name="child"></param>
        public void Add(TreeNode<V1, V2> child)
        {
            if (!_innerList.Contains(child))
            {
                lock (_innerList)
                {
                    child.Parent = this;
                    _innerList.Add(child);
                }
            }
        }

        public void Add(object objNode)
        {
            if (objNode is TreeNode<V1, V2>)
            {
                Add(objNode as TreeNode<V1, V2>);
            }
            else
                throw new Exception("指定的类型不是有效的Node");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<TreeNode<V1, V2>> items)
        {
            foreach (TreeNode<V1, V2> item in items)
            {
                Add(item);
            }
        }

        /// <summary>
        /// 添加父节点
        /// </summary>
        /// <param name="child"></param>
        public void Remove(TreeNode<V1, V2> child)
        {
            lock (_innerList)
            {
                TreeNode<V1, V2> _parent = child.Parent;
                foreach (TreeNode<V1, V2> item in child.Items)
                {
                    item.Parent = _parent;
                }
                child.Parent = null;
                _innerList.Remove(child);
            }
        }
        /// <summary>
        /// 获取一个值，该值指示此节点的子节点是否包括指定项
        /// </summary>
        /// <param name="child">子节点</param>
        /// <returns></returns>
        public bool Contains(TreeNode<V1, V2> child)
        {
            return _innerList.Contains(child);
        }

        /// <summary>
        /// 清空此节点的子节点
        /// </summary>
        /// <returns></returns>
        public void Clear()
        {
            foreach (TreeNode<V1, V2> item in _innerList)
            {
                item.Parent = null;
            }
            _innerList.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerList.GetEnumerator();
        }
        #endregion
    }
}
