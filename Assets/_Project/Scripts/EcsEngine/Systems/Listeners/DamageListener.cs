using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class DamageListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<DamageEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        
        private readonly EcsPoolInject<DamageEvent> _damageEventPool = EcsWorlds.Events;
        private readonly EcsPoolInject<Health> _healthPool;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var damageEvent = _damageEventPool.Value.Get(entity);

                ref var health = ref _healthPool.Value.Get(damageEvent.SourceId);
                health.Value -= damageEvent.Damage;
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}