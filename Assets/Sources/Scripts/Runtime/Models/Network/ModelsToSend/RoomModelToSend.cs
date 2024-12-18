using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Network.ModelsToSend
{
    public sealed class RoomModelToSend
    {
        public RoomModelToSend(IRoom room, string playerId)
        {
            Room = room;
            PlayerId = playerId;
        }

        public IRoom Room { get; private set; }
        public string PlayerId { get; private set; }
    }
}