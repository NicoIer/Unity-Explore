namespace OneButtonGame
{
    public class Player : PlanetComponent
    {
        public SimplePlanet simplePlanet;

        private void Start()
        {
            OneButton.OnButtonUp += Attack;
        }

        public void Attack()
        {
            CircleAttackComponent component = ObjectPoolManager.Instance.Get<CircleAttackComponent>();
            component.transform.localScale = simplePlanet.transform.localScale * 0.6f;
            component.Attack(new CircleAttackInfo
            {
                center = simplePlanet.transform,
                radius = simplePlanet.selfRadius * 1.1f,
                angelSpeed = 360,
                startAngel = 0,
                targetAngel = 360,
                damage = 1
            });
        }
    }
}