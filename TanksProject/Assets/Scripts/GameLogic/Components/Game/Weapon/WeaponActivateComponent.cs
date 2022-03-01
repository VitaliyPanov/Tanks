using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game.Weapon
{
    [Game]
    [Event(EventTarget.Self, EventType.Removed)]
    public sealed class WeaponActivateComponent : IComponent {}
}