using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kingdom
{
    public class CallButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
    {
        private float _lastPressedTime;
        //这一帧有没有点击
        public bool WasPerformedThisFrame()
        {
            //到上一次按下的时间小于一帧的时间 则返回true
            return Time.time - _lastPressedTime <= Time.deltaTime;
        }
        
        public bool WasPressedThisFrame()
        {
            return Time.time - _lastPressedTime <= Time.deltaTime;
        }
        
        public bool WasReleasedThisFrame()
        {
            return Time.time - _lastPressedTime > Time.deltaTime;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _lastPressedTime = Time.time;
        }
        public void OnPointerUp(PointerEventData eventData)
        {
            
        }
    }
}