using System;
using Nico;
using TMPro;
using UnityEngine;

namespace OneButtonGame
{
    public class ShipInfoPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI angelSpeedText;
        [SerializeField] private TextMeshProUGUI velocityText;
        [SerializeField] private TextMeshProUGUI distanceText;
        public SpaceShip ship=>SpaceShip.Instance;
    
        private void Update()
        {
            angelSpeedText.text = $"Angel: {ship.angelSpeed:F2}";
            velocityText.text = $"Velocity: {ship.velocity:F2}";
            float distance = (TargetPosition.Instance.position()-ship.transform.position).magnitude;
            distanceText.text = $"{(int)distance}m";

            if (distance < 10)
            {
                EventManager.Send(new GameOver());
            }
        }
    }
}