using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class TransformComponent : IComponent
    {
        public Transform Value;
    }
}