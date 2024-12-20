namespace Sources.Scripts.Runtime.Views.Notifications
{
    public interface INotificationView : IView
    {
        void Notify(string text);
    }
}