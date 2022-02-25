using Entitas;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class TargetComponent : IComponent
    {
        public GameEntity Value;
    }
}