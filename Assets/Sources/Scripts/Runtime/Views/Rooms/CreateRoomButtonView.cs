using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Common;
using UnityEngine.Device;

namespace Sources.Scripts.Runtime.Views.Rooms
{
    public class CreateRoomButtonView : ButtonView
    {
        private IPlayerPresenter _playerPresenter;

        public void Init(IPlayerPresenter playerPresenter)
        {
            _playerPresenter = playerPresenter;
        }
        
        protected override void OnClick()
        {
            _playerPresenter.CreateRoom(SystemInfo.deviceName);
        }
    }
}