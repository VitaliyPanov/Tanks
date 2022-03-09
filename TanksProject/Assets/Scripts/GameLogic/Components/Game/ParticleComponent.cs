using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class ParticleComponent : IComponent
    {
        public ParticleSystem Value;
    }
}