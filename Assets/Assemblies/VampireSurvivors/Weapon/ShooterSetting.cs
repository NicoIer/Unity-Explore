using System;
using UnityEngine;

namespace VampireSurvivors
{
    [Serializable]
    public struct ShooterSetting
    {
        public GameObject bulletPrefab;
        public string bulletName=> bulletPrefab.name;
        public float coolDown;
    }
}