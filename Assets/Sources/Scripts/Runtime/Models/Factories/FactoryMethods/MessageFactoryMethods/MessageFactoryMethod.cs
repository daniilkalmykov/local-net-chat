using Sources.Scripts.Runtime.Models.Messages;

namespace Sources.Scripts.Runtime.Models.Factories.FactoryMethods.MessageFactoryMethods
{
    internal sealed class MessageFactoryMethod : IMessageFactoryMethod
    {
        public IMessage Create(string body, string owner)
        {
            return new Message(body, owner);
        }
    }
}