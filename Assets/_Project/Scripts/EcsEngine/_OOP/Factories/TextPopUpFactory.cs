using _Project.Scripts.EcsEngine._OOP.CustomPool;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    [UsedImplicitly]
    public class TextPopUpFactory : IInitializable, ICustomInject
    {
        private const string PREFAB_KEY = "DamagaTestPopUp";
        private const int POOL_SIZE = 8;
        
        private readonly PrefabProvider PrefabProvider;
        private Pool<DamageTextPopUp> _pool;
        
        protected TextPopUpFactory(PrefabProvider prefabProvider)
        {
            PrefabProvider = prefabProvider;
        }

        public void Initialize()
        {
            CreatePool();
        }

        private void CreatePool()
        {
            var prefab = PrefabProvider.GetPrefab<DamageTextPopUp>(PREFAB_KEY);
            _pool = new Pool<DamageTextPopUp>(prefab, POOL_SIZE, PrefabProvider.WorldSpaceContainer);
        }
        
        public void Spawn(string text, Vector3 position)
        {
            var popUp = _pool.Spawn();
            popUp.OnRemove += Remove;
            popUp.Show(text, position);
        }

        private void Remove(DamageTextPopUp popUp)
        {
            popUp.OnRemove -= Remove;
            _pool.DeSpawn(popUp);
        }
    }
}