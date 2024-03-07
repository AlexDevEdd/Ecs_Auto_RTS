using System;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces
{
    public interface IVfx
    {
        public event Action<IVfx> OnVfxEnd;
        public VfxType Type { get; }
        public void Show(Vector3 position, float value = default);
    }
}