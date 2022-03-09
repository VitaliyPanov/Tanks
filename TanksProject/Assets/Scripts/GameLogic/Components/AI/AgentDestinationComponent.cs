using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.AI
{
    [AI]
    public sealed class AgentDestinationComponent : IComponent
    {
        public Vector3 Position;
    }
}