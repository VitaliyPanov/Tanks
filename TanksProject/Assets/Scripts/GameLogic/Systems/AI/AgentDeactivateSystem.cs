using System.Collections.Generic;
using Entitas;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentDeactivateSystem : ReactiveSystem<AIEntity>
    {
        private readonly AIContext _context;

        public AgentDeactivateSystem(Contexts contexts) : base(contexts.aI) => _context = contexts.aI;

        protected override ICollector<AIEntity> GetTrigger(IContext<AIEntity> context) =>
            context.CreateCollector(AIMatcher.CanBeActive.Removed());

        protected override bool Filter(AIEntity entity) => entity.hasNavMesh && entity.hasGameEntity;

        protected override void Execute(List<AIEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_context.activeAgent.Entity == entity)
                {
                    _context.RemoveActiveAgent();
                }
                entity.navMesh.Value.isStopped = true;
                entity.isDisabled = true;
            }
        }
    }
}