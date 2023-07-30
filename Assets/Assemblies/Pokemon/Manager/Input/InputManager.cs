using Nico;
using UnityEngine;

namespace Pokemon
{
    public class InputManager : GlobalSingleton<InputManager>
    {
        public PokemonInputAction inputAction { get; private set; }

        public Vector2 LeftStick => inputAction.Player.Move.ReadValue<Vector2>();

        public bool B => inputAction.Player.B.WasPerformedThisFrame();

        public bool WallGrab => false;

        public bool AHold => inputAction.Player.A.ReadValue<float>() > 0; //|| jumpButton.down;
        public bool A => inputAction.Player.A.WasPressedThisFrame(); //|| jumpButton.WaPressedThisFrame;
        private float aStartTime;

        public float AHoldTime
        {
            get
            {
                float time = 0;
                if (inputAction.Player.A.ReadValue<float>() > 0)
                {
                    time += Time.time - aStartTime;
                }

                return time;
            }
        }


        protected override void Awake()
        {
            base.Awake();
            inputAction = new PokemonInputAction();
            inputAction.Player.A.performed += ctx => { aStartTime = Time.time; };
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