using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Systems;
using _Project.Scripts.EcsEngine.Systems.Listeners;
using _Project.Scripts.EcsEngine.Systems.Movements;
using _Project.Scripts.EcsEngine.Systems.Requests;
using _Project.Scripts.EcsEngine.Systems.Spawners;
using JetBrains.Annotations;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Helpers;
using Zenject;

namespace _Project.Scripts.EcsEngine
{
    [UsedImplicitly]
    public sealed class EcsAdmin : IInitializable, ITickable, IDisposable
    {
        private EcsWorld _world;
        private EcsWorld _events;
        
        private IEcsSystems _systems;
        
        private readonly EntityManager _entityManager;
        
        private readonly object[] _customInjectObjects;
        private readonly HashSet<EcsWorld> _ecsWorlds;
        
        public EcsAdmin(IEnumerable<ICustomInject> customInjects, EntityManager entityManager)
        {
            var injects = customInjects.ToArray();
            _customInjectObjects = injects.OfType<object>().ToArray();
            _entityManager = entityManager;
            
            _ecsWorlds = new HashSet<EcsWorld>();
        }
        
        public void Initialize()
        {
            _world = new EcsWorld();
            _events = new EcsWorld();
            _ecsWorlds.Add(_world);
            _ecsWorlds.Add(_events);
            
            _systems = new EcsSystems(_world);
            
            _systems.AddWorld(_events, EcsWorlds.Events);

            _systems
                .Add(new SpawnUnitSystem())
                .Add(new SpawnUnitVfxListener())
                .Add(new HealthEmptySystem())
                .Add(new SpawnBulletSystem())
                .Add(new SpawnShootVfxSystem())

                .Add(new AttackingRequestSystem())
                .Add(new BulletMoveSystem())
                .Add(new UnitMovementSystem())
                .Add(new UnitRotationSystem())
                .Add(new FindTargetEntitySystem())

                .Add(new HitBoxCollisionRequestSystem())
                .Add(new DeSpawnRequestSystem())
                .Add(new DamageListener())
                .Add(new DeSpawnBulletSystem())
                .Add(new DeSpawnBulletLifeTimeSystem())
                .Add(new SpawnDamagePopUpSystem())

                .Add(new DeathRequestSystem())
                .Add(new SpawnSoulVfxListener())
                .Add(new BuildingDeathListener())
                .Add(new GameOverListener())
                
                .Add(new TransformViewSynchronizer())
                .Add(new AnimatorMovementListener())
                .Add(new AnimatorAttackListener())
                .Add(new AnimatorEnableColliderListener())
                .Add(new AnimatorDisableColliderListener())
                .Add(new AnimatorDeathListener())

#if UNITY_EDITOR

                .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                .DelHere<UnitSpawnEvent>()
                .DelHere<DeathEvent>()
                .DelHere<AttackEvent>()
                .DelHere<ShootEvent>();

            _entityManager.Initialize(_world);
            _systems.Inject(_customInjectObjects);
            _systems.Inject(this);
            _systems.Init(); 
        }

        public void Tick()
        {
            _systems?.Run();
        }

        public void Dispose()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_ecsWorlds.Count > 0) 
                DestroyWorlds();
        }

        private void DestroyWorlds()
        {
            for (var i = 0; i < _ecsWorlds.Count; i++)
            {
                var world = _ecsWorlds.GetEnumerator().Current;
                world?.Destroy();
            }
            
            _events = null;
            _world = null;
        }
        
        public EcsEntityBuilder CreateEntity(string worldName = null)
        {
            return new EcsEntityBuilder(_systems.GetWorld(worldName));
        }
        
        public EcsWorld GetWorld(string worldName = null)
        {
            return worldName switch
            {
                null => _world,
                EcsWorlds.Events => _events,
                _ => throw new Exception($"World with name {worldName} is not found!")
            };
        }
    }
}