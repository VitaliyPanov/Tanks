using Entitas;

namespace Tanks.GameLogic.Systems.AI
{
    public sealed class AIInitSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly AIContext _context;

        public AIInitSystem(Contexts contexts)
        {
            _context = contexts.aI;
            _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.AI));
        }

        public void Initialize()
        {
            foreach (var entity in _entities)
            {
                entity.aI.Agent.Initialize(_context.CreateEntity()); 
            }
        }
    }
}