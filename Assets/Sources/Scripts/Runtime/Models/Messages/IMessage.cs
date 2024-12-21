namespace Sources.Scripts.Runtime.Models.Messages
{
    public interface IMessage
    {
        string Body { get; }
        string Sender { get; }
        string RoomId { get; }
    }
}