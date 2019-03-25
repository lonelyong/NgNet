using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NgNet.Collections
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="K"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class KVCollection<K, V>
    {
        /// <summary>
        /// 得到集合以指定分隔符分隔的字符串
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="spliter1">分隔符1</param>
        /// <param name="spliter2">分隔符2</param>
        /// <returns></returns>
        public static string ToKeyValueString(IEnumerable<KeyValuePair<K, V>> collection, string spliter1 = ",", string spliter2 = ";")
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<K, V> kvp in collection)
            {
                sb.Append(kvp.Key);
                sb.Append(spliter1);
                sb.Append(kvp.Value);
                sb.Append(spliter2);
            }
            if (collection.Count<KeyValuePair<K, V>>() > 0)
                if (string.IsNullOrEmpty(spliter2) == false)
                    sb.Remove(sb.Length - spliter2.Length, spliter2.Length);
            return sb.ToString();
        }

        /// <summary>
        /// 得到集合以指定分隔符分隔的字符串
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="formatFlag1">Key的格式化标记</param>
        /// <param name="formatFlag2">Value的格式化标记</param>
        /// <param name="spliter1"></param>
        /// <param name="spliter2"></param>
        /// <returns></returns>
        public static string ToKeyValueString(IEnumerable<KeyValuePair<K, V>> collection, string formatFlag1, string formatFlag2, string spliter1 = ",", string spliter2 = ";")
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<K, V> kvp in collection)
            {
                sb.Append(string.Format(formatFlag1, kvp.Key));
                sb.Append(spliter1);
                sb.Append(string.Format(formatFlag2, kvp.Value));
                sb.Append(spliter2);
            }
            if (collection.Count() > 0)
                if (string.IsNullOrEmpty(spliter2) == false)
                    sb.Remove(sb.Length - spliter2.Length, spliter2.Length);
            return sb.ToString();
        }

        /// <summary>
        /// 得到集合以指定分隔符分隔的字符串
        /// </summary>
        /// <param name="collection">集合</param>
        /// <param name="spliter">分隔符</param>
        /// <returns></returns>
        public static string ToValueString(IEnumerable<KeyValuePair<K, V>> collection, string spliter)
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<K, V> kvp in collection)
            {
                sb.Append(kvp.Value);
                sb.Append(spliter);
            }
            if (collection.Count<KeyValuePair<K, V>>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }

        /// <summary>
        /// 得到集合以指定分隔符分隔的字符串
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="formatFlag">Value的格式化标记</param>
        /// <param name="spliter"></param>
        /// <returns></returns>
        public static string ToValueString(IEnumerable<KeyValuePair<K, V>> collection, string formatFlag,string spliter)
        {
            if (collection == null)
                return null;
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<K, V> kvp in collection)
            {
                sb.Append(string.Format(formatFlag, kvp.Value));
                sb.Append(spliter);
            }
            if (collection.Count<KeyValuePair<K, V>>() > 0)
                if (string.IsNullOrEmpty(spliter) == false)
                    sb.Remove(sb.Length - spliter.Length, spliter.Length);
            return sb.ToString();
        }

        /// <summary>
        /// 将两个集合的子项ToString()后，以指定的分隔符连接
        /// </summary>
        /// <param name="collection1"></param>
        /// <param name="collection2"></param>
        /// <param name="spliter1"></param>
        /// <param name="spliter2"></param>
        /// <returns></returns>
        public static string ToString(IEnumerable<K> collection1, IEnumerable<V> collection2, string spliter1 = ",", string spliter2 = ";")
        {
            if (collection1 == null || collection2 == null)
                return null;
            if (collection1.Count<K>() != collection2.Count<V>())
                return null;
            StringBuilder sb = new StringBuilder();
            K[] tmp1 = collection1.ToArray<K>();
            V[] tmp2 = collection2.ToArray<V>();
            for (int i = 0; i < tmp1.Length; i++)
            {
                sb.Append(tmp1[i]);
                sb.Append(spliter1);
                sb.Append(tmp2[i]);
                sb.Append(spliter2);
            }
            if (collection1.Count<K>() > 0)
                if (string.IsNullOrEmpty(spliter2) == false)
                    sb.Remove(sb.Length - spliter2.Length, spliter2.Length);
            return sb.ToString();
        }

        /// <summary>
        /// 将两个集合的子项ToString()后，以指定的分隔符连接
        /// </summary>
        /// <param name="collection1"></param>
        /// <param name="collection2"></param>
        /// <param name="formatFlag1"></param>
        /// <param name="formatFlag2"></param>
        /// <param name="spliter1"></param>
        /// <param name="spliter2"></param>
        /// <returns></returns>
        public static string ToString(IEnumerable<K> collection1, IEnumerable<V> collection2, string formatFlag1, string formatFlag2, string spliter1 = ",", string spliter2 = ";")
        {
            if (collection1 == null || collection2 == null)
                return null;
            if (collection1.Count<K>() != collection2.Count<V>())
                return null;
            StringBuilder sb = new StringBuilder();
            K[] tmp1 = collection1.ToArray<K>();
            V[] tmp2 = collection2.ToArray<V>();
            for (int i = 0; i < tmp1.Length; i++)
            {
                sb.Append(string.Format(formatFlag1, tmp1[i]));
                sb.Append(spliter1);
                sb.Append(string.Format(formatFlag2, tmp2[i]));
                sb.Append(spliter2);
            }
            if (collection1.Count<K>() > 0)
                if (string.IsNullOrEmpty(spliter2) == false)
                    sb.Remove(sb.Length - spliter2.Length, spliter2.Length);
            return sb.ToString();
        }

    }
}
