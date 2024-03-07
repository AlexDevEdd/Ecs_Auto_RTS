using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Movements
{
    internal sealed class UnitRotationSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Rotation, Position, TargetEntity, UnitTag>, Exc<Inactive, FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<Rotation> _rotationPool;
        private readonly EcsPoolInject<Position> _positionPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        
        public void Run(IEcsSystems systems)
        {
            var targetPool = _targetPool.Value;
            var rotationPool = _rotationPool.Value;
            var positionPool = _positionPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref var rotation = ref rotationPool.Get(entity);
                var target = targetPool.Get(entity);

                if (target.Id == default)
                    continue;

                var direction = target.Transform.position - positionPool.Get(entity).Value;
                var targetRotation = Quaternion.LookRotation(direction);
                rotation.Value = targetRotation;
            }
        }
    }
}