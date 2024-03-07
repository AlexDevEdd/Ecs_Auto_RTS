using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Factories.Units
{
    [UsedImplicitly]
    public sealed class MeleeBlueFactory : BaseUnitFactory
    {
        protected override int FactoryIndex => 1;

        public MeleeBlueFactory(EntityManager entityManager, PrefabProvider prefabProvider, GameBalance balance) 
            : base(entityManager, prefabProvider, balance) { }
    }
}