using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class DeathRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DeathRequest>, Exc<Inactive>> _filter;

        private readonly EcsPoolInject<ColliderView> _colliderViewPool;
        private readonly EcsPoolInject<DeathEvent> _eventPool;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                
                ref var collider = ref _colliderViewPool.Value.Get(entity);
                collider.Value.enabled = false;
                
                _inactivePool.Value.Add(entity) = new Inactive();
                _eventPool.Value.Add(entity) = new DeathEvent();
            }
        }
    }
}
