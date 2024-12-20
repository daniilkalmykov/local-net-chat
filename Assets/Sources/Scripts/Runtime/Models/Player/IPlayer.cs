using System;
using Sources.Scripts.Runtime.Models.Messages;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Player
{
    public interface IPlayer
    {
        event Action<IRoom> JoinedRoom;
        event Action<IRoom> LeftRoom;
        event Action<IMessage> MessageSent;
        event Action<IRoom> RoomCreated;
        
        string Id { get; }
        IRoom CurrentRoom { get;  }

        void JoinRoom(IRoom room);
        void CreateRoom(string name);
        void LeaveRoom();
        IMessage SendMessage(string messageBody);
    }
}