using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class CelesteMoveInput : MonoBehaviour, ICelesteMoveInput
    {
        public Vector2 move => InputManager.Instance.Movement;
        public bool hasXMovement => Mathf.Abs(move.x) > 0;
        public bool jump => InputManager.Instance.Jump; // 做一下预输入
        public float jumpHoldTime => InputManager.Instance.JumpHoldTime;
        public bool JumpHold => InputManager.Instance.JumpHold;
        public bool wallGrab => InputManager.Instance.WallGrab;
        public bool dash => InputManager.Instance.Dash;
    }
}