using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet
{
    public class Test
    {
        /// <summary>
        /// 判断指定的字符串是不是int32类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsInt32(string str)
        {
            try
            {
                int.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// 判断指定的字符串是不是float类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsFloat(string str)
        {
            try
            {
                float.Parse(str); 
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
