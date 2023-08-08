using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace Pokemon
{
    [RequireComponent(typeof(ParticleSystem))]
    public class DashEffect : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _targetRenderer;
        private ParticleSystem _particleSystem;
        private ParticleSystemRenderer _renderer;
        private bool _isPlaying;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _renderer = _particleSystem.GetComponent<ParticleSystemRenderer>();
        }

        public void SetTarget(SpriteRenderer renderer)
        {
            this._targetRenderer = renderer;
        }

        public void Play()
        {
            _isPlaying = true;
            _particleSystem.textureSheetAnimation.SetSprite(0, _targetRenderer.sprite);
            _particleSystem.Play();
        }

        private void Update()
        {
            if (!_isPlaying) return;
            if (_targetRenderer.flipX)
            {
                _renderer.flip = new Vector3(1,0,0);
            }
            else
            {
                _renderer.flip = Vector3.zero;
            }

            _particleSystem.textureSheetAnimation.SetSprite(0, _targetRenderer.sprite);
            if (!_particleSystem.IsAlive())
            {
                _isPlaying = false;
            }
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }
    }
}