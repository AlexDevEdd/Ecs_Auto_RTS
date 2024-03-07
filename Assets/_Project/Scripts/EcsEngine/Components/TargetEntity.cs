using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct TargetEntity
    {
        public int Id;
        public Transform Transform;
    }
}