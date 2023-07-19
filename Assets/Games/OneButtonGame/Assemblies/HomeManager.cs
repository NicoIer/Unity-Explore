using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace OneButtonGame
{
    public class HomeManager: MonoBehaviour
    {
        public Button enterGameButton;

        private void Awake()
        {
            enterGameButton.onClick.AddListener(EnterGame);
        }

        public void EnterGame()
        {
            SceneManager.LoadScene("OneButtonGame-GamePlay");
        }
    }
}