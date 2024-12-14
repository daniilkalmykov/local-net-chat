using Sources.Scripts.Runtime.Models.Messages;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods
{
    public interface IMessageFactoryMethod
    {
        IMessage Create(string body, string owner);
    }
}