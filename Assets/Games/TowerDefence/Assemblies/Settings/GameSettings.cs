using UnityEngine;

namespace TowerDefence
{
    [CreateAssetMenu(fileName = "TowerDefenceSettings", menuName = "Games/TowerDefence/GameSettings")]
    public class GameSettings : SettingScriptableObject
    {
        public string gameVersion = "0.0.0";

        public override void OnLoad()
        {
        }

        public override void OnSave()
        {
        }
    }
}