using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content.Installers
{
    public class ProjectileWeaponInstaller : EntityInstaller
    {
        [SerializeField] private Transform _firePoint;
        protected override void Install(Entity entity)
        {
            entity.AddData(new ProjectileWeapon{ FirePoint = _firePoint});
        }

        protected override void Dispose(Entity entity)
        {
           
        }
    }
}