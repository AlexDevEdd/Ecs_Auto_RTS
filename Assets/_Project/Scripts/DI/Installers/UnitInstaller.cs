using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Factories.Units;
using _Project.Scripts.EcsEngine._OOP.Systems;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class UnitInstaller : BaseInstaller
    {
        public UnitInstaller(DiContainer container)
        {
            BindInterfacesAndSelfTo<UnitSystem>(container);
            
            BindInterfacesAndSelfTo<UnitFactory>(container);
            BindInterfacesAndSelfTo<MeleeRedFactory>(container);
            BindInterfacesAndSelfTo<MeleeBlueFactory>(container);
            BindInterfacesAndSelfTo<RangeRedFactory>(container);
            BindInterfacesAndSelfTo<RangeBlueFactory>(container);
            
            BindInterfacesAndSelfTo<BulletFactory>(container);
        }
    }
}