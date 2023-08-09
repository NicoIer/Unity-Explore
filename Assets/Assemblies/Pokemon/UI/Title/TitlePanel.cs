using Nico;
using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    public partial class TitlePanel : UIPanel
    {
        [SerializeField] private Button startButton;
        [SerializeField] private Button settingButton;
        [SerializeField] private Button exitButton;

        public override void OnCreate()
        {
            base.OnCreate();
            startButton.onClick.AddListener(OnStartButtonClick);
            settingButton.onClick.AddListener(OnSettingButtonClick);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }
        
        private async void OnStartButtonClick()
        {
            UIManager.Instance.CloseUI<TitlePanel>();
            await GlobalManager.Instance.sceneManager.ToHome();
        }
        
        private async void OnSettingButtonClick()
        {
        }
        
        private void OnExitButtonClick()
        {
        }
        
    }
}