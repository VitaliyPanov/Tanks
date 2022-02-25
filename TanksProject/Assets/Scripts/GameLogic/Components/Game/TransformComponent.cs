using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class TransformComponent : IComponent
    {
        public Transform Value;
    }
}