using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    abstract class TestBase
    {
        protected void WriteLine(object obj)
        {
            Console.WriteLine(obj);
        }

        protected void WriteLine(string head, string value)
        {
            Write(head);
            WriteLine(value);
        }

        protected void Write(object obj)
        {
            Console.Write(obj);
        }

        protected void WriteDiv()
        {
            WriteLine(new string('*', 20));
        }

        public abstract void Test();
    }
}
