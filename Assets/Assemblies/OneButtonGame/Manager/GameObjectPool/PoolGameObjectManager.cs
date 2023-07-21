using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace OneButtonGame
{
    public class PoolGameObjectManager : SceneSingleton<PoolGameObjectManager>
    {
#if UNITY_EDITOR
        [field: SerializeReference]
#endif
        private readonly Dictionary<Type, PoolGameObjetPool> _pool = new Dictionary<Type, PoolGameObjetPool>();
        
        public void Register(GameObject prefab)
        {
            if (!prefab.TryGetComponent(out IPoolGameObject poolGameObject))
            {
                Debug.LogWarning($"Prefab does not contain the component: {typeof(IPoolGameObject)}");
                return;
            }

            _pool.Add(poolGameObject.GetType(), new PoolGameObjetPool(transform, prefab));
        }

        public T Get<T>() where T : IPoolGameObject
        {
            T poolGameObject = _pool[typeof(T)].Get().GetComponent<T>();
            poolGameObject.state = PoolObjectState.Spawned;
            poolGameObject.OnSpawn();
            return poolGameObject;
        }

        public void Return<T>(T obj) where T : IPoolGameObject
        {
            obj.state = PoolObjectState.Recycled;
            obj.OnRecycle();
            _pool[typeof(T)].Return(obj.GetGameObject());
        }
    }
}