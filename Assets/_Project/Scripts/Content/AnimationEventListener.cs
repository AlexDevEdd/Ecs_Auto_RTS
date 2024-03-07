using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content
{
    public class AnimationEventListener : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        [UsedImplicitly]
        public void OnDeath()
        {
            _entity.AddData(new DeSpawnRequest());
        }
        
        [UsedImplicitly]
        public void OnShoot()
        {
            if (_entity.HasData<TargetEntity>())
            {
                _entity.AddData(new ShootEvent
                {
                    SourceId = _entity.GetData<SourceEntity>().Id
                });
            }
        }
        
        [UsedImplicitly]
        public void OnEnableCollider()
        {
            _entity.AddData(new EnableColliderEvent());
        }
        
        [UsedImplicitly]
        public void OnDisableCollider()
        {
            _entity.AddData(new DisableColliderEvent());
        }
    }
}