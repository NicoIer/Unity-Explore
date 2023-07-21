using System;
using Nico;
using Nico.VirtualControl;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace OneButtonGame
{
    public class InputManager : SceneSingleton<InputManager>
    {
        private OneButtonGameInputAction _inputAction;
        [SerializeField] private OneButton right;
        [SerializeField] private OneButton left;
        [SerializeField] private OneButton ecs;
        private Vector2 inputMove => _inputAction.Computer.Move.ReadValue<Vector2>();
        private Vector2 _buttonMove;

        public Vector2 move => _buttonMove + inputMove;

        protected override void Awake()
        {
            base.Awake();
            _inputAction = new OneButtonGameInputAction();
        }

        private void OnEnable()
        {
            _inputAction.Enable();
            right.OnButtonDown += OnRightButtonDown;
            right.OnButtonUp += OnRightButtonUp;
            left.OnButtonDown += OnLeftButtonDown;
            left.OnButtonUp += OnLeftButtonUp;
            ecs.OnButtonUp += OnEcsButtonUp;
        }

        private void OnEcsButtonUp()
        {
            GameManager.Instance.BackToTitle();
        }

        private void OnDisable()
        {
            _inputAction.Disable();
            right.OnButtonDown -= OnRightButtonDown;
            right.OnButtonUp -= OnRightButtonUp;
            left.OnButtonDown -= OnLeftButtonDown;
            left.OnButtonUp -= OnLeftButtonUp;
            ecs.OnButtonUp -= OnEcsButtonUp;
        }

        private void OnRightButtonDown()
        {
            _buttonMove.x += 1f;
        }

        private void OnRightButtonUp()
        {
            _buttonMove.x -= 1f;
        }

        private void OnLeftButtonDown()
        {
            _buttonMove.x -= 1f;
        }

        private void OnLeftButtonUp()
        {
            _buttonMove.x += 1f;
        }
    }
}