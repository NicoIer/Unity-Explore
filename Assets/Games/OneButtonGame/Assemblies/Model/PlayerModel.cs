using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class PlayerModel : IModel
    {
        public int level;
        public float currentExp;
        public int health = 100;
        public void OnRegister()
        {
            level = 1;
            currentExp = 0;
        }

        public void OnSave()
        {
           
        }
    }
}