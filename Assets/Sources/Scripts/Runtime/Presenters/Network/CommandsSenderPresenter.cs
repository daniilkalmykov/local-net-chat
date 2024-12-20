using System;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Presenters.Network
{
    internal sealed class CommandsSenderPresenter : ICommandsSenderPresenter, IDisposable
    {
        private readonly IRoomService _roomService;
        private readonly IPlayer _player;

        public CommandsSenderPresenter(IRoomService roomService, IPlayer player)
        {
            _roomService = roomService;
            _player = player;

            _player.JoinedRoom += OnJoinedRoom;
            _player.RoomCreated += OnRoomCreated;
        }

        public void Dispose()
        {
            _player.JoinedRoom -= OnJoinedRoom;
            _player.RoomCreated -= OnRoomCreated;
        }
        
        private void OnJoinedRoom(IRoom room)
        {
            if (_roomService.JoinRoom(room) == false)
                _player.LeaveRoom();
        }
        
        private void OnRoomCreated(IRoom room)
        {
            if (_roomService.CreateRoom(room) == false)
                _player.LeaveRoom();
        }
    }
}