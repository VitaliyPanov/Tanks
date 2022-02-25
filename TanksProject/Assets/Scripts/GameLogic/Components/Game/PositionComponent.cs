using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class PositionComponent : IComponent
    {
        public Vector3 Value;
    }
}