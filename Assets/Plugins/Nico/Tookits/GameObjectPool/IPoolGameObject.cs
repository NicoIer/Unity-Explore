using System.Runtime.CompilerServices;
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void OnSpawn();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void OnRecycle();
    }

    public interface IPoolGameObject : IPoolObject
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public GameObject GetGameObject();
    }
}