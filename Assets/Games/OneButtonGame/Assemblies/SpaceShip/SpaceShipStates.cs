using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public partial class SpaceShip
    {
        public class RotateState : State<SpaceShip>
        {
            public RotateState(SpaceShip owner) : base(owner)
            {
            }

            public override void OnUpdate()
            {
                owner._currentAngel += owner.angelSpeed * Time.deltaTime;
                if (owner._currentAngel > 360)
                {
                    owner._currentAngel -= 360;
                }

                owner.spaceRotate.Rotate(owner._currentAngel, owner.orbitalRadius);
                owner.rb2D.velocity *= owner.slowRate;
            }
        }

        public class PauseState : State<SpaceShip>
        {
            public PauseState(SpaceShip owner) : base(owner)
            {
            }

            public override void OnEnter()
            {
                owner.spaceRotate.Pause();
                owner._pauseTime = Time.time;
            }

            public override async void OnExit()
            {
                // Debug.Log("PauseState OnExit");
                //向对应方向施加力
                owner.rb2D.velocity += owner.spaceRotate.GetVelocity(owner.velocityRate);
                owner.spaceRotate.Release();
                await UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.3f, 1f)));

                float changeValue = UnityEngine.Random.Range(30, 600) * (Time.time - owner._pauseTime);
                PlayerModelController.ExpUp(changeValue);
            }
        }
    }
}