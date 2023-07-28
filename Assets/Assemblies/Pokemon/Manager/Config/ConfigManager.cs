using System;
using System.Collections.Generic;
using Nico;
using UnityEngine;

namespace Pokemon
{
    public class ConfigManager : GlobalSingleton<ConfigManager>
    {
        [SerializeField] private List<ScriptableConfig> configs;
        private Dictionary<Type, ScriptableConfig> _configsDict;

        protected override void Awake()
        {
            base.Awake();
            _configsDict = new Dictionary<Type, ScriptableConfig>();
            foreach (var config in configs)
            {
                _configsDict[config.GetType()] = config;
            }
        }

        public T GetConfig<T>() where T : ScriptableConfig
        {
            return _configsDict[typeof(T)] as T;
        }
    }
}