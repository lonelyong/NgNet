using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class TestNgNetUi : TestBase
    {
        public override void Test()
        {
            var tb = new NgNet.UI.Forms.OpenFileDialog();
            tb.Enterpath = @"C:\Users\jiu-z\Documents\Yong\TextEncrypt\Des";
            tb.Show();
        }
    }
}
