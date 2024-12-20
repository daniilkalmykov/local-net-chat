using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Lobby
{
    public interface ILobby
    {
        IRoom GetRoomById(string id);
        void AddRoom(IRoom room);
        void DeleteRoom(IRoom room);
    }
}