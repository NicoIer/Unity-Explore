using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{
    [Serializable]
    public class CelesteMoveParams
    {
        public bool enableBetterJumping = true;
        public float fallMultiplier = 2.5f;
        public float lowJumpMultiplier = 7f;
        public float jumpHoldThreshold = 0.23f;

        public float speed = 10f;
        public float jumpForce = 15f;
        public float slideSpeed = 5f;
        public float wallJumpLerp = 10f;
        public float dashSpeed = 20f;
        public float climbSpeed = 3;
        public Vector2 wallJumpForce = new Vector2(24, 12);
        public float wallJumpLockTime = 0.1f;
        public float dashTime = 0.2f;
        public float gravityScale = 3f;
        public float grabMoveXScale = 0.2f;
        public float climbUpScale = 0.5f;
        public float climbDownScale = 1;
    }

    [CreateAssetMenu(fileName = "CelesteMoveConfig", menuName = "Pokemon/Config/CelesteMoveConfig")]
    public class CelesteMoveConfig : ScriptableConfig
    {
        public CelesteMoveParams moveParams;

#if UNITY_EDITOR
        [Sirenix.OdinInspector.Button]
        public void Reset()
        {
            moveParams = new CelesteMoveParams();
        }
#endif
    }
}