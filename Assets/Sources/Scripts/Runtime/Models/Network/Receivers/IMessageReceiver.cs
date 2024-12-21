using Sources.Scripts.Runtime.Models.Messages;

namespace Sources.Scripts.Runtime.Models.Network.Receivers
{
    public interface IMessageReceiver
    {
        bool SendMessage(IMessage message);
    }
}