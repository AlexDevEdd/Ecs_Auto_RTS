using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.EcsEngine._OOP.UI.Buttons
{
    public sealed class SimpleIconButton : BaseButton
    {
        [Space]
        [SerializeField] private Image _icon;
        
        public void SetButtonIcon(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}