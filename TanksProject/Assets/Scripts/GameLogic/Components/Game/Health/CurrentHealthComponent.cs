using Entitas;

namespace TanksGB.GameLogic.Components.Game.Health
{
    [Game]
    public sealed class CurrentHealthComponent : IComponent
    {
        public float Value;
    }
}