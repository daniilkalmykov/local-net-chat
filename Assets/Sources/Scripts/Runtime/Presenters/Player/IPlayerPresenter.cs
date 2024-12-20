using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Presenters.Player
{
    public interface IPlayerPresenter
    {
        void CreateRoom(string name);
        void JoinRoom(string id);
        void LeaveRoom();
        void SendMessage(string body);
    }
}