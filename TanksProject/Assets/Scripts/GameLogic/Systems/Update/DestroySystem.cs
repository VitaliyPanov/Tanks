using System.Collections.Generic;
using Entitas;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class DestroySystem : ICleanupSystem
    {
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _destroyGroup;
        private readonly List<GameEntity> _buffer = new();

        public DestroySystem(GameContext gameContext)
        {
            _context = gameContext;
            _destroyGroup = gameContext.GetGroup(GameMatcher.Destroy);
        }
        public void Cleanup()
        {
            foreach (var entity in _destroyGroup.GetEntities(_buffer))
            {
                if (entity.hasView)
                    _context.viewService.value.DestroyView(entity);
                entity.Destroy();
            }
        }
    }
}