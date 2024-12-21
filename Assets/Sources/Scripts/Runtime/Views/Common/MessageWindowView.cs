using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Models.Rooms;
using UnityEngine;

namespace Sources.Scripts.Runtime.Views.Common
{
    public class MessageWindowView : WindowView
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        private IPlayer _player;

        private void OnDestroy()
        {
            _player.JoinedRoom -= OnJoinedRoom;
            _player.JoinedRoom -= OnJoinedRoom;
        }

        public void Init(IPlayer player)
        {
            _player = player;

            _player.RoomCreated += OnJoinedRoom;
            _player.JoinedRoom += OnJoinedRoom;
        }

        private void OnJoinedRoom(IRoom obj)
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}