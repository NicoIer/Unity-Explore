using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pokemon
{
    public class CelesteMoveInput : MonoBehaviour, ICelesteMoveInput
    {
        public Vector2 move => PokemonInputManager.Instance.Movement;
        public bool hasXMovement => Mathf.Abs(move.x) > 0;
        public bool jump => PokemonInputManager.Instance.Jump; // 做一下预输入
        public float jumpHoldTime => PokemonInputManager.Instance.JumpHoldTime;
        public bool JumpHold => PokemonInputManager.Instance.JumpHold;
        public bool wallGrab => PokemonInputManager.Instance.ReadKey(SkillConfig.WallGrab);
        public bool dash => PokemonInputManager.Instance.ReadKey(SkillConfig.Dash);
    }
}