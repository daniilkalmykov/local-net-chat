using System;
using System.Runtime.CompilerServices;
using System.Text;
using Cysharp.Threading.Tasks;
using Sources.Scripts.Runtime.Models.Network;
using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Sources.Scripts.Runtime.Models.Network.Receivers;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Messages;
using Sources.Scripts.Runtime.Views.Notifications;
using Sources.Scripts.Runtime.Views.Rooms;

[assembly: InternalsVisibleTo("Assembly-CSharp")]

namespace Sources.Scripts.Runtime.Presenters.Network
{
    internal sealed class CommandsReceiverPresenter : ICommandsReceiverPresenter
    {
        private readonly ICommandsReceiver _commandsReceiver;
        private readonly IRoomService _roomService;
        private readonly IRoomReceiver _roomReceiver;
        private readonly IRoomFactoryMethod _roomFactoryMethod;
        private readonly IMonoBehaviourFactoryMethod _monoBehaviourFactoryMethod;
        private readonly IMonoBehaviourFactoryMethod _messageViewFactoryMethod;
        private readonly ILobby _lobby;
        private readonly INotificationView _notificationView;
        private readonly IMessageReceiver _messageReceiver;
        private readonly IPlayerPresenter _playerPresenter;

        public CommandsReceiverPresenter(ICommandsReceiver commandsReceiver, IRoomReceiver roomReceiver,
            IRoomFactoryMethod roomFactoryMethod, IMonoBehaviourFactoryMethod monoBehaviourFactoryMethod, ILobby lobby,
            INotificationView notificationView, IMessageReceiver messageReceiver,
            IMonoBehaviourFactoryMethod messageViewFactoryMethod, IPlayerPresenter playerPresenter)
        {
            _commandsReceiver = commandsReceiver;
            _roomReceiver = roomReceiver;
            _roomFactoryMethod = roomFactoryMethod;
            _monoBehaviourFactoryMethod = monoBehaviourFactoryMethod;
            _lobby = lobby;
            _notificationView = notificationView;
            _messageReceiver = messageReceiver;
            _messageViewFactoryMethod = messageViewFactoryMethod;
            _playerPresenter = playerPresenter;
        }

        public void Start()
        {
            _commandsReceiver.Received += OnReceived;
            _commandsReceiver.Start();
        }

        public void End()
        {
            _commandsReceiver.Received -= OnReceived;
            _commandsReceiver.Stop();
        }

        private async void OnReceived(byte[] data)
        {
            {
                var json = Encoding.UTF8.GetString(data);
                var command = JObject.Parse(json).Value<int>(nameof(Command));

                switch (command)
                {
                    case (int)Command.JoinRoom:
                    {
                        var modelToSend = JsonConvert.DeserializeObject<ModelToSend<RoomModelToSend>>(json);
                        await JoinRoom(modelToSend);
                        break;
                    }
                    case (int)Command.LeftRoom:
                    {
                        var modelToSend = JsonConvert.DeserializeObject<ModelToSend<RoomModelToSend>>(json);
                        await LeaveRoom(modelToSend);
                        break;
                    }
                    case (int)Command.SendMessage:
                    {
                        var modelToSend = JsonConvert.DeserializeObject<ModelToSend<Message>>(json);
                        await SendMessage(modelToSend.Value);
                        break;
                    }
                    case (int)Command.CreateRoom:
                    {
                        var modelToSend = JsonConvert.DeserializeObject<ModelToSend<RoomModelToSend>>(json);
                        await CreateRoom(modelToSend);
                        break;
                    }
                }
            }
        }

        private async UniTask SendMessage(IMessage message)
        {
            if (_messageReceiver.SendMessage(message) == false)
                return;

            var messageView = await _messageViewFactoryMethod.CreateInMainThread<IMessageView>();

            messageView.Display(message.Body, message.Sender);
        }

        private async UniTask CreateRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (_lobby.GetRoomById(modelToSend.Value.Id) != null)
                return;

            if (_roomReceiver.CreateRoom(modelToSend) == false)
                return;

            var room = _roomFactoryMethod.Create(modelToSend.Value.Name, modelToSend.Value.Owner, modelToSend.Value.Id);
            var roomView = await _monoBehaviourFactoryMethod.CreateInMainThread<IRoomView>();

            _lobby.AddRoom(room);

            if (roomView == null)
                return;

            roomView.Init(room.Id, _playerPresenter);
            roomView.TurnOn();
            roomView.Display(room.Name);
        }

        private async UniTask JoinRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            await UniTask.SwitchToMainThread();

            if (_roomReceiver.JoinRoom(modelToSend) == false)
                return;

            _notificationView.TurnOn();
            _notificationView.Notify($"Player {modelToSend.Value.PlayerId} joined room");

            await UniTask.Delay(TimeSpan.FromSeconds(4));

            _notificationView.TurnOff();
        }

        private async UniTask LeaveRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            await UniTask.SwitchToMainThread();

            if (_roomReceiver.LeaveRoom(modelToSend) == false)
                return;

            _notificationView.TurnOn();
            _notificationView.Notify($"Player {modelToSend.Value.PlayerId} left room");

            await UniTask.Delay(TimeSpan.FromSeconds(4));

            _notificationView.TurnOff();
        }
    }
}