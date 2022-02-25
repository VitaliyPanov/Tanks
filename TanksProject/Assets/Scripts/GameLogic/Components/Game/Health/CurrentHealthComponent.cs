using Entitas;

namespace Tanks.GameLogic.Components.Game.Health
{
    [Game]
    public sealed class CurrentHealthComponent : IComponent
    {
        public float Value;
    }
}