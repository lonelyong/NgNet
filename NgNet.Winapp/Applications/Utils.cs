using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet.Applications
{
    public static class Utils
    {
        public static void AutoRun(string title, string path, string cmd)
        {
            //在未加入自启动时取消自启动
            RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
            runKey.SetValue(title, $"\"{path}\" {cmd}");
            runKey.Close();
        }

        public static void CancelAutoRun(string title)
        {
            if (IsAutoRun(title))
            {
                RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                runKey.DeleteValue(title);
                runKey.Close();
            }
        }

        public static bool IsAutoRun(string title)
        {
            RegistryKey runKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run");
            string[] runList = runKey.GetValueNames();
            runKey.Close();
            foreach (string item in runList)
            {
                if (item.Equals(title))
                    return true;
            }
            return false;
        }
    }
}
