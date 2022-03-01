using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game
{
    [Game, Event(EventTarget.Self)]
    [Event(EventTarget.Self, EventType.Removed)]
    public sealed class MovableComponent : IComponent {}
}