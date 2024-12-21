using System.Runtime.CompilerServices;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Player;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Sources.Scripts.Runtime.Presenters.Player
{
    internal sealed class PlayerPresenter : IPlayerPresenter
    {
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
            {
                Debug.LogError($"Couldn't get room by id. Id = {id}");
                return;
            }
            
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