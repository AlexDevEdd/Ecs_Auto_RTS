using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories.Units
{
    [UsedImplicitly]
    public sealed class RangeBlueFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 3;

        public RangeBlueFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
}