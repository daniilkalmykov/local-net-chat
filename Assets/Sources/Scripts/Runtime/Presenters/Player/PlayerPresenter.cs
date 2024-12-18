using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Presenters.Player
{
    public sealed class PlayerPresenter : IPlayerPresenter
    {
        //TODO: Update Ui after every subscription to event
        
        private readonly IPlayer _player;

        public PlayerPresenter(IPlayer player)
        {
            _player = player;
        }

        public void CreateRoom(string name)
        {
            _player.CreateRoom(name);
        }

        public void JoinRoom(IRoom room)
        {
            _player.JoinRoom(room);
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