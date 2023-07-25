using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class CelesteMoveInput : MonoBehaviour
    {
        // public struct InputBuffer
        // {
        //     public Vector2 move;
        //     public bool jump;
        //     public bool wallGrab;
        //     public bool dash;
        // }

        public bool noInput => !hasXMovement && !jump && !wallGrab && !dash;
        public Vector2 move => PokemonInputManager.Instance.Movement;
        public bool hasXMovement => Mathf.Abs(move.x) > 0;
        public bool jump => PokemonInputManager.Instance.Jump; // 做一下预输入
        public float jumpHoldTime => PokemonInputManager.Instance.JumpHoldTime;
        public bool JumpHold => PokemonInputManager.Instance.JumpHold;
        public bool wallGrab => Keyboard.current.ctrlKey.isPressed;

        public bool dash;

        //预输入缓冲时间 0.1f前的输入 可以被缓存下来 如果 在 <0.1f 前 读取输入 则使用缓存区中的数据
        public float bufferTime = 0.1f;
        public float currentBufferTime;

        // private void Update()
        // {
        //     if (currentBufferTime > bufferTime)
        //     {
        //         
        //     }
        // }
    }
}