using Nico;
using Unity.Mathematics;
using UnityEngine;

namespace Pokemon
{
    public class CelesteWallGrabState : State<CelesteMove>
    {
        public CelesteWallGrabState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.rb.gravityScale = 0;
            owner.animator.Climb();
        }

        public override void OnUpdate()
        {
            float x = owner.input.move.x;
            float y = owner.input.move.y;

            if (x > math.abs(owner.moveParams.grabMoveXScale))
            {
                x = owner.moveParams.grabMoveXScale;
            }
            else
            {
                x = 0;
            }

            if (y > 0)
            {
                y = owner.moveParams.climbUpScale;
                owner.rb.velocity = new Vector2(x, y * owner.moveParams.climbSpeed);
            }
            else if (y <= 0)
            {
                y = owner.moveParams.climbDownScale;
                owner.rb.velocity = new Vector2(x, -y * owner.moveParams.slideSpeed);
            }

           
        }

        public override void OnExit()
        {
            owner.rb.gravityScale = owner.moveParams.gravityScale;
        }
    }
}