using System.Net;
using System.Net.Sockets;

namespace Sources.Scripts.Runtime.Core.Utils
{
    public static class NetworkHelper
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            
            return "127.0.0.1";
        }
    }
}