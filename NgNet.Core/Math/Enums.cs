
/*----------------------------------------------------------------  
// 版权所有：Yong(i.yong@outlook.com)   
//  
// 文件名： Math.cs
// 文件功能描述：程序主窗体
//  
// 创建标识：  
// 创建描述：  
//  
// 修改标识：  
// 修改描述：  
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.Math
{
    /// <summary>
    /// EnumFormula
    /// </summary>
    public enum EnumFormula
    {
        Add,//加号
        Dec,//减号
        Mul,//乘号
        Div,//除号
        Sin,//正玄
        Cos,//余玄
        Tan,//正切
        ATan,//余切
        Sqrt,//平方根
        Pow,//求幂
        None,//无
    }

    public enum BODH : int
    {
        /// <summary>
        /// 二进制
        /// </summary>
        [Description("二进制")]
        Binary =2,
        /// <summary>
        /// 八进制
        /// </summary>
        [Description("八进制")]
        Octonary = 8,
        /// <summary>
        /// 十进制
        /// </summary>
        [Description("十进制")]
        Decimal = 10,
        /// <summary>
        /// 十六进制 
        /// </summary>
        [Description("十六进制")]
        Hexadecimal = 16
    }
}
