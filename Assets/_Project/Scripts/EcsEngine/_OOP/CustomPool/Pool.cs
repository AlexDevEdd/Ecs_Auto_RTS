using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _Project.Scripts.EcsEngine._OOP.CustomPool
{
    public class Pool<T>  where T: MonoBehaviour
    {
        private readonly Queue<T> _pool = new();
        private readonly HashSet<T> _actives = new();
        
        private readonly T _prefab;
        private readonly int _size;
        
        private Transform _container;

        public Pool(T prefab, int size, Transform parent)
        {
            _prefab = prefab;
            _size = size;
            Init(parent);
        }

        private void Init(Transform parent)
        {
            for (var i = 0; i < _size; i++)
            {
                if (_container == null)
                {
                    _container = new GameObject($"{_prefab.name}s").transform;
                    _container.SetParent(parent);
                } 

                var obj = CreateObj();
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public T Spawn()
        {
            if(_pool.TryDequeue(out var obj))
            {
                _actives.Add(obj);
                obj.gameObject.SetActive(true);
                return obj;
            }

            obj = CreateObj();
            obj.gameObject.SetActive(true);
            _actives.Add(obj);
            return obj;
        }
        
        public void DeSpawn(T obj)
        {
            if (_actives.Remove(obj))
            {
                obj.transform.SetParent(_container);
                obj.gameObject.SetActive(false);
                _pool.Enqueue(obj);
            }
        }

        public void Clear()
        {
            var units = _actives.ToList();
            for (int i = 0; i < units.Count; i++)
            {
                var obj = units[i];
                obj.transform.SetParent(_container);
                obj.gameObject.SetActive(false);
               _pool.Enqueue(obj);
            }
        }
        
        private T CreateObj()
            => Object.Instantiate(_prefab, _container);
    }
}