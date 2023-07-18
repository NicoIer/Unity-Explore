using System;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OneButtonGame
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Cloud : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Rigidbody2D _rb2D;
        private float _timer;
        private float _currentTimer;

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _rb2D = GetComponent<Rigidbody2D>();
        }

        public void SetVelocity(Vector2 velocity)
        {
            // Debug.Log(velocity);
            _rb2D.velocity = velocity;
        }
        private void Update()
        {
            _currentTimer -= Time.deltaTime;
            Color color = _renderer.color;
            color.a = _currentTimer / _timer;
            _renderer.color = color;
            if (_currentTimer <= 0)
            {
                ObjectPoolManager.Return(gameObject);
            }
        }

        private async void OnEnable()
        {
            _renderer.color = Color.white;
            _currentTimer = _timer = Random.Range(3, 5);
        }

        public void OnDisable()
        {
        }
    }
}