using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    public class TitlePanel : UIPanel
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button exitButton;

        public override void OnCreate()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
            settingButton.onClick.AddListener(OnSettingButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
        
        private void OnStartButtonClick()
        {
            LogManager.LogError("Start Game");
        }
        
        private void OnSettingButtonClick()
        {
            LogManager.LogError("Setting");
        }
        
        private void OnExitButtonClick()
        {
            LogManager.LogError("Exit");
        }
        
    }
}