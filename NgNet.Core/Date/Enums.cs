
/*----------------------------------------------------------------  
// 版权所有：Yong(i.yong@outlook.com)   
//  
// 文件名： Date.cs
// 文件功能描述：时间日期处理
//  
// 创建标识：  
// 创建描述：  
//  
// 修改标识：  
// 修改描述：  
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Date
{
    public enum Month : int
    {
        January = 1
        ,
        February = 2
        ,
        March = 3
        ,
        April = 4
        ,
        May = 5
        ,
        June = 6
        ,
        July = 7
        ,
        August = 8
        ,
        September = 9
        ,
        October = 10
        ,
        November = 11
        ,
        December = 12
    }

    public enum DateUnit:int
    {
        Century = 1,
        Decade = 2,
        Year = 3,
        Month = 4,
        Day = 5,
    }

    public enum TimeUnit : int
    {
        Hour = 2,
        Minute = 3,
        Second = 4,
        Millisecond = 5
    }
}
