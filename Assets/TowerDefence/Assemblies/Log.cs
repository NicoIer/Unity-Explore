using System;
using UnityEngine;

namespace TowerDefence
{
    public static class Log
    {
        public static Action<object> Info = Debug.Log;
    }
}