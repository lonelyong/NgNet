using System;

namespace NgNet.Math
{
    /*----------------------------------------------------------------  
    // 版权所有：Yong(i.yong@outlook.com)   
    //  
    // 类名：Test
    // 类功能描述：数字检测类
    // 创建日期：2015-06-15
    // 
    //   
    // 创建标识： 
    // 创建描述：  
    //  
    // 修改标识：  
    // 修改描述：  
    //----------------------------------------------------------------*/
    public static class Test
    {
        /// <summary>
        ///  获取或设置一个值，该值指示指定的字符串是否为二进制数字
        /// </summary>
        /// <param name="binString"></param>
        /// <returns></returns>
        public static bool IsBinary(string binString)
        {
            bool result = false;
            try
            {
                int ascii;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                for (int i = 0; i < binString.Length; i++)
                {
                    ascii = asciiEncoding.GetBytes(binString.Substring(i, 1))[0];
                    if (binString.Length == 1)
                    {
                        if (ascii == 48 || ascii == 49)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    if (binString.Length > 1)
                    {
                        if (ascii == 48 
                            || ascii == 49 
                            || (ascii == 45 && binString.IndexOf("-", 2) == -1))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }

                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        ///  获取或设置一个值，该值指示指定的字符串是否为八进制数字
        /// </summary>
        /// <param name="octString"></param>
        /// <returns></returns>
        public static bool IsOctonary(string octString)
        {
            bool result = false;
            try
            {
                int ascii;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                for (int i = 0; i < octString.Length; i++)
                {
                    ascii = (int)asciiEncoding.GetBytes(octString.Substring(i, 1))[0];
                    if (octString.Length == 0) { result = false; }
                    if (octString.Length == 1)
                    {
                        if (ascii >= 48 && ascii <= 55)
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    if (octString.Length > 1)
                    {
                        if ((ascii >= 48 && ascii <= 55)
                             || (ascii == 45 && octString.IndexOf("-", 2) == -1))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        ///  获取或设置一个值，该值指示指定的字符串是否为十进制数字
        /// </summary>
        /// <param name="decString"></param>
        /// <returns></returns>
        public static bool IsDecimal(string decString)
        {
            bool result = false;
            try
            {
                int ascii;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                for (int i = 0; i < decString.Length; i++)
                {
                    ascii = asciiEncoding.GetBytes(decString.Substring(i, 1))[0];
                    if (decString.Length == 0) { result = false; }
                    if (decString.Length == 1)
                    {
                        if ((ascii >= 48 && ascii <= 57))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    if (decString.Length > 1)
                    {
                        if ((ascii >= 48 && ascii <= 57)
                            || (ascii == 45 && decString.IndexOf("-", 2) == -1))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 获取或设置一个值，该值指示指定的字符串是否为十六进制数字
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static bool IsHexadecimal(string hexString) 
        {
            bool result = false;
            try
            {
                int ascii;
                System.Text.ASCIIEncoding asciiEncoding = new System.Text.ASCIIEncoding();
                for (int i = 0; i < hexString.Length; i++)
                {
                    ascii = asciiEncoding.GetBytes(hexString.Substring(i, 1))[0];
                    if (hexString.Length == 0) { result = false; }
                    if (hexString.Length == 1)
                    {
                        if ((ascii >= 48 && ascii <= 57)
                            || (ascii >= 65 && ascii <= 70)
                            || (ascii >= 97 && ascii <= 102))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                    if (hexString.Length > 1)
                    {
                        if ((ascii >= 48 && ascii <= 57)
                            || (ascii >= 65 && ascii <= 70)
                            || (ascii >= 97 && ascii <= 102)
                            || (ascii == 45 && hexString.IndexOf("-", 2) == -1))
                        {
                            result = true;
                        }
                        else
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                result = false;
            }
            return result;
        }
        /// <summary>
        /// 判断对象是否为正确的Int32值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Int32值。</returns>
        public static bool IsInt(object obj)
        {
            try
            {
                int.Parse(ToObjectString(obj));
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 判断对象是否为正确的Long值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Long值。</returns>
        public static bool IsLong(object obj)
        {
            try
            {
                long.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }
        /// <summary>
        /// 判断对象是否为正确的Float值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Float值。</returns>
        public static bool IsFloat(object obj)
        {
            try
            {
                float.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }
        /// <summary>
        /// 判断对象是否为正确的Double值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Double值。</returns>
        public static bool IsDouble(object obj)
        {
            try
            {
                double.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }
        /// <summary>
        /// 判断对象是否为正确的Decimal值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>Decimal值。</returns>
        public static bool IsDecimal(object obj)
        {
            try
            {
                decimal.Parse(ToObjectString(obj));
                return true;
            }
            catch
            { return false; }
        }

        private static string ToObjectString(object obj)
        {
            if (obj == null)
                return null;
            else
                return obj.ToString();
        }
    }
}
