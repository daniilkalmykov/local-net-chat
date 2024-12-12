namespace Sources.Scripts.Runtime.Core.Models.Messages
{
    public interface IMessage
    {
        string Body { get; }
        string Sender { get; }
    }
}