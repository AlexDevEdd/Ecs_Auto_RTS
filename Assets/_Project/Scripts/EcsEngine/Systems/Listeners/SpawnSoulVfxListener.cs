using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class SpawnSoulVfxListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnSoulEvent, Inactive>> _filter;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transform = _transformViewPool.Value.Get(entity);
                _filter.Pools.Inc1.Del(entity);
                _vfxSystem.Value.PlayFx(VfxType.Soul, transform.Value.position);
            }
        }
    }
}