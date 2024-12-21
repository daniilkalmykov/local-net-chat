namespace Sources.Scripts.Runtime.Views.Messages
{
    public interface IMessageView : IView
    {
        void Display(string message, string sender);
    }
}