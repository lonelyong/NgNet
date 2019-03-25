using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Net;
using System.Net.NetworkInformation;

namespace NgNet.Net
{
    /// <summary>
    /// 端口帮助类
    /// </summary>
    public class PortHelper
    {
        /// <summary>
        /// 获取正在使用的端口
        /// </summary>
        /// <returns></returns>
        public static IList GetUsedIPEndPoint()
        {
            //获取一个对象，该对象提供有关本地计算机的网络连接和通信统计数据的信息。  
            IPGlobalProperties ipGlobalProperties = IPGlobalProperties.GetIPGlobalProperties();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 侦听器的终结点信息。  
            IPEndPoint[] ipEndPointTCP = ipGlobalProperties.GetActiveTcpListeners();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPv4) 用户数据报协议 (UDP) 侦听器的信息。  
            IPEndPoint[] ipEndPointUDP = ipGlobalProperties.GetActiveUdpListeners();

            //获取有关本地计算机上的 Internet 协议版本 4 (IPV4) 传输控制协议 (TCP) 连接的信息。  
            TcpConnectionInformation[] tcpConnectionInformation = ipGlobalProperties.GetActiveTcpConnections();

            IList allIPEndPoint = new List<IPEndPoint>();
            foreach (IPEndPoint iep in ipEndPointTCP)
                allIPEndPoint.Add(iep);
            foreach (IPEndPoint iep in ipEndPointUDP)
                allIPEndPoint.Add(iep);
            foreach (TcpConnectionInformation tci in tcpConnectionInformation)
                allIPEndPoint.Add(tci.LocalEndPoint);

            return allIPEndPoint;
        }
        
        ///  
        /// 判断指定的网络端点（只判断端口）是否被使用  
        ///  
        public static bool IsUsedIPEndPoint(int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }

        ///  
        /// 判断指定的网络端点（判断IP和端口）是否被使用  
        ///  
        public static bool IsUsedIPEndPoint(string ip, int port)
        {
            foreach (IPEndPoint iep in GetUsedIPEndPoint())
            {
                if (iep.Address.ToString() == ip && iep.Port == port)
                {
                    return true;
                }
            }
            return false;
        }
        ///
        /// 返回可用端口号
        ///
        /// 端口开始数字
        ///
        public static int GetUnusedPort(int startPort)
        {
            while (IsUsedIPEndPoint(startPort))
            {
                startPort++;
            }
            return startPort;
        }
        /// <summary>
        /// 获取一个值，该值指示指定的端口号是不是有效的端口号
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        public static bool IsPort(int port)
        {
            return port >= 0 && port <= 65535;
        }
    }
}
