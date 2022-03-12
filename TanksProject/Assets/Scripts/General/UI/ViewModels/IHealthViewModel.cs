using System;
using Tanks.General.UI.Models;

namespace Tanks.General.UI.ViewModels
{
    public interface IHealthViewModel
    {
        event Action<float, float> OnHealthChangedEvent;
        IHealthModel HealthModel { get; }
        bool IsDead { get; }
        void ApplyDamage(float damage);
    }
}