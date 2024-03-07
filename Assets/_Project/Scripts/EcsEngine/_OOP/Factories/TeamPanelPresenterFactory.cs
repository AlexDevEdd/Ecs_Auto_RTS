using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine._OOP.UI.TeamPanel;
using JetBrains.Annotations;

namespace _Project.Scripts.EcsEngine._OOP.Factories
{
    [UsedImplicitly]
    public class TeamPanelPresenterFactory
    {
        public ITeamPanelPresenter Create(TeamPanelSystem teamPanelSystem, TeamInfo teamInfo)
        {
            return new TeamPanelPresenter(teamPanelSystem, teamInfo);
        }
    }
}