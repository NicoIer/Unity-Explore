using System;
using Nico;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Pokemon
{
    public enum CelesteMoveFacing
    {
        Right,
        Left
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
        public CelesteMoveParams moveParams => ModelManager.Get<PlayerModel>().celesteMoveParams;
        public bool canJump = true;
        public bool canDash = true;
        public CelesteMoveFacing facing = CelesteMoveFacing.Right;
        public bool inAir => !celesteCollider.isGrounded && !celesteCollider.isTouchingWall;

        [field: SerializeReference] public CelesteMoveStateMachine stateMachine { get; private set; }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            celesteCollider = GetComponent<CelesteCollider>();
            input = GetComponent<CelesteMoveInput>();

            stateMachine = new CelesteMoveStateMachine(this);
            stateMachine.Start<CelesteIdleState>();

            rb.gravityScale = moveParams.gravityScale;
        }

        private void Update()
        {
            //更新facing
            UpdateFacing();

            stateMachine.OnUpdate();
            BetterJump();
        }

        public void UpdateFacing()
        {
            if (input.move.x > 0)
            {
                facing = CelesteMoveFacing.Right;
            }

            if (input.move.x < 0)
            {
                facing = CelesteMoveFacing.Left;
            }

            return;


            // if (celesteCollider.isTouchingWallLeft)
            // {
            //     facing = CelesteMoveFacing.Right;
            //     return;
            // }
            //
            // if (celesteCollider.isTouchingWallRight)
            // {
            //     facing = CelesteMoveFacing.Left;
            //     return;
            // }
        }

        public void BetterJump()
        {
            if (!moveParams.enableBetterJumping) return;
            if (rb.velocity.y < 0) //下落的时候 重力减少
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (moveParams.fallMultiplier - 1) * Time.deltaTime);
            } //上升的时候 
            else if (rb.velocity.y > 0 && (!input.JumpHold || input.jumpHoldTime > moveParams.jumpHoldThreshold))
            {
                rb.velocity += Vector2.up * (Physics2D.gravity.y * (moveParams.lowJumpMultiplier - 1) * Time.deltaTime);
            }
        }

        public bool HasInverseXVelocity()
        {
            if (facing == CelesteMoveFacing.Right && rb.velocity.x < 0)
            {
                return true;
            }

            if (facing == CelesteMoveFacing.Left && rb.velocity.x > 0)
            {
                return true;
            }

            return false;
        }

        public bool HasInverseXInput()
        {
            float x = input.move.x;
            if (facing == CelesteMoveFacing.Right && x < 0)
            {
                return true;
            }

            if (facing == CelesteMoveFacing.Left && x > 0)
            {
                return true;
            }

            return false;
        }

#if UNITY_EDITOR
        private void OnGUI()
        {
            GUI.Label(new Rect(0, 0, 100, 100), $"jumpHoldTime:{input.jumpHoldTime}");
        }
#endif
    }
}