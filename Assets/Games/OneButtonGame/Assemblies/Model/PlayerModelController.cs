using Nico;

namespace OneButtonGame
{
    public static class PlayerModelController
    {
        public static PlayerModel model => ModelManager.Get<PlayerModel>();
        public static void ExpUp(float exp)
        {
            float previousExp = model.currentExp;
            float levelNeedExp = GetLevelNeedExp();
            
            model.currentExp+=exp;
            EventManager.Send<ExpChange>(new ExpChange()
            {
                previousExp = previousExp,
                currentExp = model.currentExp,
                levelNeedExp = levelNeedExp,
            });
            if (model.currentExp >= levelNeedExp)
            {
                model.currentExp -= levelNeedExp;
                model.level++;
                EventManager.Send<LevelUp>(new LevelUp()
                {
                    level = model.level,
                });
            }
        }
        
        public static float GetLevelNeedExp()
        {
            return 1000 * model.level;
        }
    }
}