using _Project.Scripts.EcsEngine.Enums;
using UniRx;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.TeamPanel
{
    public interface ITeamPanelPresenter
    {
        public TeamType Type { get; }
        public string TeamName { get; }
        public Sprite MeleeIcon { get; }
        public Sprite RangeIcon { get; }
        public Color TextColor { get; }
        public ReactiveCommand<UnitType> MeleeSpawnCommand { get; }
        public ReactiveCommand<UnitType> RangeSpawnCommand { get; }
        public ReactiveCommand CloseCommand { get; }
        
        public void Subscribe();
        public void UnSubscribe();
    }
}