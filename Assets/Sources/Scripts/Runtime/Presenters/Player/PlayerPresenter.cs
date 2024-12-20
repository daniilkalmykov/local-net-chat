using System;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Player;

namespace Sources.Scripts.Runtime.Presenters.Player
{
    public sealed class PlayerPresenter : IPlayerPresenter
    {
        //TODO: Update Ui after every subscription to event
        
        private readonly IPlayer _player;
        private readonly ILobby _lobby;

        public PlayerPresenter(IPlayer player, ILobby lobby)
        {
            _player = player;
            _lobby = lobby;
        }

        public void CreateRoom(string name)
        {
            _player.CreateRoom(name);
        }

        public void JoinRoom(string id)
        {
            var roomById = _lobby.GetRoomById(id);

            if (roomById == null)
                throw new Exception($"Couldn't get room by id. Id = {id}");
            
            _player.JoinRoom(roomById);
        }

        public void LeaveRoom()
        {
            _player.LeaveRoom();
        }

        public void SendMessage(string body)
        {
            _player.SendMessage(body);
        }
    }
}