using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OneButtonGame
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Cloud : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Vector3 _velocity;
        private float _timer;
        private float _currentTimer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }

        public void SetVelocity(Vector2 velocity)
        {
            this._velocity = new Vector3(velocity.x, velocity.y, 0);
        }

        private void FixedUpdate()
        {
            transform.position += _velocity * Time.deltaTime;

            _currentTimer -= Time.deltaTime;
            Color color = _renderer.color;
            color.a = _currentTimer / _timer;
            _renderer.color = color;
            if (_currentTimer <= 0)
            {
                ObjectPoolManager.Return(gameObject);
            }
        }

        private void OnEnable()
        {
            Color color = _renderer.color;
            color.a = 1;
            _renderer.color = color;
            _currentTimer = _timer = Random.Range(3, 5);
        }

        public void OnDisable()
        {
        }
    }
}