using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Spawners
{
    internal sealed class SpawnDamagePopUpSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnDamageTextEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsWorldInject _eventWorld = EcsWorlds.Events;
        
        private readonly EcsPoolInject<SpawnDamageTextEvent> _spawnDamageTextEventPool = EcsWorlds.Events;
        
        private readonly EcsCustomInject<TextPopUpFactory> _textPopUpFactory;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var damageEvent = _spawnDamageTextEventPool.Value.Get(entity);
                var damage = damageEvent.Damage;
                var contactPos = damageEvent.ContactPosition;
                
                _textPopUpFactory.Value.Spawn(damage.ToString(), contactPos);
            
                _eventWorld.Value.DelEntity(entity);
            }
        }
    }
    
}