using System;
using System.Net;
using System.Net.Sockets;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Network.Receivers;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;
using Sources.Scripts.Runtime.Presenters.Network;
using Sources.Scripts.Runtime.Presenters.Player;
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
                
                Debug.Log($"UDP клиент успешно запущен на порту: {localPort}");
                Debug.Log($"Отправка сообщений будет идти на {remoteIP}:{remotePort}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка инициализации UDP клиента: {ex.Message}");
            }


            Debug.Log($"UDP запущен на порту: {localPort}");

            _player = new Player(new MessageFactoryMethod(), Application.isEditor ? "editor" : SystemInfo.deviceName,
                new RoomFactoryMethod());

            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);
            _commandsReceiver = new CommandReceiver(_udpClient);
            _monoBehaviourFactoryMethod = new MonoBehaviourFactoryMethod(_prefab, _position.position, _parent);
            
            var lobby = new Lobby();
            _commandsReceiverPresenter = new CommandsReceiverPresenter(_commandsReceiver, new RoomReceiver(_player),
                new RoomFactoryMethod(), _monoBehaviourFactoryMethod, lobby, _player, _notificationView);
            
            _roomService = new RoomService(_udpClient, _remoteEndPoint, _player);
            ICommandsSenderPresenter commandsSenderPresenter = new CommandsSenderPresenter(_roomService, _player);

            _createRoomButtonView.Init(new PlayerPresenter(_player, lobby));
            
            _commandsReceiverPresenter.Start();
            Debug.Log("UDP Peer запущен!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                _roomService.CreateRoom(new Room("ABOBA", Application.isEditor ? "EDITOR" : "BUILD"));
            }
        }

        private void OnApplicationQuit()
        {
            _commandsReceiverPresenter.End();
        }
    }
}
