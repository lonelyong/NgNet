using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using NgNet.Windows.Apis;

namespace NgNet.UI
{
    public static class ScreenHelper
    {
        public static class PrimaryScreen
        {
            static PrimaryScreen()
            {
                var hdc = User32.GetDC(IntPtr.Zero);
                DpiX = Gdi32.GetDeviceCaps(hdc, 88);
                DpiY = Gdi32.GetDeviceCaps(hdc, 90);
                int x = Gdi32.GetDeviceCaps(hdc, 118);
                int x1 = Gdi32.GetDeviceCaps(hdc, 8);
                int y = Gdi32.GetDeviceCaps(hdc, 117);
                int y1 = Gdi32.GetDeviceCaps(hdc, 10);
                ScaleX = x / (float)x1;
                ScaleY = y / (float)y1;
                var cmSize = GetMonitorPhysicalSize(GetMonitorPnpDeviceId().Last());
                MmX = (int)(cmSize.Width * 10);
                MmY = (int)(cmSize.Height * 10);
                PxX = x;
                PxY = y;
                Inch = MonitorScaler(cmSize);
                InchX = (float)(cmSize.Width / 30.48);
                InchY = (float)(cmSize.Height / 30.48);
                PhysicalDpi = (float)(System.Math.Sqrt(x * x + y * y) / Inch);
                User32.ReleaseDC(IntPtr.Zero, hdc);
            }
            /// <summary>
            /// 当前系统DPI_X 大小 一般为96
            /// </summary>
            public static int DpiX { get; }
            /// <summary>
            /// 当前系统DPI_Y 大小 一般为96
            /// </summary>
            public static int DpiY { get; }

            public static int PxX { get; }

            public static int PxY { get; }

            public static int MmX { get;}

            public static int MmY { get; }

            public static float InchX { get; }

            public static float InchY { get; }

            /// <summary>
            /// 获取屏幕大小
            /// </summary>
            public static float Inch { get; }

            /// <summary>
            /// 获取宽度缩放百分比
            /// </summary>
            public static float ScaleX
            {
                get;
            }

            /// <summary>
            /// 获取高度缩放百分比
            /// </summary>
            public static float ScaleY
            {
                get;
            }


            /// <summary>
            /// 获取屏幕物理dpi
            /// </summary>
            public static float PhysicalDpi { get; set; }

            public static IEnumerable<string> GetMonitorPnpDeviceId()
            {
                var ids = new List<string>();
                using (ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
                {
                    using (ManagementObjectCollection moc = mc.GetInstances())
                    {
                        foreach (var o in moc)
                        {
                            var each = (ManagementObject)o;
                            object obj = each.Properties["PNPDeviceID"].Value;
                            if (obj == null)
                                continue;
                            yield return each.Properties["PNPDeviceID"].Value.ToString();
                        }
                    }
                }
            }

            public static byte[] GetMonitorEdid(string monitorPnpDevId)
            {
                return (byte[])Registry.GetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\ControlSet001\Enum\" + monitorPnpDevId + @"\Device Parameters", "EDID", new byte[] { });
            }

            //获取显示器物理尺寸(cm)
            public static SizeF GetMonitorPhysicalSize(string monitorPnpDevId)
            {
                byte[] edid = GetMonitorEdid(monitorPnpDevId);
                if (edid.Length < 23)
                    return SizeF.Empty;

                return new SizeF(edid[21], edid[22]);
            }

            //通过屏显示器理尺寸转换为显示器大小(inch)
            public static float MonitorScaler(SizeF moniPhySize)
            {
                var s = System.Math.Sqrt(System.Math.Pow(moniPhySize.Width, 2) + System.Math.Pow(moniPhySize.Height, 2)) / 2.54d;
                return (float)System.Math.Round(s, 1);
            }
        }
    }
}
