using System;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class TargetPosition: SceneSingleton<TargetPosition>
    {
        private void Start()
        {
            transform.position = transform.position.RandomXYOffset(1000f);
        }
    }
}