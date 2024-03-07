using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using DG.Tweening;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Spawners
{
    internal sealed class DeSpawnRequestSystem : IEcsRunSystem
    {
        private const float FALL_DOWN_POS_Y = -1f;
        private const float FALL_DOWN_DURATION = 1f;
        
        private readonly EcsFilterInject<Inc<DeSpawnRequest, Inactive>> _filter;
        
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<SpawnSoulEvent> _eventPool;
        
        private readonly EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _filter.Pools.Inc1.Del(entity);
                _eventPool.Value.Add(entity) = new SpawnSoulEvent();
                DeSpawnProcess(entity);
            }
        }

        private void DeSpawnProcess(int id)
        {
            ref var transform = ref _transformViewPool.Value.Get(id);
            transform.Value.DOMoveY(FALL_DOWN_POS_Y, FALL_DOWN_DURATION)
               .OnComplete(() => DeSpawn(id));
        }

        private void DeSpawn(int id)
        {
            _unitSystem.Value.DeSpawn(id);
        }
    }
}