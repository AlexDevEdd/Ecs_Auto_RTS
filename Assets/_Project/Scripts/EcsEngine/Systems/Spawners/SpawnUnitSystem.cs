using System.Collections.Generic;
using _Game.Scripts.Tools;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Spawners
{
    internal sealed class SpawnUnitSystem : IEcsRunSystem , IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<UnitSpawnRequest>> _filter = EcsWorlds.Events;
        
        private readonly EcsPoolInject<UnitSpawnRequest> _requestPool= EcsWorlds.Events;
        private readonly EcsPoolInject<UnitSpawnEvent> _eventPool;
        
        private readonly EcsPoolInject<PrepareTime> _prepareTimePool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<SpawnerTag> _spawnTagPool;
        
        private readonly EcsCustomInject<UnitSystem> _unitSystem;
        private readonly EcsCustomInject<EntityManager> _entityManager;
        
        private IReadOnlyCollection<Entity> _spawners;
        private EcsWorldInject _world;
        
        public void Init(IEcsSystems systems)
        {
            _spawners = _entityManager.Value.GetComponents<SpawnerTag>();
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (_world.Value.IsEntityAlive(entity))
                {
                    var request = _requestPool.Value.Get(entity);
                
                    var id = GetRandomEntityByType(request.TeamType, request.UnitType);
                    var position = _positionPool.Value.Get(id);
                    var rotation = _rotationPool.Value.Get(id);
                    var prepareTime = _prepareTimePool.Value.Get(id);
                
                    _eventPool.Value.Add(entity) = new UnitSpawnEvent{Position = position.Value};
                
                    SpawnUnit(request, prepareTime.Value, position.Value, rotation.Value);
                
                    _requestPool.Value.Del(entity);
                }
            }
        }

        private void SpawnUnit(UnitSpawnRequest request, float prepareTime, Vector3 position, Quaternion rotation)
        {
            _unitSystem.Value.Spawn(request.UnitType, request.TeamType, position, rotation,prepareTime).Forget();
        }

        private int GetRandomEntityByType(TeamType teamType, UnitType unitType)
        {
            var entities = new List<Entity>();
            foreach (var entity in _spawners)
            {
                if (entity.GetData<Team>().Value == teamType && entity.GetData<UnitClass>().Value == unitType)
                {
                    entities.Add(entity);
                }
            }

            return entities.GetRandomElement().Id;
        }
    }
}