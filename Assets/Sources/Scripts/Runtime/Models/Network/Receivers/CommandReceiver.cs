using System;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace Sources.Scripts.Runtime.Models.Network.Receivers
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
                
                _udpClient.BeginReceive(ReceiveCallback, null);
                
                Received?.Invoke(receivedData);
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }
    }
}