using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content.Installers
{
    public class ColliderInstaller : EntityInstaller
    {
        [SerializeField] private Collider _collider;
        protected override void Install(Entity entity)
        {
            _collider.enabled = true;
            entity.AddData(new ColliderView{Value = _collider});
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}