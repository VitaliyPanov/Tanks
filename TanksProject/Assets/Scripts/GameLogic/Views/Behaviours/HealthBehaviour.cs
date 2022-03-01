using System;
using Entitas;
using Tanks.GameLogic.Services;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public class HealthBehaviour : MonoBehaviour, IBehaviour, IEventListener, IHealthDamageListener
    {
        private GameEntity _gameEntity;
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }
        public event Action<float, float> OnHealthChangedEvent;


        public void Initialize(IEntity entity)
        {
            if (entity is not GameEntity gameEntity) return;
            
            _gameEntity = gameEntity;
            _gameEntity.isHealth = true;
            if (_gameEntity.hasMaxHealth)
                MaxHealth = _gameEntity.maxHealth.Value;
            else
                _gameEntity.AddMaxHealth(0);
            if (!_gameEntity.hasCurrentHealth)
                _gameEntity.AddCurrentHealth(MaxHealth);
            CurrentHealth = _gameEntity.currentHealth.Value;
        }

        public void AddListener(IEntity entity) => _gameEntity.AddHealthDamageListener(this);

        public void OnHealthDamage(GameEntity entity, float value)
        {
            _gameEntity.ReplaceCurrentHealth(CurrentHealth - value);
            CurrentHealth = _gameEntity.currentHealth.Value;
            OnHealthChangedEvent?.Invoke(CurrentHealth, MaxHealth);
            _gameEntity.RemoveHealthDamage();
        }
    }
}