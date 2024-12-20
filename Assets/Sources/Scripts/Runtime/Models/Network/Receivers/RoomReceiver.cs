using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Sources.Scripts.Runtime.Models.Player;
using UnityEngine;

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

            if (modelToSend.Value.Id != _player.CurrentRoom?.Id)
                return false;

            Debug.LogError($"Notification: Player {_player} joined room");

            return true;
        }

        public bool LeaveRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (modelToSend.Value.PlayerId == _player.Id)
                return false;
            
            if (modelToSend.Value.Id != _player.CurrentRoom.Id)
                return false;

            Debug.LogError($"Notification: Player {_player} left room");
            
            return true;
        }

        public bool CreateRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (modelToSend.Value.PlayerId == _player.Id)
                return false;

            Debug.LogError($"Room {modelToSend.Value.Name} has created");
            
            return true;
        }
    }
}