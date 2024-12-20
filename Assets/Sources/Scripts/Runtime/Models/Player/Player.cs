using System;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods;
using Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Rooms;
using UnityEngine;

namespace Sources.Scripts.Runtime.Models.Player
{
    internal sealed class Player : IPlayer
    {
        private readonly IMessageFactoryMethod _messageFactoryMethod;
        private readonly IRoomFactoryMethod _roomFactoryMethod;
        private readonly string _name;

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
        public IRoom CurrentRoom { get; private set; }

        public void JoinRoom(IRoom room)
        {
            if (CurrentRoom != null)
                return;
            
            CurrentRoom = room;
            CurrentRoom.Join();
            
            JoinedRoom?.Invoke(room);
        }

        public void CreateRoom(string name)
        {
            Debug.LogError("Create");
            CurrentRoom = _roomFactoryMethod.Create(name, _name);
            RoomCreated?.Invoke(CurrentRoom);
        }

        public void LeaveRoom()
        {
            LeftRoom?.Invoke(CurrentRoom);
            CurrentRoom.Leave();
            CurrentRoom = null;
        }

        public IMessage SendMessage(string messageBody)
        {
            if (CurrentRoom == null)
                return null;

            var message = _messageFactoryMethod.Create(messageBody, _name);

            MessageSent?.Invoke(message);

            return message;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}