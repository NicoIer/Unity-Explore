using Nico;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class ForceButton: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {

        public void OnPointerDown(PointerEventData eventData)
        {
            EventManager.Send<ForceButtonDown>();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            EventManager.Send<ForceButtonUp>();
        }
    }
}