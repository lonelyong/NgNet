using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Collections
{
    /// <summary>
    ///  不允许存储重复值的集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SignleCollection<T> : CollectionBase<T>
    {
        #region constructor
        public SignleCollection(IEnumerable<T> collection) : base (collection)
        {

        }

        public SignleCollection() : base()
        {

        }
        #endregion

        #region IList<T>
        public override T this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                if (Contains(value))
                    return;
                base[index] = value;
            }
        }

        public override void Add(T item)
        {
            if (Contains(item))
                return;
            base.Add(item);
        }

        public override void Insert(int index, T item)
        {
            if (Contains(item))
                return;
            base.Insert(index, item);
        }

        public override bool Remove(T item)
        {
            return base.Remove(item);
        }

        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
        }
        #endregion

        #region public methods
        public void Add(T item, out bool existed)
        {
            if (Contains(item))
                existed = true;
            else
            {
                base.Add(item);
                existed = false;
            }
        }

        public void Insert(int index, T item, out bool existed)
        {
            if (Contains(item))
                existed = true;
            else
            {
                base.Insert(index, item);
                existed = false;
            }
        }

        public override void AddRange(IEnumerable<T> collection)
        {
            IEnumerable<T> existed;
            IEnumerable<T> notExisted;
            AddRange(collection, out existed,out notExisted);
        }

        public virtual void AddRange(IEnumerable<T> collection, out IEnumerable<T> existedItems, out IEnumerable<T> addedItems)
        {
            List<T> existed = new List<T>();
            List<T> notExisted = new List<T>();
            foreach (T item in collection)
            {
                if (Contains(item))
                    existed.Add(item);
                else
                    notExisted.Add(item);
            }
            base.AddRange(notExisted);
            existedItems = existed;
            addedItems = notExisted;
        }

        public virtual void Remove(T item, out bool exist)
        {
            if (Contains(item))
            {
                base.Remove(item);
                exist = true;
            }
            else
                exist = false;

        }

        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
        }
        #endregion
    }
}
