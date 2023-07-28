using Nico;
using UnityEngine;

namespace Pokemon
{
    /// <summary>
    /// 离开地面 向上移动状态
    /// </summary>
    public class CelesteDownState: State<CelesteMove>
    {
        public CelesteDownState(CelesteMove owner) : base(owner)
        {
        }
        public override void OnUpdate()
        {
            
                owner.rb.velocity = new Vector2(owner.input.move.x* owner.moveParams.speed, owner.rb.velocity.y);
            
        }
    }
}