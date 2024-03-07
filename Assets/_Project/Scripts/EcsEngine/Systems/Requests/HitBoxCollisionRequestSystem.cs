using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Requests
{
    internal sealed class HitBoxCollisionRequestSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<CollisionEnterRequest>> _filter = EcsWorlds.Events;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        
        private readonly EcsPoolInject<Damage> _damagePool;
        private readonly EcsPoolInject<BulletTag> _bulletTagPool;
        private readonly EcsPoolInject<CollisionEnterRequest> _collisionEnterRequestPool = EcsWorlds.Events;
        
        private readonly EcsCustomInject<VfxSystem> _vfxSystem;
        private readonly EcsCustomInject<EcsAdmin> _ecsAdmin;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var collisionEnterRequest = _collisionEnterRequestPool.Value.Get(entity);
                if (_damagePool.Value.Has(collisionEnterRequest.TargetId))
                {
                    var damage = _damagePool.Value.Get(collisionEnterRequest.TargetId);
                
                    _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                        .Add(new DamageEvent
                        {
                            SourceId = collisionEnterRequest.SourceId,
                            TargetId = collisionEnterRequest.TargetId,
                            ContactPosition =collisionEnterRequest.ContactPosition,
                            Damage = damage.Value
                        });
                
                    _ecsAdmin.Value.CreateEntity(EcsWorlds.Events)
                        .Add(new SpawnDamageTextEvent
                        {
                            ContactPosition = collisionEnterRequest.ContactPosition,
                            Damage = damage.Value
                        });
                }
                
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
}