using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods
{
    internal sealed class RoomFactoryMethod : IRoomFactoryMethod
    {
        public IRoom Create(string name, string owner, string id = "")
        {
            return string.IsNullOrEmpty(id) ? new Room(name, owner) : new Room(name, owner, id);
        }
    }
}