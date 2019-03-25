using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NgNet.UI
{
    public struct BorderSize
    {
        public int Left { get; set; }

        public int Top { get; set; }

        public int Right { get; set; }

        public int Bottom { get; set; }

        public BorderSize(int wh)
        {
            Left = wh;
            Top = wh;
            Right = wh;
            Bottom = wh;
        }

        public BorderSize(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static explicit operator BorderSize(Padding padding)
        {
            return new BorderSize(padding.Left, padding.Top, padding.Right, padding.Bottom);
        }

        public Padding ToPadding()
        {
            return new Padding(Left, Top, Right, Bottom);
        }
    }
}
