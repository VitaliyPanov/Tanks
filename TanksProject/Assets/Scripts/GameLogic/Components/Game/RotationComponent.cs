using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class RotationComponent : IComponent
    {
        public Quaternion Value;
    }
}