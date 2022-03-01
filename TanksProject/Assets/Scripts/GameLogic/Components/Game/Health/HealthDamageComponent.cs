using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game.Health
{
    [Game, Event(EventTarget.Self)]
    public sealed class HealthDamageComponent : IComponent
    {
        public float Value;
    }
}