using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteDoubleJumpState: State<CelesteMove>
    {
        public CelesteDoubleJumpState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            Debug.LogWarning("Double Jump not implemented yet!");
        }
    }
}