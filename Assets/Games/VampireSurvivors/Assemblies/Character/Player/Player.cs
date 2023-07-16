using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace VampireSurvivors
{
    public class Player : SceneSingleton<Player>
    {
        public PlayerSetting setting;
        private PlayerInputManager _inputManager;
        private MoveComponent _moveComponent;
        public List<ShooterComponent> shooterComponents;

        protected override void Awake()
        {
            base.Awake();
            _inputManager = new PlayerInputManager();

            shooterComponents = new List<ShooterComponent>();
            ShooterComponent shooterComponent = new ShooterComponent(setting.shooterSetting, FindEnemy);
            shooterComponents.Add(shooterComponent);

            _moveComponent = new MoveComponent(transform)
            {
                speed = setting.speed
            };
        }
        private void OnEnable()
        {
            _inputManager.Enable();
        }

        private void OnDisable()
        {
            _inputManager.Disable();
        }

        private void Update()
        {
            _moveComponent.move = _inputManager.input.Player.Move.ReadValue<Vector2>();
            _moveComponent.OnUpdate();

            foreach (var shooterComponent in shooterComponents)
            {
                shooterComponent.Shoot(transform.position);
            }
        }
        
        private Transform FindEnemy()
        {
            Enemy enemy = Enemy.enemies.Random();
            if (enemy != null)
            {
                return enemy.transform;
            }

            return null;
        }
    }
}