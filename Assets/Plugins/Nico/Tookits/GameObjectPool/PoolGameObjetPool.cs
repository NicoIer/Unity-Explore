using System.Collections.Generic;
using UnityEngine;

namespace OneButtonGame
{
    internal class PoolGameObjetPool
    {
        private readonly Transform _parent;
        private readonly GameObject _prefab;
        private readonly int _capacity;
        private Queue<GameObject> _pool;

        public PoolGameObjetPool(Transform parent, GameObject prefab, int capacity = 100)
        {
            this._parent = parent;
            this._prefab = prefab;
            this._capacity = capacity;
            _pool = new Queue<GameObject>(capacity);
        }

        public GameObject Get()
        {
            if (_pool.Count == 0)
            {
                return Object.Instantiate(_prefab, _parent);
            }

            return _pool.Dequeue();
        }

        public void Return(GameObject gameObject)
        {
            if (_pool.Count < _capacity)
            {
                _pool.Enqueue(gameObject);
                return;
            }

            Object.Destroy(gameObject);
        }
    }
}