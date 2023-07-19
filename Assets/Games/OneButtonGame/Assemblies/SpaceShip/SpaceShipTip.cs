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
            EventManager.Register<ExpChange>(this);
            EventManager.Register<LevelUp>(this);
            EventManager.Register<EnemyHitSpaceShip>(this);
            EventManager.Register<HealthChange>(this);
        }

        // private void OnDisable()
        // {
        //     EventManager.UnListen<ExpChange>(this);
        //     EventManager.UnListen<LevelUp>(this);
        //     EventManager.UnListen<EnemyHitSpaceShip>(this);
        // }

        public void OnReceiveEvent(ExpChange e)
        {
            GameObject prompt = ObjectPoolManager.Get(nameof(Prompt));
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
            GameObject obj = ObjectPoolManager.Get(nameof(Prompt));
            Transform parent = SpaceShip.Instance.transform;
            obj.transform.position = parent.position;
            obj.transform.SetParent(parent);
            Prompt prompt = obj.GetComponent<Prompt>();
            prompt.Print($"Level Up! {PlayerModelController.model.level}", Color.red, 3);
        }


        public void OnReceiveEvent(HealthChange e)
        {
            GameObject obj = ObjectPoolManager.Get(nameof(Prompt));
            Transform parent = SpaceShip.Instance.transform;
            obj.transform.position = parent.position;
            obj.transform.SetParent(parent);
            Prompt prompt = obj.GetComponent<Prompt>();
            prompt.Print($"{e.current - e.previous}", Color.gray);
        }

        public void OnReceiveEvent(EnemyHitSpaceShip e)
        {
            //受伤特效
            if (_canPlayHitEffect)
            {
                _canPlayHitEffect = false;
                ship.render.DOFade(0.5f, 0.3f);
                ship.render.DOFade(1, 0.3f).SetDelay(0.3f).OnComplete(() => { _canPlayHitEffect = true; });
            }
        }
    }
}