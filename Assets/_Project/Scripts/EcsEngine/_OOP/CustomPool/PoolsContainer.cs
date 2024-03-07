using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.CustomPool
{
    public sealed class PoolsContainer : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}