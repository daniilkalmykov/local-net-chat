using Sources.Scripts.Runtime.Models.Lobby;
using Sources.Scripts.Runtime.Models.Player;
using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Common;
using TMPro;
using UnityEngine;

namespace Sources.Scripts.Runtime.Views.Rooms
{
    internal sealed class RoomView : ButtonView, IRoomView
    {
        [SerializeField] private TMP_Text _name;

        private IPlayerPresenter _playerPresenter;
        private string _id;

        public void Display(string roomName)
        {
            _name.text = roomName;
        }

        public void Init(string id, IPlayer player, ILobby lobby)
        {
            _id = id;
            _playerPresenter = new PlayerPresenter(player, lobby);
        }

        protected override void OnClick()
        {
            _playerPresenter.JoinRoom(_id);
        }
    }
}