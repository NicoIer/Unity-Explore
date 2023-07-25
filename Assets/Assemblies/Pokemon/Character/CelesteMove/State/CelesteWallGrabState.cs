using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteWallGrabState: State<CelesteMove>
    {
        public CelesteWallGrabState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            owner.rb.velocity = new Vector2(0, owner.input.move.y*owner.config.climeSpeed);
        }
    }
}