using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components.EventComponents
{
    [Serializable]
    public struct SpawnDamageTextEvent
    {
        public Vector3 ContactPosition;
        public float Damage;
    }
}