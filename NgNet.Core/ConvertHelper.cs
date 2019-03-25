
/*----------------------------------------------------------------  
// 版权所有：Yong(i.yong@outlook.com)   
//  
// 文件名： hConvert.cs
// 文件功能描述：数据类型转换
//  
// 创建标识：  
// 创建描述：  
//  
// 修改标识：  
// 修改描述：  
/----------------------------------------------------------------*/
using System;
using System.Drawing;
using System.Linq;

namespace NgNet
{
    /// <summary>
    /// 类型转换帮助类
    /// </summary>
    public static class ConvertHelper
    {
        private const string CNNUM = Text.Consts.ChineseFigures;

        #region int
        /// <summary>
        /// 将阿拉伯数字转换为中文数字
        /// </summary>
        /// <param name="intNum"></param>
        /// <returns></returns>
        public static string ToChineseFigures(this int intNum)
        {
            return string.Join("", intNum.ToString().Select(t => CNNUM[t - 48]));
        }
        #endregion

        #region Icon
        /// <summary>
        /// 将bitmap转换为Icon
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static Icon Bitmap2Icon(this System.Drawing.Bitmap bitmap)
        {
            return Icon.FromHandle(bitmap.GetHicon());
        }
        #endregion

        #region DateTime
        /// <summary>
        /// 获取00:00:00(string) 
        /// 或00:00(string)总秒数
        /// </summary>
        /// <param name="timeString">输入时间字符创：样式 00:00 或 00:00:00</param>
        /// <returns>返回以long型的以秒为单位的数字</returns>
        public static long ToSeconds(this string timeString)
        {
            int s = 0;
            string[] hms = timeString.Split(new Char[] { ':' }, StringSplitOptions.None);
            for (int i = 0; i < hms.Length; i++)
            {
                s += (int)System.Math.Pow(int.Parse(hms[i]), hms.Length - i - 1);
            }
            return s;
        }
        /// <summary>
        /// 将 秒数 转换为00:00:00(string)
        /// </summary>
        /// <param name="longSecond">输入long型的总秒数</param>
        /// <returns>返回 00:00:00样式的字符创</returns>
        public static string ToTimeString(this long longSecond)
        {
            long h = 0, m = 0, s = 0;
            h = longSecond / 3600;
            m = longSecond % 3600;
            s = m % 60;
            m = m / 60;
            return $"{(longSecond < 0 ? "-" : null)}{(h == 0 ? string.Empty: h.ToString())}{(h == 0 ? null : ":")}{m.ToString("#00")}:{s.ToString("#00")}";
        }
        /// <summary>
        /// 将字符串转换为数值(DateTime)类型。
        /// </summary>
        /// <param name="datetimeString" >日期时间字符串</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>DateTime值。</returns>
        public static DateTime ToDateTime(this string datetimeString, DateTime returnValue)
        {
            try
            {
                return DateTime.Parse(datetimeString);
            }
            catch
            { return returnValue; }
        }
        #endregion

