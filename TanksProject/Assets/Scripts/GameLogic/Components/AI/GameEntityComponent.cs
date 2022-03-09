using Entitas;

namespace Tanks.GameLogic.Components.AI
{
    [AI]
    public sealed class GameEntityComponent : IComponent
    {
        public GameEntity Value;
    }
}