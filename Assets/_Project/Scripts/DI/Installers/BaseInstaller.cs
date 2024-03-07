using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public abstract class BaseInstaller
    {
        protected void Bind<T>(DiContainer container)
        {
            container.Bind<T>()
                .AsSingle()
                .NonLazy();
        }

        protected void BindInterfacesAndSelfTo<T>(DiContainer container)
        {
            container.BindInterfacesAndSelfTo<T>()
                .AsSingle()
                .NonLazy();
        }
        
        protected void BindInterfacesAndSelfToFromInstance<T>(DiContainer container, T instance)
        {
            container.BindInterfacesAndSelfTo<T>()
                .FromInstance(instance)
                .AsSingle()
                .NonLazy();
        }
        
        protected void BindInterfacesAndSelfToWithArguments<T, P>(DiContainer container, P param)
        {
            container.BindInterfacesAndSelfTo<T>()
                .AsSingle()
                .WithArguments(param)
                .NonLazy();
        }
    }
}