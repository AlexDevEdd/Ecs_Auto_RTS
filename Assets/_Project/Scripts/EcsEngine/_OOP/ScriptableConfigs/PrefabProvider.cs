using System;
using Leopotam.EcsLite.Entities;
using Plugins.Tools;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.ScriptableConfigs
{
    [Serializable]
    public class PrefabProvider : ICustomInject
    {
        public Transform PoolsContainer;
        public Transform WorldSpaceContainer;
        
        [Title("Prefab References", TitleAlignment = TitleAlignments.Centered)]
        [Space, SerializeField]
        private SerializableDictionary<string, Entity> _prefabReferences;
        
        public Entity GetPrefab(string key)
        {
            if (_prefabReferences.TryGetValue(key, out var prefab))
                return prefab;
        
            throw new ArgumentException($"Doesn't exist KEY of {key}");
        }
        
        public T GetPrefab<T>(string key) where T : Entity
        {
            if (_prefabReferences.TryGetValue(key, out var prefab))
                return prefab as T;
        
            throw new ArgumentException($"Doesn't exist KEY of {key}");
        }
    }
}