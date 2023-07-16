namespace VampireSurvivors
{
    public class PlayerInputManager
    {
        public readonly VampireSurvivorsInputAction input;

        
        public PlayerInputManager()
        {
            input = new VampireSurvivorsInputAction();
        }
        public void Enable()
        {
            input.Enable();
        }
        
        public void Disable()
        {
            input.Disable();
        }
    }
}