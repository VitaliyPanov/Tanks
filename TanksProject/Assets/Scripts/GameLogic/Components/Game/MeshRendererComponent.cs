using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game
{
    [Game]
    public sealed class MeshRendererComponent : IComponent
    {
        public MeshRenderer[] Array;
    }
}