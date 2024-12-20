using System;
using System.Collections.Generic;
using System.Linq;
using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Lobby
{
    internal sealed class Lobby : ILobby
    {
        private readonly List<IRoom> _rooms = new();
        
        public IRoom GetRoomById(string id)
        {
            return _rooms.FirstOrDefault(room => room.Id == id);
        }

        public void AddRoom(IRoom room)
        {
            if (room == null)
                throw new ArgumentNullException();
            
            _rooms.Add(room);
        }

        public void DeleteRoom(IRoom room)
        {
            if (room == null)
                throw new ArgumentNullException();
            
            _rooms.Remove(room);
        }
    }
}