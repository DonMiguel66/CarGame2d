using System;
using UnityEngine.EventSystems;
using UnityEngine;

namespace CarGame2D
{
    public class TouchButtonView : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event EventHandler ButtonDown = delegate { };
        public event EventHandler ButtonUp = delegate { };

        public void OnPointerDown(PointerEventData eventData)
        {
            ButtonDown.Invoke(this, new EventArgs());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ButtonUp.Invoke(this, new EventArgs());
        }
    }
}
