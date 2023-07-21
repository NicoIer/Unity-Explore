using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class OneButton : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public static event Action OnButtonDown;
        public static event Action OnButtonUp;

        private void Awake()
        {
            OnButtonDown = null;
            OnButtonUp = null;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnButtonDown?.Invoke();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnButtonUp?.Invoke();
        }
        
        

        private void OnDestroy()
        {
            OnButtonDown = null;
            OnButtonUp = null;
        }


    }
}