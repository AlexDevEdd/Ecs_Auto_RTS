using DG.Tweening;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.Views
{
    public class GameOverWindow : MonoBehaviour, ICustomInject
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeDuration = 0.5f;
        
        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1, _fadeDuration);
        }
    }
}