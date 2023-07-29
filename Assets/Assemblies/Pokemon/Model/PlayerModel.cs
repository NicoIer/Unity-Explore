using Nico;
using UnityEngine;

namespace Pokemon
{
    public class PlayerModel : IModel
    {
        public CelesteMoveParams celesteMoveParams;
        private bool _canJump = true;
        private bool _canDash = true;

        public bool canJump
        {
            get
            {
                if (infiniteJump) return true;
                return _canJump;
            }
            set => _canJump = value;
        }

        public bool canDash
        {
            get
            {
                if (infiniteDash)
                {
                    return true;
                }

                return _canDash;
            }
            set => _canDash = value;
        }

        public bool infiniteJump = false;
        public bool infiniteDash = false;

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