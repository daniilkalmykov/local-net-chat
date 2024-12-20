using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MonoBehaviourFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Network.Receivers;
using Sources.Scripts.Runtime.Models.Network.Services.RoomServices;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;
using Sources.Scripts.Runtime.Presenters.Network;
using UnityEngine;

namespace Sources.Scripts
{
    public class UdpPeer : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _parent;
        [SerializeField] private Transform _position;
        
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
            localPort = Application.isEditor ? remotePort : localPort;
            _udpClient = new UdpClient(localPort);

            Debug.Log($"UDP запущен на порту: {localPort}");

            _player = new Player(new MessageFactoryMethod(), Application.isEditor ? "editor" : SystemInfo.deviceName,
                new RoomFactoryMethod());

            _remoteEndPoint = new IPEndPoint(IPAddress.Parse(remoteIP), remotePort);

            _commandsReceiver = new CommandReceiver(_udpClient);

            _monoBehaviourFactoryMethod = new MonoBehaviourFactoryMethod(_prefab, _position.position, _parent);
            _commandsReceiverPresenter = new CommandsReceiverPresenter(_commandsReceiver, new RoomReceiver(_player),
                new RoomFactoryMethod(), _monoBehaviourFactoryMethod, new Lobby(), _player);
            
            _roomService = new RoomService(_udpClient, _remoteEndPoint, _player);
            ICommandsSender commandsSender = new CommandsSender(_roomService, _player);

            _commandsReceiverPresenter.Start();
            Debug.Log("UDP Peer запущен!");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SendMessage("Hello from UDP Peer!");
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                _roomService.CreateRoom(new Room("ABOBA", Application.isEditor ? "EDITOR" : "BUILD"));
            }
        }

        private void SendMessage(string message)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(message);
                _udpClient.Send(data, data.Length, _remoteEndPoint);
                Debug.Log($"Сообщение отправлено: {message}");
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка отправки сообщения: {ex.Message}");
            }
        }

        private void OnApplicationQuit()
        {
            _commandsReceiverPresenter.End();
        }
    }
}
