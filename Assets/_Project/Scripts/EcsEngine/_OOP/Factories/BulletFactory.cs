using System.Collections.Generic;
using _Project.Scripts.EcsEngine._OOP.CustomPool;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.Systems.Clear;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using _Project.Scripts.EcsEngine.Enums;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    [UsedImplicitly]
    public sealed class BulletFactory : IInitializable, ICustomInject, IClear
    {
        private readonly EntityManager _entityManager;
        private readonly PrefabProvider _prefabProvider;
        private readonly GameBalance _balance;
        
        private readonly HashSet<Entity> _activeBullets = new ();
        
        private Pool<Entity> _pool;
        
        public BulletFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance)
        {
            _entityManager = entityManager;
            _prefabProvider = prefabProvider;
            _balance = balance;
        }
        
        public void Initialize()
        {
            var config = _balance.BulletFactoryConfig;
            var prefab = _prefabProvider.GetPrefab(config.PrefabKey);

            _pool = new Pool<Entity>(prefab, config.PoolSize, _prefabProvider.PoolsContainer);
        }
        
        public void Spawn(TeamType layerName, Transform firePoint)
        {
            var config = _balance.BulletConfig;
            var entity = _pool.Spawn();
            var stringLayerName = layerName + "Bullet";
            entity.gameObject.layer = LayerMask.NameToLayer(stringLayerName);
            entity.transform.rotation = firePoint.rotation;
            
            entity.Initialize(_entityManager.GetWorld());
            entity.WithData(new TransformView { Value = entity.transform })
                .WithData(new BulletTag())
                .WithData(new Position { Value = firePoint.position })
                .WithData(new MoveSpeed { Value = config.MoveSpeed })
                .WithData(new Damage{ Value = config.Damage})
                .WithData(new LifeTime{ Value = config.LifeTime})
                .WithData(new MoveDirection{ Value = firePoint.forward});
                
                
            _entityManager.Register(entity);
            
            _activeBullets.Add(entity);
        }
        
        public void DeSpawn(int id)
        {
            var entity = _entityManager.Get(id);
            _activeBullets.Remove(entity);
            _entityManager.UnRegister(id);
            _pool.DeSpawn(entity);
        }

        public void Clear()
        {
            _activeBullets.Clear();
            _pool.Clear();
        }
    }
}