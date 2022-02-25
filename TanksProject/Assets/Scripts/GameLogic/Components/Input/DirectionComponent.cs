using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace Tanks.GameLogic.Components.Input
{
    [Input]
    [Unique]
    public sealed class DirectionComponent : IComponent
    {
        public Vector2 Value;
    }
}