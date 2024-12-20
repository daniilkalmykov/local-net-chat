using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Player;

namespace Sources.Scripts.Runtime.Views.Rooms
{
    public interface IRoomView : IView
    {
        void Display(string roomName);
        void Init(string id, IPlayer player, ILobby lobby);
    }
}