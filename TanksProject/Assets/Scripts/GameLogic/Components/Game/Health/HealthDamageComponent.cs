using Entitas;

namespace Tanks.GameLogic.Components.Game.Health
{
    [Game]
    public sealed class HealthDamageComponent : IComponent
    {
        public float Value;
    }
}