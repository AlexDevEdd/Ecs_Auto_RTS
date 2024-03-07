using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Content.Components
{
    [RequireComponent(typeof(Entity))]
    public sealed class HitBoxComponent : MonoBehaviour
    {
        private Entity _entity;
        private EcsAdmin _ecsAdmin;

        [Inject]
        public void Construct(EcsAdmin ecsAdmin)
        {
            _ecsAdmin = ecsAdmin;
        }

        private void Awake()
        {
            _entity = GetComponent<Entity>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity target))
            {
                if(target.HasData<Inactive>()) return;
                
                _ecsAdmin.CreateEntity(EcsWorlds.Events)
                    .Add(new CollisionEnterRequest
                    {
                        SourceId = _entity.Id,
                        TargetId = target.Id,
                        ContactPosition = other.ClosestPoint(target.transform.position)
                    });

                if(target.HasData<BulletTag>() && !target.HasData<Collided>())
                    target.AddData(new Collided());
            }
        }
    }
}