using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Components.WeaponComponents;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.Content.Installers
{
    public class MeleeWeaponInstaller : EntityInstaller
    {
        [SerializeField] private Entity _weaponEntity;
        [SerializeField] private float _damage;
        
        protected override void Install(Entity entity)
        {
            entity.AddData(new MeleeWeapon());
            _weaponEntity.Initialize(entity.GetWorld());
            _weaponEntity.AddData(new TransformView{Value = _weaponEntity.transform});
            _weaponEntity.AddData(new Damage { Value = _damage });
        }

        protected override void Dispose(Entity entity)
        {
            _weaponEntity.Dispose();
        }
    }
}