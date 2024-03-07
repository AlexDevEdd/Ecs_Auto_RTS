using System;
using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Enums;
using JetBrains.Annotations;
using Zenject;

namespace _Project.Scripts.EcsEngine._OOP.Systems
{
    [UsedImplicitly]
    public class TeamPanelSystem : IInitializable
    {
        private readonly TeamPanelPresenterFactory _teamPanelPresenterFactory;
        private readonly TeamPanelUI _teamPanel;
        private readonly GameBalance _balance;
        private readonly EcsAdmin _ecsAdmin;
        
        private readonly HashSet<ITeamPanelPresenter> _presenters = new();

        public TeamPanelSystem(EcsAdmin ecsAdmin, TeamPanelPresenterFactory teamPanelPresenterFactory,
            GameBalance balance, TeamPanelUI teamPanel)
        {
            _teamPanelPresenterFactory = teamPanelPresenterFactory;
            _teamPanel = teamPanel;
            _ecsAdmin = ecsAdmin;
            _balance = balance;
        }

        public void Initialize()
        {
            CreatePresenters();
        }

        private void CreatePresenters()
        {
            foreach (var teamInfo in _balance.TeamConfigs)
            {
                var presenter = _teamPanelPresenterFactory.Create(this, teamInfo);
                _presenters.Add(presenter);
            }
        }

        public void OpenPanel(TeamType type)
        {
            var presenter = _presenters.FirstOrDefault(p => p.Type == type);
            
            if (presenter != null)
                _teamPanel.Show(presenter);
            else
                throw new ArgumentException($"Does not exist presenter type of {type}");

        }
        
        public void CreateSpawnRequest(TeamType type, UnitType unitType)
        {
            _ecsAdmin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest
                {
                    TeamType = type,
                    UnitType = unitType
                });
        }
    }
}