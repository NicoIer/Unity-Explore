using Nico;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class OneButton: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            EventManager.Send<OneButtonDown>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventManager.Send<OneButtonUp>();
        }
    }
}