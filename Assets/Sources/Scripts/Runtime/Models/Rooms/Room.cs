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
        }

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