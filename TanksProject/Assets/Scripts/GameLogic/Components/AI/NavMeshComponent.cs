using Entitas;
using UnityEngine.AI;

namespace Tanks.GameLogic.Components.AI
{
    [AI]
    public class NavMeshComponent : IComponent
    {
        public NavMeshAgent Value;
    }
}