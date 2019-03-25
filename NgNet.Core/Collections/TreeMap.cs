using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Collections
{
    /// <summary>
    /// 树状集合，可以绑定2个值
    /// </summary>
    /// <typeparam name="V1">值1的类型</typeparam>
    /// <typeparam name="V2">值2的类型</typeparam>
    public class TreeMap<V1, V2>
    {
        #region private fields

        #endregion

        #region public properties
        /// <summary>
        /// 获取根节点
        /// </summary>
        public TreeNode<V1, V2> Root { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// 无参实例化对象
        /// </summary>
        public TreeMap()
        {
           // Root = new TreeNode<V1, V2>();
        }
        #endregion

        #region private methods

        #endregion

        #region public methods

        #endregion
    }
}
