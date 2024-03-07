using System;

namespace _Project.Scripts.EcsEngine._OOP.UI.Configs
{
    [Serializable]
    public struct DamageTextConfig
    {
        public float StartOffsetY;
        
        public float ScaleDuration;
        public float EndScaleValue;

        public float MoveDurationY;
        public float EndMoveY;
    }
}