using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class SortTest : TestBase
    {
        private static int[] _ints = new int[10];

        static SortTest()
        {
            Random _r = new Random();
            for (int i = 0; i < _ints.Length; i++)
            {
                _ints[i] = _r.Next(1, _ints.Length);
            }
        }

        public void SelectionSort()
        {
            int _int = 0;
            int[] _rst = new int[_ints.Length];
            _ints.CopyTo(_rst,0);
            for (int i = 0; i < _rst.Length - 1; i++)
            {
                for (int j = i + 1; j < _rst.Length; j++)
                {
                    if(_rst[i] > _rst[j])
                    {
                        _int = _rst[i];
                        _rst[i] = _rst[j];
                        _rst[j] = _int;
                    }
                }
            }
            WriteLine("选择排序：", NgNet.Collections.TCollection<int>.ToString(_rst, ", "));
        }

        public void BubbleSort()
        {
            int _int = 0;
            int[] _rst = new int[_ints.Length];
            _ints.CopyTo(_rst, 0);
            for (int i = 0; i < _rst.Length - 1; i++)
            {
                for (int j = 0; j < _rst.Length - 1 - i; j++)
                {
                    if(_rst[j] > _rst[j + 1])
                    {
                        _int = _rst[j + 1];
                        _rst[j + 1] = _rst[j];
                        _rst[j] = _int;
                    }
                }
            }
            WriteLine("冒泡排序：", NgNet.Collections.TCollection<int>.ToString(_rst, ", "));
        }

        public override void Test()
        {
            WriteLine(null);
            WriteDiv();
            WriteLine("原始数据：", NgNet.Collections.TCollection<int>.ToString(_ints, ", "));
            SelectionSort();
            BubbleSort();
            WriteDiv();
        }
    }
}
