using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class TypeConvertTest : TestBase
    {
        enum ENUM
        {
            Two = 2
        }
        public override void Test()
        {
            string _nstring = "2",_estring="Two", _bstring = "false", _dstring="2017/1/1 12:0:1";
            WriteLine(NgNet.TypeHelper.ChangeType(_nstring, typeof(decimal)));
            WriteLine(NgNet.TypeHelper.ChangeType(ENUM.Two, typeof(ENUM)));
            WriteLine(NgNet.TypeHelper.ChangeType(_bstring, typeof(bool)));
            WriteLine(NgNet.TypeHelper.ChangeType(_dstring, typeof(DateTime)));
        }
    }
}
