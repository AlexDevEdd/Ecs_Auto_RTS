using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Content.Installers
{
    public class BuildingInstaller : EntityInstaller
    {
        [SerializeField] private TeamType _teamType;
        [SerializeField] private float _health;

        [Inject] private UnitSystem _unitSystem;
        protected override void Install(Entity entity)
        {
            entity.WithData(new BuildingTag())
                .WithData(new TransformView { Value = transform })
                .WithData(new Team { Value = _teamType })
                .WithData(new Health { Value = _health });
            
            _unitSystem.AddTarget(entity);
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}