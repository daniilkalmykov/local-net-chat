using System;
using System.Net;
using System.Net.Sockets;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Network.Receivers;
using Sources.Scripts.Runtime.Models.Network.Services.MessageServices;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Presenters.Network;
using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Common;
using Sources.Scripts.Runtime.Views.Messages;
using Sources.Scripts.Runtime.Views.Notifications;
using Sources.Scripts.Runtime.Views.Rooms;
using UnityEngine;

namespace Sources.Scripts
{
    public class UdpPeer : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _position;
        [SerializeField] private NotificationView _notificationView;
        [SerializeField] private CreateRoomButtonView _createRoomButtonView;
        [SerializeField] private MessageWindowView _messageWindowView;
        [SerializeField] private SendMessageButton _sendMessageButton;
        [SerializeField] private MessageView _messageViewPrefab;
        [SerializeField] private Transform _messageParent;
        [SerializeField] private LeaveRoomButtonView _leaveRoomButtonView;
        
        public string remoteIP = "127.0.0.1";
        public int remotePort = 7777;
        public int localPort = 7778;

        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;
        private ICommandsReceiver _commandsReceiver;
        private ICommandsReceiverPresenter _commandsReceiverPresenter;
        private IPlayer _player;
        private IMonoBehaviourFactoryMethod _monoBehaviourFactoryMethod;
        private IRoomService _roomService;

        private void Start()
        {
            localPort = Application.isEditor ? 7777 : 7778;
            remotePort = Application.isEditor ? 7778 : 7777;
            
            try
            {
                _udpClient = new UdpClient(localPort);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка инициализации UDP клиента: {ex.Message}");
            }

            _player = new Player(new MessageFactoryMethod(), Application.isEditor ? "Editor" : SystemInfo.deviceName,
                new RoomFactoryMethod());

            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            _commandsReceiver = new CommandReceiver(_udpClient);
            _monoBehaviourFactoryMethod = new MonoBehaviourFactoryMethod(_prefab, _position.position, _parent);
            
            var lobby = new Lobby();

            var limitedMonoBehaviourFactoryMethod = new LimitedMonoBehaviourFactoryMethod(_messageViewPrefab.gameObject, _messageParent.position,
                _messageParent, 6);
            _commandsReceiverPresenter = new CommandsReceiverPresenter(_commandsReceiver, new RoomReceiver(_player),
                new RoomFactoryMethod(), _monoBehaviourFactoryMethod, lobby, _player, _notificationView,
                new MessageReceiver(_player),
                limitedMonoBehaviourFactoryMethod);
            
            _roomService = new RoomService(_udpClient, _remoteEndPoint, _player);
            _ = new CommandsSenderPresenter(_roomService, _player, new MessageService(_udpClient, _remoteEndPoint),
                limitedMonoBehaviourFactoryMethod);

            _createRoomButtonView.Init(new PlayerPresenter(_player, lobby));
            _messageWindowView.Init(_player);
            _leaveRoomButtonView.Init(new PlayerPresenter(_player, lobby));
            _sendMessageButton.Init(new PlayerPresenter(_player, lobby));

            _commandsReceiverPresenter.Start();
        }

        private void OnApplicationQuit()
        {
            _commandsReceiverPresenter.End();
        }
    }
}
