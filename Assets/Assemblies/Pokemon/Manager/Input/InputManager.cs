using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class InputManager : GlobalSingleton<InputManager>
    {
        public PokemonInputAction inputAction { get; private set; }

        public Vector2 Movement => inputAction.Player.Move.ReadValue<Vector2>();

        public bool Dash
        {
            get
            {
                return inputAction.Player.Skill1.WasPerformedThisFrame();
            }
        }

        public bool WallGrab
        {
            get
            {
                return false;
            }
        }

        public bool JumpHold => inputAction.Player.Jump.ReadValue<float>() > 0; //|| jumpButton.down;
        public bool Jump => inputAction.Player.Jump.WasPressedThisFrame(); //|| jumpButton.WaPressedThisFrame;
        private float _jumpStartTime;

        public float JumpHoldTime
        {
            get
            {
                float time = 0;
                if (inputAction.Player.Jump.ReadValue<float>() > 0)
                {
                    time += Time.time - _jumpStartTime;
                }

                return time;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            inputAction = new PokemonInputAction();
            inputAction.Player.Jump.performed += ctx => { _jumpStartTime = Time.time; };
        }


        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            if (inputAction != null)
            {
                inputAction.Disable();
            }
        }
    }
}