using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteMoveStateMachine : StateMachine<CelesteMove>
    {
        public CelesteMoveStateMachine(CelesteMove owner) : base(owner)
        {
            Add(new CelesteIdleState(owner));
            Add(new CelesteWalkState(owner));
            
            Add(new CelesteJumpState(owner));
            Add(new CelesteDoubleJumpState(owner));
            Add(new CelesteWallJumpState(owner));
            
            Add(new CelesteUpState(owner));
            Add(new CelesteDownState(owner));
            
            Add(new CelesteWallGrabState(owner));
            Add(new CelesteWallSlideState(owner));
            
            Add(new CelesteDashState(owner));
        }

        public override void OnUpdate()
        {
            //做一些通用的事情 判断状态的切换 让 各个状态只需要做自己该做的事情 无需关心状态切换

            //可以冲刺 按下了 冲刺键
            if (Owner.input.dash && Owner.canDash)
            {
                Change<CelesteDashState>();
                base.OnUpdate();
                return;
            }
            
            //在地上 且 没有按下移动键 且 没有按下跳跃键 且 没有向上的速度
            if (Owner.celesteCollider.isGrounded && !Owner.input.hasXMovement && !Owner.input.jump && Owner.rb.velocity.y <= 0)
            {
                // Debug.Log("idle");
                Change<CelesteIdleState>();
                base.OnUpdate();
                return;
            }

            //在地上 且 按下了跳跃键 且 可以跳跃
            if (Owner.celesteCollider.isGrounded && Owner.input.jump && Owner.canJump)
            {
                Change<CelesteJumpState>();
                base.OnUpdate();
                return;
            }

            //在地上 且 按下了移动键 且 没有按下跳跃键
            if (Owner.celesteCollider.isGrounded && Owner.input.hasXMovement && !Owner.input.jump)
            {
                Change<CelesteWalkState>();
                base.OnUpdate();
                return;
            }

            //在空中 且 可以跳 且 没有按下跳跃键 且 跳跃次数=0 
            //这种情况出现在: 1从悬崖掉下去
            if (Owner.inAir && Owner.canJump && Owner.input.jump && Owner.jumpCount == 0)
            {
                Change<CelesteJumpState>();
                base.OnUpdate();
                return;
            }

            //在空中 且 可以跳 且 按下了跳跃键 且 跳跃次数=1
            if (Owner.inAir && Owner.canJump && Owner.input.jump && Owner.jumpCount == 1)
            {
                Change<CelesteDoubleJumpState>();
                base.OnUpdate();
                return;
            }

            //在空中 且 当前在上升 且 没有按下跳跃键
            if (Owner.inAir && Owner.rb.velocity.y > 0 && !Owner.input.jump)
            {
                Change<CelesteUpState>();
                base.OnUpdate();
                return;
            }

            //在空中 且 当前在下降 且 没有按下跳跃键
            if (Owner.inAir && Owner.rb.velocity.y < 0 && !Owner.input.jump)
            {
                Change<CelesteDownState>();
                base.OnUpdate();
                return;
            }

            //不在地上 靠近墙壁 且 按下了 抓墙键 且 没有按下跳跃键
            if (!Owner.celesteCollider.isGrounded && Owner.celesteCollider.isTouchingWall && Owner.input.wallGrab &&
                !Owner.input.jump)
            {
                Change<CelesteWallGrabState>();
                base.OnUpdate();
                return;
            }

            //不在地面 靠近墙壁 且 没有按下跳跃键 且 没有按下 抓墙键
            if (!Owner.celesteCollider.isGrounded && Owner.celesteCollider.isTouchingWall && !Owner.input.jump &&
                !Owner.input.wallGrab)
            {
                Change<CelesteWallSlideState>();
                base.OnUpdate();
                return;
            }

            //不在地面 靠近墙壁 且 按下了跳跃键 且 可以跳跃
            if (!Owner.celesteCollider.isGrounded && Owner.celesteCollider.isTouchingWall && Owner.input.jump &&
                Owner.canJump)
            {
                Change<CelesteWallJumpState>();
                base.OnUpdate();
                return;
            }
        }

        public override void Change<T>()
        {
            //如果当前状态和要切换的状态相同 则不切换
            if (typeof(T) == currentState.GetType())
            {
                return;
            }

            Debug.Log($"{currentState.GetType().Name}-->{typeof(T).Name}");
            base.Change<T>();
        }
    }
}