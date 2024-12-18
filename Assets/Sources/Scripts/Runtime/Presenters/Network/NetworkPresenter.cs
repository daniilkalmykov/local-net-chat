using System.Net;
using System.Net.Sockets;

namespace Sources.Scripts.Runtime.Presenters.Network
{
    public abstract class NetworkPresenter
    {
        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;
    }
}