using TMPro;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.UI.Buttons
{
    public sealed class SimpleTextButton : BaseButton
    {
        [Space]
        [SerializeField] private TextMeshProUGUI _buttonText;
        
        public void SetButtonText(string text)
        {
            _buttonText.text = text;
        }
    }
}