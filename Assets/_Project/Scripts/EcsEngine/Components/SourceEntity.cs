using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine.Components
{
    [Serializable]
    public struct SourceEntity
    {
        public int Id;
        public Transform Transform;
    }
}