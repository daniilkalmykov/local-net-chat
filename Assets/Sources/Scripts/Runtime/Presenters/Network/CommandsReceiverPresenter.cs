using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Sources.Scripts.Runtime.Models.Network;
using Sources.Scripts.Runtime.Models.Network.ModelsToSend;
using Sources.Scripts.Runtime.Models.Network.Receivers;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Newtonsoft.Json;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Views.Rooms;
using UnityEngine;

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
        private readonly ILobby _lobby;
        private readonly IPlayer _player;

        public CommandsReceiverPresenter(ICommandsReceiver commandsReceiver, IRoomReceiver roomReceiver,
            IRoomFactoryMethod roomFactoryMethod, IMonoBehaviourFactoryMethod monoBehaviourFactoryMethod, ILobby lobby, IPlayer player)
        {
            _commandsReceiver = commandsReceiver;
            _roomReceiver = roomReceiver;
            _roomFactoryMethod = roomFactoryMethod;
            _monoBehaviourFactoryMethod = monoBehaviourFactoryMethod;
            _lobby = lobby;
            _player = player;
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
            try
            {
                var json = Encoding.UTF8.GetString(data);

                var modelToSend = JsonConvert.DeserializeObject<ModelToSend<RoomModelToSend>>(json);

                if (modelToSend != null)
                {
                    var command = modelToSend.Command;

                    switch (command)
                    {
                        case (int)Command.JoinRoom:
                        {
                            if (_roomReceiver.JoinRoom(modelToSend) == false)
                                return;

                            break;
                        }
                        case (int)Command.LeftRoom:
                            break;
                        case (int)Command.SendMessage:
                            //mess if player has the same room
                            break;
                        case (int)Command.CreateRoom:
                        {
                            await CreateRoom(modelToSend);
                            
                            break;
                        }
                    }
                }
                else
                {
                    throw new Exception("Couldn't recognize response");
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private async UniTask CreateRoom(ModelToSend<RoomModelToSend> modelToSend)
        {
            if (_roomReceiver.CreateRoom(modelToSend) == false)
                return;

            var room = _roomFactoryMethod.Create(modelToSend.Value.Name, modelToSend.Value.Owner);
            var roomView = await _monoBehaviourFactoryMethod.CreateInMainThread<IRoomView>();

            _lobby.AddRoom(room);

            if (roomView == null)
                return;

            roomView.Init(room.Id, _player, _lobby);
            roomView.TurnOn();
            roomView.Display(room.Name);
        }
    }
}