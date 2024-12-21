using UnityEngine;

namespace Sources.Scripts.Runtime.Views.Common
{
    public class WindowView : MonoBehaviour, IView
    {
        public void TurnOn()
        {
            gameObject.SetActive(true);
        }

        public void TurnOff()
        {
            gameObject.SetActive(false);
        }
    }
}