using UnityEngine;

namespace OneButtonGame
{
    public enum PoolObjectState
    {
        Spawned,
        Recycled
    }

    public interface IPoolObject
    {
        public PoolObjectState state { get; internal set; }
        void OnSpawn();
        void OnRecycle();
    }
    public interface IPoolGameObject: IPoolObject
    {
        public GameObject GetGameObject();
    }
}