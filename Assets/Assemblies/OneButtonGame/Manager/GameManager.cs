using System.Collections.Generic;
using UnityEngine;


namespace OneButtonGame
{
    public class GameManager : MonoBehaviour
    {
        public GameObject simPlanetPrefab;
        public GameObject circleAttackComponent;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            ObjectPoolManager.Instance.Register<SimplePlanet>(simPlanetPrefab);
            ObjectPoolManager.Instance.Register<CircleAttackComponent>(circleAttackComponent);
        }
    }
}