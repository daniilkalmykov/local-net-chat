namespace Sources.Scripts.Runtime.Models.Messages
{
    internal sealed class Message : IMessage
    {
        public Message(string body, string sender)
        {
            Body = body;
            Sender = sender;
        }

        public string Body { get; }
        public string Sender { get; }
    }
}