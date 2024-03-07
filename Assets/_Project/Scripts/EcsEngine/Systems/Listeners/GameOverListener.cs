using System;
using _Project.Scripts.EcsEngine._OOP.Systems.Clear;
using _Project.Scripts.EcsEngine._OOP.UI.Views;
using _Project.Scripts.EcsEngine.Components.EventComponents;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace _Project.Scripts.EcsEngine.Systems.Listeners
{
    internal sealed class GameOverListener : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameOverEvent>> _filter = EcsWorlds.Events;
        
        private readonly EcsCustomInject<GameOverWindow> _gameOverWindow;
        private readonly EcsCustomInject<ClearSystem> _clearSystem;
        private readonly EcsCustomInject<EcsAdmin> _admin;
        
        private readonly EcsWorldInject _world = EcsWorlds.Events;
        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                
                _world.Value.DelEntity(entity);
                ClearAsync().Forget();
            }
        }

        private async UniTaskVoid ClearAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(2));
            _gameOverWindow.Value.Show();
            _clearSystem.Value.Clear();
            await UniTask.Delay(TimeSpan.FromSeconds(4));
            _admin.Value.Dispose();
        }
    }
}