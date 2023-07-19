using DG.Tweening;
using Nico;
using UnityEngine;

namespace OneButtonGame
{
    public class SpaceShipTip : MonoBehaviour, IEventListener<ExpChange>, IEventListener<LevelUp>,
        IEventListener<EnemyHitSpaceShip>, IEventListener<HealthChange>
    {
        public SpaceShip ship => SpaceShip.Instance;
        private bool _canPlayHitEffect = true;

        private void OnEnable()
        {
            EventManager.Listen<ExpChange>(this);
            EventManager.Listen<LevelUp>(this);
            EventManager.Listen<EnemyHitSpaceShip>(this);
            EventManager.Listen<HealthChange>(this);
        }

        // private void OnDisable()
        // {
        //     EventManager.UnListen<ExpChange>(this);
        //     EventManager.UnListen<LevelUp>(this);
        //     EventManager.UnListen<EnemyHitSpaceShip>(this);
        // }

        public void OnReceiveEvent(ExpChange e)
        {
            GameObject prompt = ObjectPoolManager.Instance.Get(nameof(Prompt));
            var transform1 = SpaceShip.Instance.spaceRotate.transform;
            prompt.transform.position = transform1.position;
            prompt.transform.SetParent(transform1);
            int changeValue = (int)(e.currentExp - e.previousExp);
            if (e.currentExp - e.previousExp > e.levelNeedExp * 0.4)
            {
                prompt.GetComponent<Prompt>().Print(changeValue.ToString(), Color.green, 2);
            }
            else
            {
                prompt.GetComponent<Prompt>().Print(changeValue.ToString(), Color.green);
            }
        }

        public void OnReceiveEvent(LevelUp e)
        {
            GameObject obj = ObjectPoolManager.Instance.Get(nameof(Prompt));
            Transform parent = SpaceShip.Instance.transform;
            obj.transform.position = parent.position;
            obj.transform.SetParent(parent);
            Prompt prompt = obj.GetComponent<Prompt>();
            prompt.Print($"Level Up! {PlayerModelController.model.level}", Color.red, 3);

            Color color = Color.yellow;
            color.a = ship.render.color.a;
            Color color2 = ship.render.color;
            ship.render.DOColor(color, 0.5f);
            ship.render.DOColor(color2, 0.5f).SetDelay(0.5f);
        }


        public void OnReceiveEvent(HealthChange e)
        {
            GameObject obj = ObjectPoolManager.Instance.Get(nameof(Prompt));
            Transform parent = SpaceShip.Instance.transform;
            obj.transform.position = parent.position;
            obj.transform.SetParent(parent);
            Prompt prompt = obj.GetComponent<Prompt>();
            prompt.Print($"{e.current - e.previous}", Color.gray);

            //受伤效果
            Color color = ship.render.color;
            color.a = e.current / e.maxHealth;
            ship.render.color = color;
        }

        public void OnReceiveEvent(EnemyHitSpaceShip e)
        {
            //受伤特效
            if (_canPlayHitEffect)
            {
                _canPlayHitEffect = false;
                Color color = ship.render.color;
                ship.render.DOFade(color.a*0.5f, 0.3f);
                ship.render.DOFade(color.a, 0.3f).SetDelay(0.3f).OnComplete(() => { _canPlayHitEffect = true; });
            }
        }
    }
}