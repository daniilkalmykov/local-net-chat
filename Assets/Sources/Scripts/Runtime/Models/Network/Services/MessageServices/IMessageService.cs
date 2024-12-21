using Sources.Scripts.Runtime.Models.Messages;

namespace Sources.Scripts.Runtime.Models.Network.Services.MessageServices
{
    public interface IMessageService
    {
        bool SentMessage(IMessage message);
    }
}