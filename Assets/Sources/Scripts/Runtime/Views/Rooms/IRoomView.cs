using Sources.Scripts.Runtime.Presenters.Player;

namespace Sources.Scripts.Runtime.Views.Rooms
{
    public interface IRoomView : IView
    {
        void Display(string roomName);
        void Init(string id, IPlayerPresenter playerPresenter);
    }
}