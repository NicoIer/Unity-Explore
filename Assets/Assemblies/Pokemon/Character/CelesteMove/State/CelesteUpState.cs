using Nico;
using UnityEngine;

namespace Pokemon
{
    /// <summary>
    /// 离开地面 向上移动状态
    /// </summary>
    public class CelesteUpState : State<CelesteMove>
    {
        public CelesteUpState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnUpdate()
        {
            // Debug.Log($"Upping,{owner.input.move.x}");
            owner.rb.velocity = new Vector2(owner.input.move.x * owner.config.speed, owner.rb.velocity.y);
        }
    }
}