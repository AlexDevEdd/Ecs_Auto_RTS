using System;

namespace _Project.Scripts.EcsEngine.Components.AttackComponents
{
    [Serializable]
    public struct AttackCoolDown
    {
        public float CurrentValue;
        public float OriginValue;
    }
}