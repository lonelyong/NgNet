using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace NgNet.Hardware
{
    public static class GetInfo
    {
        #region private fields
        private static string _cpuSn;
        #endregion

        #region public proeprties
        public static string CpuSn { get
            {
                if (_cpuSn == null)
                    return GetProcessorSerialNumber();
                else
                    return _cpuSn;
            } }
        #endregion
        /// <summary>
        /// 获取cpu序列号
        /// </summary>
        /// <returns></returns>
        public static string GetProcessorSerialNumber()
        {
            ManagementClass myCpu = new ManagementClass("win32_Processor");
            ManagementObjectCollection myCpuConnection = myCpu.GetInstances();
            foreach (ManagementObject myObject in myCpuConnection)
            {
                _cpuSn= myObject.Properties["Processorid"].Value.ToString();
                break;
            }
            return _cpuSn;
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public static string GetDiskSerialNumber(string volume)
        {
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObject disk = new ManagementObject($"win32_logicaldisk.deviceid=\"{volume}:\"");
            disk.Get();
            return disk.GetPropertyValue("VolumeSerialNumber").ToString();
        }
    }
}