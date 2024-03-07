using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Leopotam.EcsLite.Entities
{
    public sealed class EntityManager : ICustomInject
    {
        private EcsWorld _world;

        private readonly Dictionary<int, Entity> _entities = new();
        
        public void Initialize(EcsWorld world)
        {
            Entity[] entities = GameObject.FindObjectsOfType<Entity>();
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                Entity entity = entities[i];
                entity.Initialize(world);
                _entities.Add(entity.Id, entity);
            }
            
            _world = world;
        }

        public Entity Create(Entity prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            Entity entity = GameObject.Instantiate(prefab, position, rotation, parent);
            entity.Initialize(_world);
            _entities.Add(entity.Id, entity);
            return entity;
        }

        public void Destroy(int id)
        {
            if (_entities.Remove(id, out Entity entity))
            {
                entity.Dispose();
                GameObject.Destroy(entity.gameObject);
            }
        }
        
        public void Register(Entity entity)
        {
            _entities.Add(entity.Id, entity);
        }

        public Entity UnRegister(int id)
        {
            if (_entities.Remove(id, out var entity))
            {
                entity.Dispose();
                return entity;
            }

            throw new ArgumentException($"Entity with ID {id} doesn't exist");

        }

        public Entity Get(int id)
        {
            return _entities[id];
        }

        public IReadOnlyCollection<Entity> GetComponents<T>() where T : struct
        {
            return _entities.Values.Where(e => e.HasData<T>()).ToList();
        }
        
        public EcsWorld GetWorld()
        {
            return _world;
        }
    }
}