using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.UI
{
#if DEBUG
    public class TestClass
    {
        public static string Test()
        {
            return NgNet.TestClass.Test();
        }
    }
#endif
}
