using _Project.Scripts.EcsEngine._OOP.Factories;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Spawners
{
    internal sealed class DeSpawnBulletSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<BulletTag, Collided>, Exc<Inactive>> _filter;
        private readonly EcsPoolInject<Inactive> _inactivePool;
        private readonly EcsPoolInject<Collided> _collidedPool;
        
        private readonly EcsCustomInject<BulletFactory> _bulletFactory;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                _collidedPool.Value.Del(entity);
                _inactivePool.Value.Add(entity) = new Inactive();
                _bulletFactory.Value.DeSpawn(entity);
            }
        }
    }
}