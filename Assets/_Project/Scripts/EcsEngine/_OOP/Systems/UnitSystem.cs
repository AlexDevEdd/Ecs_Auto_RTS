using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using _Project.Scripts.EcsEngine._OOP.Factories.Units;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Enums;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems
{
    [UsedImplicitly]
    public sealed class UnitSystem : ICustomInject, IDisposable
    {
        private const float FIND_TARGET_DELAY = 1;
        
        private readonly UnitFactory _unitFactory;
        private readonly EntityManager _entityManager;
        
        private readonly List<Entity> _activeUnits = new ();
        private List<CancellationTokenSource> _tokens = new ();
        
        public UnitSystem(UnitFactory unitFactory, EntityManager entityManager)
        {
            _unitFactory = unitFactory;
            _entityManager = entityManager;
        }

        public async UniTaskVoid Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation)
        {
            var token = new CancellationTokenSource();
            _tokens.Add(token);
            
            var entity = _unitFactory.Spawn(type, teamType, position, rotation);
            _activeUnits.Add(entity);
            
            await UniTask.Delay(TimeSpan.FromSeconds(FIND_TARGET_DELAY), cancellationToken: token.Token);
            
            _tokens.Remove(token);
            token.Dispose();
            
            entity.WithData(new FindTargetRequest());
        }
        
        public async UniTaskVoid Spawn(UnitType type, TeamType teamType, Vector3 position, 
            Quaternion rotation, float prepareTime)
        {
            var token = new CancellationTokenSource();
            _tokens.Add(token);
            
            await UniTask.Delay(TimeSpan.FromSeconds(prepareTime), cancellationToken: token.Token);
            
            var entity = _unitFactory.Spawn(type, teamType, position, rotation);
            _activeUnits.Add(entity);
            
            await UniTask.Delay(TimeSpan.FromSeconds(FIND_TARGET_DELAY), cancellationToken: token.Token);
            
            _tokens.Remove(token);
            token.Dispose();
            
            entity.WithData(new FindTargetRequest());
        }

        public void DeSpawn(int id)
        {
            var entity = _entityManager.Get(id);
            _activeUnits.Remove(entity);
            _unitFactory.DeSpawn(entity);
        }

        public bool IsHasTeam(TeamType teamType)
        {
            return _activeUnits.Any(u => u.GetData<Team>().Value == teamType);
        }

        public Entity GetClosestByTeam(TransformView ownerTransform, TeamType teamType)
        {
            var entity = _activeUnits
                .Where(e => e.GetData<Team>().Value == teamType && !e.HasData<Inactive>())
                .OrderBy(e => Vector3.Distance(ownerTransform.Value.position, e.transform.position))
                .FirstOrDefault();
            
            return entity;
        }

        public void AddTarget(Entity entity)
        {
            _activeUnits.Add(entity);
        }
        
        public void RemoveTarget(Entity entity)
        {
            _activeUnits.Remove(entity);
        }

        public void Dispose()
        {
            for (var i = 0; i < _tokens.Count; i++)
            {
                _tokens[i].Cancel();
                _tokens[i].Dispose();
            }

            _tokens = null;
        }
    }
}