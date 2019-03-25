using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace NgNet.Text
{
    public static class Utils
    {
        /// <summary>
        /// 将用户输入的字符串转换为可换行、替换Html编码、无危害数据库特殊字符、去掉首尾空白、的安全方便代码。
        /// </summary>
        /// <param name="inputString">用户输入字符串</param>
        public static string GetSafeStr(string inputString)
        {
            string retVal = inputString;
            //retVal=retVal.Replace("&","&amp;"); 
            retVal = retVal.Replace("\"", "&quot;");
            retVal = retVal.Replace("<", "&lt;");
            retVal = retVal.Replace(">", "&gt;");
            retVal = retVal.Replace(" ", "&nbsp;");
            retVal = retVal.Replace("  ", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\t", "&nbsp;&nbsp;");
            retVal = retVal.Replace("\r", "<br>");
            return retVal;
        }

        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            foreach (byte item in s)
            {
                if ((int)item == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 计算字符串中包含几个字符
        /// </summary>
        /// <param name="text"></param>
        /// <param name="chr">查找的字符</param>
        /// <param name="caseNgNetitive">是否区分大小写</param>
        /// <returns></returns>
        public static int GetCharCount(string text, char chr, bool caseSensitive)
        {
            if (string.IsNullOrEmpty(text))
                return 0;
            if (caseSensitive == false)
            {
                chr = (char)chr.ToString().ToLower()[0];
                text = text.ToLower();
               
            }
            return text.Count(w => w == chr);
        }

        /// <summary>
        /// 当字符串长度大于指定长度时，用指定长度的字符串替换中间多出的字符串以达到指定长度的要求
        /// </summary>
        /// <param name="text">输入的字符串</param>
        /// <param name="length">指定的长度</param>
        /// <param name="replaceChar">替换字符串</param>
        /// <returns></returns>
        public static string GetShorterText(string text, int length, char replaceChar = '.')
        {
            if (string.IsNullOrEmpty(text) || text.Length <= length)
                return text;
            if (length < 1)
                throw new System.Exception("转换后的字符数至少为1");
            int _replaceCount = 3;
            if (length <= 4)
                return string.Format("{0}{1}", text.Substring(0, 1), new string(replaceChar, length - 1));
            int _lenLeft = length - _replaceCount;
            int _lenLeftCenter = _lenLeft / 2;
            bool bLen = _lenLeft % 2 == 0;
            return string.Format("{0}{1}{2}",
                text.Substring(0, _lenLeftCenter),
                new string(replaceChar, _replaceCount),
                text.Substring(text.Length - (bLen ? _lenLeftCenter : ++_lenLeftCenter), bLen ? _lenLeftCenter : _lenLeftCenter));
        }

        /// <summary>
        /// 在指定的文本中添加指定的字符将其转换为指定长度的文本
        /// </summary>
        /// <param name="text">要转换的文本</param>
        /// <param name="length">转换后的文本长度，如果长度小于指定的文本的长度将会返回未处理的文本</param>
        /// <param name="padChar">添加的字符</param>
        /// <returns></returns>
        public static string GetLongerText(string text, int length, char padChar)
        {
            int _textLength = text == null ? 0 : text.Length;
            if (_textLength >= length)
                return text;
            int _len = length - _textLength;
            if (_textLength <= 1)
            {
                float _lenHalf = _len / 2f;
                int _lenLeft = (int)System.Math.Ceiling(_lenHalf);
                int _lenRight = (int)System.Math.Floor(_lenHalf);
                return $"{new string(padChar, _lenLeft)}{text}{new string(padChar, _lenRight)}";
            }
            else
            {
                float _lenAver = _len / (_textLength - 1f);// 平均每个间隔需要添加几个字符
                int _lenBigger = (int)System.Math.Ceiling(_lenAver);
                int _lenSmaller = (int)System.Math.Floor(_lenAver);
                int _count = _textLength - 1 - _len; //需要添加的字符长度与原字符间隔数的差值
                StringBuilder _sb = new StringBuilder();
                int _biggerCount = _len % (_textLength - 1);
                _sb.Append(text.First());
                for (int i = 1; i < _textLength; i++)
                {
                    if (_biggerCount-- > 0)
                        _sb.Append(padChar, _lenBigger);
                    else
                        _sb.Append(padChar, _lenSmaller);
                    _sb.Append(text[i]);
                }
                return _sb.ToString();
            }
        }
    }
}
