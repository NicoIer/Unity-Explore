using Nico;
using UnityEngine;
using UnityEngine.EventSystems;

namespace OneButtonGame
{
    public class RotateButton: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            EventManager.Send<RotateButtonDown>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventManager.Send<RotateButtonUp>();
        }
    }
}