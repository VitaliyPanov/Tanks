using System.Collections.Generic;
using Entitas;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentToggleActiveSystem : IExecuteSystem
    {
        private readonly AIContext _context;
        private readonly IGroup<AIEntity> _entities;
        private readonly IGroup<AIEntity> _deactivateEntities;
        private readonly List<AIEntity> _buffer = new();

        public AgentToggleActiveSystem(AIContext aiContext)
        {
            _context = aiContext;
            _entities = _context.GetGroup(AIMatcher
                .AllOf(AIMatcher.NavMesh, AIMatcher.CanBeActive));
        }

        public void Execute()
        {
            if (_entities.count == 0 || _context.hasActiveAgent) return;
            var firstAgent = _entities.GetEntities(_buffer)[0];
            _context.ReplaceActiveAgent(firstAgent);
            firstAgent.isDisabled = false;
        }
    }
}