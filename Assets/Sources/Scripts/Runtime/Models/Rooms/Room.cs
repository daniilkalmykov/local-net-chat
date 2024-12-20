using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Sources.Scripts.Runtime.Models.Rooms
{
    internal sealed class Room : IRoom
    {
        public Room(string name, string owner)
        {
            Name = name;
            Owner = owner;
            Id = Guid.NewGuid().ToString();
        }
        
        public Room(string name, string owner, string id)
        {
            Name = name;
            Owner = owner;
            Id = id;
        }

        public string Id { get; }
        public string Name { get; }
        public string Owner { get; }
        public int Participants { get; private set; } = 1;
       
        public void Join()
        {
            Participants += 1;
        }

        public void Leave()
        {
            Participants -= 1;
        }
    }
}