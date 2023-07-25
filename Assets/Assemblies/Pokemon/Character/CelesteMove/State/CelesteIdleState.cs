using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteIdleState: State<CelesteMove>
    {
        public CelesteIdleState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.rb.velocity = Vector2.zero;
        }
    }
}