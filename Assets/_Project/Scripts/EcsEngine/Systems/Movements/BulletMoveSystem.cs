using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Movements
{
    internal sealed class BulletMoveSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MoveSpeed, Position, MoveDirection, BulletTag>,
            Exc<Inactive>> _moveFilter;
        
        public void Run(IEcsSystems systems)
        {
            var deltaTime = Time.deltaTime;
            
            var speedPool = _moveFilter.Pools.Inc1;
            var positionPool = _moveFilter.Pools.Inc2;
            var moveDirectionPool = _moveFilter.Pools.Inc3;
            foreach (var entity in _moveFilter.Value)
            {
                ref var position = ref positionPool.Get(entity);
                var moveDirection = moveDirectionPool.Get(entity); 
                var moveSpeed = speedPool.Get(entity);
                
                position.Value += moveDirection.Value * moveSpeed.Value * deltaTime;
            }
        }
    }
}