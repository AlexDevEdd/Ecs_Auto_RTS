using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine._OOP.Systems;
using Leopotam.EcsLite.Entities;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Content
{
    public class TestMonoBe : MonoBehaviour
    {
        [SerializeField] private Entity _entity;
        
        private UnitSystem _unitSystem;
        private EntityManager _manager;
        private EcsAdmin _admin;
        
        [Inject]
        public void Construct(UnitSystem factory, EntityManager manager, EcsAdmin admin)
        {
            _unitSystem = factory;
            _manager = manager;
            _admin = admin;
        }

        [Button]
        public void DisposeWorld()
        {
            _admin.Dispose();
        }
    }
}