using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace NgNet.Collections
{
    public class TCollection<T>
    {
        /// <summary>
        /// 将集合中的对象以指定的flag格式化为字符串，并以指定的分隔符连接
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="spliter">分隔符</param>
        /// <returns>字符串</returns>
        public static string ToString(IEnumerable<T> collection, string spliter = ",")
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (T item in collection)
            {
                sb.Append(item);
                sb.Append(spliter);
            }
            if (collection.Count<T>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }
        /// <summary>
        /// 将集合中的对象以指定的flag格式化为字符串，并以指定的分隔符连接
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="formatFlag">格式化flag，string.Format()格式化，格式：{0:D2}</param>
        /// <param name="spliter">分隔符</param>
        /// <returns>字符串</returns>
        public static string ToString(IEnumerable<T> collection, string formatFlag, string spliter = ",")
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (T item in collection)
            {
                sb.Append(string.Format(formatFlag, item));
                sb.Append(spliter);
            }
            if (collection.Count<T>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }
        /// <summary>
        /// 拼接集合字符串
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="pi"></param>
        /// <param name="formatFlag"></param>
        /// <param name="spliter"></param>
        /// <returns></returns>
        public static string ToString(IEnumerable<T> collection, PropertyInfo pi, string formatFlag, string spliter = ",")
        {
            if (collection == null)
                return null;
            if (pi == null)
                throw new System.Exception($"{nameof(pi)}不能为null");

            StringBuilder sb = new StringBuilder();
            string format = string.IsNullOrWhiteSpace(formatFlag) ? "{0}" : "{0:" + formatFlag + "}";

            foreach (T item in collection)
            {
                if(item != null)
                {
                    object _obj = pi.GetValue(item);
                    if (_obj != null)
                        sb.Append(string.Format(format, _obj));
                }           
                sb.Append(spliter);
            }
            if (collection.Count<T>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }
        /// <summary>
        /// 拼接集合中的字符串
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="piParent">T的属性，用于获取TItem的指定属性值</param>
        /// <param name="piChild">T属性的属性</param>
        /// <param name="formatFlag"></param>
        /// <param name="spliter"></param>
        /// <returns></returns>
        public static string ToString(IEnumerable<T> collection, PropertyInfo piParent, PropertyInfo piChild, string formatFlag, string spliter = ",")
        {
            if (collection == null)
                return null;
            if (piParent == null || piChild == null)
                throw new System.Exception($"{nameof(piParent)},{nameof(piChild)}不能为null");
            StringBuilder sb = new StringBuilder();
            string format = string.IsNullOrWhiteSpace(formatFlag) ? "{0}" : "{0:" + formatFlag + "}";
            foreach (T item in collection)
            {
                if (item != null)
                {
                    object _obj0 = piParent.GetValue(item);
                    if (_obj0 != null)
                    {
                        object _obj1 = piChild.GetValue(_obj0);
                        if (_obj1 != null)
                            sb.Append(string.Format(format, _obj1));
                    }
                        
                }
                sb.Append(spliter);
            }
            if (collection.Count<T>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }
    }
}