        #region Bool
        /// <summary>
        /// 将指定的布尔字符串转换为布尔值
        /// </summary>
        /// <param name="boolString">bool字符串</param>
        /// <returns>返回转换后的值，如果转换失败则返回Null</returns>
        public static bool? ToBool(this string boolString)
        {
            switch (boolString?.ToLower())
            {
                case "0":
                case "false":
                    return false;
                case "1":
                case "true":
                    return true;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 将字符串转换为布尔
        /// 如 False|True（不分大小写,当无法适配true时返回指定的值）
        /// </summary>
        /// <param name="boolString">输入形如false或true的字符串</param>
        /// <param name="returnValue">无法转换时返回指定的值</param>
        /// <returns></returns>
        public static bool ToBool(this string boolString, bool returnValue)
        {
            var value = ToBool(boolString);
            return value == null ? returnValue : value.Value;
        }
        #endregion

        #region Point
        /// <summary>
        /// 从字符串转换成Point实体
        /// </summary>
        /// <param name="pointString">点字符串</param>
        /// <returns>返回转换后的值</returns>
        public static Point? ToPoint(this string pointString)
        {
            if (string.IsNullOrWhiteSpace(pointString))
                return null;
            if (pointString.IndexOf(",") == -1)
                return null;
            string _x = NgNet.Text.RegexHelper.GetTxtFigures(pointString.Substring(0, pointString.IndexOf(",")));
            string _y = NgNet.Text.RegexHelper.GetTxtFigures(pointString.Substring(pointString.IndexOf(",")));

            if (string.IsNullOrWhiteSpace(_x) || string.IsNullOrWhiteSpace(_y))
                return null;
            return new Point(System.Convert.ToInt32(_x), System.Convert.ToInt32(_y));
        }

        /// <summary>
        /// 把{x,y}/[x,y]/(1,2)/{x=0,y=0}/[width=0,y=0]/(x=0,y=0)等(key=value,key=value)格式的字符串转换为点坐标(Point)
        /// </summary>
        /// <param name="pointString">形如{x,y}/[x,y]/(x,y)/{x=0,y=0}/[x=0,y=0]/(x=0,y=0)格式的字符串，不区分大小写</param>
        /// <param name="returnValue">无法转换时返回指定的点</param>
        /// <returns>返回点坐标</returns>
        public static Point ToPoint(this string pointString, System.Drawing.Point returnValue)
        {
            Point? _p = ToPoint(pointString);
            if (_p == null)
                return returnValue;
            else
                return (Point)_p;
        }
        #endregion

        #region Size
        /// <summary>
        /// 把{x,y}/[x,y]/(1,2)/{x=0,y=0}/[width=0,y=0]/(x=0,y=0)等(key=value,key=value)格式的字符串转换为Size
        /// </summary>
        /// <param name="sizeString">形如{x,y}/[x,y]/(x,y)/{x=0,y=0}/[x=0,y=0]/(x=0,y=0)格式的字符串，不区分大小写且忽略一切空格</param>
        /// <param name="returnValue">无法转换时返回指定的Size</param>
        /// <returns>返回点坐标</returns>
        public static Size ToSize(this string sizeString, System.Drawing.Size returnValue)
        {
            Size? _s = ToSize(sizeString);
            if (_s == null)
                return returnValue;
            else
                return (Size)_s;
        }

        /// <summary>
        /// 从字符串转换为Size实体
        /// </summary>
        /// <param name="sizeString">size字符串</param>
        /// <returns>返回转换后的结果，转换失败则返回Null</returns>
        public static Size? ToSize(this string sizeString)
        {
            if (string.IsNullOrWhiteSpace(sizeString))
                return null;
            if (sizeString.IndexOf(",") == -1)
                return null;
            string _w = NgNet.Text.RegexHelper.GetTxtFigures(sizeString.Substring(0, sizeString.IndexOf(",")));
            string _h = NgNet.Text.RegexHelper.GetTxtFigures(sizeString.Substring(sizeString.IndexOf(",")));

            if (string.IsNullOrWhiteSpace(_w) || string.IsNullOrWhiteSpace(_h))
                return null;
            return new Size(System.Convert.ToInt32(_w), System.Convert.ToInt32(_h));
        }
        #endregion

        #region Color
        /// <summary>
        /// 将颜色名字转换为颜色
        /// </summary>
        /// <param name="colorString">颜色的名字</param>
        /// <param name="returnValue">未转换成功则返回</param>
        /// <returns></returns>
        public static Color ToColor(string colorString, Color returnValue)
        {
            if (string.IsNullOrWhiteSpace(colorString))
                return returnValue;
            try
            {
                return ColorTranslator.FromHtml(colorString);
            }
            catch
            {
                return returnValue;
            }
        }
        #endregion

        #region FontStyle
        /// <summary>
        /// 将指定形式的字符串转换为字体样式
        /// </summary>
        /// <param name="fontStyleString">"Regular,Blod" 或 "0" 或 "3"</param>
        /// <param name="returnValue">为转换成功则返回该值</param>
        /// <returns>放回字体样式 ，若不符合规则，则默认返回  “FontStyle.Regular”</returns>
        public static FontStyle ToFontStyle(string fontStyleString, System.Drawing.FontStyle returnValue)
        {
            if (string.IsNullOrWhiteSpace(fontStyleString))
            {
                return returnValue;
            }
            return (FontStyle)Enum.Parse(typeof(FontStyle), fontStyleString);
        }
        #endregion

        #region object
        /// <summary>
        /// 返回对象obj的String值,obj为null时返回空值。
        /// </summary>
        /// <param name="obj">对象。</param>
        /// <returns>字符串。</returns>
        public static string ToStringOrDefault(object obj)
        {
            return obj?.ToString();
        }
        #endregion

        #region Number
        /// <summary>
        /// 将字符串转换为数值(byte)类型。
        /// </summary>
        /// <param name="byteString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static byte ToByte(this string byteString, byte returnValue)
        {
            try
            {
                return byte.Parse(byteString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Int32)类型。
        /// </summary>
        /// <param name="intString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static int ToInt(this string intString, int returnValue)
        {
            try
            {
                return int.Parse(intString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Long)类型。
        /// </summary>
        /// <param name="longString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Long数值。</returns>
        public static long ToLong(this string longString, long returnValue)
        {
            try
            {
                return long.Parse(longString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Decimal)类型。
        /// </summary>
        /// <param name="decimalString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Decimal数值。</returns>
        public static decimal ToDecimal(this string decimalString, decimal returnValue)
        {
            try
            {
                return decimal.Parse(decimalString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Double)类型。
        /// </summary>
        /// <param name="doubleString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Double数值。</returns>
        public static double ToDouble(this string doubleString, double returnValue)
        {
            try
            {
                return double.Parse(doubleString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Float)类型。
        /// </summary>
        /// <param name="floatString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Float数值。</returns>
        public static float ToFloat(this string floatString, float returnValue)
        {
            try
            {
                return float.Parse(floatString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(sbyte)类型。
        /// </summary>
        /// <param name="sbyteString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static sbyte ToSByte(this string sbyteString, sbyte returnValue)
        {
            try
            {
                return sbyte.Parse(sbyteString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(UInt16)类型。
        /// </summary>
        /// <param name="uint16String">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static ushort ToUShort(this string uint16String, ushort returnValue)
        {
            try
            {
                return UInt16.Parse(uint16String);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(UInt32)类型。
        /// </summary>
        /// <param name="uintString">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static uint ToUInt(this string uintString, uint returnValue)
        {
            try
            {
                return uint.Parse(uintString);
            }
            catch
            { return returnValue; }
        }
        /// <summary>
        /// 将字符串转换为数值(Int32)类型。
        /// </summary>
        /// <param name="uint64String">对象。</param>
        /// <param name="returnValue">转换失败返回该值。</param>
        /// <returns>Int32数值。</returns>
        public static ulong ToULong(this string uint64String, ulong returnValue)
        {
            try
            {
                return UInt64.Parse(uint64String);
            }
            catch
            { return returnValue; }
        }
        #endregion
    }
}

