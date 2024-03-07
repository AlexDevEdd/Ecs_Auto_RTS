using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Spawners
{
    internal sealed class SpawnShootVfxSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ProjectileWeapon, ShootEvent>, Exc<Inactive>> _filter;
        
        private readonly EcsPoolInject<ProjectileWeapon> _projectileWeaponPool;
        
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var firePoint = _projectileWeaponPool.Value.Get(entity);
                _vfxSystem.Value.PlayFx(VfxType.Shoot, firePoint.FirePoint.position);
            }
        }
    }
}