using _Project.Scripts.EcsEngine._OOP.CustomPool;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Views;
using JetBrains.Annotations;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories
{
    [UsedImplicitly]
    public sealed class VfxShootFactory : VfxFactory, IVfxFactory
    {
        private Pool<ShootVfxView> _pool;
        
        public VfxShootFactory(PrefabProvider prefabProvider, VfxType type, GameBalance balance)
            : base(prefabProvider, type, balance) { }
        
        protected override void CreatePool()
        {
            var prefab = PrefabProvider.GetPrefab<ShootVfxView>(PrefabKey);
            _pool = new Pool<ShootVfxView>(prefab, PoolSize, PrefabProvider.PoolsContainer);
        }

        public override IVfx Spawn()
        {
            return _pool.Spawn();
        }

        public override void Remove(IVfx vfx)
        {
            _pool.DeSpawn(vfx as ShootVfxView);
        }
    }
}