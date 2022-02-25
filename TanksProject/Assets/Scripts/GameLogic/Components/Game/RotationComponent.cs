using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class RotationComponent : IComponent
    {
        public Quaternion Value;
    }
}