using System.Runtime.InteropServices;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Input
{
    [Input]
    [Unique]
    public sealed class DirectionComponent : IComponent
    {
        public Vector2 Value;
    }
}