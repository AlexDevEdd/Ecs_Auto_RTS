using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class SpawnUnitVfxListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitSpawnEvent>> _filter;
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var @event = _filter.Pools.Inc1.Get(entity);
                PlayVfx(@event.Position);
            }
        }

        private void PlayVfx(Vector3 position)
        {
            _vfxSystem.Value.PlayFx(VfxType.Spawn, position);
        }
    }
}