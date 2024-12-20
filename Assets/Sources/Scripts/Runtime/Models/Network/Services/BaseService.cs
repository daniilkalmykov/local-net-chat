using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Newtonsoft.Json;

namespace Sources.Scripts.Runtime.Models.Network.Services
{
    internal abstract class BaseService
    {
        private readonly UdpClient _udpClient;
        private readonly IPEndPoint _endPoint;

        protected BaseService(UdpClient udpClient, IPEndPoint endPoint)
        {
            _udpClient = udpClient;
            _endPoint = endPoint;
        }

        protected bool Send<T>(ModelToSend<T> modelToSend)
        {
            try
            {
                var json = JsonConvert.SerializeObject(modelToSend);
                var jsonToSend = new UTF8Encoding().GetBytes(json);

                _udpClient.Send(jsonToSend, jsonToSend.Length, _endPoint);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}