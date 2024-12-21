using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Sources.Scripts.Runtime.Models.Player;

namespace Sources.Scripts.Runtime.Models.Network.Receivers
{
    internal sealed class RoomReceiver : IRoomReceiver
    {
        private readonly IPlayer _player;

        public RoomReceiver(IPlayer player)
        {
            _player = player;
        }

        public bool JoinRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (modelToSend.Value.PlayerId == _player.Id)
                return false;

            return modelToSend.Value.Id == _player.CurrentRoom?.Id;
        }

        public bool LeaveRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (modelToSend.Value.PlayerId == _player.Id)
                return false;
            
            return modelToSend.Value.Id == _player.CurrentRoom.Id;
        }

        public bool CreateRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            return modelToSend.Value.PlayerId != _player.Id;
        }
    }
}