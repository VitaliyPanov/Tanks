using System.Collections.Generic;
using Entitas;
using General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class TimeTrippingSystem : IExecuteSystem
    {
        private readonly ITimeService _timeService;
        private readonly IGroup<GameEntity> _entities;
        private readonly InputContext _inputContext;
        private List<GameEntity> _buffer = new();

        public TimeTrippingSystem(Contexts contexts)
        {
            _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Timer, GameMatcher.Target, GameMatcher.ComponentIndex).NoneOf(GameMatcher.Destroy));
            _inputContext = contexts.input;
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var timer = entity.timer;
                if (!entity.target.Value.HasComponent(entity.componentIndex.Value))
                    entity.isDestroy = true;
                else
                {
                    timer.Value -= _inputContext.fixedDeltaTime.Value;
                    if (timer.Value <= 0f)
                    {
                        entity.target.Value.RemoveComponent(entity.componentIndex.Value);
                        entity.isDestroy = true;
                    }
                }
            }
        }
    }
}