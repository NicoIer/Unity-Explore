using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace OneButtonGame
{
    public class InfoPanel : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void Update()
        {
            text.text = $"{math.abs(Player.Instance.transform.position.y - 0):F2}m";
        }
    }
}