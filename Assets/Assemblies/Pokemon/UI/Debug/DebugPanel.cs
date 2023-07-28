using Nico;
using UnityEngine;
using UnityEngine.UI;

namespace Pokemon
{
    //基于UITookit实现的调试面板
    public class DebugPanel : UIPanel
    {
        [SerializeField] private Button closeButton;
        
        [SerializeField] private Toggle enableButterJumpingToggle;
        private PlayerModel playerModel => ModelManager.Get<PlayerModel>();
        public override void OnCreate()
        {
            closeButton.onClick.AddListener(UIManager.Instance.CloseUI<DebugPanel>);
            enableButterJumpingToggle.SetIsOnWithoutNotify(playerModel.celesteMoveParams.enableBetterJumping);
            enableButterJumpingToggle.onValueChanged.AddListener(value =>
            {
               playerModel.celesteMoveParams.enableBetterJumping = value;
            });
        }
        public override UILayer Layer()
        {
            return UILayer.Top;
        }
    }
}