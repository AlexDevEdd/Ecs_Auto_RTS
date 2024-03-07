using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Factories;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class VfxInstaller : BaseInstaller
    {
        public VfxInstaller(DiContainer container)
        {
            BindInterfacesAndSelfTo<VfxSystem>(container);
            BindInterfacesAndSelfTo<TextPopUpFactory>(container);
            
            BindInterfacesAndSelfToWithArguments<VfxSoulFactory, VfxType>(container, VfxType.Soul);
            BindInterfacesAndSelfToWithArguments<VfxSpawnUnitFactory, VfxType>(container, VfxType.Spawn);
            BindInterfacesAndSelfToWithArguments<VfxDamageFactory, VfxType>(container, VfxType.Damage);
            BindInterfacesAndSelfToWithArguments<VfxShootFactory, VfxType>(container, VfxType.Shoot);
        }
    }
}