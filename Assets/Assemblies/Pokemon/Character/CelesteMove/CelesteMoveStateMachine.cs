using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class CelesteMoveStateMachine : StateMachine<CelesteMove>
    {
        const double TOLERANCE = 0.01;
        private bool _lockedState = false;
        private CancellationTokenSource _lockCts;

        public CelesteMoveStateMachine(CelesteMove owner) : base(owner)
        {
            Add(new CelesteIdleState(owner));
            Add(new CelesteWalkState(owner));

            Add(new CelesteJumpState(owner));
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

            if (_lockedState)
            {
                Debug.Log("Locked");
                base.OnUpdate();
                return;
            }
            
            //在地上 且 按下了移动键 且 没有按下跳跃键 且当前<=0
            if (Owner.celesteCollider.isGrounded && Owner.input.hasXMovement && !Owner.input.jump &&
                Owner.rb.velocity.y <= TOLERANCE && !Owner.input.dash)
            {
                Change<CelesteWalkState>();
                base.OnUpdate();
                return;
            }

            //可以冲刺 按下了 冲刺键
            if (Owner.input.dash && Owner.canDash)
            {
                Debug.Log("dash");
                Change<CelesteDashState>();
                base.OnUpdate();
                return;
            }
            
            //在地上 且 没有按下移动键 且 没有按下跳跃键 且 没有向上的速度
            if (Owner.celesteCollider.isGrounded && !Owner.input.hasXMovement && !Owner.input.jump &&
                Owner.rb.velocity.y <= TOLERANCE)
            {
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

            //在空中 且 可以跳 且 没有按下跳跃键 且 跳跃次数=0 
            //这种情况出现在: 1从悬崖掉下去
            if (Owner.inAir && Owner.canJump && Owner.input.jump)
            {
                Change<CelesteJumpState>();
                base.OnUpdate();
                return;
            }

            //不在地面上 且 向上的速度>0 且 没有按下跳跃键
            if (!Owner.celesteCollider.isGrounded && Owner.rb.velocity.y > TOLERANCE && !Owner.input.jump)
            {
                Change<CelesteUpState>();
                base.OnUpdate();
                return;
            }

            //不在地面 靠近墙壁 且 按下了跳跃键 且 可以跳跃 且 有反墙方向的输入
            if (!Owner.celesteCollider.isGrounded && Owner.celesteCollider.isTouchingWall && Owner.input.jump &&
                Owner.canJump)
            {
                Change<CelesteWallJumpState>();
                base.OnUpdate();
                LockState(Owner.config.wallJumpLockTime);
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

            //靠墙 有反墙方向的输入 且 没有按下跳跃键 且 没有按下 抓墙键 且当前没有向上的速度
            if (Owner.celesteCollider.isTouchingWall && Owner.HasInverseXInput() && !Owner.input.jump &&
                !Owner.input.wallGrab && Owner.rb.velocity.y <= 0)
            {
                Change<CelesteDownState>();
                base.OnUpdate();
                return;
            }

            //不在地面 靠近墙壁 且 没有按下跳跃键 且 没有按下 抓墙键 且当前没有向上的速度
            if (!Owner.celesteCollider.isGrounded && Owner.celesteCollider.isTouchingWall && !Owner.input.jump &&
                !Owner.input.wallGrab && Owner.rb.velocity.y <= 0 && !Owner.HasInverseXVelocity() &&
                currentState is not CelesteWallSlideState)
            {
                Change<CelesteWallSlideState>();
                base.OnUpdate();
                return;
            }

            if (Owner.inAir && Owner.rb.velocity.y <= TOLERANCE)
            {
                Change<CelesteDownState>();
                base.OnUpdate();
                return;
            }
        }

        //锁定某个状态 在时间结束前 不更新
        public void LockState(float timer)
        {
            _lockCts?.Cancel();
            _lockedState = true;
            _lock_state_task(timer, _lockCts = new CancellationTokenSource()).Forget();
        }

        private async UniTask _lock_state_task(float timer, CancellationTokenSource cts)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(timer), cancellationToken: cts.Token);
            _lockedState = false;
            Debug.Log("unlocked");
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