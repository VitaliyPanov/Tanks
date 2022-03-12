using Tanks.General.UI.Models;

namespace Tanks.UI.Models
{
    public sealed class HealthModel : IHealthModel
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }

        public HealthModel(float maxHealth, float currentHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = currentHealth;
        }
    }
}