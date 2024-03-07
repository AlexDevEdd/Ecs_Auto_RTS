using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class AnimatorDisableColliderListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AnimatorView, WeaponColliderView, DisableColliderEvent>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<DisableColliderEvent> _eventPool;
        private readonly EcsPoolInject<WeaponColliderView> _colliderViewPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _eventPool.Value.Del(entity);
                ref var colliderView = ref _colliderViewPool.Value.Get(entity);
                colliderView.Value.enabled = false;
            }
        }
    }
}