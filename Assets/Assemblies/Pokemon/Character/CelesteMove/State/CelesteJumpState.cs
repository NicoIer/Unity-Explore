using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteJumpState : State<CelesteMove>
    {
        public CelesteJumpState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.rb.velocity = new Vector2(owner.rb.velocity.x, owner.config.jumpForce);
            owner.jumpCount = 1;
        }
    }
}