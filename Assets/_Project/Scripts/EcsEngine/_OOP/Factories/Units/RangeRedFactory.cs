using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories.Units
{
    [UsedImplicitly]
    public sealed class RangeRedFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 2;

        public RangeRedFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
}