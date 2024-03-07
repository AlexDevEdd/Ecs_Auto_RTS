using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class BuildingDeathListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BuildingTag, DeathEvent>> _filter;

        private readonly EcsPoolInject<TransformView> _transformView;
        
        private readonly EcsCustomInject<TeamPanelUI> _teamPanel;
        private readonly EcsCustomInject<EcsAdmin> _ecsAdmin;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transformView =  _transformView.Value.Get(entity);
                transformView.Value.gameObject.SetActive(false);
                
                _teamPanel.Value.Hide();
                
                _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                    .Add(new GameOverEvent());
            }
        }
    }
}