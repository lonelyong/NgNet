using System;
using System.Text;

namespace NgNet.IO
{
    public class TextHelper
    {
        /// <summary>
        /// 读取文本文件
        /// </summary>
        /// <param name="txtpath"></param>
        /// <returns></returns>
        public static string ReadText(string txtpath, Encoding encoding = null)
        {
            encoding = encoding == null ? Encoding.UTF8 : encoding;
            using (System.IO.StreamReader sr = new System.IO.StreamReader(txtpath, encoding))
            {
                string Text = sr.ReadToEnd();
                sr.Close();
                return Text;
            }
        }

        /// <summary>
        /// 保存文本
        /// </summary>
        /// <param name="content">要保存的文本</param>
        /// <param name="filePath">路径</param>
        /// <param name="append">覆盖原文件为false，追加到文件结尾为true</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>返回是否保存成功</returns>
        public static void SaveText(string content, string filePath, bool append = true, Encoding encoding = null)
        {
            encoding = encoding == null ? Encoding.Default : encoding;
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter(filePath, append, encoding))
            {
                sw.Write(content);
                sw.Close();
            }
        }
    }
}
