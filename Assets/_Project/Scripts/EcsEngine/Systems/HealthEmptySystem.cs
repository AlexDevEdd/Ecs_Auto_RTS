using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class HealthEmptySystem : IEcsRunSystem
    {
        private const int ZERO_HEALTH = 0;
        
        private EcsFilterInject<Inc<Health>, Exc<DeathRequest, Inactive>> _filter;
        
        private readonly EcsPoolInject<Health> _healthPool;
        private readonly EcsPoolInject<DeathRequest> _deathPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var health = _healthPool.Value.Get(entity);
                if (health.Value <= ZERO_HEALTH)
                {
                    _targetPool.Value.Del(entity);
                    _deathPool.Value.Add(entity) = new DeathRequest();
                }
            }
        }
    }
}