using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace NgNet
{
#if DEBUG
    public class TestClass
    {
        public static string Test()
        {
            return Assembly.GetEntryAssembly().GetName().Name + "\n" 
                + Assembly.GetCallingAssembly().GetName().Name + "\n" 
                + Assembly.GetExecutingAssembly().GetName().Name;
        }
    }
#endif
}
