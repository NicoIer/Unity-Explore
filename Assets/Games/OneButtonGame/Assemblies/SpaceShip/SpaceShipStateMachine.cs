using Nico;

namespace OneButtonGame
{
    public class SpaceShipStateMachine: StateMachine<SpaceShip>
    {
        public SpaceShipStateMachine(SpaceShip owner) : base(owner)
        {
        }
    }
}