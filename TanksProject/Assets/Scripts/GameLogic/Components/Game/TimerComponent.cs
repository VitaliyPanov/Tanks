using Entitas;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class TimerComponent : IComponent
    {
        public float Value;
    }
}