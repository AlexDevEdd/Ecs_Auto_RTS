using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class TeamPanelInstaller : BaseInstaller
    {
        public TeamPanelInstaller(DiContainer container, TeamPanelUI panel)
        {
            Bind<TeamPanelPresenterFactory>(container);
            BindInterfacesAndSelfTo<TeamPanelSystem>(container);
            BindInterfacesAndSelfToFromInstance(container, panel);
        }
    }
}