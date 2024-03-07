using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class TransformViewSynchronizer : IEcsPostRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformView, Position>,
            Exc<Inactive, FindTargetRequest>> _filter;
        
        private readonly EcsPoolInject<RotationSpeed> _rotationSpeedPool;
        private readonly EcsPoolInject<Rotation> _rotationPool;

        public void PostRun(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            var rotationPool = _rotationPool.Value;
            var rotationSpeedPool = _rotationSpeedPool.Value;
            
            foreach (var entity in _filter.Value)
            {
                ref var transform = ref _filter.Pools.Inc1.Get(entity);
                var position = _filter.Pools.Inc2.Get(entity);
                
                transform.Value.position = position.Value;

                if (rotationPool.Has(entity))
                {
                    var rotation = rotationPool.Get(entity).Value;
                    var rotationSpeed = rotationSpeedPool.Get(entity).Value;
                    transform.Value.rotation =
                        Quaternion.RotateTowards(transform.Value.rotation, rotation, rotationSpeed * deltaTime);
                }
            }
        }
    }
}