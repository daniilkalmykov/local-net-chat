using UnityEngine;
using UnityEngine.UI;

namespace Sources.Scripts.Runtime.Views.Common
{
    public abstract class ButtonView : MonoBehaviour, IView
    {
        [SerializeField] private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }

        protected abstract void OnClick();
    }
}