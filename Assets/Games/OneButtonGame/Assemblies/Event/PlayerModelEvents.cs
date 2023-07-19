using Nico;

namespace OneButtonGame
{
    public struct ExpChange: IEvent
    {
        public float previousExp;
        public float currentExp;
        public float levelNeedExp;
    }
    
    public struct LevelUp: IEvent
    {
        public int level;
    }
    
    public struct HealthChange: IEvent
    {
        public int previous;
        public int current;
        public int maxHealth;
    }
}