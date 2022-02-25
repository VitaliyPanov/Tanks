using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    [Unique]
    public sealed class ControllableComponent : IComponent
    {
        public GameEntity Entity;
    }
}