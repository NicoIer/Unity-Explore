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
        }

        public override void OnUpdate()
        {
            float x = owner.input.move.x;
            float y = owner.input.move.y;

            if (x > math.abs(owner.config.grabMoveXScale))
            {
                x = owner.config.grabMoveXScale;
            }
            else
            {
                x = 0;
            }

            if (y > 0)
            {
                y = owner.config.climeUpScale;
                owner.rb.velocity = new Vector2(x, y * owner.config.climbSpeed);
            }
            else if (y <= 0)
            {
                y = owner.config.climeDownScale;
                owner.rb.velocity = new Vector2(x, -y * owner.config.slideSpeed);
            }

           
        }

        public override void OnExit()
        {
            owner.rb.gravityScale = owner.config.gravityScale;
        }
    }
}