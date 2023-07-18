using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneButtonGame
{
    public enum SpaceShipState
    {
        Rotate,
        SpeedUp,
        Pause,
    }

    [RequireComponent(typeof(Rigidbody2D))]
    public partial class SpaceShip : MonoBehaviour, IEventListener<OneButtonDown>, IEventListener<OneButtonUp>,
        IEventListener<LevelUp>, IEventListener<ExpChange>
    {
        public float forceRate = 1000;
        public float angelSpeed = 5; //角速度
        private float _currentAngel = 0;
        public float radius = 10;
        private SpaceShipState _state = SpaceShipState.Rotate;
        private Rigidbody2D _rb2D;
        public SpaceRotate spaceRotate;
        private float _pauseTime;
        private SpaceShipStateMachine _stateMachine;

        private void Awake()
        {
            spaceRotate = GetComponentInChildren<SpaceRotate>();
            _rb2D = GetComponent<Rigidbody2D>();
            EventManager.Listen<OneButtonDown>(this);
            EventManager.Listen<OneButtonUp>(this);
            EventManager.Listen<LevelUp>(this);
            EventManager.Listen<ExpChange>(this);
            
            _stateMachine = new SpaceShipStateMachine(this);
            _stateMachine.Add(new RotateState(this));
            _stateMachine.Add(new PauseState(this));
            
            _stateMachine.Start<RotateState>();
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (spaceRotate == null)
            {
                spaceRotate = GetComponentInChildren<SpaceRotate>();
            }
            spaceRotate.Modify(radius);
        }
#endif
        

        private void Update()
        {
            _stateMachine.OnUpdate();
        }


        public void OnReceiveEvent(OneButtonDown e)
        {
            _stateMachine.Change<PauseState>();
        }

        public void OnReceiveEvent(OneButtonUp e)
        {
            // Debug.Log("OneButtonUp");
            _stateMachine.Change<RotateState>();
        }

        public void OnReceiveEvent(LevelUp e)
        {
            GameObject obj = ObjectPoolManager.Get(nameof(Prompt));
            obj.transform.position = transform.position.RandomXYOffset(4);
            Prompt prompt = obj.GetComponent<Prompt>();
            prompt.Print($"Level Up! {PlayerModelController.model.level}", Color.red, 2);
        }

        public void OnReceiveEvent(ExpChange e)
        {
            //生成一个提示信息
            GameObject prompt = ObjectPoolManager.Get(nameof(Prompt));
            prompt.transform.position = spaceRotate.transform.position;
            int changeValue = (int)(e.currentExp - e.previousExp);
            if (e.currentExp - e.previousExp > e.levelNeedExp * 0.4)
            {
                prompt.GetComponent<Prompt>().Print(changeValue.ToString(), Color.green, 2);
            }
            else
            {
                prompt.GetComponent<Prompt>().Print(changeValue.ToString(), Color.green);
            }
        }
    }
}