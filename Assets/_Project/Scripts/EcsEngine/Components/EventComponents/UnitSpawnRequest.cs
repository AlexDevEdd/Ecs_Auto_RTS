using System;
using _Project.Scripts.EcsEngine.Enums;

namespace _Project.Scripts.EcsEngine.Components.EventComponents
{
    [Serializable]
    public struct UnitSpawnRequest
    {
        public UnitType UnitType;
        public TeamType TeamType;
    }
}