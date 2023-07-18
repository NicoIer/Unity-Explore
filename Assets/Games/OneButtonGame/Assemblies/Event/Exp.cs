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
}