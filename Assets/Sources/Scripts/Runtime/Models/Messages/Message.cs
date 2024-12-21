namespace Sources.Scripts.Runtime.Models.Messages
{
    public sealed class Message : IMessage
    {
        public Message(string body, string sender, string roomId)
        {
            Body = body;
            Sender = sender;
            RoomId = roomId;
        }

        public string Body { get; }
        public string Sender { get; }
        public string RoomId { get; }
    }
}