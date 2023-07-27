using UnityEngine;

namespace Pokemon
{
    public static class LogManager
    {
        public static void LogError(object msg)
        {
            Debug.Log(msg);
        }
    }
}