using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Nico;
using UnityEngine;
using UnityEngine.AddressableAssets;


namespace OneButtonGame
{
    public class GameManager : SceneSingleton<GameManager>
    {
        public List<GameObject> poolGameObjects;
        public AssetReference mainScene;

        protected override void Awake()
        {
            base.Awake();
            Application.targetFrameRate = 60;
            foreach (var poolGameObject in poolGameObjects)
            {
                PoolGameObjectManager.Instance.Register(poolGameObject);
            }
        }

        public void BackToTitle()
        {
            Addressables.LoadSceneAsync(mainScene);
        }
    }
}