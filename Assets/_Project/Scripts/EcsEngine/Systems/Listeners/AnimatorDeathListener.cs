using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class AnimatorDeathListener : IEcsRunSystem
    {
        private static readonly int Death = Animator.StringToHash("Death");

        private readonly EcsFilterInject<Inc<AnimatorView, DeathEvent>> _filter;
        
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _animatorPool.Value.Get(entity).Value.SetTrigger(Death);
            }
        }
    }
}