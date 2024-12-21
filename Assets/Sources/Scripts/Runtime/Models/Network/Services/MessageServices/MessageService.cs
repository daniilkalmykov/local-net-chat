using System.Net;
using System.Net.Sockets;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Network.ModelsToSend;

namespace Sources.Scripts.Runtime.Models.Network.Services.MessageServices
{
    internal sealed class MessageService : BaseService, IMessageService
    {
        public MessageService(UdpClient udpClient, IPEndPoint endPoint) : base(udpClient, endPoint)
        {
        }

        public bool SentMessage(IMessage message)
        {
            var modelToSend = new ModelToSend<IMessage>(Command.SendMessage, message);

            return Send(modelToSend);
        }
    }
}