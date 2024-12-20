using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

[assembly: InternalsVisibleTo("Assembly-CSharp")]
namespace Sources.Scripts.Runtime.Views.Notifications
{
    internal sealed class NotificationView : MonoBehaviour, INotificationView
    {
        [SerializeField] private TMP_Text _text;
        
        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }

        public void Notify(string text)
        {
            _text.text = text;
        }
    }
}