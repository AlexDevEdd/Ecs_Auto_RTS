using _Project.Scripts.EcsEngine._OOP.UI.Buttons;
using _Project.Scripts.EcsEngine.Enums;
using DG.Tweening;
using Leopotam.EcsLite.Entities;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.TeamPanel
{
    public class TeamPanelUI : MonoBehaviour , ICustomInject
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _teamNameText;
        [SerializeField] private SimpleIconButton _meleeButton;
        [SerializeField] private SimpleIconButton _rangeButton;
        [SerializeField] private CloseButton _closeButton;

        private ITeamPanelPresenter _presenter;

        public void Show(ITeamPanelPresenter presenter)
        {
            if (isActiveAndEnabled)
            {
                Unsubscribes();
            }
            else
            {
                gameObject.SetActive(true);
                _canvasGroup.DOFade(1, 0.5f);
            }
            
            _presenter = presenter;
            _teamNameText.text = presenter.TeamName;
            _teamNameText.color = presenter.TextColor;
            _meleeButton.SetButtonIcon(presenter.MeleeIcon);
            _rangeButton.SetButtonIcon(presenter.RangeIcon);
            _presenter.Subscribe();
            
            Subscribes();
        }
        
        private void Subscribes()
        {
            _meleeButton.AddListener(MeleeButtonClicked);
            _rangeButton.AddListener(RangeButtonClicked);
            _closeButton.AddListener(Hide);
        }

        private void MeleeButtonClicked()
        {
            _presenter.MeleeSpawnCommand.Execute(UnitType.Melee);
        }
        
        private void RangeButtonClicked()
        {
            _presenter.RangeSpawnCommand.Execute(UnitType.Range);
        }
        
        private void Unsubscribes()
        {
            _meleeButton.RemoveListener(MeleeButtonClicked);
            _rangeButton.RemoveListener(RangeButtonClicked);
            _closeButton.RemoveListener(Hide);
            _presenter.UnSubscribe();
        }
        
        public void Hide()
        {
            if (isActiveAndEnabled)
            {
                _presenter.CloseCommand.Execute();
                Unsubscribes();
                
                _canvasGroup.DOFade(0, 0.5f)
                    .OnComplete(()=> gameObject.SetActive(false));
            }
        }
    }
}