using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class RigidbodyComponent : IComponent
    {
        public Rigidbody Value;
    }
}