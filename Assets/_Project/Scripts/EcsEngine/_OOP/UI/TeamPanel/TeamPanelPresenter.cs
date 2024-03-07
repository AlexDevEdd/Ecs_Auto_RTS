using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Enums;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.TeamPanel
{
    public class TeamPanelPresenter : ITeamPanelPresenter
    {
        private readonly TeamPanelSystem _teamPanelSystem;
        private CompositeDisposable _compositeDisposable;
        
        public TeamType Type { get; }
        public string TeamName { get; }
        public Sprite MeleeIcon { get; }
        public Sprite RangeIcon { get; }
        public Color TextColor { get; }
        public ReactiveCommand<UnitType> MeleeSpawnCommand { get; }
        public ReactiveCommand<UnitType> RangeSpawnCommand { get; }
        public ReactiveCommand CloseCommand { get; }
        
        public TeamPanelPresenter(TeamPanelSystem teamPanelSystem, TeamInfo teamInfo)
        {
            _teamPanelSystem = teamPanelSystem;
            
            Type = teamInfo.Type;
            TeamName = teamInfo.TeamName;
            TextColor = teamInfo.TeamColor;
            MeleeIcon = teamInfo.MeleeIcon;
            RangeIcon = teamInfo.RangeIcon;
            
            MeleeSpawnCommand = new ReactiveCommand<UnitType>();
            RangeSpawnCommand = new ReactiveCommand<UnitType>();
            CloseCommand = new ReactiveCommand();
        }
        
        public void Subscribe()
        {
            _compositeDisposable = new CompositeDisposable();
            
            MeleeSpawnCommand.Subscribe(SpawnUnitCommand)
                .AddTo(_compositeDisposable);
            
            RangeSpawnCommand.Subscribe(SpawnUnitCommand)
                .AddTo(_compositeDisposable);

            CloseCommand.Subscribe(OnCloseCommand)
                .AddTo(_compositeDisposable);
        }

        private void SpawnUnitCommand(UnitType unitType)
        {
            _teamPanelSystem.CreateSpawnRequest(Type, unitType);
        }
        
        private void OnCloseCommand(Unit obj)
        {
            UnSubscribe();
        }

        public void UnSubscribe()
        {
            _compositeDisposable?.Dispose();
        }

    }
}