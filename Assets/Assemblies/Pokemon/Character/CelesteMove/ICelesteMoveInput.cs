using Unity.Mathematics;
using UnityEngine;

namespace Pokemon
{
    public interface ICelesteMoveInput
    {
        public Vector2 move { get; }
        public bool hasXMovement => math.abs(move.x) > 0;
        public bool jump { get; }
        public float jumpHoldTime { get; }
        public bool JumpHold { get; }
        public bool wallGrab { get; }
        public bool dash { get; }
    }
}