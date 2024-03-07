using Leopotam.EcsLite.Entities;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEntityManager();
            new UnitInstaller(Container);
            new VfxInstaller(Container);

        }

        private void BindEntityManager()
        {
            Container.BindInterfacesAndSelfTo<EntityManager>()
                .AsSingle()
                .NonLazy();
        }
    }
}