using Nico;
using UnityEngine;

namespace OneButtonGame
{
    [AddComponentMenu("OneButtonGame/Player")]
    public class Player : SceneSingleton<Player>
    {
        public Rigidbody2D rb2D;
        public float forceRate = 10f;
        public float maxSpeed = 30f;
        public float gravity = 10f;

        protected override void Awake()
        {
            base.Awake();
            rb2D = GetComponent<Rigidbody2D>();
        }

        protected void Update()
        {
            Vector2 move = InputManager.Instance.move * forceRate;
            move.y -= gravity;
            Vector2 velocity = rb2D.velocity;
            velocity += move;
            rb2D.velocity = velocity;
            rb2D.velocity = Vector2.ClampMagnitude(velocity, maxSpeed);
        }
    }
}