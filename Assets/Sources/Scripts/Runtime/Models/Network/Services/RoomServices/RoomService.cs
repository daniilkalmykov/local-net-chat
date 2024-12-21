using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;

[assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace Sources.Scripts.Runtime.Models.Network.Services.RoomServices
{
    internal sealed class RoomService : BaseService, IRoomService
    {
        private readonly IPlayer _player;

        public RoomService(UdpClient udpClient, IPEndPoint endPoint, IPlayer player) : base(udpClient, endPoint)
        {
            _player = player;
        }

        public bool JoinRoom(IRoom room)
        {
            var modelToSend = new ModelToSend<RoomModelToSend>(Command.JoinRoom, new RoomModelToSend(room, _player.Id));

            return Send(modelToSend);
        }

        public bool CreateRoom(IRoom room)
        {
            var modelToSend =
                new ModelToSend<RoomModelToSend>(Command.CreateRoom, new RoomModelToSend(room, _player.Id));
            
            return Send(modelToSend);
        }

        public bool LeaveRoom(IRoom room)
        {
            var modelToSend = new ModelToSend<RoomModelToSend>(Command.LeftRoom, new RoomModelToSend(room, _player.Id));

            return Send(modelToSend);
        }
    }
}