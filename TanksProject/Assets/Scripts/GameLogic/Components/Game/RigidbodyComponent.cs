using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class RigidbodyComponent : IComponent
    {
        public Rigidbody Value;
    }
}