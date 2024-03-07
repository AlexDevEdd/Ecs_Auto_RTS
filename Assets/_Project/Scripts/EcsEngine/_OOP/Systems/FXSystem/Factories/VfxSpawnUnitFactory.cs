using _Project.Scripts.EcsEngine._OOP.CustomPool;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Views;
using JetBrains.Annotations;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories
{
    [UsedImplicitly]
    public sealed class VfxSpawnUnitFactory : VfxFactory, IVfxFactory
    {
        private Pool<SpawnUnitVfxView> _pool;
        
        public VfxSpawnUnitFactory(PrefabProvider prefabProvider, VfxType type, GameBalance balance)
            : base(prefabProvider, type, balance) { }
        
        protected override void CreatePool()
        {
            var prefab = PrefabProvider.GetPrefab<SpawnUnitVfxView>(PrefabKey);
            _pool = new Pool<SpawnUnitVfxView>(prefab, PoolSize, PrefabProvider.PoolsContainer);
        }

        public override IVfx Spawn()
        {
            return _pool.Spawn();
        }

        public override void Remove(IVfx vfx)
        {
            _pool.DeSpawn(vfx as SpawnUnitVfxView);
        }
    }
}