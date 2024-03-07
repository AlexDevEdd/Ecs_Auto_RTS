using System;
using _Project.Scripts.EcsEngine._OOP.UI.Configs;
using DG.Tweening;
using Leopotam.EcsLite.Entities;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.Views
{
    public class DamageTextPopUp : Entity
    {
        public event Action<DamageTextPopUp> OnRemove;
        
        [SerializeField] private TextMeshProUGUI _valueText;
        [SerializeField] private DamageTextConfig _config;
        
        private Tween _tween;
        
        public void Show(string text, Vector3 position)
        {
            var pos = position + new Vector3(0, _config.StartOffsetY, 0);
            UpdatePosition(pos);
            _valueText.enabled = true;
            
            _valueText.text = "-" + text;
            _tween?.Kill();
            _tween = transform.DOScale(_config.EndScaleValue, _config.ScaleDuration)
                .SetEase(Ease.OutBack)
                .OnComplete(Hide);
            
            transform.DOLocalMoveY(_config.EndMoveY, _config.MoveDurationY)
                .SetDelay(0.3f)
                .SetRelative(true);
        }
        
        private void SetPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        private void UpdatePosition(Vector3 position)
        {
            SetPosition(position);
            transform.localRotation = new Quaternion(0, 0, 0, 1);
        }

        private void Hide()
        {
            _valueText.enabled = false;
            OnRemove?.Invoke(this);
        }
    }
}
