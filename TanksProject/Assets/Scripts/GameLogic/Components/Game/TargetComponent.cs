using Entitas;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class TargetComponent : IComponent
    {
        public GameEntity Value;
    }
}