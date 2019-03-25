using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI.Forms
{
    /// <summary>
    /// 表示指示用户输入的内容类型
    /// </summary>
    public enum InputTypes : int
    {
        /// <summary>
        /// 纯文本
        /// </summary>
        [Description("纯文本")]
        Text = 0,
        /// <summary>
        /// 整数
        /// </summary>
        [Description("整数")]
        Int = 1,
        /// <summary>
        /// 正整数
        /// </summary>
        [Description("自然数")]
        UInt = 2,
        /// <summary>
        /// 数字
        /// </summary>
        [Description("数字")]
        Number = 4,
        /// <summary>
        /// 正数
        /// </summary>
        [Description("正数")]
        UNumber = 5,
        /// <summary>
        /// 英文字母
        /// </summary>
        [Description("英文字母")]
        English = 6,
        /// <summary>
        /// 数字或字母
        /// </summary>
        [Description("数字和英文字母")]
        NumberEnglish = 7,
        /// <summary>
        /// 中文字符
        /// </summary>
        [Description("中文文本")]
        Chinese = 8,
        /// <summary>
        /// 时间或日期
        /// </summary>
        [Description("时间与日期")]
        DateTime = 10,
        /// <summary>
        /// 不能输入空格
        /// </summary>
        [Description("无空格")]
        NoSpace = 12,
        /// <summary>
        /// 通常常设置密码的字符可以输入，如数字、字母、<>/?-_@%&*|!#$()\等ASC||字符
        /// </summary>
        [Description("密码")]
        Password = 14
    }
}
