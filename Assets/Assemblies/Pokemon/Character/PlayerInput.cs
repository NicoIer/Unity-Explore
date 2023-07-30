using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class PlayerInput : MonoBehaviour, ICelesteMoveInput
    {
        public Vector2 move => InputManager.Instance.LeftStick;
        public bool hasXMovement => Mathf.Abs(move.x) > 0;
        public bool jump => InputManager.Instance.A; // 做一下预输入
        public float jumpHoldTime => InputManager.Instance.AHoldTime;
        public bool JumpHold => InputManager.Instance.AHold;
        public bool wallGrab => InputManager.Instance.WallGrab;
        public bool dash => InputManager.Instance.B;
    }
}