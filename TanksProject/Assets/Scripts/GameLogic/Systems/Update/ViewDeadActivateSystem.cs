using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class ViewDeadActivateSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly InputContext _context;
        private List<GameEntity> _buffer = new();

        public ViewDeadActivateSystem(Contexts contexts)
        {
            _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.View, GameMatcher.Dead).NoneOf(GameMatcher.Destroy));
            _context = contexts.input;
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                entity.isDestroy = true;
            }
        }
    }
}