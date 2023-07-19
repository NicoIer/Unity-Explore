using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneButtonGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class SpaceShip : SceneSingleton<SpaceShip>, IEventListener<OneButtonDown>,
        IEventListener<OneButtonUp>,
        IEventListener<LevelUp>, IEventListener<EnemyHitSpaceShip>, IEventListener<HealthChange>
    {
        public float velocityRate = 40;
        public float angelSpeed = 5; //角速度
        public float slowRate = 0.9f;
        private float _currentAngel;
        public float orbitalRadius = 10;
        public float radius = 0.5f;
        public Rigidbody2D rb2D { get; private set; }
        public Vector2 velocity => rb2D.velocity;
        public SpaceRotate spaceRotate;
        private float _pauseTime;
        private SpaceShipStateMachine _stateMachine;
        public SpriteRenderer render { get; private set; }

        public Transform maskTransform;

        protected override void Awake()
        {
            base.Awake();

            spaceRotate = GetComponentInChildren<SpaceRotate>();
            rb2D = GetComponent<Rigidbody2D>();
            render = GetComponent<SpriteRenderer>();

            _stateMachine = new SpaceShipStateMachine(this);
            _stateMachine.Add(new RotateState(this));
            _stateMachine.Add(new PauseState(this));

            _stateMachine.Start<RotateState>();
        }

        private void OnEnable()
        {
            EventManager.Register<OneButtonDown>(this);
            EventManager.Register<OneButtonUp>(this);
            EventManager.Register<LevelUp>(this);
            EventManager.Register<EnemyHitSpaceShip>(this);
        }

        private void OnDisable()
        {
            EventManager.UnListen<OneButtonDown>(this);
            EventManager.UnListen<OneButtonUp>(this);
            EventManager.UnListen<LevelUp>(this);
            EventManager.UnListen<EnemyHitSpaceShip>(this);
        }

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
            _stateMachine.Change<RotateState>();
        }

        public void OnReceiveEvent(LevelUp e)
        {
            render.DOColor(Color.yellow, 0.5f);
            render.DOColor(Color.white, 0.5f).SetDelay(0.5f);
            angelSpeed += 5f;
            velocityRate += 5f;
        }

        public void OnReceiveEvent(HealthChange e)
        {
            maskTransform.transform.localScale = Vector3.one * e.current / e.maxHealth;
        }

        public void OnReceiveEvent(EnemyHitSpaceShip e)
        {
            Vector3 vecDir = (transform.position - e.pos).normalized;
            //计算动量 速度*质量 速度 *power
            Vector3 momentum = vecDir * ((e.speed - velocity.magnitude) * e.hitPower);
            Vector2 momentum2D = new Vector2(momentum.x, momentum.y);
            //自己减速
            rb2D.velocity += momentum2D / rb2D.mass;
        }


#if UNITY_EDITOR

        private void OnGUI()
        {
            GUILayout.Label($"velocity:{velocity}");
        }

        private void OnValidate()
        {
            if (spaceRotate == null)
            {
                spaceRotate = GetComponentInChildren<SpaceRotate>();
            }

            spaceRotate.Modify(orbitalRadius);
        }

        private void OnDrawGizmos()
        {
            var position = transform.position;
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(position, orbitalRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(position, radius);
        }
#endif
    }
}