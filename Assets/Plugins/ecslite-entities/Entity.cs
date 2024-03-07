using UnityEngine;

// ReSharper disable UnusedMember.Global
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Leopotam.EcsLite.Entities
{
    public class Entity : MonoBehaviour
    {
        public int Id => _id;

        private EcsWorld _world;
        private int _id = -1;

        [SerializeField]
        private EntityInstaller[] _installers;

        public bool IsAlive()
        {
            return _id != -1 && _world != null;
        }

        public void Initialize(EcsWorld world)
        {
            int entity = world.NewEntity();
            Initialize(entity, world);
        }

        public void Initialize(int id, EcsWorld world)
        {
            _id = id;
            _world = world;
            
            for (int i = 0, count = _installers.Length; i < count; i++)
            {
                EntityInstaller installer = _installers[i];
                installer.Install(this);
            }
        }

        public void Dispose()
        {
            for (int i = 0, count = _installers.Length; i < count; i++)
            {
                EntityInstaller installer = _installers[i];
                installer.Dispose(this);
            }
            
            _world.DelEntity(_id);
            _world = null;
            _id = -1;
        }

        public void AddData<T>(T component) where T : struct
        {
            var pool = _world.GetPool<T>();
            pool.Add(_id) = component;
        }
        
        public Entity WithData<T>(T component) where T : struct
        {
            var pool = _world.GetPool<T>();
            pool.Add(_id) = component;
            return this;
        }

        public void RemoveData<T>() where T : struct
        {
            var pool = _world.GetPool<T>();
            pool.Del(_id);
        }

        public ref T GetData<T>() where T : struct
        {
            EcsPool<T> pool = _world.GetPool<T>();
            return ref pool.Get(_id);
        }

        public void SetData<T>(T data) where T : struct
        {
            var pool = _world.GetPool<T>();
            if (pool.Has(_id))
            {
                pool.Get(_id) = data;
            }
            else
            {
                pool.Add(_id) = data;
            }
        }

        public bool TryGetData<T>(out T data) where T : struct
        {
            var pool = _world.GetPool<T>();
            if (pool.Has(_id))
            {
                data = pool.Get(_id);
                return true;
            }

            data = default;
            return false;
        }

        public bool HasData<T>() where T : struct
        {
            var pool = _world.GetPool<T>();
            return pool.Has(_id);
        }

        public int GetComponentsNonAlloc(ref object[] components)
        {
            return _world.GetComponents(_id, ref components);
        }

        public EcsWorld GetWorld()
        {
            return _world;
        }
    }
}