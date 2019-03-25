using System.Net;
namespace NgNet.Net
{
    public class IpHelper
    {
        #region public methods
        /// <summary>
        /// 获取当前主机的内网Ipv4地址
        /// </summary>
        /// <returns></returns>
        public static IPAddress GetInnerIpv4()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var item in hostEntry.AddressList)
            {
                if(item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return item;
                }
            }
            return null;
        }
        #endregion
    }
}
