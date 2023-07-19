using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Nico;
using UnityEngine;
using UnityEngine.Serialization;

namespace OneButtonGame
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class SpaceShip : SceneSingleton<SpaceShip>, IEventListener<LevelUp>,
        IEventListener<EnemyHitSpaceShip>
    {
        public float angelSpeed = 5; //角速度
        private float _currentAngel;
        public float orbitalRadius = 10;
        public float radius = 0.5f;
        public float slowTimer;
        public float slowNeedTime = 4f;
        public float speedMultiplier =1.5f;
        public Rigidbody2D rb2D { get; private set; }
        public Vector2 velocity => rb2D.velocity;
        public SpaceRotate spaceRotate { get; private set; }
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
            EventManager.Listen<LevelUp>(this);
            EventManager.Listen<EnemyHitSpaceShip>(this);
        }

        private void OnDisable()
        {
            EventManager.UnListen<LevelUp>(this);
            EventManager.UnListen<EnemyHitSpaceShip>(this);
            _stateMachine = null;
        }

        private void Update()
        {
            _stateMachine.OnUpdate();
            slowTimer += Time.deltaTime;
            if (slowTimer >slowNeedTime)
            {
                rb2D.velocity *= 0.98f;
            }
        }

        public void OnReceiveEvent(LevelUp e)
        {
            angelSpeed += 5f;
            speedMultiplier += 0.2f;
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