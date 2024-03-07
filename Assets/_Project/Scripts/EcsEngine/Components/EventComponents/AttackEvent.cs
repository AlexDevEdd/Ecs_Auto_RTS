using System;

namespace _Project.Scripts.EcsEngine.Components.EventComponents
{
    [Serializable]
    public struct AttackEvent
    {
        public int SourceEntity;
        public int TargetEntity;
    }
}