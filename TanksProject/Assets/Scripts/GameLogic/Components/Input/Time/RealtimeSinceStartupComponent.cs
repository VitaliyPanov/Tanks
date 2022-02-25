using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Input.Time
{
    [Input]
    [Unique]
    public sealed class RealtimeSinceStartupComponent : IComponent
    {
        public float Value;
    }
}