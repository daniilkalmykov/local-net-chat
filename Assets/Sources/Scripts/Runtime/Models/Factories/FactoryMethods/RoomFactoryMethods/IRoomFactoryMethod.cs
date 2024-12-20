using Sources.Scripts.Runtime.Models.Rooms;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.RoomFactoryMethods
{
    public interface IRoomFactoryMethod
    {
        IRoom Create(string name, string owner, string id = "");
    }
}