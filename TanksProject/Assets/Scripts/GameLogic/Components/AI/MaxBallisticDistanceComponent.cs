using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.AI
{
    [AI]    
    [Unique]
    public sealed class MaxBallisticDistanceComponent : IComponent
    {
        public float Value;
    }
}