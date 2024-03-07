using System;
using System.Threading;
using _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Interfaces;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite.Entities;
using UnityEngine;

namespace _Project.Scripts.EcsEngine._OOP.Systems.FXSystem.Views
{
    public abstract class VfxBaseView : Entity, IVfx
    {
        public event Action<IVfx> OnVfxEnd;

        [SerializeField] private VfxType _type;
        [SerializeField] protected float Duration;
        [SerializeField] protected ParticleSystem Particle;

        private CancellationTokenSource _tokenSource;
        
        public VfxType Type => _type;
        
        public void Show(Vector3 position, float value = default)
        {
            transform.position = position;
            Particle.Play();
            Remove().Forget();
        }

        private async UniTaskVoid Remove()
        {
            _tokenSource = new CancellationTokenSource();
            await UniTask.Delay(TimeSpan.FromSeconds(Duration), cancellationToken: _tokenSource.Token);
            ClearToken();
            OnVfxEnd?.Invoke(this);
        }
        
        private void OnDisable()
        {
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }

        private void ClearToken()
        {
            _tokenSource?.Dispose();
            _tokenSource = null; 
        }
    }
}