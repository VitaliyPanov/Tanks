using System;
using Tanks.General.UI.Models;
using Tanks.General.UI.ViewModels;

namespace Tanks.UI.ViewModels
{
    public sealed class HealthViewModel : IHealthViewModel
    {
        private bool _isDead;
        public event Action<float, float> OnHealthChangedEvent;
        public IHealthModel HealthModel { get; }

        public bool IsDead => _isDead;
        public HealthViewModel(IHealthModel healthModel) => HealthModel = healthModel;

        public void ApplyDamage(float damage)
        {
            HealthModel.CurrentHealth -= damage;
            if (HealthModel.CurrentHealth <= 0)
                _isDead = true;
            OnHealthChangedEvent?.Invoke(HealthModel.CurrentHealth, HealthModel.MaxHealth);
        }
    }
}