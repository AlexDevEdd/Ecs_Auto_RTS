using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories
{
    public abstract class VfxFactory : IInitializable
    {
        protected readonly PrefabProvider PrefabProvider;
        protected readonly string PrefabKey;
        protected readonly int PoolSize;

        public VfxType Type { get; }

        protected VfxFactory(PrefabProvider prefabProvider, VfxType type, GameBalance balance)
        {
            var config = balance.GetVfxFactoryConfigByType(type);
            PrefabProvider = prefabProvider;
            PrefabKey = config.PrefabKey;
            PoolSize = config.PoolSize;
            Type = type;
        }

        public void Initialize()
        {
            CreatePool();
        }

        protected abstract void CreatePool();
        public abstract IVfx Spawn();
        public abstract void Remove(IVfx vfx);
    }
}