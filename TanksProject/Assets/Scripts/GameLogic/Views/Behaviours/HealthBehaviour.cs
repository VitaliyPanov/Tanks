using System;
using Entitas;
using Tanks.GameLogic.Services;
using Tanks.General.UI.Models;
using Tanks.General.UI.ViewModels;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public class HealthBehaviour : MonoBehaviour, IBehaviour, IEventListener, IHealthDamageListener
    {
        public float MaxHealth => _gameEntity.maxHealth.Value;
        public float CurrentHealth => _gameEntity.currentHealth.Value;
        
        private GameEntity _gameEntity;
        private IHealthViewModel _healthView;

        public void Construct(IEntity entity)
        {
            if (entity is not GameEntity gameEntity) return;

            _gameEntity = gameEntity;
            _gameEntity.isHealth = true;
            if (!_gameEntity.hasMaxHealth)
                _gameEntity.AddMaxHealth(0);
            if (!_gameEntity.hasCurrentHealth)
                _gameEntity.AddCurrentHealth(_gameEntity.maxHealth.Value);
        }

        public void Construct(IHealthViewModel viewModel) => _healthView ??= viewModel;

        public void AddListener(IEntity entity) => _gameEntity.AddHealthDamageListener(this);

        public void OnHealthDamage(GameEntity entity, float value)
        {
            _gameEntity.ReplaceCurrentHealth(CurrentHealth - value);
            _healthView.ApplyDamage(value);
            _gameEntity.RemoveHealthDamage();
        }
    }
}