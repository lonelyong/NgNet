using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Collections
{
    /// <summary>
    /// 基础结合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class CollectionBase<T> : IList<T>, IReadOnlyList<T>
    {
        #region protected fields
        /// <summary>
        /// 内部集合
        /// </summary>
        protected List<T> InnerList;
        #endregion

        #region IList<T>
        /// <summary>
        /// 获取或设置索引为Index的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual T this[int index]
        {
            get
            {
                return InnerList[index];
            }
            set
            {
                InnerList[index] = value;
            }
        }

        /// <summary>
        /// 获取集合元素的数量
        /// </summary>
        public int Count
        {
            get
            {
                return InnerList.Count;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示当前集合是否是只读的
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return (InnerList as IList<T>).IsReadOnly;
            }
        }

        /// <summary>
        /// 将指定的元素添加到集合
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            InnerList.Add(item);
        }
        /// <summary>
        /// 清空集合
        /// </summary>
        public virtual void Clear()
        {
            InnerList.Clear();
        }

        /// <summary>
        /// 获取一个值，该值指示集合中是否存在指定的元素
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        /// <summary>
        ///  
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 获取一个值，该值指示指定元素在集合中的索引
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public int IndexOf(T item, int index)
        {
            return InnerList.IndexOf(item, index);
        }

        public int IndexOf(T item, int index, int count)
        {
            return InnerList.IndexOf(item, index, count);
        }

        public virtual void Insert(int index, T item)
        {
            InnerList.Insert(index, item);
        }

        public virtual bool Remove(T item)
        {
            return InnerList.Remove(item); 
        }

        public virtual void RemoveAt(int index)
        {
            InnerList.RemoveAt(index);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }
        #endregion

        #region public methods
        public virtual void AddRange(IEnumerable<T> collection)
        {
            InnerList.AddRange(collection);
        }

        public virtual void RemoveRange(int index, int count)
        {
            InnerList.RemoveRange(index, count);
        }

        public int LastIndexOf(T item)
        {
            return InnerList.LastIndexOf(item);
        }

        public int LastIndexOf(T item, int index)
        {
            return InnerList.IndexOf(item, index);
        }

        public int LastIndexOf(T item, int index, int count)
        {
            return InnerList.IndexOf(item, index, count);
        }
        #endregion

        #region constructor
        /// <summary>
        /// 初始化包含指定集合的实例
        /// </summary>
        /// <param name="collection"></param>
        public CollectionBase(IEnumerable<T> collection)
        {
            InnerList = new List<T>(collection);
        }
        /// <summary>
        ///创建集合元素为空的集合实例
        /// </summary>
        public CollectionBase() : this(new T[] { })
        {

        }
        #endregion

    }
}
