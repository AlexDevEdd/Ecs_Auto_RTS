using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class AnimatorAttackListener : IEcsRunSystem
    {
        private static readonly int Attack = Animator.StringToHash("Attack");

        private readonly EcsFilterInject<Inc<AnimatorView, AttackEvent>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<AnimatorView> _animatorPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _animatorPool.Value.Get(entity).Value.SetTrigger(Attack);
            }
        }
    }
}