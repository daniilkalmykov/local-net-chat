using System;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Player
{
    internal sealed class Player : IPlayer
    {
        private readonly IMessageFactoryMethod _messageFactoryMethod;
        private readonly IRoomFactoryMethod _roomFactoryMethod;
        private readonly string _name;

        private IRoom _currentRoom;

        public Player(IMessageFactoryMethod messageFactoryMethod, string name, IRoomFactoryMethod roomFactoryMethod)
        {
            _messageFactoryMethod = messageFactoryMethod;
            _name = name;
            _roomFactoryMethod = roomFactoryMethod;
        }

        public event Action<IRoom> JoinedRoom;
        public event Action<IRoom> LeftRoom;
        public event Action<IMessage> MessageSent;
        public event Action<IRoom> RoomCreated;

        public string Id { get; } = Guid.NewGuid().ToString();

        public void JoinRoom(IRoom room)
        {
            if (_currentRoom != null)
                return;
            
            _currentRoom = room;
            _currentRoom.Join();
            
            JoinedRoom?.Invoke(room);
        }

        public void CreateRoom(string name)
        {
            _currentRoom = _roomFactoryMethod.Create(name, _name);
            RoomCreated?.Invoke(_currentRoom);
        }

        public void LeaveRoom()
        {
            LeftRoom?.Invoke(_currentRoom);
            _currentRoom.Leave();
            _currentRoom = null;
        }

        public IMessage SendMessage(string messageBody)
        {
            if (_currentRoom == null)
                return null;

            var message = _messageFactoryMethod.Create(messageBody, _name);

            MessageSent?.Invoke(message);

            return message;
        }
    }
}