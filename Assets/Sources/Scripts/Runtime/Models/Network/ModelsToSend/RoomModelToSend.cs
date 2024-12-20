using JetBrains.Annotations;
using Sources.Scripts.Runtime.Models.Rooms;
using Newtonsoft.Json;

namespace Sources.Scripts.Runtime.Models.Network.ModelsToSend
{
    public sealed class RoomModelToSend
    {
        public RoomModelToSend(IRoom room, string playerId)
        {
            PlayerId = playerId;
            Id = room.Id;
            Name = room.Name;
            Owner = room.Owner;
            Id = room.Id;
            Participants = room.Participants;
        }

        [JsonConstructor, UsedImplicitly]
        public RoomModelToSend(string id, string name, string owner, int participants, string playerId)
        {
            Id = id;
            Name = name;
            Owner = owner;
            Participants = participants;
            PlayerId = playerId;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Owner { get; private set; }
        public int Participants { get; private set; }
        public string PlayerId { get; private set; }
    }
}