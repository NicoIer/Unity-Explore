// using System;
// using System.Collections.Generic;
// using System.Runtime.Remoting.Messaging;
// using UnityEngine;
//
// namespace Nico
// {
//     
//     internal static class MonoBehaviourPools<T> where T: MonoBehaviour,IPoolObject
//     {
//         internal static MonoBehaviourPool<T> pool;
//     }
//     internal class MonoBehaviourPool<T> where T: MonoBehaviour, IPoolObject
//     {
//         private readonly Queue<GameObject> _gameObjects = new Queue<GameObject>();
//         private readonly GameObject _prefab;
//         
//         internal MonoBehaviourPool(GameObject prefab,int defaultCount = 0)
//         {
//             if (!prefab.TryGetComponent<T>(out T t))
//             {
//                 throw new ArgumentException($"prefab must have a component of type {typeof(T)}");
//             }
//             this._prefab = prefab;
//             for (int i = 0; i < defaultCount; i++)
//             {
//                 var obj = UnityEngine.Object.Instantiate(_prefab);
//                 Return(obj);
//             }
//         }
//
//         internal T Get()
//         {
//             if (_gameObjects.Count == 0)
//             {
//                 GameObject gameObject = UnityEngine.Object.Instantiate(_prefab);
//                 T monoBehaviour = gameObject.GetComponent<T>();
//                 monoBehaviour.OnSpawn();
//                 return monoBehaviour;
//             }
//             GameObject gameObject2 = _gameObjects.Dequeue();
//             T monoBehaviour2 = gameObject2.GetComponent<T>();
//             monoBehaviour2.OnSpawn();
//             return monoBehaviour2;
//         }
//         
//         internal void Return(GameObject gameObject)
//         {
//             T monoBehaviour = gameObject.GetComponent<T>();
//             monoBehaviour.OnRecycle();
//             _gameObjects.Enqueue(gameObject);
//         }
//
//         internal void Return(T t)
//         {
//             t.OnRecycle();
//             _gameObjects.Enqueue(t.gameObject);
//         }
//     }
// }