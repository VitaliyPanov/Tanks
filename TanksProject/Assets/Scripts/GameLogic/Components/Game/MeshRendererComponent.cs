using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game
{
    [Game]
    public sealed class MeshRendererComponent : IComponent
    {
        public MeshRenderer[] Array;
    }
}