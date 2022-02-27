using Entitas;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentSetupSystem : IInitializeSystem
    {
        private readonly IGroup<AIEntity> _entities;

        public AgentSetupSystem(Contexts contexts)
        {
            _entities = contexts.aI.GetGroup(AIMatcher.NavMesh);
        }

        public void Initialize()
        {
            foreach (var entity in _entities)
            {
                entity.navMesh.Value.stoppingDistance = 5f;
            }
        }
    }
}