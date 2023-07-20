using System;
using Nico;
using Nico.VirtualControl;
using UnityEngine;

namespace OneButtonGame
{
    public class InputManager: SceneSingleton<InputManager>
    {
        public VirtualStick virtualStick;

        private void Update()
        {
            Debug.Log(virtualStick.ReadAxis());
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), virtualStick.ReadAxis().ToString());
        }
    }
}