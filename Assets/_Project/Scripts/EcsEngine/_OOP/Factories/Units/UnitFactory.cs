using System;
using System.Collections.Generic;
using System.Text;
using _Project.Scripts.EcsEngine._OOP.ScriptableConfigs;
using _Project.Scripts.EcsEngine.Components;
using _Project.Scripts.EcsEngine.Components.AttackComponents;
using _Project.Scripts.EcsEngine.Components.MovementComponents;
using _Project.Scripts.EcsEngine.Components.TagComponents;
using _Project.Scripts.EcsEngine.Components.ViewComponents;
using _Project.Scripts.EcsEngine.Enums;
using JetBrains.Annotations;
using Leopotam.EcsLite.Entities;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace _Project.Scripts.EcsEngine._OOP.Factories.Units
{
    [UsedImplicitly]
    public class UnitFactory : IInitializable
    {
        private readonly EntityManager _entityManager;
        private readonly GameBalance _balance;
        private readonly IEnumerable<IConcreteUnitFactory> _factories;

        private readonly Dictionary<string, IConcreteUnitFactory> _unitFactories = new ();
        
        public UnitFactory(EntityManager entityManager, GameBalance balance, IEnumerable<IConcreteUnitFactory> factories)
        {
            _entityManager = entityManager;
            _balance = balance;
            _factories = factories;
        }
        
        public void Initialize()
        {
            foreach (var factory in _factories) 
                _unitFactories.TryAdd(factory.PrefabKey, factory);
        }
        
        public Entity Spawn(UnitType type, TeamType teamType, Vector3 position, Quaternion rotation)
        {
            var config = _balance.GetUnitConfigByClass(type);
            if (_unitFactories.TryGetValue(ConvertToKey(type, teamType), out var factory))
            {
                var entity = factory.Spawn();
                entity.Initialize(_entityManager.GetWorld());
                entity.WithData(new TransformView{Value = entity.transform})
                    .WithData(new SourceEntity{Id = entity.Id, Transform = entity.transform})
                    .WithData(new UnitTag())
                    .WithData(new Team{Value = teamType})
                    .WithData(new UnitClass{Value = type})
                    .WithData(new Position{Value = position})
                    .WithData(new Rotation{Value = rotation})
                    .WithData(new MoveSpeed{Value = config.MoveSpeed})
                    .WithData(new RotationSpeed{Value = config.RotationSpeed})
                    .WithData(new Health{Value = config.Health})
                    .WithData(new MoveDirection())
                    .WithData(new Reached{IsReached = false})
                    .WithData(new AttackDistance{Value = config.AttackDistance})
                    .WithData(new AttackCoolDown
                    {
                        CurrentValue = 0f,
                        OriginValue = Random.Range(config.MinAttackDelay, config.MaxAttackDelay)
                    });
                
                
                _entityManager.Register(entity);
                
                return entity;
            }
            
            throw new ArgumentException($"Doesn't exist KEY of {type}+{teamType}");
           
        }
        
        public void DeSpawn(Entity entity)
        {
            var unitType = entity.GetData<UnitClass>();
            var teamType = entity.GetData<Team>();
            
            if (_unitFactories.TryGetValue(ConvertToKey(unitType.Value, teamType.Value), out var factory))
            {
                factory.DeSpawn(entity.Id); 
            }
        }
        
        private string ConvertToKey(UnitType type, TeamType teamType)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(Enum.GetName(typeof(UnitType), type));
            stringBuilder.Append(Enum.GetName(typeof(TeamType), teamType));
            return stringBuilder.ToString();
        }
    }
}