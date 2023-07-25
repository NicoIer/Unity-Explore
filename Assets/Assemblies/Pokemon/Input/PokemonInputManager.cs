using System;
using System.Collections.Generic;
using Nico;
using Nico.VirtualControl;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{

    public class PokemonInputManager : SceneSingleton<PokemonInputManager>,IEventListener<SkillButtonClickedEvent>
    {
        private PokemonInputAction _inputAction;
        [SerializeField] private VirtualStick virtualStick;
        [SerializeField] private JumpButton jumpButton;

        public Vector2 Movement => (_inputAction.Player.Move.ReadValue<Vector2>() + virtualStick.ReadAxis()).normalized;

        public bool JumpHold => _inputAction.Player.Jump.ReadValue<float>() > 0 || jumpButton.down;
        public bool Jump => _inputAction.Player.Jump.WasPressedThisFrame() || jumpButton.WaPressedThisFrame;
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

                if (jumpButton.down)
                {
                    time += jumpButton.holdTime;
                }

                return time;
            }
        }
        
        public Dictionary<int,bool> skillInput= new Dictionary<int, bool>();


        protected override void Awake()
        {
            base.Awake();
            _inputAction = new PokemonInputAction();
            _inputAction.Player.Jump.performed += ctx => { _jumpStartTime = Time.time; };

            //仅在移动端显示虚拟摇杆
#if UNITY_ANDROID || UNITY_IOS
            virtualStick.gameObject.SetActive(true);
            jumpButton.gameObject.SetActive(true);
#else
            virtualStick.gameObject.SetActive(false);
            jumpButton.gameObject.SetActive(false);
#endif
        }
        
        public bool ReadKey(int key)
        {
            if (skillInput.TryGetValue(key, out var readKey))
            {
                return readKey;
            }
            return false;
        }


        private void OnEnable()
        {
            EventManager.Listen<SkillButtonClickedEvent>(this);
            _inputAction.Enable();
        }

        private void OnDisable()
        {
            EventManager.UnListen<SkillButtonClickedEvent>(this);
            _inputAction.Disable();
        }

        public void OnReceiveEvent(SkillButtonClickedEvent e)
        {
            skillInput[e.config.id] = true;
        }

        private void LateUpdate()
        {
            skillInput.Clear();
        }
    }
}