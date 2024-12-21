using System;
using Cysharp.Threading.Tasks;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Network.Services.MessageServices;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;
using Sources.Scripts.Runtime.Views.Messages;
using UnityEngine;

namespace Sources.Scripts.Runtime.Presenters.Network
{
    internal sealed class CommandsSenderPresenter : ICommandsSenderPresenter, IDisposable
    {
        private readonly IRoomService _roomService;
        private readonly IPlayer _player;
        private readonly IMessageService _messageService;
        private readonly IMonoBehaviourFactoryMethod _messageViewFactoryMethod;

        public CommandsSenderPresenter(IRoomService roomService, IPlayer player, IMessageService messageService,
            IMonoBehaviourFactoryMethod messageViewFactoryMethod)
        {
            _roomService = roomService;
            _player = player;
            _messageService = messageService;
            _messageViewFactoryMethod = messageViewFactoryMethod;

            _player.JoinedRoom += OnJoinedRoom;
            _player.RoomCreated += OnRoomCreated;
            _player.MessageSent += OnMessageSent;
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
            else
                AnnounceRoom(room).Forget();
        }

        private async void OnMessageSent(IMessage message)
        {
            if (_messageService.SentMessage(message) == false)
            {
                Debug.LogError("Failed sending message");
                return;
            }

            var messageView = await _messageViewFactoryMethod.CreateInMainThread<IMessageView>();

            messageView.Display(message.Body, message.Sender);
        }

        private async UniTask AnnounceRoom(IRoom room)
        {
            while (_player.CurrentRoom == room || _player.CurrentRoom != null)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(5));

                _roomService.CreateRoom(room);
            }
        }
    }
}