using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Math
{
    /*----------------------------------------------------------------  
    // 版权所有：Yong(i.yong@outlook.com)   
    //  
    // 类名：Convert
    // 类功能描述：字符串转换数字，尽职转换
    // 创建日期：2015-06-15
    // 
    //   
    // 创建标识： 
    // 创建描述：  
    //  
    // 修改标识：  
    // 修改描述：  
    //----------------------------------------------------------------*/
    public static class Convert
    {
        public static string Bin2Oct(string bin)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(bin, 2);
            return System.Convert.ToString(result, 8);
        }

        public static string Bin2Dec(string bin)
        {
            return System.Convert.ToInt64(bin, 2).ToString();
        }

        public static string Bin2Hex(string bin)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(bin, 2);
            return System.Convert.ToString(result, 16);

        }

        public static string Oct2Bin(string oct)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(oct, 8);
            return System.Convert.ToString(result, 2);
        }

        public static string Oct2Dec(string oct)
        {
            return System.Convert.ToInt64(oct, 8).ToString();
        }

        public static string Oct2Hex(string oct)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(oct, 8);
            return System.Convert.ToString(result, 16);
        }

        public static string Dec2Bin(string dec)
        {
            return System.Convert.ToString(Int64.Parse(dec), 2);
        }

        public static string Dec2Oct(string dec)
        {
            return System.Convert.ToString(Int64.Parse(dec), 8);
        }

        public static string Dec2Hex(string dec)
        {
            return System.Convert.ToString(Int64.Parse(dec), 16);
        }

        public static string Hex2Bin(string hex)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(hex, 16);
            return System.Convert.ToString(result, 2);
        }

        public static string Hex2Oct(string hex)
        {
            Int64 result = 0;
            result = System.Convert.ToInt64(hex, 16);
            return System.Convert.ToString(result, 8);
        }

        public static string Hex2Dec(string hex)
        {
            return System.Convert.ToInt64(hex, 16).ToString();
        }


        /// <summary>
        /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
        /// </summary>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string BODHConvert(string value, BODH from, BODH to)
        {
            try
            {
                int decValue = System.Convert.ToInt32(value, (int)from);  //先转成10进制
                string result = System.Convert.ToString(decValue, (int)to);  //再转成目标进制
                if (to == BODH.Binary)
                {
                    int resultLength = result.Length;  //获取二进制的长度
                    switch (resultLength)
                    {
                        case 7:
                            result = "0" + result;
                            break;
                        case 6:
                            result = "00" + result;
                            break;
                        case 5:
                            result = "000" + result;
                            break;
                        case 4:
                            result = "0000" + result;
                            break;
                        case 3:
                            result = "00000" + result;
                            break;
                    }
                }
                return result;
            }
            catch
            {
                return "0";
            }
        }
    }
}
