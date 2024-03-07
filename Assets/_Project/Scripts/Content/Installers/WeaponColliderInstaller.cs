using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content.Installers
{
    public class WeaponColliderInstaller : EntityInstaller
    {
        [SerializeField] private Collider _collider;
        protected override void Install(Entity entity)
        {
            _collider.enabled = false;
            entity.AddData(new WeaponColliderView{Value = _collider});
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}