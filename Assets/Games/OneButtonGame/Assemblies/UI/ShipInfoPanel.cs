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
        public SpaceShip ship=>SpaceShip.Instance;
    
        private void Update()
        {
            angelSpeedText.text = $"Angel: {ship.angelSpeed:F2}";
            velocityText.text = $"Velocity: {ship.velocity:F2}";
        }
    }
}