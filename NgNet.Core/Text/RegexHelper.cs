using System;
using System.Text;
using System.Text.RegularExpressions;

namespace NgNet.Text
{
    public static class RegexHelper
    {
        public const string PatternChineseHandphone = @"^1[3,5,7,8]\d{9}$|^\+861[3,5,7,8]\d{9}$";
        public const string PatternChinesePhone = @"^(\d{3,4}-)?\d{6,8}$";
        public const string PattternBase64 = @"^[A-Z0-9/+=]*$";
        public const string PatternInt = @"^[0-9]+[0-9]*$";
        public const string PatternDouble = @"^[0-9]+[0-9]*[.]?[0-9]*$";
        public const string PatternIdCard = @"^(11|12|13|14|15|21|22|23|31|32|33|34|35|36|37|41|42|43|44|45|46|50|51|52|53|54|61|62|63|64|65|71|81|82|91)(\d{13}|\d{15}[\dx])$";
        public const string PatternEmail = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
        public const string PatternIpv4 = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";
        public const string PatternDate = @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-9]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$";
        public const string PatternTime = @"^((20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$";
        public const string PatternDateTime = @"^(((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-)) (20|21|22|23|[0-1]?\d):[0-5]?\d:[0-5]?\d)$ ";
        public const string PatternIpv6 = "";
        /// <summary>
        /// 是否是Base64字符串
        /// </summary>
        /// <param name="eStr"></param>
        /// <returns></returns>
        public static bool IsBase64(string eStr)
        {
            if (string.IsNullOrWhiteSpace(eStr))
                return false;
            if ((eStr.Length % 4) != 0)
                return false;
            if (Regex.IsMatch(eStr, PattternBase64, RegexOptions.IgnoreCase) == false)
                return false;
            return true;
        }
        /// <summary>
        /// 判断字符串是不是由字母和数字组成
        /// </summary>
        /// <param name="inputStr"></param>
        /// <returns></returns>
        public static bool IsLettersOrFigures(string inputStr)
        {
            if (string.IsNullOrWhiteSpace(inputStr))
                return false;
            if (Regex.IsMatch(inputStr, "^[A-Z0-9]*$", RegexOptions.IgnoreCase))
                return true;
            return false;
        }
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsDouble(string number)
        {
            //如果为空，认为验证不合格
            if (String.IsNullOrEmpty(number))
                return false;

            //清除要验证字符串中的空格
            number = number.Trim();

            //验证
            return RegexHelper.IsMatch(number,PatternDouble, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options = RegexOptions.IgnoreCase)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(input, pattern, options);
        }
        /// <summary>
        /// 验证身份证是否合法
        /// </summary>
        /// <param name="idCard">要验证的身份证</param>        
        public static bool IsIdCard(string idCard)
        {
            //如果为空，认为验证合格
            if (String.IsNullOrEmpty(idCard))
                return false;

            //清除要验证字符串中的空格
            idCard = idCard.Trim();

            //验证
            return RegexHelper.IsMatch(idCard, PatternIdCard, RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>        
        public static bool IsInt(string number)
        {
            //如果为空，认为验证不合格
            if (string.IsNullOrEmpty(number))
                return false;

            //清除要验证字符串中的空格
            number = number.Trim();

            //验证
            return IsMatch(number, PatternInt);
        }
        /// <summary>
        /// 验证EMail是否合法
        /// </summary>
        /// <param name="email">要验证的Email</param>
        public static bool IsEmail(string email)
        {
            //如果为空，认为验证不合格
            if (string.IsNullOrEmpty(email))
                return false;

            //清除要验证字符串中的空格
            email = email.Trim();
         
            //验证
            return IsMatch(email, PatternEmail);
        }
        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>        
        public static bool IsIP(string ip)
        {
            //如果为空，认为验证合格
            if (string.IsNullOrEmpty(ip))
                return false;

            //清除要验证字符串中的空格
            ip = ip.Trim();

            //验证
            return IsMatch(ip, PatternIpv4);
        }
        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] patternAry ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = patternAry[0];
            string strOutput = strHtml;

            foreach (var item in patternAry)
            {
                Regex regex = new Regex(item, RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }
            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        /// <summary>
        /// 返回字符串中的数字
        /// </summary>
        /// <param name="text">输入包含数字的字符串</param>
        /// <returns>返回按顺序排列的数字字符串</returns>
        public static string GetTxtFigures(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return null;
            else
            {
                Regex rg = new Regex(@"\D");
                return rg.Replace(text, string.Empty);
            }
        }
        /// <summary>
        ///  判断指定的字符串是不是合法的日期
        /// </summary>
        /// <param name="dateString">字符串日期</param>
        /// <returns></returns>
        public static bool IsDateString(string dateString)
        {
            return IsMatch(dateString, PatternDate);
        }
        /// <summary>
        /// 判断指定的字符串是不是合法的事件==时间
        /// </summary>
        /// <param name="timeString">字符串时间</param>
        /// <returns></returns>
        public static bool IsTimeString(string timeString)
        {
            return IsMatch(timeString, PatternTime);
        }
        /// <summary>
        /// 判断指定的日期只不是合法的日期时间
        /// </summary>
        /// <param name="datetimeString">日期时间字符串</param>
        /// <returns></returns>
        public static bool IsDateTimeString(string datetimeString)
        {
            return IsMatch(datetimeString, PatternDateTime);
        }
        /// <summary>
        /// 获取一个值，该值指示指定的字符串是不是中国手机号
        /// </summary>
        /// <param name="handphoneNumber">1[3,5,7,8]000000000</param>
        /// <returns></returns>
        public static bool IsChineseHandphone(string handphoneNumber)
        {
            return IsMatch(handphoneNumber, PatternChineseHandphone);
        }

        /// <summary>
        /// 获取一个值，该值指示指定的字符串是不是中国电话号
        /// </summary>
        /// <param name="phoneNumber">(3-4位)-(6-8位)</param>
        /// <returns></returns>
        public static bool IsChinesePhone(string phoneNumber)
        {
            return IsMatch(phoneNumber, PatternChinesePhone);
        }
    }
}
