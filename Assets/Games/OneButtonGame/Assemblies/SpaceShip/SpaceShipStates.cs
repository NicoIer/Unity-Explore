using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public partial class SpaceShip
    {
        public class RotateState : State<SpaceShip>, IEventListener<ForceButtonDown>
        ,IEventListener<RotateButtonDown>,IEventListener<RotateButtonUp>
        {
            private float startRotateTime;
            private bool rotateButtonDown;
            public override void OnUpdate()
            {
                owner._currentAngel += owner.angelSpeed * Time.deltaTime;
                if (rotateButtonDown)
                {
                    owner._currentAngel+= (Time.time - startRotateTime);
                }

                if (owner._currentAngel > 360)
                {
                    owner._currentAngel -= 360;
                }

                owner.spaceRotate.Rotate(owner._currentAngel, owner.orbitalRadius);
            }

            public override void OnEnter()
            {
                EventManager.Listen<ForceButtonDown>(this);
                EventManager.Listen<RotateButtonDown>(this);
                EventManager.Listen<RotateButtonUp>(this);
                
            }

            public override void OnExit()
            {
                EventManager.UnListen<ForceButtonDown>(this);
                EventManager.UnListen<RotateButtonDown>(this);
                EventManager.UnListen<RotateButtonUp>(this);
            }
            public RotateState(SpaceShip owner) : base(owner)
            {
            }
            public void OnReceiveEvent(RotateButtonDown e)
            {
                startRotateTime = Time.time;
                rotateButtonDown = true;
            }

            public void OnReceiveEvent(RotateButtonUp e)
            {
                rotateButtonDown = false;
            }
            



            public void OnReceiveEvent(ForceButtonDown e)
            {
                owner._stateMachine.Change<PauseState>();
            }


        }

        
        public class PauseState : State<SpaceShip>, IEventListener<ForceButtonUp>
        {
            public PauseState(SpaceShip owner) : base(owner)
            {
            }

            public override void OnEnter()
            {
                owner.spaceRotate.Pause();
                owner._pauseTime = Time.time;
                EventManager.Listen<ForceButtonUp>(this);
            }

            public override void OnExit()
            {
                Debug.Log("PauseState OnExit");
                //向对应方向施加力
                owner.rb2D.velocity += owner.spaceRotate.GetVelocity() * owner.speedMultiplier;
                owner.spaceRotate.Release();
                EventManager.UnListen<ForceButtonUp>(this);

                UniTask.Delay(TimeSpan.FromSeconds(UnityEngine.Random.Range(0.3f, 1f))).ContinueWith(() =>
                {
                    float changeValue = UnityEngine.Random.Range(30, 600) * (Time.time - owner._pauseTime);
                    PlayerModelController.ExpUp(changeValue);
                }).Forget();
            }

            public void OnReceiveEvent(ForceButtonUp e)
            {
                owner._stateMachine.Change<RotateState>();
            }
        }
    }
}