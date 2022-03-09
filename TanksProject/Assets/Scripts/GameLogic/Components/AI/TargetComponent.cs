using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.AI
{
    [AI]
    public sealed class TargetComponent : IComponent
    {
        public Transform Value;
    }
}