using UnityEngine;

namespace OneButtonGame
{
    public class GameManager: MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
        }

    }
}