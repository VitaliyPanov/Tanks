using System.Collections.Generic;
using Entitas;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class DestroySystem : ICleanupSystem
    {
        private readonly Contexts _context;
        private readonly IGroup<GameEntity> _destroyGroup;
        private List<GameEntity> _buffer = new();

        public DestroySystem(Contexts contexts)
        {
            _context = contexts;
            _destroyGroup = contexts.game.GetGroup(GameMatcher.Destroy);
        }
        public void Cleanup()
        {
            foreach (var entity in _destroyGroup.GetEntities(_buffer))
            {
                if (entity.hasView)
                    _context.game.viewService.value.DestroyView(entity);
                entity.Destroy();
            }
        }
    }
}