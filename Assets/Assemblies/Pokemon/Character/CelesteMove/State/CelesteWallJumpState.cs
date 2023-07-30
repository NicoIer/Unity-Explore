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
            owner.stateMachine.LockState(owner.moveParams.wallJumpLockTime);
            if (owner.facing == CelesteMoveFacing.Right)
            {
                //给左上方的速度
                owner.rb.velocity = new Vector2(-owner.moveParams.wallJumpForce.x, owner.moveParams.wallJumpForce.y);
                owner.rb.velocity = Vector2.Lerp(owner.rb.velocity,
                    (new Vector2(owner.input.move.x * owner.moveParams.speed, owner.rb.velocity.y)),
                    owner.moveParams.wallJumpLerp * Time.deltaTime);
                owner.SetFacing(CelesteMoveFacing.Left);
            }
            else if (owner.facing == CelesteMoveFacing.Left)
            {
                //给右上方的速度
                owner.rb.velocity = new Vector2(owner.moveParams.wallJumpForce.x, owner.moveParams.wallJumpForce.y);
                owner.rb.velocity = Vector2.Lerp(owner.rb.velocity,
                    (new Vector2(owner.input.move.x * owner.moveParams.speed, owner.rb.velocity.y)),
                    owner.moveParams.wallJumpLerp * Time.deltaTime);
                owner.SetFacing(CelesteMoveFacing.Right);
            }

            owner.animator.WallJump();
        }

        public override void OnUpdate()
        {
            // owner.rb.velocity = Vector2.Lerp(owner.rb.velocity,
            //     (new Vector2(owner.rb.velocity.x , owner.rb.velocity.y)),
            //     owner.moveParams.wallJumpLerp * Time.deltaTime);
        }

        public override void OnExit()
        {
            owner.rb.velocity = new Vector2(owner.rb.velocity.x, 0);
        }
    }
}