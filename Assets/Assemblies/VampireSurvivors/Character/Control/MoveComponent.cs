using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VampireSurvivors
{
    public class MoveComponent
    {
        public Vector2 move;
        public float speed;
        private Transform _transform;

        public MoveComponent(Transform transform)
        {
            this._transform = transform;
        }
        public void OnUpdate()
        {
            if (move == Vector2.zero)
            {
                return;
            }
            _transform.position += new Vector3(move.x, move.y, 0) * (Time.deltaTime * speed);
        }
    }
}

