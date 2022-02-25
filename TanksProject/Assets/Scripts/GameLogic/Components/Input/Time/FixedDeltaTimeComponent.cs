using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace TanksGB.GameLogic.Components.Input.Time
{
    [Input]
    [Unique]
    public sealed class FixedDeltaTimeComponent : IComponent
    {
        public float Value;
    }
}