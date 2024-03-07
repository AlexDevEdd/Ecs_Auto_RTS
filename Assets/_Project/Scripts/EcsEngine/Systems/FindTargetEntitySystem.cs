using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine.Systems
{
    internal sealed class FindTargetEntitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<TransformView, Team, FindTargetRequest>,
            Exc<Inactive, TargetEntity>> _filter;
        
        private readonly EcsPoolInject<FindTargetRequest> _findTargetRequestPool;
        private readonly EcsPoolInject<TransformView> _transformViewPool;
        private readonly EcsPoolInject<TargetEntity> _targetPool;
        private readonly EcsPoolInject<Team> _teamPool;
        
        private EcsCustomInject<UnitSystem> _unitSystem;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var transformView = _transformViewPool.Value.Get(entity);
                Entity targetEntity = default;
                switch (_teamPool.Value.Get(entity).Value)
                {
                    case TeamType.Red:
                        if(!_unitSystem.Value.IsHasTeam(TeamType.Blue)) continue;
                        
                        targetEntity = _unitSystem.Value.GetClosestByTeam(transformView, TeamType.Blue);
                        break;
                    case TeamType.Blue:
                        if(!_unitSystem.Value.IsHasTeam(TeamType.Red)) continue;
                        
                        targetEntity = _unitSystem.Value.GetClosestByTeam(transformView, TeamType.Red);
                        break;
                }
                
                if(targetEntity == default || targetEntity == null) continue;
                
                _targetPool.Value.Add(entity) = new TargetEntity
                {
                    Id = targetEntity.Id,
                    Transform = targetEntity.transform
                };
                
                _findTargetRequestPool.Value.Del(entity);
            }
        }
    }
}