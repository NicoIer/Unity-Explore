using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class OneButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public event Action OnButtonDown;
        public event Action OnButtonUp;

        public void OnPointerDown(PointerEventData eventData)
        {
            OnButtonDown?.Invoke();
            Debug.Log("OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnButtonUp?.Invoke();
        }
    }
}