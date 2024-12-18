using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Network.Services.RoomServices
{
    public interface IRoomService
    {
        bool JoinRoom(IRoom room);
        bool CreateRoom(IRoom room);
        bool LeaveRoom(IRoom room);
    }
}