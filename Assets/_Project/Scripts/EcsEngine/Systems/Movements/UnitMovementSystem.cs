using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.AttackComponents;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Movements
{
    internal sealed class UnitMovementSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveSpeed, Position, TargetEntity, AttackDistance, MoveDirection>,
            Exc<Inactive, AttackRequest, FindTargetRequest>> _moveFilter;
        
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPool;
        private readonly EcsPoolInject<AttackRequest> _attackingRequestPool;
        private readonly EcsPoolInject<AttackDistance> _attackDistancePool;
        private readonly EcsPoolInject<MoveDirection> _moveDirectionPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<Reached> _reachedPool;
        
        private EcsCustomInject<EntityManager> _entityManager;
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            var speedPool = _moveFilter.Pools.Inc1;
            var positionPool = _moveFilter.Pools.Inc2;
            
            foreach (var entity in _moveFilter.Value)
            {
                ref var moveDirection = ref _moveDirectionPool.Value.Get(entity); 
                var target = _targetPool.Value.Get(entity);
                
                if (_world.Value.IsEntityAlive(target.Id))
                {
                    var targetHealth = _healthPool.Value.Get(target.Id);

                    if (targetHealth.Value <= 0)
                    {
                        moveDirection.Value = Vector3.zero;
                        _targetPool.Value.Del(entity);
                        _findTargetRequestPool.Value.Add(entity) = new FindTargetRequest();
                        continue;
                    }
                    
                    ref var position = ref positionPool.Get(entity);
                    var moveSpeed = speedPool.Get(entity);

                    var direction = target.Transform.position - position.Value;
                    var distance = direction.magnitude;
                    var normalizedDirection = direction / distance;

                    if (distance > _attackDistancePool.Value.Get(entity).Value)
                    {
                        moveDirection.Value = normalizedDirection * moveSpeed.Value * deltaTime;
                        position.Value += normalizedDirection * moveSpeed.Value * deltaTime;
                    }
                    else
                    {
                        ref var isReached = ref _reachedPool.Value.Get(entity);
                        isReached.IsReached = true;
                        
                        moveDirection.Value = Vector3.zero;
                        _attackingRequestPool.Value.Add(entity) = new AttackRequest{TargetId = target.Id};
                    } 
                }
            }
        }
    }
}