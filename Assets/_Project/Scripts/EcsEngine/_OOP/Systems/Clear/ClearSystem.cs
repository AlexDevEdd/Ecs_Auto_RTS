using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;

namespace _Project.Scripts.EcsEngine._OOP.Systems.Clear
{
    [UsedImplicitly]
    public class ClearSystem : ICustomInject
    {
        private readonly List<IClear> _clearList = new ();

        public ClearSystem(IEnumerable<IClear> clearList)
        {
            _clearList = clearList.ToList();
        }

        public void Clear()
        {
            foreach (var clear in _clearList)
            {
                clear.Clear();
            }
        }
    }
}