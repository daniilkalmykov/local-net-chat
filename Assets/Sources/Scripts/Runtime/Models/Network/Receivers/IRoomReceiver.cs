using Sources.Scripts.Runtime.Models.Network.ModelsToSend;

namespace Sources.Scripts.Runtime.Models.Network.Receivers
{
    public interface IRoomReceiver
    {
        bool JoinRoom(ModelToSend<RoomModelToSend> modelToSend);
        bool LeaveRoom(ModelToSend<RoomModelToSend> modelToSend);
        bool CreateRoom(ModelToSend<RoomModelToSend> modelToSend);
    }
}