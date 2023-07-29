using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class JumpButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public bool down { get; private set; } = false;
        public bool WaPressedThisFrame => down && !holding;
        private bool holding => holdTime > 0.01f;
        private float _holdTime;

        private void OnEnable()
        {
            InputManager.Instance.inputAction.Player.Jump.AddBinding();
        }

        public float holdTime
        {
            get
            {
                if (down)
                {
                    return Time.time - _holdTime;
                }

                return 0;
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            down = true;
            _holdTime = Time.time;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            down = false;
        }
    }
}