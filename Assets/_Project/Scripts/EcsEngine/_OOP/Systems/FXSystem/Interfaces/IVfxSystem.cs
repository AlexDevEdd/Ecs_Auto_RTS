using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces
{
    public interface IVfxSystem
    {
        void PlayFx(VfxType type, Vector3 position);
    }
}