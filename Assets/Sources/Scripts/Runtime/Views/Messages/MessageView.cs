using TMPro;
using UnityEngine;

namespace Sources.Scripts.Runtime.Views.Messages
{
    public sealed class MessageView : MonoBehaviour, IMessageView
    {
        [SerializeField] private TMP_Text _body;
        
        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }

        public void Display(string message, string sender)
        {
            _body.text = $"{sender}: {message}";
        }
    }
}