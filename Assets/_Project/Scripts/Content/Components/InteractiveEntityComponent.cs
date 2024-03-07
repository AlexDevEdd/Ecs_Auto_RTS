using _Project.Scripts.EcsEngine._OOP.Systems;
using _Project.Scripts.EcsEngine.Enums;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace _Project.Scripts.Content.Components
{
    [RequireComponent(typeof(Entity))]
    public sealed class InteractiveEntityComponent : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TeamType _teamType;
        [SerializeField] private Light _light;

        private TeamPanelSystem _teamPanelSystem;

        [Inject]
        public void Construct(TeamPanelSystem teamPanelSystem)
        {
            _teamPanelSystem = teamPanelSystem;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            _light.enabled = false;
            _teamPanelSystem.OpenPanel(_teamType);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _light.enabled = true;
           
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _light.enabled = false;
        }
    }
}