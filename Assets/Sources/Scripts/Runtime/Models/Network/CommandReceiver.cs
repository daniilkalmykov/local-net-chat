using System;
using System.Net;
using System.Net.Sockets;

namespace Sources.Scripts.Runtime.Models.Network
{
    internal sealed class CommandReceiver : ICommandsReceiver, IDisposable
    {
        private readonly UdpClient _udpClient;

        public CommandReceiver(UdpClient udpClient)
        {
            _udpClient = udpClient;
        }

        public event Action<byte[]> Received;
        
        public void Start()
        {
            _udpClient.BeginReceive(ReceiveCallback, null);
        }

        public void Stop()
        {
            _udpClient.Close();
        }

        public void Dispose()
        {
            Stop();
        }
        
        private void ReceiveCallback(IAsyncResult result)
        {
            try
            {
                var senderEndPoint = new IPEndPoint(IPAddress.Any, 0);
                var receivedData = _udpClient.EndReceive(result, ref senderEndPoint);

                Received?.Invoke(receivedData);

                _udpClient.BeginReceive(ReceiveCallback, null);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}