using System;
using Nico;
using UnityEngine;

namespace Pokemon
{
    /// <summary>
    /// 地面行走状态
    /// </summary>
    public class CelesteWalkState : State<CelesteMove>
    {
        public CelesteWalkState(CelesteMove owner) : base(owner)
        {
        }

        public override void OnEnter()
        {
            owner.animator.Walk();
        }

        public override void OnUpdate()
        {
            owner.rb.velocity = new Vector2(owner.input.move.x * owner.moveParams.speed, 0);
        }
    }
}