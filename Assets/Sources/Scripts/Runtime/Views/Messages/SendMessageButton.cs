using Sources.Scripts.Runtime.Presenters.Player;
using Sources.Scripts.Runtime.Views.Common;
using TMPro;
using UnityEngine;

namespace Sources.Scripts.Runtime.Views.Messages
{
    internal sealed class SendMessageButton : ButtonView
    {
        [SerializeField] private TMP_InputField _inputField;

        private IPlayerPresenter _playerPresenter;
        
        public void Init(IPlayerPresenter playerPresenter)
        {
            _playerPresenter = playerPresenter;
        }
        
        protected override void OnClick()
        {
            if (string.IsNullOrEmpty(_inputField.text))
                return;

            _playerPresenter.SendMessage(_inputField.text);
        }
    }
}