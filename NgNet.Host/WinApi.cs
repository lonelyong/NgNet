using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Host
{
    class WinApi
    {
        [DllImport("user32.dll", EntryPoint = "ShowWindow", SetLastError = true)]
        internal static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
    }
}
