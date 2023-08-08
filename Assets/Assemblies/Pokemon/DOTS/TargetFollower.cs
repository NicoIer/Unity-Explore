using System;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

namespace Pokemon
{

    //将来来实现这个以优化性能
    public struct TargetFollowParallelJob: IJobParallelForTransform
    {
        public void Execute(int index, TransformAccess transform)
        {
            throw new System.NotImplementedException();
        }
    }
    public class TargetFollower: MonoBehaviour
    {
        public Transform target;
        public Vector3 offset;
        public TargetFollowParallelJob parallelJob;
        public bool following;
        public void SetTarget(Transform target)
        {
            following = true;
        }

        private void Update()
        {
            if (following)
            {
                transform.position = target.position + offset;
            }
        }
    }
}