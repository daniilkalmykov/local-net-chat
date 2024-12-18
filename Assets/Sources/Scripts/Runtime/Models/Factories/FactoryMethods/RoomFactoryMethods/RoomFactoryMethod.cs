using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods
{
    internal sealed class RoomFactoryMethod : IRoomFactoryMethod
    {
        public IRoom Create(string name, string owner)
        {
            return new Room(name, owner);
        }
    }
}