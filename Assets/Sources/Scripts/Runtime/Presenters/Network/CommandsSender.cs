using System;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Presenters.Network
{
    internal sealed class CommandsSender : ICommandsSender, IDisposable
    {
        private readonly IRoomService _roomService;
        private readonly IPlayer _player;

        public CommandsSender(IRoomService roomService, IPlayer player)
        {
            _roomService = roomService;
            _player = player;
            
            _player.JoinedRoom += OnJoinedRoom;
        }

        public void Dispose()
        {
            _player.JoinedRoom -= OnJoinedRoom;
        }
        
        private void OnJoinedRoom(IRoom room)
        {
            _roomService.JoinRoom(room);
        }
    }
}