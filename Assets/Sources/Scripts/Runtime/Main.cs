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

namespace Sources.Scripts.Runtime
{
    public class Main : MonoBehaviour
    {
        private const int RemotePort = 7777;
        private const int LocalPort = 7778;
        
        [SerializeField] private GameObject _room;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _position;
        [SerializeField] private NotificationView _notificationView;
        [SerializeField] private CreateRoomButtonView _createRoomButtonView;
        [SerializeField] private MessageWindowView _messageWindowView;
        [SerializeField] private SendMessageButton _sendMessageButton;
        [SerializeField] private MessageView _messageViewPrefab;
        [SerializeField] private Transform _messageParent;
        [SerializeField] private LeaveRoomButtonView _leaveRoomButtonView;
        [SerializeField] private int _messagesLimit;

        [SerializeField] private string _remoteIP;

        private UdpClient _udpClient;
        private IPEndPoint _remoteEndPoint;
        private ICommandsReceiver _commandsReceiver;
        private ICommandsReceiverPresenter _commandsReceiverPresenter;
        private IPlayer _player;
        private IMonoBehaviourFactoryMethod _monoBehaviourFactoryMethod;
        private IRoomService _roomService;
        private CommandsSenderPresenter _commandsSenderPresenter;
        private IPlayerPresenter _playerPresenter;
        private ILobby _lobby;
        private IMonoBehaviourFactoryMethod _limitedMonoBehaviourFactoryMethod;
        private IRoomFactoryMethod _roomFactoryMethod;
        private IMessageFactoryMethod _messageFactoryMethod;
        private IRoomReceiver _roomReceiver;
        private IMessageReceiver _messageReceiver;
        private MessageService _messageService;

        private void OnDisable()
        {
            _commandsReceiverPresenter.End();
            _commandsSenderPresenter.End();
        }
        
        private void Start()
        {
            BindP2PClient();
            BindFactoryMethods();
            BindPlayer();
            BindReceivers();
            BindServices();
            InitViews();

            _commandsReceiverPresenter.Start();
            _commandsSenderPresenter.Start();
        }

        private void InitViews()
        {
            _createRoomButtonView.Init(_playerPresenter);
            _messageWindowView.Init(_player);
            _leaveRoomButtonView.Init(_playerPresenter);
            _sendMessageButton.Init(_playerPresenter);
        }

        private void BindServices()
        {
            _roomService = new RoomService(_udpClient, _remoteEndPoint, _player);
            _messageService = new MessageService(_udpClient, _remoteEndPoint);
            _commandsSenderPresenter = new CommandsSenderPresenter(_roomService, _player, _messageService,
                _limitedMonoBehaviourFactoryMethod);
        }

        private void BindReceivers()
        {
            _commandsReceiver = new CommandReceiver(_udpClient);
            _roomReceiver = new RoomReceiver(_player);
            _messageReceiver = new MessageReceiver(_player);
            _commandsReceiverPresenter = new CommandsReceiverPresenter(_commandsReceiver, _roomReceiver,
                _roomFactoryMethod, _monoBehaviourFactoryMethod, _lobby, _notificationView, _messageReceiver,
                _limitedMonoBehaviourFactoryMethod, _playerPresenter);
        }

        private void BindP2PClient()
        {
            var localPort = Application.isEditor ? LocalPort : RemotePort;
            var remotePort = Application.isEditor ? RemotePort : LocalPort;

            try
            {
                _udpClient = new UdpClient(localPort);
                _remoteEndPoint = new IPEndPoint(IPAddress.Parse(_remoteIP), remotePort);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка инициализации UDP клиента: {ex.Message}");
            }
        }

        private void BindPlayer()
        {
            _player = new Player(_messageFactoryMethod, Application.isEditor ? "Editor" : SystemInfo.deviceName,
                _roomFactoryMethod);
            _lobby = new Lobby();
            _playerPresenter = new PlayerPresenter(_player, _lobby);
        }

        private void BindFactoryMethods()
        {
            _messageFactoryMethod = new MessageFactoryMethod();
            _roomFactoryMethod = new RoomFactoryMethod();
            _monoBehaviourFactoryMethod = new MonoBehaviourFactoryMethod(_room, _position.position, _parent);
            _limitedMonoBehaviourFactoryMethod = new LimitedMonoBehaviourFactoryMethod(_messageViewPrefab.gameObject,
                _messageParent.position,
                _messageParent, _messagesLimit);
        }
    }
}