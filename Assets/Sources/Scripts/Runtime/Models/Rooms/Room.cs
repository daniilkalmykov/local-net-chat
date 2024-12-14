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
    }
}