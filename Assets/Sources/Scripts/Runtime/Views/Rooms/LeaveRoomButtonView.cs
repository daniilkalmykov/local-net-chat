using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Common;

namespace Sources.Scripts.Runtime.Views.Rooms
{
    internal sealed class LeaveRoomButtonView : ButtonView
    {
        private IPlayerPresenter _playerPresenter;

        public void Init(IPlayerPresenter playerPresenter)
        {
            _playerPresenter = playerPresenter;
        }
        
        protected override void OnClick()
        {
            _playerPresenter.LeaveRoom();
        }
    }
}