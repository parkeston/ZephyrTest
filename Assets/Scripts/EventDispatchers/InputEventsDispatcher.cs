using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace EventDispatchers
{
    public class InputEventsDispatcher : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public static event Action OnPointerDownEvent;
        public static event Action OnPointerUpEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownEvent?.Invoke();
       
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpEvent?.Invoke();
        }
    }
}
