using System;
using Nico;
using UnityEngine;

namespace Pokemon
{
    /// <summary>
    /// 模仿蔚蓝手感 的 移动组件
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class CelesteMove : MonoBehaviour
    {
        public ICelesteCollider celesteCollider { get; private set; }
        public ICelesteMoveAnimator animator { get; private set; }
        public ICelesteMoveInput input { get; private set; }
        public Rigidbody2D rb { get; private set; }
        public CelesteMoveParams moveParams => ModelManager.Get<PlayerModel>().celesteMoveParams;
        public PlayerModel playerModel => ModelManager.Get<PlayerModel>();


        public bool canJump
        {
            get => playerModel.canJump;
            set => playerModel.canJump = value;
        }

        public bool canDash
        {
            get => playerModel.canDash;
            set => playerModel.canDash = value;
        }

        public CelesteMoveFacing facing = CelesteMoveFacing.Right;
        public event Action<CelesteMoveFacing> OnFacingChange; 
        public bool inAir => !celesteCollider.isGrounded && !celesteCollider.isTouchingWall;

        [field: SerializeReference] public CelesteMoveStateMachine stateMachine { get; private set; }

        private void Awake()
        {
            
            celesteCollider = GetComponent<ICelesteCollider>();
            input = GetComponent<ICelesteMoveInput>();  
            animator = GetComponent<ICelesteMoveAnimator>();
            
            rb = GetComponent<Rigidbody2D>();
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
            CelesteMoveFacing pre = facing;
            if (input.move.x > 0)
            {
                facing = CelesteMoveFacing.Right;
            }

            if (input.move.x < 0)
            {
                facing = CelesteMoveFacing.Left;
            }

            if (facing != pre)
            {
                OnFacingChange?.Invoke(facing);
            }

            return;
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
    }
}