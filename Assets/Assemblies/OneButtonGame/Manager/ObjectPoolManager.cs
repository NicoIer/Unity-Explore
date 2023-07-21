using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace OneButtonGame
{
    public class ObjectPoolManager : SceneSingleton<ObjectPoolManager>
    {
        [field: SerializeReference ]private readonly Dictionary<string, GameObjectPool> _normalObjectPools =
            new Dictionary<string, GameObjectPool>();

        private readonly Dictionary<Type, GameObjectPool>
            _componentObjectPools = new Dictionary<Type, GameObjectPool>();

        public void Register(GameObject prefab)
        {
            _normalObjectPools.Add(prefab.name, new GameObjectPool(null, prefab));
        }

        public void Register<T>(GameObject prefab) where T : Component
        {
            _componentObjectPools.Add(typeof(T), new GameObjectPool(null, prefab));
        }

        public GameObject Get(string name)
        {
            return _normalObjectPools[name].Get();
        }

        public T Get<T>() where T : Component
        {
            return _componentObjectPools[typeof(T)].Get().GetComponent<T>();
        }

        public void Return(GameObject gameObject)
        {
            _normalObjectPools[gameObject.name].Return(gameObject);
        }

        public void Return<T>(T component) where T : Component
        {
            _componentObjectPools[typeof(T)].Return(component.gameObject);
        }
    }
    
    internal class GameObjectPool
    {
        private readonly Transform _parent;
        private readonly GameObject _prefab;
        private int _capacity;
        private readonly Stack<GameObject> _pool = new Stack<GameObject>();

        public GameObjectPool(Transform parent, GameObject prefab, int capacity = 100)
        {
            this._prefab = prefab;
            this._capacity = capacity;
            this._parent = parent;
        }

        public GameObject Get()
        {
            if (_pool.Count > 0)
            {
                GameObject obj =  _pool.Pop();
                obj.name = _prefab.name;
                obj.SetActive(true);
                return obj;
            }

            GameObject obj2= Object.Instantiate(_prefab);
            obj2.SetActive(true);
            obj2.name = _prefab.name;
            return obj2;
        }

        public void Return(GameObject gameObject)
        {
            if (_capacity > 0 && _pool.Count >= _capacity)
            {
                Object.Destroy(gameObject);
                return;
            }
            
            gameObject.transform.SetParent(_parent);
            gameObject.SetActive(false);
            gameObject.name = _prefab.name;
            _pool.Push(gameObject);
        }
    }
}