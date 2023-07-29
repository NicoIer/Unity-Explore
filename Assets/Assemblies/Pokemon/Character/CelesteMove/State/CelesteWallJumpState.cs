using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteWallJumpState : State<CelesteMove>
    {
        public CelesteWallJumpState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            Debug.Log("WallJump");
            owner.stateMachine.LockState(owner.moveParams.wallJumpLockTime);
            if (owner.facing == CelesteMoveFacing.Right)
            {
                //给左上方的速度
                owner.rb.velocity = new Vector2(-owner.moveParams.wallJumpForce.x, owner.moveParams.wallJumpForce.y);
                return;
            }

            if (owner.facing == CelesteMoveFacing.Left)
            {
                //给右上方的速度
                owner.rb.velocity = new Vector2(owner.moveParams.wallJumpForce.x, owner.moveParams.wallJumpForce.y);
                return;
            }
            
        }

        public override void OnUpdate()
        {
            Debug.Log("WallJumping");
            owner.rb.velocity = Vector2.Lerp(owner.rb.velocity, (new Vector2(owner.input.move.x * owner.moveParams.speed, owner.rb.velocity.y)), owner.moveParams.wallJumpLerp * Time.deltaTime);
        }

        public override void OnExit()
        {
            owner.rb.velocity = new Vector2(owner.rb.velocity.x, 0);
        }
    }
}