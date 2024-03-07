using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.Systems.Clear;
using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.DI.Installers
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private TeamPanelUI teamPanelUI;
        [SerializeField] private GameOverWindow _gameOverWindow;
        
        public override void InstallBindings()
        {
            BindEcsAdmin();
            BindGameOverWindow();
            new TeamPanelInstaller(Container, teamPanelUI);
        }

        private void BindEcsAdmin()
        {
            Container.BindInterfacesAndSelfTo<EcsAdmin>()
                .AsSingle()
                .NonLazy();
            
            Container.BindInterfacesAndSelfTo<ClearSystem>()
                .AsSingle()
                .NonLazy(); 
        }
        
        private void BindGameOverWindow()
        {
            Container.BindInterfacesAndSelfTo<GameOverWindow>()
                .FromInstance(_gameOverWindow)
                .AsSingle()
                .NonLazy();
        }
    }
}