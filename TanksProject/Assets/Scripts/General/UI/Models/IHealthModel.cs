namespace Tanks.General.UI.Models
{
    public interface IHealthModel
    {
        public float MaxHealth { get; }
        public float CurrentHealth { get; set; }
    }
}