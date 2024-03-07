using _Project.Scripts.EcsEngine;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using _Project.Scripts.EcsEngine.Enums;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Content
{
    public class GameStartComponent : MonoBehaviour
    {
        private EcsAdmin _admin;
        
        [Inject]
        public void Construct(EcsAdmin admin)
        {
            _admin = admin;
        }

        private void Start()
        {
            _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest { TeamType = TeamType.Red, UnitType = UnitType.Melee }); 
                
            _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest { TeamType = TeamType.Blue, UnitType = UnitType.Melee });
                
            _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest { TeamType = TeamType.Red, UnitType = UnitType.Range });
                
            _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest { TeamType = TeamType.Blue, UnitType = UnitType.Range });
                
            _admin.CreateEntity(EcsWorlds.Events)
                .Add(new UnitSpawnRequest { TeamType = TeamType.Red, UnitType = UnitType.Range });
        }
    }
}