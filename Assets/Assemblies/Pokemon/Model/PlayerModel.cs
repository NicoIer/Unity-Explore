using Nico;
using UnityEngine;

namespace Pokemon
{
    public class PlayerModel : IModel
    {
        public CelesteMoveParams celesteMoveParams;
        public void OnRegister()
        {
            celesteMoveParams = ConfigManager.Instance.GetConfig<CelesteMoveConfig>().moveParams;
            Debug.Log("PlayerModel OnRegister");
        }

        public void OnSave()
        {
            
        }
    }
}