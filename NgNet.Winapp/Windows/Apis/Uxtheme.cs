using System;
using System.Runtime.InteropServices;

namespace NgNet.Windows.Apis
{
    public class Uxtheme
    {
        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int DrawThemeBackground(IntPtr hTheme, IntPtr hdc, int partId, int stateId, [In]System.Drawing.Rectangle pRect, [In] System.Drawing.Rectangle pClipRect);


        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr OpenThemeData(IntPtr hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszClassList);

        [DllImport("uxtheme.dll", CharSet = CharSet.Auto)]
        public static extern int CloseThemeData(IntPtr hTheme);
    }
}
