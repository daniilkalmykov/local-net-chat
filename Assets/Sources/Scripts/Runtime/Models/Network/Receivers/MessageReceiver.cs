using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Player;

namespace Sources.Scripts.Runtime.Models.Network.Receivers
{
    internal sealed class MessageReceiver : IMessageReceiver
    {
        private readonly IPlayer _player;

        public MessageReceiver(IPlayer player)
        {
            _player = player;
        }

        public bool SendMessage(IMessage message)
        {
            return message.RoomId == _player.CurrentRoom?.Id && message.Sender != _player.Id;
        }
    }
}