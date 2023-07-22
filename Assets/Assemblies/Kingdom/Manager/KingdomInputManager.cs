#define GAME_DEBUG

using System;
using Nico;
using Nico.VirtualControl;
using UnityEngine;
using UnityEngine.UI;

namespace Kingdom
{
    public class KingdomInputManager : SceneSingleton<KingdomInputManager>
    {
        private KingdomInputAction _inputAction;
        [SerializeField] private VirtualStick _leftStick;
        [SerializeField] private CallButton _callButton;
        public Vector2 Move()
        {
            return _inputAction.Player.Move.ReadValue<Vector2>() + _leftStick.ReadAxis();
        }

        public bool Call()
        {
            return _inputAction.Player.Call.WasPerformedThisFrame() || _callButton.WasPerformedThisFrame();
        }

        protected override void Awake()
        {
            base.Awake();
            _inputAction = new KingdomInputAction();

#if UNITY_ANDROID
            _leftStick.transform.gameObject.SetActive(true);
            _callButton.gameObject.SetActive(true);
#else
            _leftStick.transform.gameObject.SetActive(false);
            _callButton.gameObject.SetActive(false);
#endif
        }

        private void OnEnable()
        {
            _inputAction.Enable();
#if UNITY_ANDROID
            _leftStick.transform.gameObject.SetActive(true);
            _callButton.gameObject.SetActive(true);
#endif
        }

        private void OnDisable()
        {
            _inputAction.Disable();
#if UNITY_ANDROID
            _leftStick.transform.gameObject.SetActive(false);
            _callButton.gameObject.SetActive(false);
#endif
        }

#if GAME_DEBUG
        private void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 150, 100), "move: " + Move());
            GUI.Label(new Rect(20, 40, 150, 100), "call: " + Call());
        }

#endif
    }
}