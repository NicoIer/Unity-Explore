using System;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class PokemonInputManager : SceneSingleton<PokemonInputManager>
    {
        private PokemonInputAction _inputAction;

        public Vector2 Movement => _inputAction.Player.Move.ReadValue<Vector2>();
        public bool JumpHold => _inputAction.Player.Jump.ReadValue<float>() > 0;
        public bool Jump => _inputAction.Player.Jump.WasPerformedThisFrame();

        public float JumpHoldTime
        {
            get
            {
                if (JumpHold)
                {
                    return Time.time - _jumpStartTime;
                }

                return -1;
            }
        } 
        private float _jumpStartTime;
        public bool leftAttack => _inputAction.Player.LeftAttack.WasPerformedThisFrame();


        protected override void Awake()
        {
            base.Awake();
            _inputAction = new PokemonInputAction();
            _inputAction.Player.Jump.performed += ctx =>
            {
                _jumpStartTime = Time.time;
            };
        }


        private void OnEnable()
        {
            _inputAction.Enable();
        }

        private void OnDisable()
        {
            _inputAction.Disable();
        }
    }
}