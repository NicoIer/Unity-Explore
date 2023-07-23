using System;
using Nico;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Pokemon
{
    [Serializable]
    public struct CelesteMoveConfig
    {
        public bool enableBetterJumping; //= true
        public float fallMultiplier; // = 2.5f;
        public float lowJumpMultiplier; // = 2f;
        public float jumpHoldThreshold; // = 0.2f;


        public float speed; // = 7f;
        public float jumpForce; //= 12f;
        public float slideSpeed; // = 5f;
        public float wallJumpLerp; // = 0.5f;
        public float dashSpeed; //= 20f;
    }

    /// <summary>
    /// 模仿蔚蓝手感 的 移动组件
    /// </summary>
    [RequireComponent(typeof(CelesteCollider))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CelesteMoveInput))]
    public class CelesteMove : MonoBehaviour
    {
        public CelesteCollider celesteCollider { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CelesteMoveInput input { get; private set; }
        public CelesteMoveConfig config;
        public bool canJump;
        public int jumpCount;
        public bool canDash;
        public bool inAir => !celesteCollider.isGrounded && !celesteCollider.isTouchingWall;

        [field: SerializeReference] public CelesteMoveStateMachine stateMachine { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            celesteCollider = GetComponent<CelesteCollider>();
            input = GetComponent<CelesteMoveInput>();

            stateMachine = new CelesteMoveStateMachine(this);
            stateMachine.Start<CelesteIdleState>();
        }

        private void Update()
        {
            stateMachine.OnUpdate();
            BetterJump();
        }

        public void BetterJump()
        {
            if (!config.enableBetterJumping) return;
            if (rb.velocity.y < 0) //下落的时候 重力逐渐增加
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (config.fallMultiplier - 1) * Time.deltaTime);
            }//上升的时候 根据 jump按钮按的时长 给予补偿
            else if (rb.velocity.y > 0 && !input.JumpHold) ;// && input.jumpHoldTime > config.jumpHoldThreshold)
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (config.lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }
#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), $"jumpHoldTime:{input.jumpHoldTime}");
        }        
#endif

    }
}