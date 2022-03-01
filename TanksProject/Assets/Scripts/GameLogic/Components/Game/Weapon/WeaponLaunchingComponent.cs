using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game.Weapon
{
    [Game]
    [Event(EventTarget.Self, EventType.Added)]
    public sealed class WeaponLaunchingComponent : IComponent {}
}