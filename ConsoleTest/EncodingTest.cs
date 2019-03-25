using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class EncodingTest : TestBase
    {
        public override void Test() 
        {
            string _s = "ABC中国农业银行";
            WriteDiv();
            Console.WriteLine(System.Text.Encoding.Default.EncodingName);
            Console.WriteLine(Encoding.Default.GetBytes(_s).Length);
            Console.WriteLine(System.Text.Encoding.ASCII.EncodingName);
            Console.WriteLine(Encoding.ASCII.GetBytes(_s).Length);
            Console.WriteLine(System.Text.Encoding.UTF7.EncodingName);
            Console.WriteLine(Encoding.UTF7.GetBytes(_s).Length);
            Console.WriteLine(System.Text.Encoding.UTF8.EncodingName);
            Console.WriteLine(Encoding.UTF8.GetBytes(_s).Length);
            Console.WriteLine(System.Text.Encoding.Unicode.EncodingName);
            Console.WriteLine(Encoding.Unicode.GetBytes(_s).Length);
            Console.WriteLine(System.Text.Encoding.UTF32.EncodingName);
            Console.WriteLine(Encoding.UTF32.GetBytes(_s).Length);
            WriteDiv();
        }
    }
}
