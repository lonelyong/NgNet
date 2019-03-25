using System;
using System.Windows.Forms;
namespace NgNet.UI.Forms
{
    public class TreeViewHelper
    {
        #region static methods
        /// <summary>
        /// 返回具有指定级别的父节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="level">level必须小于或等于指定节点的level</param>
        /// <returns></returns>
        public static TreeNode GetParentByLevel(TreeNode node, int level)
        {
            if (node.Level < level)
                throw new ArgumentOutOfRangeException("指定的级别大于当前Node的级别！");
            if (node.Level == level)
                return node;
            else
            {
                return GetParentByLevel(node.Parent, level);
            }
        }
        #endregion
    }
}
