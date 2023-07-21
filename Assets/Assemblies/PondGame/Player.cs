using UnityEngine;
using UnityEngine.EventSystems;

namespace PondGame
{
    [AddComponentMenu("PondGame/Player")]
    public class Player: MonoBehaviour,IPointerDownHandler,IPointerUpHandler
    {
        private Camera _mainCamera;
        public bool pressing;
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(eventData.position);
            pressing = true;
            Debug.Log("OnPointerDown");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Vector3 worldPoint = _mainCamera.ScreenToWorldPoint(eventData.position);
            pressing = false;
            Debug.Log("OnPointerUp");
        }
    }
}