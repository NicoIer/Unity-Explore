using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class InputManager : GlobalSingleton<InputManager>
    {
        private PokemonInputAction _inputAction;

        public Vector2 Movement
        {
            get { return (_inputAction.Player.Move.ReadValue<Vector2>()); }
        }

        public bool Dash => ReadKey<bool>(InputConst.Dash);
        public bool WallGrab => ReadKey<bool>(InputConst.WallGrab);

        public bool JumpHold => _inputAction.Player.Jump.ReadValue<float>() > 0; //|| jumpButton.down;
        public bool Jump => _inputAction.Player.Jump.WasPressedThisFrame(); //|| jumpButton.WaPressedThisFrame;
        private float _jumpStartTime;

        public float JumpHoldTime
        {
            get
            {
                float time = 0;
                if (_inputAction.Player.Jump.ReadValue<float>() > 0)
                {
                    time += Time.time - _jumpStartTime;
                }

                return time;
            }
        }

        private Dictionary<int,object> _keys = new Dictionary<int, object>();
        protected override void Awake()
        {
            base.Awake();
            _inputAction = new PokemonInputAction();
            _inputAction.Player.Jump.performed += ctx => { _jumpStartTime = Time.time; };
        }


        public T ReadKey<T>(int key)
        {
            if(_keys.TryGetValue(key,out object value))
                return (T) value;
            return default;
        }

        public void SetKey<T>(int key, T value)
        {
            _keys[key] = value;
        }
        


        private void OnEnable()
        {
            _inputAction.Enable();
        }

        private void OnDisable()
        {
            _inputAction.Disable();
        }

        private void LateUpdate()
        {
            _keys.Clear();
        }
    }
}