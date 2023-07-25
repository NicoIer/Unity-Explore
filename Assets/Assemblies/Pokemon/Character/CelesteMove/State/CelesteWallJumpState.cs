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
            if (owner.facing == CelesteMoveFacing.Right)
            {
                //给左上方的速度
                owner.rb.velocity = new Vector2(-owner.config.wallJumpForce.x, owner.config.wallJumpForce.y);
                return;
            }

            if (owner.facing == CelesteMoveFacing.Left)
            {
                //给右上方的速度
                owner.rb.velocity = new Vector2(owner.config.wallJumpForce.x, owner.config.wallJumpForce.y);
                return;
            }
        }

        public override void OnUpdate()
        {
            owner.rb.velocity = Vector2.Lerp(owner.rb.velocity, (new Vector2(owner.input.move.x * owner.config.speed, owner.rb.velocity.y)), owner.config.wallJumpLerp * Time.deltaTime);
        }

        public override void OnExit()
        {
            owner.rb.velocity = new Vector2(owner.rb.velocity.x, 0);
        }
    }
}